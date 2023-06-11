using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Common;
using Domain;

namespace Application.Features.DoctorProfiles.DTOs
{
    public class UpdateDoctorProfileDto : BaseDto
    {
        public string FullName { get; set; }
        public string About { get; set; }
        public string Email { get; set; }
        public DateTime CareerStartTime { get; set; }
        public Gender Gender { get; set; }

    }
}