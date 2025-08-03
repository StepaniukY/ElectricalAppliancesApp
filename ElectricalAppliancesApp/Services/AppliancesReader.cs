using ElectricalAppliancesApp.Models;
using log4net;

namespace ElectricalAppliancesApp.Services
{
    public class AppliancesReader
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ElectricalAppliance[] Read(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File \"{filePath}\" not found.");
                    return Array.Empty<ElectricalAppliance>();
                }

                string[] lines = File.ReadAllLines(filePath);

                ElectricalAppliance[] appliances = new ElectricalAppliance[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(';');

                    if (parts.Length < 6)
                    {
                        Console.WriteLine($"Line {i + 1} has insufficient data: \"{lines[i]}\"");
                        continue;
                    }

                    string type = parts[0];
                    string brand = parts[1];
                    string name = parts[2];

                    if (!double.TryParse(parts[3], out double price) || double.IsNaN(price) || double.IsInfinity(price))
                    {
                        Console.WriteLine($"Invalid price on line {i + 1}: \"{parts[3]}\"");
                        continue;
                    }

                    try
                    {

                        switch (type)
                        {
                            case "VacuumCleaner":
                                if (int.TryParse(parts[4], out int powerVc))
                                {
                                    string color = parts[5];
                                    appliances[i] = new VacuumCleaner(brand, name, price, powerVc, color);
                                }
                                else
                                {
                                    Console.WriteLine($"Invalid power value on line {i + 1}: \"{parts[4]}\"");
                                }
                                break;

                            case "WashingMachine":
                                if (int.TryParse(parts[4], out int programCount) &&
                                    double.TryParse(parts[5], out double volume))
                                {
                                    appliances[i] = new WashingMachine(brand, name, price, programCount, volume);
                                }
                                else
                                {
                                    Console.WriteLine($"Invalid data for WashingMachine on line {i + 1}: \"{parts[4]}, {parts[5]}\"");
                                }
                                break;

                            case "FoodProcessor":
                                if (int.TryParse(parts[4], out int powerFp) &&
                                    int.TryParse(parts[5], out int functionCount))
                                {
                                    appliances[i] = new FoodProcessor(brand, name, price, powerFp, functionCount);
                                }
                                else
                                {
                                    Console.WriteLine($"Invalid data for FoodProcessor on line {i + 1}: \"{parts[4]}, {parts[5]}\"");
                                }
                                break;

                            default:
                                Console.WriteLine($"Unknown device type: {type} in line {i + 1}");
                                break;
                        }
                    }
                    catch (FormatException fe)
                    {
                        Log.Error($"Invalid number format on line {i + 1}: {fe.Message}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Unexpected error on line {i + 1}: {ex.Message}");
                    }
                }
                Console.WriteLine("Data successfully loaded from file:");
                Console.WriteLine($"    \"{filePath}\"");
                return appliances.Where(a => a != null).ToArray();

            }
            catch (IOException ioEx)
            {
                Log.Error($"File read error: {ioEx.Message}");
                return Array.Empty<ElectricalAppliance>();
            }
            catch (Exception ex)
            {
                Log.Error($"Unexpected error while reading file: {ex.Message}");
                return Array.Empty<ElectricalAppliance>();
            }
        }
        }
}
