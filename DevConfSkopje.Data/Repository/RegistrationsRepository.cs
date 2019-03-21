using DevConfSkopje.DataModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DevConfSkopje.Data.Repository
{
    public class RegistrationsRepository : Repository<ConferenceRegistration>
    {
        public RegistrationsRepository(DbContext context) : base(context)
        {

        }

        public void AddNewRegistration(ConferenceRegistration conferenceRegistration)
        {
            if (conferenceRegistration != null)
            {
                this.Add(conferenceRegistration);
            }
        }

        public List<ConferenceRegistration> AllRegistrations()
        {
            List<ConferenceRegistration> returnList = this.All().ToList();

            return returnList;
        }

        public void SaveDBChanges()
        {
            this.SaveChanges();
        }
    }
}
