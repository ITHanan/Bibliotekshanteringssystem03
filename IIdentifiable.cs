using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekshanteringssystem03
{
    public interface IIdentifiable
    {
       int Id { get; set; }
       // int BookId { get; }
        string Name { get; set; }

      //  string Aname { get; set; }
      //  int AuthorId { get; }
        
           
    }
}
