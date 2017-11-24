using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataEntities
{
    public class MovieDetails
    {
        public int ID { get; set; }
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage ="The Year should be between 1900 to 2099")]
        public int YearOfRelease { get; set; }
    }
}
