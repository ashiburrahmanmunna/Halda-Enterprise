using Halda.Core.Enums;
using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public CompanyRole Role { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; } = false;
        public string? DepartmentId { get; set; }
        public string? DesignationId { get; set; }
        public string? CompanyId { get; set; }
        public string? UserId { get; set; }
        public string? UpdateByUserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateAdded { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
