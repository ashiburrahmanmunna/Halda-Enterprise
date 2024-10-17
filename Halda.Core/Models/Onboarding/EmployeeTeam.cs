using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeTeam : BaseModel
    {
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public string MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Employee? Member { get; set; }
        public DateTime? JoinedDate { get; set; }
        public bool? IsTeamHead { get; set; }
       

       
    }

}
