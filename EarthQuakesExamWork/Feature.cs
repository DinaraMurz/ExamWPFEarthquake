using Newtonsoft.Json;

namespace EarthQuakesExamWork
{

    public class Feature
    {
        [JsonProperty("properties")]
        public Information Information { get; set; }
        public string Id { get; set; }
    }

}