using ElectricalAppliancesApp.Models;
using ElectricalAppliancesApp.Services;

namespace ElectricalApplianceAppTests
{
    [TestClass]
    public sealed class AppliancesReaderTests : TestFileBase
    {
        [TestMethod]
        public void Read_FileDoesNotExist_ReturnsEmptyArray()
        {
            var result = AppliancesReader.Read("nonexistent.txt");
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Read_LineWithInvalidDataLength_LogsWarningAndSkips()
        {
            File.WriteAllText(testFilePath, "VacuumCleaner;BrandX;ModelX;299.99");

            var result = AppliancesReader.Read(testFilePath);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Read_InvalidPrice_SkipsLine()
        {
            File.WriteAllText(testFilePath, "VacuumCleaner;BrandX;ModelX;NaN;500;Black");

            var result = AppliancesReader.Read(testFilePath);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Read_ValidVacuumCleanerLine_ReturnsOneItem()
        {
            File.WriteAllText(testFilePath, "VacuumCleaner;Dyson;V11;499.99;600;Black");

            var result = AppliancesReader.Read(testFilePath);
            Assert.AreEqual(1, result.Length);
            Assert.IsInstanceOfType(result[0], typeof(VacuumCleaner));
            Assert.AreEqual("Dyson", result[0].Brand);
        }

        [TestMethod]
        public void Read_ValidWashingMachineLine_ReturnsOneItem()
        {
            File.WriteAllText(testFilePath, "WashingMachine;Bosch;Serie6;699.99;12;7.5");

            var result = AppliancesReader.Read(testFilePath);
            Assert.AreEqual(1, result.Length);
            Assert.IsInstanceOfType(result[0], typeof(WashingMachine));
        }

        [TestMethod]
        public void Read_ValidFoodProcessorLine_ReturnsOneItem()
        {
            File.WriteAllText(testFilePath, "FoodProcessor;Philips;HR1234;249.99;800;10");

            var result = AppliancesReader.Read(testFilePath);
            Assert.AreEqual(1, result.Length);
            Assert.IsInstanceOfType(result[0], typeof(FoodProcessor));
        }

        [TestMethod]
        public void Read_UnknownType_SkipsLine()
        {
            File.WriteAllText(testFilePath, "Toaster;BrandY;X1;59.99;700;Silver");

            var result = AppliancesReader.Read(testFilePath);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Read_InvalidPowerInVacuumCleaner_SkipsLine()
        {
            File.WriteAllText(testFilePath, "VacuumCleaner;Dyson;V12;499.99;notanumber;Black");

            var result = AppliancesReader.Read(testFilePath);
            Assert.AreEqual(0, result.Length);
        }
    }
}
