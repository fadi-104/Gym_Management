using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;

namespace BusinessLogicLayer.Mapping
{
    public static class UserMapper
    {
        public static AppUser ToAppUser(AppUser entity, UserRequests user)
        {

            entity.UserName = user.UserName;
            entity.Email = user.Email;
            entity.PasswordHash = user.Password;
            entity.PhoneNumber = user.PhoneNumber;
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName; 

            return entity;
        }
    }
}
