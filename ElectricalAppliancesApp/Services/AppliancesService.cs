using ElectricalAppliancesApp.Models;

namespace ElectricalAppliancesApp.Services
{
    public class AppliancesService
    {
        private readonly string inputPath;
        private readonly string file1Path;
        private readonly string file2Path;

        public AppliancesService()
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            inputPath = Path.Combine(desktop, "Electrical appliance.txt");
            file1Path = Path.Combine(desktop, "file1.txt");
            file2Path = Path.Combine(desktop, "file2.txt");
        }

        public AppliancesService(string inputPath, string file1Path, string file2Path)
        {
            this.inputPath = inputPath;
            this.file1Path = file1Path;
            this.file2Path = file2Path;
        }

        public ElectricalAppliance[] ProcessWithoutBrand()
        {
            ElectricalAppliance[] appliances = AppliancesReader.Read(inputPath);
            AppliancesWriter.WriteSortedWithCountToFile(appliances, file1Path);

            return appliances;
        }

        public void ProcessByBrand(ElectricalAppliance[] appliances, string brand)
        {
            AppliancesWriter.WriteByBrandToFile(appliances, brand, file2Path);
        }

    }
}
