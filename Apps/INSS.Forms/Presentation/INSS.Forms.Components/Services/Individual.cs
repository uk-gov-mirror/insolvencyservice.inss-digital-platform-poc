using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSS.Forms.Components.Services
{
    public record Individual
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}

