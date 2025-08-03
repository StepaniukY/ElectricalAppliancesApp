using System.Runtime.Serialization;

namespace ElectricalAppliancesApp.Models
{
    [DataContract]
    public class WashingMachine : ElectricalAppliance
    {
        [DataMember]
        public int ProgramCount { get; set; }

        [DataMember]
        public double Volume { get; set; }

        public WashingMachine(string brand, string name, double price, int programCount, double volume) 
            :base(brand, name, price)
        {
            this.ProgramCount = programCount;
            this.Volume = volume;
        }

        public override string Output()
        {
            return base.Output() + $" | Programs: {ProgramCount,2} | Volume: {Volume,4:F1}";
        }
    }
}
