using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Emulates the serial protocol for a Taiwanese inverter variously known as:
// MPP Solar PIP HS
// Voltronic Axpert MKS 5K

namespace InverterEmulator
{
    public partial class frmMain : Form
    {
        InverterComms _inverterComms;

        public frmMain()
        {
            InitializeComponent();
            RefreshComPorts();
        }

        private void cmdRefreshComPorts_Click(object sender, EventArgs e)
        {
            
        }

        private void RefreshComPorts()
        {
            comComPorts.Items.Clear();
            comComPorts.Items.AddRange(SerialPort.GetPortNames());
        }

        private void comComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            Connect(comboBox.SelectedItem.ToString());
        }

        private void cmdOpenComPort_Click(object sender, EventArgs e)
        {
            Connect(comComPorts.Text);
            tabMessages_SelectedIndexChanged(tabMessages, null);

        }

        void Connect(string comPortName)
        {
            if (_inverterComms != null)
            {
                _inverterComms.OnRecievedCommand -= _inverterComms_OnRecievedCommand;
                
                _inverterComms.Dispose();
            }
            _inverterComms = new InverterComms(comPortName);
            _inverterComms.OnRecievedCommand += _inverterComms_OnRecievedCommand;
            
            _inverterComms.IsOpen = true;
        }

        Message _inverterComms_OnRecievedCommand(Message recievedCommand, Message defaultReply)
        {
            return (Message)this.Invoke(new InverterComms.OnRecievedCommandDelegate(OnRecievedCommand), new object[] { recievedCommand, defaultReply });
        }

        Message OnRecievedCommand(Message recievedCommand, Message defaultReply)
        {
            lstCommands.Items.Add( recievedCommand );
            lstCommands.Items.Add( "  " + defaultReply);
            if (!lstCommands.Focused)
                lstCommands.SelectedIndex = lstCommands.Items.Count - 1;
            return defaultReply;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }





        public struct ComboValueItem
        {
            public ComboValueItem(string name, int value, string format)
            {
                Name = name;
                Value = value;
                Format = format;
            }

            public string Name;
            public int Value;
            public string Format;

            public override string ToString()
            {
                return Value.ToString(Format) + ". " + Name;
            }
        }

        void PlaceField(string propertyName, FieldAttribute field, Message msg)
        {
            var panel = new Panel();
            panel.Width = tlpControls.Width - 30;
            panel.Height = 20;
            panel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            panel.Top = 0;
            panel.Left = 0;
            panel.Visible = true;

            var label = new Label();
            label.Width = (panel.Width / 2) - 2;
            label.Height = panel.Height;
            label.Top = 2;
            label.Left = 0;
            label.Text = field.Name;
            label.TextAlign = ContentAlignment.TopRight;
            label.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; 
            label.Visible = true;
            panel.Controls.Add(label);

            var property = msg.GetType().GetField(propertyName);
            var fieldValue = property.GetValue(msg);

            if (field is NumericFieldAttribute)
            {
                var numericField = field as NumericFieldAttribute;

                var numeric = new NumericUpDown();
                numeric.Width = (panel.Width / 2) - 2;
                numeric.Height = panel.Height;
                numeric.Top = 0;
                numeric.Left = label.Width;
                numeric.Visible = true;
                numeric.DecimalPlaces = numericField.NumDecimals;
                numeric.Maximum = (int)Math.Pow(10, numericField.NumDigits) - 1;

                float v = (float)fieldValue;
                numeric.Value = (decimal)v;// numericField.DefaultValue;

                numeric.Increment = (decimal)(1 / Math.Pow(10, numericField.NumDecimals));
                numeric.Tag = new Tuple<Message, NumericFieldAttribute, string>(msg, numericField, propertyName);
                numeric.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                numeric_ValueChanged(numeric, null);
                
                panel.Controls.Add(numeric);

                numeric.ValueChanged += numeric_ValueChanged;
            }

            if (field is EnumFieldAttribute)
            {
                var enumField = field as EnumFieldAttribute;
                var enumType = msg.GetType().GetField(propertyName).FieldType;

                var combo = new ComboBox();
                combo.Width = (panel.Width / 2) - 2;
                combo.Height = panel.Height;
                combo.Top = 0;
                combo.Left = label.Width;
                combo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                combo.Visible = true;

                var values = Enum.GetValues(enumType);
                int numItems = 0;
                int selectItem = 0;
                foreach (var value in values)
                {
                    var name = Enum.GetName(enumType, value);

                    //See if a description attribute was given
                    var enumFieldInfo = enumType.GetField(name);
                    var enumFieldAttribute = enumFieldInfo.GetCustomAttributes(typeof(EnumValueAttribute), true).FirstOrDefault() as EnumValueAttribute;
                    if (enumFieldAttribute != null)
                        name = enumFieldAttribute.Description;

                    //Value might be a special supplied string, or just the enum's value
                    if (fieldValue is string)
                    {
                        if ((string)fieldValue == name)
                            selectItem = numItems;
                    }
                    else
                    {
                        if ((int)fieldValue == (int)value)
                            selectItem = numItems;
                    }

                    combo.Items.Add(new ComboValueItem(name, (int)value, field.GetFormat()));
                    numItems++;
                }
                combo.SelectedIndex = selectItem;
                combo.Tag = new Tuple<Message, EnumFieldAttribute, string>(msg, enumField, propertyName);
                
                panel.Controls.Add(combo);
                combo.SelectedIndexChanged += combo_SelectedIndexChanged;
                combo_SelectedIndexChanged(combo, null);
            }

            tlpControls.Controls.Add(panel);
        }

