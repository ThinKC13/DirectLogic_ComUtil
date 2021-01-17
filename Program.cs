using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using AMWD.Modbus.Tcp;
using AMWD.Modbus.Tcp.Client;

namespace AutomationDirectComDriver
{
    class Program
    {
        // Initialize logger
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static readonly bool debug = true;

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Load configuration for logger (file and colored console)
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            // debug for number of input arguments
            if (debug) { log.Debug("Recieved Arguments: " + args.Length.ToString()); }

        
            DirectLogic_PLC.DL06 dl06 = new DirectLogic_PLC.DL06();
            log.Info("CPU Name = " + dl06.CPUName);
            log.Info("Input Qty = " + dl06.Inputs.Qty.ToString());

            ModbusClient modbus = new ModbusClient(IPAddress.Parse("127.0.0.1"));
            await modbus.Connect();
            var coils = await modbus.ReadCoils(0, 0, 1);

            
            foreach (var coil in coils)
            {
                log.Info("Coil: " + coil.BoolValue.ToString());
            }
            

            // check if asking for help
            if (args.Length == 0 || (args.Length == 1 && HelpRequired(args[0])))
            {
                log.Info("Help requested...");
                DisplayHelp();
            }
            // Parse communication type
            else if (args[0] == "mtcp")
            {
                ModbusParser(args);                
            }
            else
            {
                DisplayHelp();
            }


        }

        private static bool HelpRequired(string param)
        {
            return param == "-h" || param == "--help" || param == "/?";
        }

        private static void DisplayHelp()
        {
            // Read the help.txt file to console
            var lines = File.ReadLines(".\\help.txt");
            foreach (var line in lines)
            {
                Console.WriteLine(line.ToString());
            }
        }

        private static void ModbusParser(string[] args)
        {
            if (args.Length == 5)
            {
                // organize command line arguments
                string IPString = args[1];
                string portString = args[2];
                string deviceAddrString = args[3];
                string deviceName = args[4];
                
                // check valid Ip address, port and device address 
                bool validIP = IPAddress.TryParse(IPString, out IPAddress iPAddress);

                // (port must be less that 65536)
                bool validPort = Int32.TryParse(portString, out int port);
                if (validPort) { validPort = validPort && port < 65536; }

                // (modbus device address can't be more than 247)
                bool validDevAddr = Int32.TryParse(deviceAddrString, out int deviceAddress);
                if (validDevAddr) { validDevAddr = validDevAddr && deviceAddress <= 247; }

                // check if device and PLC address are valid
                DeviceDataInterface deviceData = new DeviceDataInterface();
                bool validDevice = deviceData.ValidDevice(deviceName);
                
                // log issues
                if (!validIP) { log.Error(IPString + " is not a valid IP Address"); }
                if (!validPort) { log.Error(portString + " is not a valid Port Number. Modbus is generally 502."); }
                if (!validDevAddr) { log.Error(deviceAddrString + " is not a valid device address."); }
                if (!validDevice) { log.Error(deviceName + " is not a valid Device."); }
                

            }
            else
            {
                log.Error("Improper Modbus TCP format. Please check help file with -h.");
            }
        }






    }
}
