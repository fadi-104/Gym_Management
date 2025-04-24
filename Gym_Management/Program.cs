using BusinessLogicLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.IdentityRepository;
using DomainEntitiesLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BusinessLogicLayer.IdentityService;
using BusinessLogicLayer.Storage;
using BusinessLogicLayer.ExerciseCategoryService;
using DataAccessLayer.Repository.ExerciseCategories;
using DataAccessLayer.Repository.Exercises;
using BusinessLogicLayer.ExerciseService;
using Gym_Management.Middlewares;
using DataAccessLayer.Repository.SubscriptionPlans;
using BusinessLogicLayer.SubscriptionPlanServices;
using DataAccessLayer.Repository.TrainerAppointments;
using BusinessLogicLayer.TrainerAppointmentService;
using DataAccessLayer.Repository.Workout;
using BusinessLogicLayer.WorkoutServices;
using DataAccessLayer.Repository.TraineeDatas;
using BusinessLogicLayer.TraineeDataServices;
using DataAccessLayer.Repository.TrainerSubscriptions;
using BusinessLogicLayer.TrainerSubServices;
using Gym_Management;
using DataAccessLayer.Repository.TrainerProfiles;
using BusinessLogicLayer.TrainerProfileServices;
using DataAccessLayer.Repository.UserAppo;
using BusinessLogicLayer.UserAppoServices;
using DataAccessLayer.Repository.Subscriptions;
using BusinessLogicLayer.SubscriptionServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);
// أضف هذا في بداية بناء التطبيق (قبل أي خدمات أخرى)
builder.WebHost.UseUrls("http://*:5117"); // الأهم! للاستماع على جميع الواجهات

// تكوين CORS (ضروري لطلبات Flutter)
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", policy => 
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(GeneralProfile));

//Repository
builder.Services.AddScoped<IAppUserManager,AppUserManager>();
builder.Services.AddScoped<IAppRoleManager,AppRoleManager>();
builder.Services.AddScoped<IAppRoleManager,AppRoleManager>();
builder.Services.AddScoped<IAppSignManager, AppSignInManager>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IExercisRepository, ExerciseRepository>();
builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository >();
builder.Services.AddScoped<ITrainerAppoRepository, TrainerAppoRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<ITraineeProfileRepository, TraineeDataRepository>();
builder.Services.AddScoped<ITrainerSubRepository,  TrainerSubRepository>();
builder.Services.AddScoped<ITrainerProfileRepository, TrainerProfileRepository>();
builder.Services.AddScoped<IUserAppoRepository, UserAppoRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

//Service
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStorageService, FileDiskStorageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService >();
builder.Services.AddScoped<ITrainerAppoService, TrainerAppoService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<ITraineeDataService,  TraineeDataService>();
builder.Services.AddScoped<ITrainerSubService, TrainerSubService>();
builder.Services.AddScoped<ITrainerProfileService, TrainerProfileService>();
builder.Services.AddScoped<IUserAppoService, UserAppoService>();
builder.Services.AddScoped<ISubscriptionServices, SubscriptionService>();

var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(connectionStrings);
});

builder.Services.AddIdentity<AppUser, AppRole>(option =>
{
    // Password Settings
    option.Password.RequiredLength = 6;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireDigit = true;

    // Lockout Settings
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    option.Lockout.MaxFailedAccessAttempts = 5;

    option.User.RequireUniqueEmail = true;
})

    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        
    };
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "GYM Management"
    });
    
    var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var fullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
    option.IncludeXmlComments(fullPath);
});


var app = builder.Build();
app.UseCors("AllowAll"); // أضف هذا قبل Middlewares الأخرى

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

using(var scopes = app.Services.CreateScope())
{
     await Seed.AddSeed(scopes);
}

app.Run();
