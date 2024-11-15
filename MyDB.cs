
using System.Text.Json.Serialization;

namespace Bibliotekshanteringssystem03
{
    public class MyDB
    {
        [JsonPropertyName("Books")]
        public List<Book> AllbooksfromDB { get; set; } = new List<Book>();

        [JsonPropertyName("Authors")]
        public List<Author> allaAuthorsDatafromDB { get; set; } = new List<Author>();
    }
}
