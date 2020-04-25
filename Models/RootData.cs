using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4_Group8.Models
{
    public class RootData
    {
        [Key]
        public string objectid { get; set; }
        public string kilonbr { get; set; }
        public string blockaddress { get; set; }
        public string cmid { get; set; }
        public string cmagency { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string score { get; set; }
        public string side { get; set; }

    }
}
