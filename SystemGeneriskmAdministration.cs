using System.Text.Json;

namespace Bibliotekshanteringssystem03
{
    public class SystemGeneriskmAdministration<T> where T : IIdentifiable 
    {

        public readonly List<T> _items = new List<T>();

        public void AddTo(T item)
        {
            _items.Add(item);

        }


        public List<T> GetAll()
        {

            return _items;

        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id)!;

        }

        public T GetByName(string name)
        {
            return _items.FirstOrDefault(n => n.Name.Equals(name, StringComparison.OrdinalIgnoreCase))!;

        }

        public void Updater(T newitem)
        {

            var TheExistingItem = GetById(newitem.Id);
            if (TheExistingItem != null)
            {
                int index = _items.IndexOf(newitem);
                _items[index] = newitem;
            }

        }
        public void Remove(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                _items.Remove(item);
            }

        }

        public void SaveAllData(MyDB myDB)
        {
            string dataJsonFilePath = "LibraryData.json";

            string updatedlillaDB = JsonSerializer.Serialize(myDB, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(dataJsonFilePath, updatedlillaDB);

            Console.WriteLine("The data has been saved");




        }


        public bool AddRating(int bookId, int rating)
        {
            // Check if the rating is between 1 and 5
            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("Rating must be between 1 and 5.");
                return false;
            }

            // Find the book by its ID
            var book = GetById(bookId) as Book;
            if (book != null)
            {
                book.Ratings.Add(rating);  // Add rating to the book's Ratings list
                return true;
            }

            Console.WriteLine("Book not found.");
            return false;
        }



    }

}
