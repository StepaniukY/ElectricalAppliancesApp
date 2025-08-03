using ElectricalAppliancesApp.Models;
using log4net;

namespace ElectricalAppliancesApp.Services
{
    public class AppliancesWriter
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void WriteSortedWithCountToFile(ElectricalAppliance[] appliances, string filePath)
        {
            try
            {
                if (appliances == null || appliances.Length == 0)
                {
                    Console.WriteLine("No appliances to write.");
                    return;
                }

                var grouped = appliances
                   .Where(a => a != null)
                   .GroupBy(a => a.Name)
                   .OrderBy(g => g.Key);

                using StreamWriter writer = new StreamWriter(filePath);
                {
                    foreach (var group in grouped)
                    {
                        string name = group.Key;
                        int count = group.Count();
                        writer.WriteLine($"{name} – {count} pcs.");
                    }
                }

                Console.WriteLine(new string('=', 95));
                Console.WriteLine("All appliances have been sorted by name and saved to:");
                Console.WriteLine($"    \"{filePath}\"");
                Console.WriteLine("     (includes name + count of each appliance)");
                Console.WriteLine(new string('=', 95));

            }
            catch (IOException ioEx)
            {
                Log.Error($"I/O error while writing to file \"{filePath}\": {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Log.Error($"Unexpected error in WriteSortedWithCountToFile: {ex.Message}");
            }
        }

        public static void WriteByBrandToFile(ElectricalAppliance[] appliances, string brand, string filePath)
        {
            try
            {
                if (appliances == null || appliances.Length == 0)
                {
                    Console.WriteLine("No appliances to write");
                    Console.WriteLine(new string('=', 95));
                    return;
                }

                if (string.IsNullOrWhiteSpace(brand))
                {
                    Console.WriteLine("Brand not provided. Skipping brand filtering");
                    Console.WriteLine(new string('=', 95));
                    return;
                }

                var filtered = appliances.Where(a => a.Brand == brand);

                if (filtered.Count() == 0)
                {
                    Console.WriteLine($"No appliances found for brand \"{brand}\"");
                    Console.WriteLine(new string('=', 95));
                    return;
                }

                double total = filtered.Sum(a => a.Price);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Electrical appliances of the {brand}:");

                    foreach (var item in filtered)
                    {
                        writer.WriteLine($"- {item.Name}: {item.Price} USD");
                    }

                    writer.WriteLine($"Total price: {total} USD");
                }

                Console.WriteLine($"\nAppliances of brand \"{brand}\" have been saved to:");
                Console.WriteLine($"    \"{filePath}\"");
                Console.WriteLine("    (includes name + total price)");
                Console.WriteLine(new string('=', 95));
            }
            catch (IOException ioEx)
            {
                Log.Error($"I/O error while writing to file \"{filePath}\": {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Log.Error($"Unexpected error in WriteByBrandToFile: {ex.Message}");
            }
        }

    }
}
