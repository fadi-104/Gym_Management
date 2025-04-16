using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<UserRequests,AppUser>().ReverseMap();
            CreateMap<AppUser,UserResponses>().ReverseMap();

            CreateMap<AppRole,RoleResponses>().ReverseMap();

            CreateMap<ExerciseCategoryRequests,ExerciseCategory>().ReverseMap();
            CreateMap<ExerciseCategory,ExerciseCategoryResponses>().ReverseMap();

            CreateMap<ExerciseRequests, Exercise>().ReverseMap();
            CreateMap<Exercise, ExerciseResponses>()
                .ForMember(x => x.CategoryName, x => x.MapFrom(x => x.Category.Name))
                .ReverseMap();

            CreateMap<SubscriptionPlanRequests, SubscriptionPlan>().ReverseMap();
            CreateMap<SubscriptionPlan, SubscriptionPlanResponses>()
                .ForMember(x => x.Period, x => x.MapFrom(x => $"{x.Period} {x.TimeUnit}"))
                .ReverseMap();
                

            CreateMap<TrainerAppoRequests, TrainerAppointment>().ReverseMap();
            CreateMap<TrainerAppointment, TrainerAppoResponses>()
                .ForMember(x => x.TrainerName, x => x.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
                .ReverseMap();

            CreateMap<WorkoutDataRequests, WorkoutData>().ReverseMap();
            CreateMap<WorkoutData, WorkoutDataResponses>()
                .ForMember(x => x.UserName, x => x.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
                .ForMember(e => e.ExerciseName, e => e.MapFrom(e => e.Exercise.Name));

            CreateMap<TrainerSubscriptionRequests, TrainerSubscription>().ReverseMap();
            CreateMap<TrainerSubscription, TrainerSubscriptionResponses>()
                .ForMember(x => x.TraineeFullName ,x => x.MapFrom(x => $"{x.Trainee.FirstName} {x.Trainee.LastName}"))
                .ForMember(x => x.TrainerFullName, x => x.MapFrom(x => $"{x.Trainer.FirstName} {x.Trainer.LastName}"))
                .ReverseMap();

            CreateMap<TraineeDataRequests, TraineeData>().ReverseMap();
            CreateMap<TraineeData, TraineeDataResponses>()
                .ForMember(x => x.TraineeName, x => x.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
                .ReverseMap();

            CreateMap<TrainerProfileRequests, TrainerProfile>().ReverseMap();
            CreateMap<TrainerProfile, TrainerProfileResponses>()
                .ForMember(x => x.FirstName, x => x.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(x => x.User.LastName))
                .ForMember(x => x.Email, x => x.MapFrom(x => x.User.Email))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(x => x.User.PhoneNumber))
                .ForMember(x => x.Gender, x => x.MapFrom(x => x.User.Gender))
                .ForMember(x => x.Age, x => x.MapFrom(x => x.User.Age))
                .ReverseMap();

            CreateMap<UserAppointmentRequests, UserAppointment>().ReverseMap();
            CreateMap<UserAppointment, UserAppointmentResponses>()
            .ForMember(x => x.UserFullName, x => x.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
            .ForMember(x => x.DateTime, x => x.MapFrom(x => x.TrainerAppointment.StartDate))
                .ReverseMap();
            
            CreateMap<SubscriptionRequests, Subscription>().ReverseMap();
            CreateMap<Subscription, SubscriptionResponses>()
            .ForMember(x => x.UserName, x => x.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
            .ForMember(x => x.SubscriptionName, x => x.MapFrom(x => x.Plan.Name))
            .ForMember(x => x.price, x => x.MapFrom(x => x.Plan.price))
                .ReverseMap();
        }
    }
}
