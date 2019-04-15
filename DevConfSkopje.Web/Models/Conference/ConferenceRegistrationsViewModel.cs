using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevConfSkopje.Web.Models.Conference
{
    public class ConferenceRegistrationsViewModel
    {
        public ConferenceRegistrationsViewModel()
        {
            ConferenceRegistrations = new List<ConferenceRegistrationViewModel>();  
        }

        public List<ConferenceRegistrationViewModel> ConferenceRegistrations { get; set; }
        public int TotalRegistrations
        {
            get
            {
                return this.ConferenceRegistrations.Count;
            }
        }  
    }
}