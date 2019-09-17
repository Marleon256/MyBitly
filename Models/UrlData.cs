using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MyBitly.Models
{
    public class UrlData
    {
        public string FullUrl { get; set; }
        [Key]
        public string ShortUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public int HopCount { get; set; }
    }
}
