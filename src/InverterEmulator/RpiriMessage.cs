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
    public class RpiriMessage : MessageWithFields
    {
        public override string Command
        {
            get
            {
                return "(" + base.Command;
            }
        }

        [NumericField(0, "Grid Volts", "230.0")]
        public float GridRatedVoltage;

        [NumericField(1, "Grid Amps", "21.7")]
        public float GridRatedCurrent;

        [NumericField(2, "AC Output Volts", "230.0")]
        public float OutputRatedVoltage;

        [NumericField(3, "AC Output Frequency", "50.0")]
        public float OutputRatedFrequency;

        [NumericField(4, "AC Output Current", "21.7")]
        public float OutputRatedCurrent;

        [NumericField(5, "AC Output Apparent Power", "5000")]
        public float OutputRatedApparentPower;

        [NumericField(6, "AC Output Active Power", "4000")]
        public float OutputRatedRealPower;

        [NumericField(7, "Battery Voltage", "48.0")]
        public float BatteryRatedVoltage ;

        [NumericField(8, "Reserved K", "46.0")]
        public float RatingReservedK;

        [NumericField(9, "Reserved L", "42.0")]
        public float RatingReservedL;

        [NumericField(10, "Reserved M", "56.4")]
        public float RatingReservedM;

        [NumericField(11, "Reserved N", "54.0")]
        public float RatingReservedN;

        public enum RatedBatteryTypeEnum
        {
            Agm = 0,
            Flooded = 1,
        }
        [EnumField(12, "Battery Type", "0")]
        public RatedBatteryTypeEnum RatedBatteryType;

        [NumericField(13, "Max Charging Current", "10")]
        public float MaxRatedChargingCurrent;

        [NumericField(14, "Charging Current", "010")]
        public float RatedChargingCurrent;

        public enum RatedInputVoltageRangeEnum
        {
            Appliance = 0,
            UPS = 1
        }
        [EnumField(15, "Input Voltage Range", "1")]
        public RatedInputVoltageRangeEnum RatedInputVoltageRange;

        public enum RatedOutputSourcePriorityEnum
        {
            [EnumValue("Utility First")]
            UtilityFirst = 0,
            [EnumValue("Solar First")]
            SolarFirst = 1,
            [EnumValue("Solar Battery Utility")]
            SbuPriority = 2
        }
        [EnumField(16, "Output Source Priority", "0")]
        public RatedOutputSourcePriorityEnum RatedOutputSourcePriority;

        [NumericField(17, "Reserved R", "6")]
        public float RatedReservedR;

        public enum RatedMachineTypeEnum
        {
            [EnumValue("Grid Tie")]
            GridTie = 0,
            [EnumValue("Off Grid")]
            OffGrid = 1,
            [EnumValue("Hybrid")]
            Hybrid = 2
        }
        [EnumField(18, "Machine Type", "01")]
        public RatedMachineTypeEnum RatedMachineType;

        public enum RatedTopologyEnum
        {
            Transformerless = 0,
            Transformer = 1
        }
        [EnumField(19, "Topology", "0")]
        public RatedTopologyEnum RatedTopology;

        [NumericField(20, "Reserved U", "0")]
        public float RatedReservedU;

        [NumericField(21, "Reserved V", "54.0")]
        public float RatedReservedV;

        [NumericField(22, "Reserved W", "0")]
        public float RatedReservedW;

        [NumericField(23, "Reserved X", "1")]
        public float RatedReservedX;


    }
}
