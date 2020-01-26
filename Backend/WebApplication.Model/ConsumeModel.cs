using System.Runtime.Serialization;

namespace WebApplication.Model
{
    [DataContract]
    public class ConsumeModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double AnnualCost { get; set; }
    }
}
