using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverterEmulator
{

    /// <summary>
    /// Rated parameters message
    /// </summary>
    public class RpigsMessage : MessageWithFields
    {
        public override string Command
        {
            get
            {
                return "(" + base.Command;
            }
        }

        [NumericField(0, "Grid Volts", "240.9")]
        public float GridVolts;

        [NumericField(1, "Grid Frequency", "50.1")]
        public float GridFrequency;

        [NumericField(2, "AC Output Volts", "000.0")]
        public float AcOutputVoltage;

        [NumericField(3, "AC Output Frequency", "00.0")]
        public float AcOutputFrequency;

        [NumericField(4, "AC Output Apparent Power", "0000")]
        public float AcOutputApparentPower;

        [NumericField(5, "AC Output Active Power", "0000")]
        public float AcOutputActivePower;

        [NumericField(6, "Output Load Percent", "000")]
        public float OutputLoadPercent;

        [NumericField(7, "Bus Voltage", "435")]
        public float BusVoltage;

        [NumericField(8, "Battery Voltage", "54.00")]
        public float BatteryVoltage;

        [NumericField(9, "Battery Charging Current", "001")]
        public float BatteryChargingCurrent;

        [NumericField(10, "Battery Capacity", "100")]
        public float BatteryCapacity;

        [NumericField(11, "Reserved P", "0027")]
        public float ReservedP;

        [NumericField(12, "Solar Input Current", "0000")]
        public float ReservedQ;

        [NumericField(13, "Solar Input Voltage", "000.0")]
        public float ReservedR;

        [NumericField(14, "Reserved S", "00.00")]
        public float ReservedS;

        [NumericField(15, "Reserved T", "00000")]
        public float ReservedT;

        public enum Flags1Type
        {
            [EnumValue("")]
            v0 = 0,
            [EnumValue("")]
            v1 = 1
        }

        public enum Flags2Type
        {
            [EnumValue("")]
            v00 = 00, //Yes, it's in decimal
            [EnumValue("")]
            v01 = 01,
            [EnumValue("")]
            v10 = 10,
            [EnumValue("")]
            v11 = 11,
        }

        [EnumField(16, "Reserved U7, U6", "00", true)]
        public Flags2Type ReservedU7U6;

        public enum FlagsNoYesType
        {
            No = 0,
            Yes = 1,
        }
        [EnumField(17, "Scc Firmware Updated", "0", true)]
        public FlagsNoYesType SccFirmwareUpdated;

        [EnumField(18, "Load On", "0", true)]
        public FlagsNoYesType LoadOn;

        [EnumField(19, "Reserved U4", "0", true)]
        public Flags1Type ReservedU4;

        public enum FlagsChargingType
        {
            [EnumValue("Not Charging")]
            v00 = 000,
            [EnumValue("AC Charging")]
            v01 = 101,
            [EnumValue("Solar Charging")]
            v10 = 110,
            [EnumValue("Solar and AC Charging")]
            v11 = 111,
        }
        [EnumField(20, "Charging Status", "101", false)]
        public FlagsChargingType FlagsCharging;

        //00 00 00000 100
        [NumericField(21, "Reserved Y", "00")]
        public float ReservedY;

        [NumericField(22, "Reserved Z", "00")]
        public float ReservedZ;

        [NumericField(23, "Reserved AA", "00000")]
        public float ReservedAA;

        [NumericField(24, "Reserved BB", "100")]
        public float ReservedBB;

    }


}
