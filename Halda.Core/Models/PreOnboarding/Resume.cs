using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Halda.Core.Models
{
    public class Resume
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Summary { get; set; }       
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Certification> Certifications { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }

    public class Education
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Resume")]
        public string ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }

    public class WorkExperience
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("Resume")]
        public string ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }

    public class Skill
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Resume")]
        public string ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
        public string Name { get; set; }
        public string Proficiency { get; set; } 
    }

    public class Certification
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Resume")]
        public string ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
        public string Name { get; set; }
        public string IssuingOrganization { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime? DateExpiry { get; set; }
    }

    public class Project
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Resume")]
        public string ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
