using Newtonsoft.Json;

namespace FlexisourceIT.Models
{
    /// <summary>
    /// Rainfall reading response
    /// </summary>
    public class RainfallReadingResponse
    {
        public List<RainfallReading> Readings { get; set; }
    }

    /// <summary>
    /// Details of a rainfall reading
    /// </summary>
    public class RainfallReading
    {
        //[JsonProperty("@id")]
        //public string Id { get; set; }
        //public DateTime DateTime { get; set; }
        //public string Measure { get; set; }
        //public decimal Value { get; set; }

        [JsonProperty("DateTime")]
        public DateTime DateMeasured { get; set; }
        [JsonProperty("Value")]
        public decimal AmountMeasured { get; set; }
    }

    public class RootObject
    {
        public string Context { get; set; }
        public Meta Meta { get; set; }
        public List<RainfallReading> Items { get; set; } // Modified to include Items
    }

    public class Meta
    {
        public string Publisher { get; set; }
        public string Licence { get; set; }
        public string Documentation { get; set; }
        public string Version { get; set; }
        public string Comment { get; set; }
        public List<string> HasFormat { get; set; }
        public int Limit { get; set; }
    }
}
