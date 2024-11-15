

using System.Xml.Linq;

namespace Bibliotekshanteringssystem03
{

    public class Author : IIdentifiable
    {

        public int Id { get; set; }

        public string Name { get; set; }//Author name 

       
        public string AuthorsCountry { get; set; }

        public List<int> booksIsWritten { get; set; } = new List<int>();

        //public string Name => Name;


        public Author(int id, string name, string authorsCountry)
        {
            Id = id;
            Name = name;
            AuthorsCountry = authorsCountry;

        }


    }


}
