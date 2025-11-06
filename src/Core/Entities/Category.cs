using System.Text.Json.Serialization;

namespace SpendWise.Core.Entities
{
    // El convertidor permite enviar/recibir los valores como texto legible en Swagger
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Category
    {
        Transporte,
        Comida,
        Salud,
        Entretenimiento,
        Varios,
        Educación,     
        Vivienda,
        Ahorro     
    }
}
