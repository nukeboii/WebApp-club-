using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsId { get; set; }
        [Required(ErrorMessage ="Title is Required")]
        public string NewsTitle { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public Member Member { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
