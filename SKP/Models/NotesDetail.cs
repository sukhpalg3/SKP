using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace SKP.Models
{
    public class NotesDetail
    {
        [Key]
        public int NotesID { get; set; }

        [Required]
        [StringLength(200)]
        public string NameName { get; set; }

        [StringLength(400)]
        public string Author { get; set; }

        
        [StringLength(400)]
        public string NotesDescription { get; set; }
        [Required]
        [StringLength(20)]
        public string Extension { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("NotesDetail")]

        public virtual Category NotesCategories { get; set; }
        

        [NotMapped]
        public NotesSingleFileUpload File { get; set; }
    }

    public class NotesSingleFileUpload
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
