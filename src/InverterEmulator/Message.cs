using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverterEmulator
{
    public class EnumValueAttribute : Attribute
    {
        public EnumValueAttribute(string description, string value = null)
        {
            Description = description;
            Value = value;
        }
        public string Value;
        public string Description;
    }

    /// <summary>
    /// Base class for a message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Message()
        {
            Crc = 0;
            CrcValid = false;
        }

        /// <summary>
        /// Set command and crc from packed byte array
        /// </summary>
        /// <param name="commandAndCrc"></param>
        public Message(byte[] commandAndCrc)
        {
            Crc = (ushort)((commandAndCrc[commandAndCrc.Length - 2] << 8) | commandAndCrc[commandAndCrc.Length - 1]);
            Command = Encoding.ASCII.GetString(commandAndCrc, 0, commandAndCrc.Length - 2);
            CrcValid = CRC16.Calculate(Encoding.ASCII.GetBytes(Command)) == Crc;
        }

        /// <summary>
        /// Set the command and crc, calculates whether crc is valid
        /// </summary>
        /// <param name="command"></param>
        /// <param name="crc"></param>
        public Message(string command, ushort crc)
        {
            Command = command;
            Crc = crc;
            CrcValid = CRC16.Calculate(Encoding.ASCII.GetBytes(command)) == crc;
        }

        /// <summary>
        /// Set the command and calculate the crc, always sets crcValid to true
        /// </summary>
        /// <param name="command"></param>
        public Message(string command)
        {
            Command = command;
            Crc = CRC16.Calculate(Encoding.ASCII.GetBytes(command));
            CrcValid = true;
        }

        private string _command;
        /// <summary>
        /// Payload text
        /// </summary>
        public virtual string Command
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
            }
        }

        /// <summary>
        /// CRC16
        /// </summary>
        ushort Crc;

        /// <summary>
        /// Does the CRC match the command payload?
        /// </summary>
        public bool CrcValid;

        public override string ToString()
        {
            return Command + " :" + Crc.ToString("x04") + ":" + (CrcValid ? "Y" : "N");
        }

        /// <summary>
        /// Get the low byte of the CRC
        /// </summary>
        public byte CrcA { get { return (byte)((Crc >> 8) & 0xFF); } }
        /// <summary>
        /// Get the high byte of the CRC
        /// </summary>
        public byte CrcB { get { return (byte)((Crc >> 0) & 0xFF); } }

        /// <summary>
        /// Get the packed message bytes
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            byte[] result = new byte[Command.Length + 3];
            Encoding.ASCII.GetBytes(Command, 0, Command.Length, result, 0);
            result[result.Length - 3] = CrcA;
            result[result.Length - 2] = CrcB;
            result[result.Length - 1] = 0x0d;
            return result;
        }

        /// <summary>
        /// Gets all FieldAttribute fields and their values
        /// </summary>
        /// <returns></returns>
        public Tuple<string, FieldAttribute, object>[] GetFields()
        {
            var fields = new List<Tuple<string, FieldAttribute, object>>();

            var properties = this.GetType().GetFields();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(FieldAttribute), true).FirstOrDefault() as FieldAttribute;
                if (attribute != null)
                {
                    var value = property.GetValue(this);
                    fields.Add(new Tuple<string, FieldAttribute, object>(property.Name, attribute, value));
                }
            }

            return fields.OrderBy(i => i.Item2.Order).ToArray();
        }

    }



    /// <summary>
    /// Command overrided to generate from fields with fieldAttributes
    /// </summary>
    public class MessageWithFields : Message
    {
        public MessageWithFields()
        {
            InitialiseFieldsWithDefaultValues();
        }

        /// <summary>
        /// Pack the fields into the command text
        /// </summary>
        public override string Command
        {
            get
            {
                var values = new List<string>();

                //Get list of fields with FieldAttribute
                var fields = GetFields();
                for (int i = 0; i < fields.Length; i++)
                {
                    var field = fields[i];

                    var propertyName = field.Item1;
                    var fieldAttribute = field.Item2;
                    var format = fieldAttribute.GetFormat();

                    var property = this.GetType().GetField(propertyName);
                    var valueObject = property.GetValue(this);

                    var valueString = "";
                    if (fieldAttribute is NumericFieldAttribute)
                    {
                        var value = (float)property.GetValue(this);
                        valueString = value.ToString(format);
                    }
                    else if (fieldAttribute is EnumFieldAttribute)
                    {
                        var enumFieldAttribute = fieldAttribute as EnumFieldAttribute;
                        var enumType = property.FieldType; // enumFieldAttribute.EnumType;
                        var enumValue = (int)property.GetValue(this);
                        var enumName = Enum.GetName(enumType, enumValue);

                        //See if a specific value string was given, otherwise feed the enum value through the format
                        var enumField = enumType.GetField(enumName);
                        var enumValueAttribute = enumField.GetCustomAttributes(typeof(EnumValueAttribute), true).FirstOrDefault() as EnumValueAttribute;
                        if ((enumValueAttribute != null) && (enumValueAttribute.Value != null))
                        {
                            valueString = enumValueAttribute.Value;
                        }
                        else
                        {
                            var value = (int)property.GetValue(this);
                            valueString = value.ToString(format);
                        }
                    }

                    //Add space if requested and not last item
                    if ((fieldAttribute.NoSpaceAfter == false) && (i < fields.Length - 1))
                        valueString += " ";

                    values.Add(valueString);
                }

                return string.Join("", values.ToArray());
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        
        /// <summary>
        /// Use field attributes to set default values
        /// </summary>
        public void InitialiseFieldsWithDefaultValues()
        {

            //Get list of fields with FieldAttribute
            var fields = GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];

                var propertyName = field.Item1;
                var fieldAttribute = field.Item2;
                var format = fieldAttribute.GetFormat();

                var property = this.GetType().GetField(propertyName);
                var valueObject = property.GetValue(this);

                var valueString = "";
                if (fieldAttribute is NumericFieldAttribute)
                {
                    property.SetValue(this, fieldAttribute.DefaultValue);
                }
                else if (fieldAttribute is EnumFieldAttribute)
                {
                    property.SetValue(this, (int)fieldAttribute.DefaultValue);
                }                    
            }

        }

        /// <summary>
        /// Gets values and names for enum fields
        /// </summary>
        public KeyValuePair<int, string>[] GetEnumValues(Type enumType)
        {
            var values = (int[])Enum.GetValues(enumType);
            return values.Select(i => new KeyValuePair<int, string>(i, Enum.GetName(enumType, i))).ToArray();
        }

    }

    /// <summary>
    /// Fixed width field
    /// </summary>
    public class FieldAttribute : Attribute
    {
        public readonly int Order;
        public readonly string Name;
        public readonly string DefaultFormattedValue;
        public readonly bool NoSpaceAfter;

        public readonly int NumDecimals = 0;
        public readonly int NumDigits = 0;
        public readonly float DefaultValue = 0;

        
        /// <summary>
        /// Setup the field
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultFormattedValue"></param>
        public FieldAttribute(int order, string name, string defaultFormattedValue, bool noSpaceAfter = false)
        {
            Order = order;
            Name = name;
            DefaultFormattedValue = defaultFormattedValue;
            NoSpaceAfter = noSpaceAfter;

            var tokens = defaultFormattedValue.Split(new char[] {'.'});
            if (tokens.Length > 0)
                NumDigits = tokens[0].Length;
            if (tokens.Length > 1)
                NumDecimals = tokens[1].Length;

            float.TryParse(DefaultFormattedValue, out DefaultValue);
        }

        public string GetFormat()
        {
            var format = new string('0', NumDigits);
            if (NumDecimals > 0)
                format += "." + new string('0', NumDecimals);

            return format;
        }

    }

    /// <summary>
    /// Field holds a number
    /// </summary>
    public class NumericFieldAttribute: FieldAttribute
    {
        public NumericFieldAttribute(int order, string name, string defaultFormattedValue, bool noSpaceAfter = false)
            : base(order, name, defaultFormattedValue)
        {
        }
    }

    /// <summary>
    /// Field is based on an enum
    /// </summary>
    public class EnumFieldAttribute : FieldAttribute
    {
        /// <summary>
        /// Enum field
        /// </summary>
        /// <param name="order">Order of field in output message</param>
        /// <param name="name">Friendly name to display for field</param>
        /// <param name="defaultFormattedValue">Default value, formatted like it must appear. String valued enums must still be initialised using numeric enum index</param>
        /// <param name="noSpaceAfter">Set to true to suppress the space after this field</param>
        public EnumFieldAttribute(int order, string name, string defaultFormattedValue, bool noSpaceAfter = false)
            : base(order, name, defaultFormattedValue, noSpaceAfter)
        {

        }

    }




}
