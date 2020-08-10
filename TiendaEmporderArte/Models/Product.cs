namespace TiendaEmporderArte.Models
{
    using Newtonsoft.Json;
    using TiendaEmporderArte.Models.Enums;
    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "size")]
        public Size Size { get; set; }
        [JsonProperty(PropertyName = "color")]
        public Color Color { get; set; }

    }
}
