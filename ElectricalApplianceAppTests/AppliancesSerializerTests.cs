using ElectricalAppliancesApp.Models;
using ElectricalAppliancesApp.Services;

namespace ElectricalApplianceAppTests
{
    [TestClass]
    public class AppliancesSerializerTests : TestFileBase
    {
        [TestMethod]
        public void SerializeToJson_ValidData_CreatesFile()
        {
            var appliances = new List<ElectricalAppliance>
            {
                new VacuumCleaner("Samsung", "DustBlaster", 120, 800, "Black")
            };

            AppliancesSerializer.SerializeToJson(appliances, testFilePath);

            Assert.IsTrue(File.Exists(testFilePath), "JSON file must be created");
            var json = File.ReadAllText(testFilePath);
            Assert.IsTrue(json.Contains("Samsung"));
        }

        [TestMethod]
        public void DeserializeFromJson_ValidData_ReturnsList()
        {
            var appliances = new List<ElectricalAppliance>
            {
                new VacuumCleaner("LG", "MiniVac", 99.99, 400, "Green")
            };
            AppliancesSerializer.SerializeToJson(appliances, testFilePath);

            var result = AppliancesSerializer.DeserializeFromJson(testFilePath);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("LG", result[0].Brand);
        }

        [TestMethod]
        public void DeserializeFromJson_FileNotFound_ReturnsEmptyList()
        {
            var result = AppliancesSerializer.DeserializeFromJson("missing.json");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
