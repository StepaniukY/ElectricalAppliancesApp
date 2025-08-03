using ElectricalAppliancesApp.Models;
using ElectricalAppliancesApp.Services;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ElectricalAppliancesApp
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Main()
        {
            Log.Info("Program started");

            var service = new AppliancesService();

            ElectricalAppliance[] appliances = service.ProcessWithoutBrand();

            Console.Write("Please enter the name of the brand to filter: ");
            string brand = Console.ReadLine();

            service.ProcessByBrand(appliances, brand);

            //JSON
            string jsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "appliances.json");
            List<ElectricalAppliance> applianceList = appliances.Where(a => a != null).ToList();

            AppliancesSerializer.SerializeToJson(applianceList, jsonPath);

            Console.WriteLine("JSON deserialization result:");
            Console.WriteLine($"(Deserialized from file: \"{jsonPath}\")");
        
            var appliancesFromJson = AppliancesSerializer.DeserializeFromJson(jsonPath);

            Console.WriteLine(new string('=', 95));
            Console.WriteLine($"{"Type",-15} | {"Name",-20} | {"Brand",-12} | {"Price",-5} | Extra info");
            Console.WriteLine(new string('-', 95));

            foreach (var a in appliancesFromJson)
            {
                Console.WriteLine(a.Output());
            }
            Console.WriteLine(new string('=', 95));
            Console.WriteLine($"Total appliances loaded from JSON: {appliancesFromJson.Count}");
            Console.WriteLine(new string('=', 95));

        }
    }
}
