using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain
{
    public class DoctorProfile : BaseDomainEntity
    {
        public string FullName{get;set;}
        public string Email{get;set;}
        public string Photo {get;set;}
        public DateTime TimeStamp {get;set;}
        
        
    }
}