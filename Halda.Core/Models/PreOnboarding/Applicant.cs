using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class Applicant
    {
        [Key]
        public string ApplicantId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FatherName { get; set; }

        public string? MotherName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Required]
        public string? Gender { get; set; }

        public string? Religion { get; set; }

        public string? MaritalStatus { get; set; }

        public string? Nationality { get; set; }


        public string? NID { get; set; }

        public string? PassportNumber { get; set; }

        public DateTime? PassportIssueDate { get; set; }

        public string? PrimaryMNo { get; set; }

        public string? SecMNo { get; set; }

        public string? EmergencyContact { get; set; }


        public string? PrimaryEmail { get; set; }

        public string? PassportNumberWithoutUnderscores { get; set; }


    }
}
