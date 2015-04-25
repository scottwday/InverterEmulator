using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverterEmulator
{

    /// <summary>
    /// Mode info message
    /// </summary>
    public class RmodMessage : MessageWithFields
    {
        public override string Command
        {
            get
            {
                return "(" + base.Command;
            }
        }

        public enum ModeType
        {
            [EnumValue("Power On", "P")]
            PowerOn,
            [EnumValue("Standby", "S")]
            Standby,
            [EnumValue("Line", "L")]
            Line,
            [EnumValue("Battery", "B")]
            Battery,
            [EnumValue("Fault", "F")]
            Fault,
            [EnumValue("Power Saving", "H")]
            PowerSaving
        }

        [EnumField(1, "Mode", "0")]
        public ModeType Mode;
    }
}