        /// <summary>
        /// User selected a combo item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo != null)
            {
                var tag = combo.Tag as Tuple<Message, EnumFieldAttribute, string>;
                var msg = tag.Item1;
                var numericField = tag.Item2;
                var propertyName = tag.Item3;

                var property = msg.GetType().GetField(propertyName);

                var item = (ComboValueItem)combo.SelectedItem;
                property.SetValue(msg, (int)item.Value);
                
                txtCommand.Text = msg.Command;
            }
        }

        /// <summary>
        /// Numeric up down control value changed, values packed in tag member
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void numeric_ValueChanged(object sender, EventArgs e)
        {
            var numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                var tag = numeric.Tag as Tuple<Message, NumericFieldAttribute, string>;
                var msg = tag.Item1;
                var numericField = tag.Item2;
                var propertyName = tag.Item3;

                var property = msg.GetType().GetField(propertyName);
                property.SetValue(msg, (float)numeric.Value);

                txtCommand.Text = msg.Command;
            }

            

        }



        void PlaceControls(Message msg)
        {
            tlpControls.Controls.Clear();

            foreach (var field in msg.GetFields())
            {
                PlaceField(field.Item1, field.Item2, msg);
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            var msg = new RpigsMessage();
            PlaceControls(msg);
        }

        private void tlpControls_Resize(object sender, EventArgs e)
        {
            var flp = sender as FlowLayoutPanel;

            if (flp != null)
            {
                foreach (var c in flp.Controls)
                {
                    if (c is Panel)
                    {
                        var panel = c as Panel;
                        panel.Width = flp.Width - 30;
                    }
                }
            }

        }

        private void tabMessages_TabIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void tabMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_inverterComms == null)
                return;

            var tabs = sender as TabControl;
            var tab = tabs.SelectedTab;
            var tabName = tabs.SelectedTab.Text.Split(new char[] { ' ' }).FirstOrDefault();

            var tabTag = (string)tab.Tag;

            if (tabTag.StartsWith("Q"))
            {
                var key = tabTag.Substring(1);

                if (_inverterComms.QueryMessages.ContainsKey(key))
                {
                    var msg = _inverterComms.QueryMessages[key];
                    PlaceControls(msg);
                }
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
        
    }
}
