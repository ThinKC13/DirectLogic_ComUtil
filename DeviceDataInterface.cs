using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AutomationDirectComDriver
{
    class DeviceDataInterface
    {
        // initialize by reading device data file and mapping to device manager class
        readonly static string jsonString = File.ReadAllText(".\\devicedata.json");
        readonly DeviceManager deviceManager = JsonSerializer.Deserialize<DeviceManager>(jsonString);

        public List<string> GetCompatibleDevices()
        {
            List<string> compatibleDevices = new List<string>();

            // iterate through available devices and get device names
            foreach (Device device in deviceManager.DeviceList)
            {
                compatibleDevices.Add(device.DeviceName);
            }

            return compatibleDevices;
        }
        
        public bool ValidDevice(string deviceName)
        {
            return GetCompatibleDevices().Contains(deviceName, StringComparer.OrdinalIgnoreCase);
        }
    }
}
