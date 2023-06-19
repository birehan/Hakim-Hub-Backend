using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Common;
using Domain;

namespace Application.Features.DoctorProfiles.DTOs
{
    public class DoctorProfileDto : BaseDto
    {
        public string FullName { get; set; }
        public string photoId { get; set; }
        public Guid MainInstitutionId {get;set;}
        public DateTime CareerStartTime { get; set; }
        


    }
}