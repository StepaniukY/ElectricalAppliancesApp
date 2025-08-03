using ElectricalAppliancesApp.Models;
using System.Runtime.Serialization.Json;

namespace ElectricalApplianceAppTests
{
    [TestClass]
    public class ElectricalApplianceTests : TestFileBase
    {
        [TestMethod]
        public void Output_ReturnsFormattedString_ForVacuumCleaner()
        {
            var appliance = new VacuumCleaner("Samsung", "DustPro", 149.99, 2000, "Red");

            var output = appliance.Output();

            StringAssert.Contains(output, "VacuumCleaner");
            StringAssert.Contains(output, "DustPro");
            StringAssert.Contains(output, "Samsung");
            StringAssert.Contains(output, "149.99");
        }

        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            var appliance = new FoodProcessor("Bosch", "MultiPro", 299.99, 1000, 5);

            Assert.AreEqual("Bosch", appliance.Brand);
            Assert.AreEqual("MultiPro", appliance.Name);
            Assert.AreEqual(299.99, appliance.Price);
        }

        [TestMethod]
        public void Appliance_CanBeSerializedAndDeserialized()
        {
            var original = new WashingMachine("LG", "WashX", 499.99, 10, 7.5);

            var serializer = new DataContractJsonSerializer(typeof(List<ElectricalAppliance>));
            using (var stream = new FileStream(testFilePath, FileMode.Create))
            {
                serializer.WriteObject(stream, new List<ElectricalAppliance> { original });
            }

            List<ElectricalAppliance> deserialized;
            using (var stream = new FileStream(testFilePath, FileMode.Open))
            {
                deserialized = (List<ElectricalAppliance>)serializer.ReadObject(stream);
            }

            Assert.AreEqual(1, deserialized.Count);
            Assert.AreEqual("LG", deserialized[0].Brand);
            Assert.AreEqual("WashX", deserialized[0].Name);
            Assert.AreEqual(499.99, deserialized[0].Price);
        }
    }
}
