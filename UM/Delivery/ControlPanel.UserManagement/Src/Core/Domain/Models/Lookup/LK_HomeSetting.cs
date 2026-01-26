using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4EPlatform_Domain.Models.Lookup
{
    public class LK_HomeSetting : InfoEntity
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }

        public virtual ICollection<HomeSetting> HomeSettings { get; set; }
    }
}
