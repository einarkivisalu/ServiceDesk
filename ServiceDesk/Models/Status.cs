using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk.Models
{
    public enum Status
    {
        [Display(Name = "Not Started")]
        NotStarted = 0,
        Postponed = 1,
        [Display(Name = "In Progress")]
        InProgress = 2,
        Completed = 3,
        Closed = 4
    }
}
