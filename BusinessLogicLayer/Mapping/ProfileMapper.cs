using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;

namespace BusinessLogicLayer.Mapping
{
    public static class ProfileMapper
    {
        public static AppUser ToAppUserProfile(this TrainerProfileRequests user, AppUser entity)
        {
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.Email = user.Email;
            entity.PhoneNumber = user.PhoneNumber;
            entity.Gender = user.Gender;
            entity.Age = user.Age;

            return entity;
        }

        public static TrainerProfile ToTrainerProfile(this TrainerProfileRequests trainer, TrainerProfile entity)
        {
            entity.Description = trainer.Description;
            entity.Experience = trainer.Experience;
            entity.Championship = trainer.Championship;

            return entity;
        }
    }
}
