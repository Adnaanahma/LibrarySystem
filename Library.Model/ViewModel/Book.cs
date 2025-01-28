using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.ViewModel;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public DateTime PublicationDate { get; set; }
    public string AvailabilityStatus { get; set; }
    public string Edition { get; set; }
    public string Summary { get; set; }
}
