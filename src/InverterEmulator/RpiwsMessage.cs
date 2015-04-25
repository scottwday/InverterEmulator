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
    public class RpiwsMessage : MessageWithFields
    {
        public override string Command
        {
            get
            {
                return "(" + base.Command;
            }
        }

        public enum OkFailType
        {
            Ok,
            Failed
        }

        [EnumField(0, "Reserved", "0", true)]
        public OkFailType Reserved0;

        [EnumField(1, "Inverter Fault", "0", true)]
        public OkFailType InverterFault;

        [EnumField(2, "Bus Voltage too high", "0", true)]
        public OkFailType BusVoltageTooHigh;

        [EnumField(3, "Bus Voltage too low", "0", true)]
        public OkFailType BusVoltageTooLow;

        [EnumField(4, "Bus Soft Failure", "0", true)]
        public OkFailType BusSoftFailure;

        [EnumField(5, "Line Failure Warning", "0", true)]
        public OkFailType LineFailureWarning;

        [EnumField(6, "OPV Short Warning", "0", true)]
        public OkFailType OpvShortWarning;

        [EnumField(7, "Voltage too low", "0", true)]
        public OkFailType VoltageTooLow;

        [EnumField(8, "Voltage too high", "0", true)]
        public OkFailType VoltageTooHigh;

        [EnumField(9, "Temperature too high", "0", true)]
        public OkFailType TemperatureTooHigh;

        [EnumField(10, "Fan Locked", "0", true)]
        public OkFailType FanLocked;

        [EnumField(11, "Battery Voltage too high", "0", true)]
        public OkFailType BatteryVoltageTooHigh;

        [EnumField(12, "Battery Voltage too low", "0", true)]
        public OkFailType BatteryVoltageTooLow;

        [EnumField(13, "Reserved", "0", true)]
        public OkFailType Reserved13;

        [EnumField(14, "Battery Shutdown Warning", "0", true)]
        public OkFailType BatteryShutdownWarning;

        [EnumField(15, "Reserved", "0", true)]
        public OkFailType Reserved15;

        [EnumField(16, "Overload", "0", true)]
        public OkFailType Overload;

        [EnumField(17, "Eeprom Fault", "0", true)]
        public OkFailType EepromFault;

        [EnumField(18, "Reserved", "0", true)]
        public OkFailType Reserved18;

        [EnumField(19, "Reserved", "0", true)]
        public OkFailType Reserved19;

        [EnumField(20, "Reserved", "0", true)]
        public OkFailType Reserved20;

        [EnumField(21, "Reserved", "0", true)]
        public OkFailType Reserved21;

        [EnumField(22, "Reserved", "0", true)]
        public OkFailType Reserved22;

        [EnumField(23, "Reserved", "0", true)]
        public OkFailType Reserved23;

        [EnumField(24, "Reserved", "0", true)]
        public OkFailType Reserved24;

        [EnumField(25, "Reserved", "0", true)]
        public OkFailType Reserved25;

        [EnumField(26, "Reserved", "0", true)]
        public OkFailType Reserved26;

        [EnumField(27, "Reserved", "0", true)]
        public OkFailType Reserved27;

        [EnumField(28, "Reserved", "0", true)]
        public OkFailType Reserved28;

        [EnumField(29, "Reserved", "0", true)]
        public OkFailType Reserved29;

        [EnumField(30, "Reserved", "0", true)]
        public OkFailType Reserved30;

        [EnumField(31, "Reserved", "0", true)]
        public OkFailType Reserved31;


    }
}
