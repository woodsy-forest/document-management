using System.Text.Json.Serialization;

namespace DocumentManagement.Dto
{
    public class DocumentDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Location")]
        public string Location { get; set; }

        [JsonPropertyName("FileSize")]
        public long FileSize { get; set; }

    }
}
