using ElectricalAppliancesApp.Services;

namespace ElectricalApplianceAppTests
{
    [TestClass]
    public class AppliancesServiceTests : TestFileBase
    {
        [TestMethod]
        public void ProcessWithoutBrand_ReadsInputAndWritesSortedOutput()
        {
            File.WriteAllText(inputPath, "VacuumCleaner;BrandA;ModelX;100;1200;Red");

            var service = new AppliancesService(inputPath, file1Path, file2Path);

            var result = service.ProcessWithoutBrand();

            Assert.AreEqual(1, result.Length);
            Assert.IsTrue(File.Exists(file1Path));
            var output = File.ReadAllText(file1Path);
            Assert.IsTrue(output.Contains("ModelX"));
        }

        [TestMethod]
        public void ProcessByBrand_FiltersAndWritesToFile2()
        {
            File.WriteAllText(inputPath,
                "VacuumCleaner;BrandA;ModelX;100;1200;Red\n" +
                "WashingMachine;BrandB;Washer1;300;5;50");

            var appliances = AppliancesReader.Read(inputPath);
            var service = new AppliancesService(inputPath, file1Path, file2Path);

            service.ProcessByBrand(appliances, "BrandA");

            Assert.IsTrue(File.Exists(file2Path));
            string output = File.ReadAllText(file2Path);
            Assert.IsTrue(output.Contains("ModelX"));
            Assert.IsFalse(output.Contains("Washer1"));
        }

    }
}
