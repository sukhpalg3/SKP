using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace SKP.Models
{
    public class VideoLecDetail
    {
        [Key]
        public int VidID { get; set; }

        [Required]
        [StringLength(200)]
        public string VidName { get; set; }

        [StringLength(400)]
        public string Author { get; set; }

        
        [StringLength(400)]
        public string VidDescription { get; set; }
        [Required]
        [StringLength(20)]
        public string Extension { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("VideoLecDetail")]

        public virtual Category VideoCategories { get; set; }
        


        [NotMapped]
        public VidSingleFileUpload File { get; set; }
    }

    public class VidSingleFileUpload
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
