using System.Text.Json;

namespace Bibliotekshanteringssystem03
{
    public class Program
    {
        static void Main(string[] args)
        {

            //string dataJsonFilePath = "LibraryData.json";
            //string alldatasomJSONType = File.ReadAllText(dataJsonFilePath);
            //MyDB myDB = JsonSerializer.Deserialize<MyDB>(alldatasomJSONType)!;
            LibrarySystem librarySystem = new LibrarySystem();
            


            DisplayUserLibrarySystemInteraction displayUserLibrarySystemInteraction = new DisplayUserLibrarySystemInteraction();
            displayUserLibrarySystemInteraction.Run();

        }

    }

        
    
}
