using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutomationDirectComDriver
{
    public class DirectLogic_PLC
    {
        public class CPU
        {
            public string CPUName { get; set; }
            public Input Inputs { get; set; }
            public Output Outputs { get; set; }
            public SpecialRelay SpecialRelays { get; set; }

            // CONSTRUCTOR
            public CPU() { }
            public CPU(string cpuName, Input inputs, Output outputs, SpecialRelay specialRelay)
            {
                CPUName = cpuName;
                Inputs = inputs;
                Outputs = outputs;
                SpecialRelays = specialRelay;
            }
        }

        public class MemoryDatum
        {
            public string MemoryType { get; set; }
            public string MemoryFormat { get; set; }
            public string PLCRangeStartOct { get; set; }
            public string PLCRangeEndOct { get; set; }
            public int Qty
            {
                get
                {
                    return Convert.ToInt32(PLCRangeEndOct, 8) - Convert.ToInt32(PLCRangeStartOct, 8) + 1;
                }
            }
            public string Prefix { get; set; }
            public int StartAddress { get; set; }
            public int EndAddress { get; set; }
            public string DataType { get; set; }
            public MemoryDatum() { }
            public MemoryDatum(string memoryType, string memoryFormat, string plcRangeStart,
                             string plcRangeEnd, string prefix, int startAddress, int endAddress, string dataType)
            {
                MemoryType = memoryType;
                MemoryFormat = memoryFormat;
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                Prefix = prefix;
                StartAddress = startAddress;
                EndAddress = endAddress;
                DataType = dataType;
            }

            public int ConvertToModbusAddress(string PLCAddress)
            {

                var array = Regex.Matches(PLCAddress, @"\D+|\d+")
                                 .Cast<Match>()
                                 .Select(m => m.Value)
                                 .ToArray();

                foreach (var item in array)
                {
                    Console.WriteLine(item.ToString());
                }

                int modbusAddress = 0;
                return modbusAddress;
            }

            public bool ValidPLCAddress(string PLCAddress)
            {
                // parse PLC address with letter - number or letter-letter-number
                // check if it begins with one or two letters followed by a number less than 99999
                bool test = Regex.IsMatch(PLCAddress, @"\b[a-zA-Z]{1,2}\d{0,5}\b");
                return test;
            }

        }

        public class Input : MemoryDatum
        {
            public Input(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Inputs";
                MemoryFormat = "Discrete";
                Prefix = "X";
                DataType = "Input";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class Output : MemoryDatum
        {
            public Output(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Outputs";
                MemoryFormat = "Discrete";
                Prefix = "Y";
                DataType = "Coil";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class SpecialRelay : MemoryDatum
        {
            public SpecialRelay(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Special Relays";
                MemoryFormat = "Discrete";
                Prefix = "SP";
                DataType = "Input";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class ControlRelay : MemoryDatum
        {
            public ControlRelay(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Control Relays";
                MemoryFormat = "Discrete";
                Prefix = "C";
                DataType = "Coil";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class TimerContacts : MemoryDatum
        {
            public TimerContacts(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Timer Contacts";
                MemoryFormat = "Discrete";
                Prefix = "T";
                DataType = "Coil";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class CounterContacts : MemoryDatum
        {
            public CounterContacts(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Counter Contacts";
                MemoryFormat = "Discrete";
                Prefix = "CT";
                DataType = "Coil";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class StageStatusBits : MemoryDatum
        {
            public StageStatusBits(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Stage Status Bits";
                MemoryFormat = "Discrete";
                Prefix = "S";
                DataType = "Coil";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class GlobalInputs : MemoryDatum
        {
            public GlobalInputs(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Global Inputs";
                MemoryFormat = "Discrete";
                Prefix = "GX";
                DataType = "Input";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class GlobalOutputs : MemoryDatum
        {
            public GlobalOutputs(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Global Outputs";
                MemoryFormat = "Discrete";
                Prefix = "GY";
                DataType = "Coil";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class TimerCurrentValues : MemoryDatum
        {
            public TimerCurrentValues(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Timer Current Values";
                MemoryFormat = "Word";
                Prefix = "V";
                DataType = "Input Register";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class CounterCurrentValues : MemoryDatum
        {
            public CounterCurrentValues(string plcRangeStart, string plcRangeEnd, int startAddress, int endAddress)
            {
                MemoryType = "Counter Current Values";
                MemoryFormat = "Word";
                Prefix = "V";
                DataType = "Input Register";
                PLCRangeStartOct = plcRangeStart;
                PLCRangeEndOct = plcRangeEnd;
                StartAddress = startAddress;
                EndAddress = endAddress;
            }
        }

        public class UserDataVMem : MemoryDatum
        {

        }

        public class ModbusCom
        {
            public string IPAddress { get; set; }
            public int Port { get; set; }
            public int DeviceAddress { get; set; }
        }

        public class DL06 : CPU
        {
            public DL06()
            {
                this.CPUName = "DL06";
                this.Inputs = new Input("0", "777", 2048, 2559);
                this.Outputs = new Output("0", "777", 2048, 2559);
                this.SpecialRelays = new SpecialRelay("0", "777", 3072, 3583);

            }
        }
    }
}
