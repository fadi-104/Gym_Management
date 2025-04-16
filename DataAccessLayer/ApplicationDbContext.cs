using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        DbSet<TrainerProfile> TrainerProfiles { get; set; }
        DbSet<Subscription> Subscriptions { get; set; }
        DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        DbSet<TrainerAppointment> TrainerAppointments { get; set; }
        DbSet<TrainerSubscription> TrainerSubscriptions { get; set; }
        DbSet<UserAppointment> UserAppointments { get; set; }
        DbSet<ExerciseCategory> ExerciseCategories { get; set; }
        DbSet<Exercise> Exercises { get; set; }
        DbSet<TraineeData> TraineeData { get; set; }
        DbSet<WorkoutData> WorkoutDatas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

    }
}
