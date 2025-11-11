using System.Text.Json.Serialization;
using System.Collections.Generic;

// Archivo: Models/AcsEventDtos.cs

// Clase raíz que contiene todo el evento
public class AcsEventResponse
{
    [JsonPropertyName("AcsEvent")]
    public AcsEvent AcsEvent { get; set; }
}

// Representa el objeto "AcsEvent"
public class AcsEvent
{
    [JsonPropertyName("searchID")]
    public string SearchID { get; set; }

    [JsonPropertyName("totalMatches")]
    public int TotalMatches { get; set; }

    [JsonPropertyName("responseStatusStrg")]
    public string ResponseStatusString { get; set; }

    [JsonPropertyName("InfoList")]
    public List<EventInfo> InfoList { get; set; }
}

// Representa cada uno de los eventos en "InfoList"
public class EventInfo
{
    [JsonPropertyName("major")]
    public int Major { get; set; }

    [JsonPropertyName("minor")]
    public int Minor { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("employeeNoString")]
    public string EmployeeNoString { get; set; }

    [JsonPropertyName("serialNo")]
    public int SerialNo { get; set; }

    [JsonPropertyName("pictureURL")]
    public string PictureURL { get; set; }

    // Agrega aquí cualquier otro campo que necesites del JSON...
}