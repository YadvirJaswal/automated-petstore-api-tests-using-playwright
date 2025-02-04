using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SwaggerPetstoreAutomation.Models
{
    public class OrderModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("petId")]
        public int PetId {  get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("shipDate")]
        public string ShipDate {  get; set; }

        [JsonPropertyName("status")]
        public string Status {  get; set; }

        [JsonPropertyName("complete")]
        public bool Complete {  get; set; }
    }
}
