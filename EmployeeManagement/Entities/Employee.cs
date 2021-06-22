using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        [Column(TypeName = "varchar(11)")]
        [StringLength(11, MinimumLength = 11)]
        public string Pesel { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50, MinimumLength = 1)]
        public string Surname { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public ESex Sex { get; set; }
    }
}
