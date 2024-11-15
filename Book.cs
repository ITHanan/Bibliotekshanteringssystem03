

using System.Xml.Linq;

namespace Bibliotekshanteringssystem03
{
    public class Book : IIdentifiable
    {
       public int Id { get; set; }
        public string BookTitle { get; set; }// Book Name

        public string Name { get; set; }

        //Author author { get; set; }

      //  public string AuthorName { get; set; }
      
        public string Genre { get; set; }
        public int PublishedYear { get; set; }
        public int ISBNCode { get; set; }

        public List<int> Ratings { get; set; } = new List<int>();

       // public string Name => Name;

        public double BooksAveragerating
        {
            get
            {
                if (Ratings.Count > 0)
                {
                    return Ratings.Average();
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public void AddRating(int rating)
        {

            if (rating >= 1 && rating <= 5)
            {
                Ratings.Add(rating);
                Console.WriteLine($"Rating {rating} has been added to this book {BookTitle} ");
            }

            else
            {
                Console.WriteLine("Your rating must be between 1-5");
            }

        }

        public Book(int id, string bookTitle, string name, string genre, int publishedYear, int iSBNCode)
        {
            Id = id;
            BookTitle = bookTitle;
            Name = name;   
            Genre = genre;
            PublishedYear = publishedYear;
            ISBNCode = iSBNCode;
        }

    }
}
