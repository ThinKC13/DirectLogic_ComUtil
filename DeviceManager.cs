using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutomationDirectComDriver
{
    public class DeviceManager
    {
        public string DeviceType { get; set; }
        public List<Device> DeviceList { get; set; }

    }

    public class Device
    {
        public string DeviceName { get; set; }
        public List<MemoryData> MemoryData { get; set; }
    }

    public class MemoryData
    {
        public string MemoryType { get; set; }
        public string MemoryFormat { get; set; }
        public int Qty { get; set; }
        public int PLCRangeStart { get; set; }
        public int PLCRangeEnd { get; set; }
        public char Letter { get; set;}
        public int StartAddress { get; set; }
        public int EndAddress { get; set; }
        public string DataType { get; set; }

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

   
}
