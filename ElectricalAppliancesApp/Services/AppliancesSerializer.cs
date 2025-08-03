using ElectricalAppliancesApp.Models;
using log4net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace ElectricalAppliancesApp.Services
{
    public class AppliancesSerializer
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void SerializeToJson(List<ElectricalAppliance> appliances, string filePath)
        {
            try
            {
                if (appliances == null || appliances.Count == 0)
                {
                    Console.WriteLine("No appliances to serialize.");
                    return;
                }

                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<ElectricalAppliance>));
                using FileStream stream = new FileStream(filePath, FileMode.Create);
                json.WriteObject(stream, appliances);
            }
            catch (IOException ioEx)
            {
                Log.Error($"I/O error during JSON serialization: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Log.Error($"Error during JSON serialization: {ex.Message}");
            }  
        }

        public static List<ElectricalAppliance> DeserializeFromJson(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"JSON file \"{filePath}\" not found");
                    Console.WriteLine(new string('=', 95));
                    return new List<ElectricalAppliance>();
                }

                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<ElectricalAppliance>));
                using FileStream stream = new FileStream(filePath, FileMode.Open);

                var result = (List<ElectricalAppliance>)json.ReadObject(stream);

                if (result == null || result.Count == 0)
                {
                    Console.WriteLine("No data found in the JSON file");
                }

                return result ?? new List<ElectricalAppliance>();
            }
            catch (SerializationException se)
            {
                Log.Error($"Serialization error while reading JSON: {se.Message}");
                return new List<ElectricalAppliance>();
            }
            catch (IOException ioEx)
            {
                Log.Error($"I/O error during JSON deserialization: {ioEx.Message}");
                return new List<ElectricalAppliance>();
            }
            catch (Exception ex)
            {
                Log.Error($"Error during JSON deserialization: {ex.Message}");
                return new List<ElectricalAppliance>();
            }
        }

    }
}
