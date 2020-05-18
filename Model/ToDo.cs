using System;
using System.ComponentModel.DataAnnotations;

namespace RAD4M_Test.Model
{
    public class ToDo
    {
        [Key]
        public int Todoid { get; set; }
        public String TodoTittle { get; set; }
        public String TodoDesc { get; set; }
        public DateTime TodoDate { get; set; }
        public Decimal TodoPrecentage { get; set; }
        public Boolean TodoStatus { get; set; }
    }
}
