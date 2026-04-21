using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CEA.Application.DTOs.Checador
{
    // Clase raíz que contiene todo el evento (LA RESPUESTA)
    public class AcsEventResponse
    {
        [JsonPropertyName("AcsEvent")]
        public AcsEvent AcsEvent { get; set; }
    }

    public class AcsEvent
    {
        [JsonPropertyName("searchID")]
        public string SearchID { get; set; }

        [JsonPropertyName("totalMatches")]
        public int TotalMatches { get; set; }

        [JsonPropertyName("responseStatusStrg")]
        public string ResponseStatusString { get; set; }

        [JsonPropertyName("numOfMatches")]
        public int NumOfMatches { get; set; }

        [JsonPropertyName("InfoList")]
        public List<ChecadorEventInfo> InfoList { get; set; } // <-- CAMBIO AQUÍ
    }

    // RENOMBRAMOS LA CLASE de EventInfo a ChecadorEventInfo
    public class ChecadorEventInfo
    {
        [JsonPropertyName("major")]
        public int Major { get; set; }

        [JsonPropertyName("minor")]
        public int Minor { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("cardNo")]
        public string CardNo { get; set; }

        [JsonPropertyName("cardType")]
        public int CardType { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cardReaderNo")]
        public int CardReaderNo { get; set; }

        [JsonPropertyName("doorNo")]
        public int DoorNo { get; set; }

        [JsonPropertyName("employeeNoString")]
        public string EmployeeNoString { get; set; }

        [JsonPropertyName("serialNo")]
        public int SerialNo { get; set; }

        [JsonPropertyName("userType")]
        public string UserType { get; set; }

        [JsonPropertyName("currentVerifyMode")]
        public string CurrentVerifyMode { get; set; }

        [JsonPropertyName("mask")]
        public string Mask { get; set; }

        [JsonPropertyName("pictureURL")]
        public string PictureURL { get; set; }

        [JsonPropertyName("FaceRect")]
        public FaceRect FaceRect { get; set; }
    }

    public class FaceRect
    {
        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }
    }
}