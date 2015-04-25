using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverterEmulator
{
    /// <summary>
    /// Handles the serial connection to the inverter
    /// </summary>
    class InverterComms: IDisposable
    {
        SerialPort _serialPort;

        /// <summary>
        /// Messages to serve. Note not all are implemented and just return default values
        /// </summary>
        public Dictionary<string, Message> QueryMessages = new Dictionary<string, Message>
        {
            { "PI", new Message("(PI30") },
            { "SID", new Message("(1111111111111111111111") },
            { "ID", new Message("(1111111111111111111111") },
            { "PIRI", new RpiriMessage() },
            { "VFW", new Message("(VERFW:00052.30") },
            { "VFW2", new Message("(VERFW2:00000.00") },
            { "MCHGCR", new Message("(010 020 030 040 050 060 070 080 090 100 110 120") },
            { "MUCHGCR", new Message("(002 010 020 030 040 050 060") },
            { "FLAG", new Message("(ExDabjkuvyz") },
            { "DI", new Message("(230.0 50.0 0030 42.0 54.0 56.4 46.0 60 0 0 2 0 0 0 0 0 1 1 0 0 1 0 54.0 0 1") },
            { "MOD", new RmodMessage() },
            { "PIGS", new RpigsMessage() },
            { "PIWS", new RpiwsMessage() }
        };

        public InverterComms(string comPortName)
        {
            _serialPort = new SerialPort(comPortName, 2400, Parity.None, 8);
        }

        /// <summary>
        /// Gets the link state or sets the requested state
        /// </summary>
        public bool IsOpen
        {
            get 
            { 
                return _serialPort.IsOpen; 
            }
            set
            {
                if (value)
                {
                    _serialPort.Open();
                    _serialPort.DataReceived -= _serialPort_DataReceived;
                    _serialPort.DataReceived += _serialPort_DataReceived;
                }
                else
                {
                    _serialPort.DataReceived -= _serialPort_DataReceived;
                    _serialPort.Close();
                }
            }
        }

        const int maxRxStreamLength = 1024;
        MemoryStream _rxStream = new MemoryStream(maxRxStreamLength);

        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = sender as SerialPort;
            if (serialPort != null)
            {
                if (e.EventType == SerialData.Chars)
                {
                    var rxBuffer = new byte[maxRxStreamLength];
                    var rxLen = serialPort.Read(rxBuffer, 0, rxBuffer.Length);
                    if (rxLen > 0)
                    {
                        //Split lines by <cr> and parse
                        int startByte = 0;
                        for (int i=0; i<rxLen; i++)
                        {
                            if ((rxBuffer[i] == (byte)'\r') || (rxBuffer[i] == (byte)'\n'))
                            {
                                //Write chars up till <cr> to stream then call ParseLine
                                _rxStream.Write(rxBuffer, startByte, i-startByte);
                                ParseLine(_rxStream.ToArray());
                                _rxStream = new MemoryStream(1024);
                                startByte = i+1;
                            }
                        }

                        //Write any leftover chars to the rx stream
                        if (startByte < rxLen)
                            _rxStream.Write(rxBuffer, startByte, rxLen - startByte);

                        //Reset the rx stream if it's being spammed
                        if (_rxStream.Length > maxRxStreamLength)
                            _rxStream = new MemoryStream(maxRxStreamLength);
                            
                    }
                }
                else if (e.EventType == SerialData.Eof)
                {
                    //Reset the rx stream
                    _rxStream = new MemoryStream(maxRxStreamLength);
                }
            }
        }

        void ParseLine(byte[] lineBytes)
        {
            if (lineBytes.Length < 3)
                return;

            var msg = new Message(lineBytes);
            var commandText = msg.Command;

            Message defaultReply = null;

            if (commandText.StartsWith("Q"))
            {
                if (QueryMessages.ContainsKey(commandText.Substring(1)))
                {
                    defaultReply = QueryMessages[commandText.Substring(1)];
                }
            }

            var reply = defaultReply;
            if (OnRecievedCommand != null)
            {
                reply = OnRecievedCommand(msg, defaultReply);
            }

            if (reply != null)
                Send(reply.Command);
        }

        /// <summary>
        /// Send a response
        /// </summary>
        /// <param name="command"></param>
        void Send(string command)
        {
            var msg = new Message(command);
            var bytes = msg.GetBytes();
            _serialPort.Write(bytes, 0, bytes.Length);
        }

        public delegate Message OnRecievedCommandDelegate(Message recievedCommand, Message defaultReply);
        public event OnRecievedCommandDelegate OnRecievedCommand;

        public void Dispose()
        {
            Dispose(true);
        }

        ~InverterComms()
        {
            Dispose(false);
        }

        private void Dispose(bool isDisposing)
        {
            
        }
    }





}
