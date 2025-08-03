using System.Runtime.Serialization;

namespace ElectricalAppliancesApp.Models
{
    [DataContract]
    [KnownType(typeof(VacuumCleaner))]
    [KnownType(typeof(WashingMachine))]
    [KnownType(typeof(FoodProcessor))]
    public abstract class ElectricalAppliance
    {
        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Price { get; set; }

        protected ElectricalAppliance(string brand, string name, double price)
        {
            this.Brand = brand;
            this.Name = name;
            this.Price = price;
        }

        public virtual string Output()
        {
            return $"{GetType().Name,-15} | {Name,-20} | {Brand,-12} | {Price,-5}";
        }
    }
}
