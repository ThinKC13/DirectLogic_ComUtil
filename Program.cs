using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace AutomationDirectComDriver
{
    class Program
    {
        // Initialize logger
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            // Load configuration for logger (file and colored console)
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            // debug for number of input arguments
            log.Debug("Arguments: " + args.Length.ToString());

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
            if (args.Length == 4)
            {
                //IPAddress iPAddress = new IPAddress(null);
                bool validIP = IPAddress.TryParse(args[1], out IPAddress iPAddress);
                bool validPORT = Int32.TryParse(args[2], out int port);
                bool validDevAddr = Int32.TryParse(args[3], out int deviceAddress);

                log.Info("To be implemented...");

            }
            else
            {
                log.Warn("Improper Modbus TCP format. Not enough arguments.");
            }
        }






    }
}
