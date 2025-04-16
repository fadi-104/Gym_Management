using BusinessLogicLayer.Constants;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.ExerciseCategories;
using DataAccessLayer.Repository.Exercises;
using DataAccessLayer.Repository.SubscriptionPlans;
using DataAccessLayer.Repository.TraineeDatas;
using DataAccessLayer.Repository.TrainerProfiles;
using DataAccessLayer.Repository.TrainerSubscriptions;
using DataAccessLayer.Repository.UserAppo;
using DataAccessLayer.Repository.Workout;
using DomainEntitiesLayer.Entities;

namespace Gym_Management
{
    public static class Seed
    {
        public static async Task AddSeed(IServiceScope scopes)
        {
            var _appUserManager = scopes.ServiceProvider.GetService<IAppUserManager>();
            var _appRoleManager = scopes.ServiceProvider.GetService<IAppRoleManager>();
            var _categoryService = scopes.ServiceProvider.GetService<ICategoryRepository>();
            var _exerciseService = scopes.ServiceProvider.GetService<IExercisRepository>();
            var _subscriptionPlanService = scopes.ServiceProvider.GetService<ISubscriptionPlanRepository>();
        

            if (_appUserManager != null)
            {
                if (await _appUserManager.CountAsync() == 0)
                {
                    var user = new AppUser()
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        FirstName = "Admin",
                        LastName = "User",
                        PhoneNumber = "0981102144",
                        Gender = "male",
                        IsActive = true,
                    };
                    await _appUserManager.CreateAsync(user, "admin123");
                }
            }

            if (_appRoleManager != null)
            {
                if (!await _appRoleManager.RoleExistsAsync(UserRole.Admin))
                {
                    var Role = new AppRole()
                    {
                        Name = UserRole.Admin,
                    };

                    await _appRoleManager.CreateAsync(Role);
                }

                if (!await _appRoleManager.RoleExistsAsync(UserRole.Trainee))
                {
                    var Role = new AppRole()
                    {
                        Name = UserRole.Trainee,
                    };

                    await _appRoleManager.CreateAsync(Role);
                }

                if (!await _appRoleManager.RoleExistsAsync(UserRole.Employee))
                {
                    var Role = new AppRole()
                    {
                        Name = UserRole.Employee,
                    };

                    await _appRoleManager.CreateAsync(Role);
                }

                if (!await _appRoleManager.RoleExistsAsync(UserRole.Trainer))
                {
                    var Role = new AppRole()
                    {
                        Name = UserRole.Trainer,
                    };

                    await _appRoleManager.CreateAsync(Role);
                }
            }

            if (_categoryService != null)
            {
                if (await _categoryService.CountAsync() == 0)
                {
                    var Category = new ExerciseCategory()
                    {
                        Name = "Chest"
                    };

                    await _categoryService.AddAsync(Category);
                }
            }

            if (_exerciseService != null)
            {
                if (await _exerciseService.CountAsync() == 0)
                {
                    var Exercise = new Exercise()
                    {
                        CategoryId = 2002,
                        Name = "Bench Press"
                    };

                    await _exerciseService.AddAsync(Exercise);
                }
            }

            if (_subscriptionPlanService != null)
            {
                if (await _subscriptionPlanService.CountAsync() == 0)
                {
                    var plan = new SubscriptionPlan()
                    {
                        Name = "Monthly plan",
                        Period = 6,
                        TimeUnit = "Month",
                        price = 500000,
                        IsActive = true,
                    };

                    await _subscriptionPlanService.AddAsync(plan);
                }
            }

        }

    }
}
