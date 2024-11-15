using System.Text.Json;

namespace Bibliotekshanteringssystem03
{
    public class DisplayUserLibrarySystemInteraction
    {
        LibrarySystem library = new LibrarySystem();


        public void Run()
        {
            LibrarySystem library = new LibrarySystem();
            string dataJsonFilePath = "LibraryData.json";
            string alldatasomJSONType = File.ReadAllText(dataJsonFilePath);
            MyDB myDB = JsonSerializer.Deserialize<MyDB>(alldatasomJSONType)!;
            LibrarySystem librarySystem = new LibrarySystem();


           
            bool running = true;

            while (running)
            {
                DisplayMenu();
                string userInputsomString = Console.ReadLine()!;
                int userInputInt = Convert.ToInt32(userInputsomString);

                switch (userInputInt)
                {

                    case 1:

                        librarySystem.AddnewBook(myDB);
                        break;
                    case 2:
                        librarySystem.AddnewAuthor(myDB);

                        break;
                    case 3:

                        librarySystem.UpdateBookDetails(myDB);
                        break;
                    case 4:

                        librarySystem.UpdateAuthorDetails(myDB);
                        break;
                    case 5:

                        librarySystem.DeleteBook(myDB);
                        break;
                    case 6:

                        librarySystem.DeleteAuthor(myDB);
                        break;
                    case 7:

                        librarySystem.ListAll(myDB.AllbooksfromDB, myDB.allaAuthorsDatafromDB);
                        break;
                    case 8:
                        librarySystem.SearchAndFilterBooks(myDB);
                        break;
                    case 9:
                        librarySystem.SaveAllDataandExit(myDB);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again!");
                        break;




                }

            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("--------welcome to Hanans Librar--------");
            Console.WriteLine("\n------ Library Menu ------");
            Console.WriteLine("Please Choose one option: ");
            Console.WriteLine("1. Add New Book");
            Console.WriteLine("2. Add New Author");
            Console.WriteLine("3. Update Book details");
            Console.WriteLine("4. Update Author details");
            Console.WriteLine("5. Delete Book");
            Console.WriteLine("6. Delete Author");
            Console.WriteLine("7. List All Books and Author");
            Console.WriteLine("8. Search and Filter Books");
            Console.WriteLine("9. Save All Data And Exit");
            Console.WriteLine("0. Exit");

        }
    }
}