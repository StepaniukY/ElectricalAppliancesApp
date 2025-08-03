using ElectricalAppliancesApp.Models;
using ElectricalAppliancesApp.Services;

namespace ElectricalApplianceAppTests
{
    [TestClass]
    public class AppliancesWriterTests : TestFileBase
    {
        [TestMethod]
        public void WriteSortedWithCountToFile_ValidData_WritesSortedCount()
        {
            var appliances = new ElectricalAppliance[]
            {
                new VacuumCleaner("LG", "DustMax", 100, 1200, "Black"),
                new VacuumCleaner("Samsung", "DustMax", 110, 1300, "White"),
                new WashingMachine("LG", "CleanPro", 200, 8, 5.5)
            };

            AppliancesWriter.WriteSortedWithCountToFile(appliances, testFilePath);

            var lines = File.ReadAllLines(testFilePath);

            Assert.AreEqual(2, lines.Length);
            Assert.IsTrue(lines[0].StartsWith("CleanPro – 1"));
            Assert.IsTrue(lines[1].StartsWith("DustMax – 2"));
        }

        [TestMethod]
        public void WriteSortedWithCountToFile_NullInput_DoesNotThrow()
        {
            AppliancesWriter.WriteSortedWithCountToFile(null, testFilePath);
            Assert.IsFalse(File.Exists(testFilePath) && new FileInfo(testFilePath).Length > 0);
        }

        [TestMethod]
        public void WriteSortedWithCountToFile_ArrayContainsNull_IgnoresNullsAndWritesCorrectly()
        {
            var appliances = new ElectricalAppliance[]
            {
                new WashingMachine("LG", "QuickSpin", 300, 8, 6),
                null,
                new WashingMachine("LG", "QuickSpin", 350, 8, 7)
            };

            AppliancesWriter.WriteSortedWithCountToFile(appliances, testFilePath);

            var lines = File.ReadAllLines(testFilePath);
            CollectionAssert.AreEqual(new[]
            {
                "QuickSpin – 2 pcs."
            }, lines);
        }

        [TestMethod]
        public void WriteByBrandToFile_ValidBrand_WritesCorrectData()
        {
            var appliances = new ElectricalAppliance[]
            {
                new VacuumCleaner("LG", "DustMax", 100, 1200, "Black"),
                new WashingMachine("LG", "CleanPro", 200, 8, 5.5),
                new FoodProcessor("Bosch", "FoodMaster", 300, 800, 5)
            };

            AppliancesWriter.WriteByBrandToFile(appliances, "LG", testFilePath);

            var content = File.ReadAllText(testFilePath);

            Assert.IsTrue(content.Contains("Electrical appliances of the LG:"));
            Assert.IsTrue(content.Contains("DustMax"));
            Assert.IsTrue(content.Contains("CleanPro"));
            Assert.IsTrue(content.Contains("Total price: 300"));
        }

        [TestMethod]
        public void WriteByBrandToFile_InvalidBrand_WritesNoData()
        {
            var appliances = new ElectricalAppliance[]
            {
                new VacuumCleaner("LG", "DustMax", 100, 1200, "Black")
            };

            AppliancesWriter.WriteByBrandToFile(appliances, "Nonexistent", testFilePath);

            var content = File.ReadAllText(testFilePath);
            Assert.IsTrue(!File.Exists(testFilePath) || new FileInfo(testFilePath).Length == 0);
        }

        [TestMethod]
        public void WriteByBrandToFile_NullBrand_DoesNotWriteFile()
        {
            var appliances = new ElectricalAppliance[]
            {
                new VacuumCleaner("LG", "PowerClean", 100, 1200, "Gray")
            };

            AppliancesWriter.WriteByBrandToFile(appliances, null, testFilePath);

            Assert.IsFalse(File.Exists(testFilePath) && new FileInfo(testFilePath).Length > 0);
        }
    }
}
