using System.Collections;
using System.Collections.Generic;

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
        public string PLCRange { get; set; }
        public char Letter { get; set;}
        public int StartAddress { get; set; }
        public int EndAddress { get; set; }
        public string DataType { get; set; }
    }

   
}
