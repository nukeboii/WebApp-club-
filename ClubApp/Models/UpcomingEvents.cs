using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Models
{
    public class UpcomingEvents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        [Required(ErrorMessage = "Competition Name is Required")]
        public string CompetitionName { get; set; }
        public string CompetitionLocation { get; set; }
        public Member Member { get; set; }
        public int UserId { get; set; }
        public DateTime CompetitionDate { get; set; }
    }
}
