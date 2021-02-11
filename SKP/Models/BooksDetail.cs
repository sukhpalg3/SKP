using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace SKP.Models
{
    public class BooksDetail
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [StringLength(200)]
        public string BookName { get; set; }

        [StringLength(400)]
        public string Author { get; set; }

        [StringLength(400)]
        public string Publisher { get; set; }

        [StringLength(400)]
        public string BookDescription { get; set; }
        [Required]
        [StringLength(20)]
        public string Extension { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("BooksDetail")]

        public virtual Category BooksCategories { get; set; }
       
        [NotMapped]
        public SingleFileUpload File { get; set; }
    }

    public class SingleFileUpload
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
