using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO;
public class JobListDTO
{
    public string? Id { get; set; }
    public string? DesignationId { get; set; }
    public string? JobTittel { get; set; }
    public List<string>? JobTypes { get; set; }
    public List<string>? JobTags { get; set; }
    public string? Email { get; set; }
    public string? LastDate { get; set; }
    public string? Location { get; set; }

    //public string? SalaryMin { get; set; }
    //public string? SalaryMax { get; set; }
    //public string? Description { get; set; }
    //public List<string>? Responsibilities { get; set; }
    //public List<string>? Qualifications { get; set; }
    //public List<string>? Benefits { get; set; }
    //public List<string>? OtherInformation { get; set; }
}

