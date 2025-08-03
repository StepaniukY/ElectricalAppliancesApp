using System.Runtime.Serialization;

namespace ElectricalAppliancesApp.Models
{
    [DataContract]
    public class FoodProcessor : ElectricalAppliance
    {
        [DataMember]
        public int PowerFp { get; set; }

        [DataMember]
        public int FunctionCount { get; set; }

        public FoodProcessor(string brand, string name, double price, int powerFp, int functionCount)
            : base(brand, name, price)
        {
            this.PowerFp = powerFp;
            this.FunctionCount = functionCount;
        }

        public override string Output()
        {
            return base.Output() + $" | Power: {PowerFp,5} | Functions: {FunctionCount,2} ";
        }
    }
}
