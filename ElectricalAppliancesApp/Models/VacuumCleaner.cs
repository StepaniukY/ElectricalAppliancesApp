using System.Runtime.Serialization;

namespace ElectricalAppliancesApp.Models
{
    [DataContract]
    public class VacuumCleaner : ElectricalAppliance
    {
        [DataMember]
        public int PowerVc { get; set; }

        [DataMember]
        public string Color { get; set; }

        public VacuumCleaner(string brand, string name, double price, int powerVc, string color)
            : base(brand, name, price)
        {
            this.PowerVc = powerVc;
            this.Color = color;
        }

        public override string Output()
        {
            return base.Output() + $" | Power: {PowerVc,5} | Color: {Color,-10} ";
        }
    }
}
