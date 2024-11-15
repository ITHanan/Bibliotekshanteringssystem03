

using System.Text.Json;

namespace Bibliotekshanteringssystem03
{
    public class LibrarySystem
    {

        SystemGeneriskmAdministration<Book> bookAdministration = new SystemGeneriskmAdministration<Book>();
        SystemGeneriskmAdministration<Author> AuthorAdministration = new SystemGeneriskmAdministration<Author>();






        public void AddnewBook(MyDB myDB)

        {
            var bookAdministration = new SystemGeneriskmAdministration<Book>();

            foreach (var b in myDB.AllbooksfromDB)
            {
                bookAdministration.AddTo(b);
            }


            Console.WriteLine("------Please, enter all book's details------\n");
            Console.WriteLine("Enter book title:");
            string BookTitle = Console.ReadLine()!;




            Console.WriteLine("Enter author name:");
            string name = Console.ReadLine()!;



            var author = myDB.allaAuthorsDatafromDB.FirstOrDefault(author => author.Name.Equals(name, StringComparison.OrdinalIgnoreCase))!;

            if (author == null)
            {

                Console.WriteLine("The author not found. Add the Author first ");
                return;

            }

            Console.WriteLine("Enter genre:");
            string genre = Console.ReadLine()!;

            Console.WriteLine("Enter Published Year:");

            if (!int.TryParse(Console.ReadLine(), out int publishedYear))
            {
                Console.WriteLine("Invalid input for Published Year. Please enter a valid number.");
                return;
            }

            Console.WriteLine("Enter ISBN Code:");

            if (!int.TryParse(Console.ReadLine(), out int isbnCode))
            {
                Console.WriteLine("Invalid input for ISBN Code. Please enter a valid number.");
                return;
            }



            Book newbooks = new(myDB.allaAuthorsDatafromDB.Count + 1, BookTitle, name, genre, publishedYear, isbnCode)

            {
                Id = bookAdministration.GetAll().Count + 1,  // Set a unique ID for the new book
                BookTitle = BookTitle,
                Name = name,
                Genre = genre,
                PublishedYear = publishedYear,
                ISBNCode = isbnCode,
                Ratings = new List<int>()
            }; // 


            bookAdministration.AddTo(newbooks);

            myDB.AllbooksfromDB = bookAdministration.GetAll();

            author.booksIsWritten.Add(newbooks.Id);

            SaveAllData(myDB);

            MirrorChangesToProjectRoot("LibraryData.json");

            Console.WriteLine($"The book {BookTitle} added successfully.");


        }



        public void AddnewAuthor(MyDB myDB)
        {

            // Initialize the generic administration system for managing authors
            var AuthorAdministration = new SystemGeneriskmAdministration<Author>();

            // Load all existing authors from `minLillaDB.allaAuthorsDatafromDB` into `AuthorRepo`
            foreach (var a in myDB.allaAuthorsDatafromDB)
            {
                AuthorAdministration.AddTo(a);
            }

            Console.WriteLine("------Please, enter all author's details------\n");

            // Get author's name
            Console.WriteLine("Enter Author's name:");
            string name = Console.ReadLine()!;

            // Get author's nationality

            Console.WriteLine("Enter Author's nationality:");
            string authorsCountry = Console.ReadLine()!;

            // Create a new author object with a unique ID


            Author newAuthor = new(myDB.allaAuthorsDatafromDB.Count + 1, name, authorsCountry)
            {
                Id = myDB.allaAuthorsDatafromDB.Count + 1,
                Name = name,
                AuthorsCountry = authorsCountry
            };

            // Add the new author to the repository


            AuthorAdministration.AddTo(newAuthor);

            // Sync the updated list of authors back to `minLillaDB.allaAuthorsDatafromDB`


            myDB.allaAuthorsDatafromDB = AuthorAdministration.GetAll();

            // Save changes to the JSON file

            SaveAllData(myDB);


            MirrorChangesToProjectRoot("LibraryData.json");

            Console.WriteLine("Auther added successfully");

        }

