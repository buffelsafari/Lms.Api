using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    public class ModuleDto
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopeDate { get => StartDate.AddMonths(1); }
    }
}
