using Microsoft.AspNetCore.Http;
using SKP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SKP.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        public virtual ICollection<BooksDetail> BooksDetail { get; set; }
        public virtual ICollection<NotesDetail> NotesDetail { get; set; }
        public virtual ICollection<VideoLecDetail> VideoLecDetail { get; set; }


    }
}
