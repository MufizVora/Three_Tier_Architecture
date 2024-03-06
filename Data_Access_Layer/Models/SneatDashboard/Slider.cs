using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.SneatDashboard
{
    [Table("Auth_Slider")]
    public class Slider
    {
        [Key]

        public int Id { get; set; }

        public string? AdminId { get; set; }


        public string? Title { get; set; }

        public string? File { get; set; }

        public string? MultiImage { get; set; }

    }
}
