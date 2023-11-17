using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace CompositeService.Models
{
    public class Doctor
    {     
        public int Id { get; set; }     
        public string Surname { get; set; }      
        public string Name { get; set; }     
        public int Experience { get; set; }      
        public string Post { get; set; }     
        public string SpecName { get; set; }
    }
}
