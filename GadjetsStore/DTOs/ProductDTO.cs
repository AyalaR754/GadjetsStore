using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record ProductDTO( string Name, string Description, double Price, string UrlImage,string categoryName); 
}
