using System;

using Newtonsoft.Json;

namespace B2CGraphSDK.Models
{
    public class ServiceResult<T>
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }

        [JsonProperty("value")]
        public T Value { get; set; }
    }
}