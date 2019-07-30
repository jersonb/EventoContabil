using System.Collections.Generic;
using Newtonsoft.Json;

namespace EventoContabil.Models
{
    public class JsonObjects
    {
        [JsonProperty("sucess")]
        public bool Sucess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("result")]
        public List<EventoConvert> Result { get; set; }

        public List<EventoConvert> ToJson(object json)
        {
            var jsonObject = JsonConvert.DeserializeObject<JsonObjects>(json.ToString());
            var listEventoConvert = jsonObject.Result;
            return listEventoConvert;
        }
    }
}