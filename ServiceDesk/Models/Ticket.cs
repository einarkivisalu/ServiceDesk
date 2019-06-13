using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.TicketPriority = Priority.Medium;
            this.TicketDate = DateTime.Now;
            this.TicketStatus = Status.NotStarted;
        }

        [Display(Name = "Ticket Number")]
        public int TicketId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Ticket Title")]
        public string TicketTitle { get; set; }

        [DataType(DataType.Date)]
        public DateTime TicketDate { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Problem Description")]
        public string TicketProblem { get; set; }

        [Display(Name = "Priority")]
        public Priority TicketPriority { get; set; }
        
        [Display(Name = "Status")]
        public Status TicketStatus { get; set; }
    }
}
