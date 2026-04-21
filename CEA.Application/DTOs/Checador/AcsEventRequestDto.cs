using System.Text.Json.Serialization;

namespace CEA.Application.DTOs.Checador
{
    // Clase raíz del JSON que envías (LA SOLICITUD)
    public class AcsEventRequestDto
    {
        [JsonPropertyName("AcsEventCond")]
        public AcsEventCond AcsEventCond { get; set; }
    }

    public class AcsEventCond
    {
        [JsonPropertyName("searchID")]
        public string SearchID { get; set; } = "1";

        [JsonPropertyName("searchResultPosition")]
        public int SearchResultPosition { get; set; } = 0;

        [JsonPropertyName("maxResults")]
        public int MaxResults { get; set; } = 1000;

        [JsonPropertyName("major")]
        public int Major { get; set; } = 5;

        [JsonPropertyName("minor")]
        public int Minor { get; set; } = 75;

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }
    }
}