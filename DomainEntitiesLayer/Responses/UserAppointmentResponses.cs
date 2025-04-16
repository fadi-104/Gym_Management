using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Responses
{
    public class UserAppointmentResponses
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public DateTime DateTime {  get; set; }

    }
}
