using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
  public class Containerobj
    {
        [Key]
        public int ContainerID { get; set; }
        public int ContainerNumber { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