        public void ListAll(List<Book> books, List<Author> authors)
        {
            Console.WriteLine("Books:");
            foreach (var book in books)

                Console.WriteLine($"{book.Id}:{book.BookTitle} by {book.Name} - Rating {book.BooksAveragerating}");

            Console.WriteLine("\nAuthors:");
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.Id}:{author.Name} from: {author.AuthorsCountry}");


            }

        }

        public void UpdateBookDetails(MyDB myDB)
        {
            //Logic
            SystemGeneriskmAdministration<Book> bookAdministration = new SystemGeneriskmAdministration<Book>();

            foreach (var b in myDB.AllbooksfromDB)
            {
                bookAdministration.AddTo(b);

            }

            int bookUpdateID = int.Parse(Prompt("Enter the book Id that you want to Update:"));

            var book = bookAdministration.GetById(bookUpdateID);


            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }
            book.BookTitle = Prompt($"Enter new title of the current{book.BookTitle}:", book.BookTitle);

            book.Genre = Prompt($"Enter new genre of the current {book.Genre}:", book.Genre);

            book.PublishedYear = int.Parse(Prompt($"Enter new publication year of the current {book.PublishedYear}:", book.PublishedYear.ToString()));

            bookAdministration.Updater(book);

            myDB.AllbooksfromDB = bookAdministration.GetAll();

            SaveAllData(myDB);

            MirrorChangesToProjectRoot("LibraryData.json");

            Console.WriteLine("Book details updated");

        }





        //logic

        public void UpdateAuthorDetails(MyDB myDB)
        {
            // Initialize the generic administration system for authors
            SystemGeneriskmAdministration<Author> AuthorAdministration = new SystemGeneriskmAdministration<Author>();

            // Load all authors from `minLillaDB.allaAuthorsDatafromDB` into `authorAdministration`
            foreach (var a in myDB.allaAuthorsDatafromDB)
            {
                AuthorAdministration.AddTo(a);
            }

            // Prompt for the ID of the author to update
            int updatedAuthorID = int.Parse(Prompt("Enter the author Id number that you want to update:"));

            // Use the generic class to get the author by ID
            var author = AuthorAdministration.GetById(updatedAuthorID);

            if (author == null)
            {
                Console.WriteLine("Author not found");
                return;
            }

            // Prompt for new author details
            author.Name = Prompt($"Enter new author name (current name: {author.Name}):", author.Name);
            author.AuthorsCountry = Prompt($"Enter new country (current country: {author.AuthorsCountry}):", author.AuthorsCountry);

            // Update the author details in the administration system
            AuthorAdministration.Updater(author);

            // Sync the updated authors back to `minLillaDB.allaAuthorsDatafromDB`
            myDB.allaAuthorsDatafromDB = AuthorAdministration.GetAll();

            // Save changes to the JSON file
            SaveAllData(myDB);
            MirrorChangesToProjectRoot("LibraryData.json");

            Console.WriteLine("Author details updated.");



        }

        public void DeleteBook(MyDB myDB)
        {
            // Initialize the generic administration system for managing books
            var bookRepo = new SystemGeneriskmAdministration<Book>();

            // Load all books from `minLillaDB.AllbooksfromDB` into `bookRepo`
            foreach (var b in myDB.AllbooksfromDB)
            {
                bookRepo.AddTo(b);
            }

            // Prompt for the book ID to delete
            int bookIDtoDelete = int.Parse(Prompt("Enter the book ID that you want to delete:"));

            // Retrieve the book by ID using the generic class
            var book = bookRepo.GetById(bookIDtoDelete);
            if (book == null)
            {
                Console.WriteLine("The book is not found.");
                return;
            }

            // Remove the book from the repository
            bookRepo.Remove(bookIDtoDelete);

            // Update the author's list of written books to remove the deleted book ID
            var author = myDB.allaAuthorsDatafromDB.FirstOrDefault(author => author.Name == book.Name);
            if (author != null)
            {
                author.booksIsWritten.Remove(bookIDtoDelete);
                Console.WriteLine("Book has been deleted.");
            }

            // Sync the updated books back to `minLillaDB.AllbooksfromDB`
            myDB.AllbooksfromDB = bookRepo.GetAll();

            // Save changes to the JSON file
            SaveAllData(myDB);
            MirrorChangesToProjectRoot("LibraryData.json");

        }


        public void DeleteAuthor(MyDB myDB)
        {
            // Initialize the generic administration systems for managing authors and books
            var AuthorAdministration = new SystemGeneriskmAdministration<Author>();
            var bookAdministration = new SystemGeneriskmAdministration<Book>();

            // Load existing authors and books into the generic repositories
            foreach (var a in myDB.allaAuthorsDatafromDB)
            {
                AuthorAdministration.AddTo(a);
            }
            foreach (var b in myDB.AllbooksfromDB)
            {
                bookAdministration.AddTo(b);
            }

            // Prompt for the author ID to delete
            int authorIdToDelete = int.Parse(Prompt("Enter the author Id to delete:"));

            // Retrieve the author by ID using the generic class
            var author = AuthorAdministration.GetById(authorIdToDelete);
            if (author == null)
            {
                Console.WriteLine("Author not found.");
                return;
            }

            // Remove the author from the repository
            AuthorAdministration.Remove(authorIdToDelete);

            // Remove all books associated with this author from the book repository
            foreach (var book in bookAdministration.GetAll().Where(book => book.Name == author.Name).ToList())
            {
                bookAdministration.Remove(book.Id);
            }

            // Sync the updated lists back to the main database in MinLillaDB
            myDB.allaAuthorsDatafromDB = AuthorAdministration.GetAll();
            myDB.AllbooksfromDB = bookAdministration.GetAll();

            Console.WriteLine("Author and their books have been deleted.");

            // Save changes to the JSON file
            SaveAllData(myDB);
            MirrorChangesToProjectRoot("LibraryData.json");
        }


        public void SearchAndFilterBooks(MyDB myDB)
        {
            // Initialize the book repository using the generic class
            var bookAdministration = new SystemGeneriskmAdministration<Book>();

            // Load existing books into the repository
            foreach (var book in myDB.AllbooksfromDB)
            {
                bookAdministration.AddTo(book);
            }

            Console.WriteLine("--------Search and Filter Option:--------");
            Console.WriteLine("1. Search by Genre ");
            Console.WriteLine("2. Search by Author");
            Console.WriteLine("3. Search by Publication Year");
            Console.WriteLine("4. List all books with Average Rating Above Threshold");
            Console.WriteLine("5. Sort books by Publication Year");
            Console.WriteLine("6. Sort books by Title");
            Console.WriteLine("7. Sort books by Author Name");
            Console.WriteLine("8. Add Rating to a Book");
            Console.WriteLine("Choose one option:");

            string alternatives = Console.ReadLine()!;

            IEnumerable<Book> filteredBooks = bookAdministration
                .GetAll();
            IEnumerable<Book> sortedBooks = bookAdministration.GetAll();

            switch (alternatives)
            {
                case "1":
                    string genre = Prompt("Enter Genre: ");
                    filteredBooks = filteredBooks.Where(book => book.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
                    DisplayBooks(filteredBooks);
                    break;

                case "2":
                    string name = Prompt("Enter Author Name:");
                    filteredBooks = filteredBooks.Where(book => book.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                    DisplayBooks(filteredBooks);
                    break;

                case "3":
                    int year = int.Parse(Prompt("Enter Published Year: "));
                    filteredBooks = filteredBooks.Where(book => book.PublishedYear == year);
                    DisplayBooks(filteredBooks);
                    break;

                case "4":
                    Console.WriteLine("Enter the rating threshold (1-5): ");
                    double thresholdRating = Convert.ToDouble(Console.ReadLine());

                    var booksAboveThreshold = bookAdministration.GetAll().OfType<Book>()
                        .Where(book => book.BooksAveragerating > thresholdRating);

                    DisplayBooks(booksAboveThreshold, $"Books with average rating above {thresholdRating}");
                    break;


                case "5":
                    sortedBooks = sortedBooks.OrderBy(book => book.PublishedYear);
                    DisplayBooks(sortedBooks, "Books sorted by Publication Year:");
                    break;

                case "6":
                    sortedBooks = sortedBooks.OrderBy(book => book.BookTitle);
                    DisplayBooks(sortedBooks, "Books sorted by Title:");
                    break;

                case "7":
                    sortedBooks = sortedBooks.OrderBy(book => book.BookTitle);
                    DisplayBooks(sortedBooks, "Books sorted by Author Name:");
                    break;

                case "8":
                    int bookIdToRate = int.Parse(Prompt("Enter the Book ID to rate:"));
                    int rating = int.Parse(Prompt("Enter rating (1-5):"));

                    if (bookAdministration.AddRating(bookIdToRate, rating))
                    {
                        Console.WriteLine("Rating added successfully.");
                        SaveAllData(myDB);
                        MirrorChangesToProjectRoot("LibraryData.json");
                    }
                    else
                    {
                        Console.WriteLine("Invalid rating or book ID.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }



        }

        public void AddRatingToBookOption(MyDB myDB)
        {
            var bookAdministration = new SystemGeneriskmAdministration<Book>();

            int bookIdToRate = int.Parse(Prompt("Enter the Book ID to rate:"));
            int rating = int.Parse(Prompt("Enter rating (1-5):"));

            if (bookAdministration.AddRating(bookIdToRate, rating))
            {
                Console.WriteLine("Rating added successfully.");
                SaveAllData(myDB);  // Save the updated data to JSON
                MirrorChangesToProjectRoot("LibraryData.json");  // Update the project file
            }
            else
            {
                Console.WriteLine("Failed to add rating. Please check the book ID or rating value.");
            }
        }




        // Helper methods to display books
        private static void DisplayBooks(IEnumerable<Book> books, string title = "Filtered Books:")
        {
            Console.WriteLine($"\n{title}");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}: {book.BookTitle} by {book.BookTitle}, Genre: {book.Genre}, " +
                                  $"Published in {book.PublishedYear}, ISBN: {book.ISBNCode}, Average Rating: {book.BooksAveragerating}");
            }
        }







        public void SaveAllData(MyDB myDB)
        {
            string dataJsonFilePath = "LibraryData.json";

            string updatedlillaDB = JsonSerializer.Serialize(myDB, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(dataJsonFilePath, updatedlillaDB);

            MirrorChangesToProjectRoot("LibraryData.json");

            Console.WriteLine("The data has been saved to JSON file ");


        }



        static void MirrorChangesToProjectRoot(string fileName)
        {
            // Get the path to the output directory
            string outputDir = AppDomain.CurrentDomain.BaseDirectory;

            // Get the path to the project root directory
            string projectRootDir = Path.Combine(outputDir, "../../../");

            // Define paths for the source (output directory) and destination (project root)
            string sourceFilePath = Path.Combine(outputDir, fileName);
            string destFilePath = Path.Combine(projectRootDir, fileName);

            // Copy the file if it exists
            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destFilePath, true); // true to overwrite
                Console.WriteLine($"{fileName} has been mirrored to the project root.");
            }
            else
            {
                Console.WriteLine($"Source file {fileName} not found.");
            }




        }

        public string Prompt(string message, string defaultValue = "")
        {
            Console.WriteLine(message);
            string input = Console.ReadLine()!;
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }

        public void SaveAllDataandExit(MyDB myDB)
        {
            SaveAllData(myDB);
            MirrorChangesToProjectRoot("LibraryData.json");
            Console.WriteLine("Do you want to complete? (J/N)");
            string continueChoice = Console.ReadLine()!;
            if (continueChoice.ToUpper() == "N")
            {
                Environment.Exit(0);
            }

        }
    }
}
