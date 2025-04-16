using System.Numerics;
using AutoMapper;
using BusinessLogicLayer.Constants;
using BusinessLogicLayer.Mapping;
using BusinessLogicLayer.Storage;
using Core.Models;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.TraineeDatas;
using DataAccessLayer.Repository.TrainerProfiles;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using Microsoft.AspNetCore.Hosting;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.IdentityService
{
    public class UserService : IUserService
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppSignManager _appSignManager;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private readonly ITrainerProfileRepository _trainerProfileRepository;
        public UserService(IAppUserManager appUserManager, IMapper mapper, IStorageService storageService, ITrainerProfileRepository trainerProfileRepository, IAppSignManager appSignManager
            )
        {
            _appUserManager = appUserManager;
            _mapper = mapper;
            _storageService = storageService;
            _trainerProfileRepository = trainerProfileRepository;
            _appSignManager = appSignManager;
        }

        public async Task<PagedResponse<List<UserResponses>>> GetAllAsync(TableOptions options, string role, bool? isActive)
        {
            if (role is null)
            {
                throw new DataNotFoundException("Role must be set");
            }

            var totalCount = await _appUserManager.CountAsync();

            var entity = await _appUserManager.GetAllAsNoTrackingAsync(options.Skip, options.PageSize, options.OrderBy, options.OrderByDirection, role, isActive);
            var response = entity.Select(x => _mapper.Map<UserResponses>(x)).ToList();

            return new PagedResponse<List<UserResponses>>
            {
                Data = response,
                TotalCount = totalCount,
            };
        }

        public async Task<UserResponses> GetAsync(int id)
        {
           
            var item = await _appUserManager.GetByIdAsync(id);

            if (item is null)
                throw new DataNotFoundException("The provided entity is not found");

            var request = _mapper.Map<UserResponses>(item);
            return request;
        }

        public async Task CreateAsync(UserRequests request)
        {
            using(var transaction = await _appUserManager.BeginTransactionAsync())
            {
                if (request.Id > 0)
                    throw new DataValidationException("Id Must not to be set");

                var entity = _mapper.Map<AppUser>(request);
                entity.Image = await _storageService.SaveFileAsync(request.ImageFile, "Images\\Users");

                var result = await _appUserManager.CreateAsync(entity, request.Password);

                if (!result.Succeeded)
                    throw new DataValidationException(result.Errors.First().Description);

                result = await _appUserManager.AddToRoleAsync(entity, request.Role);

                if (!result.Succeeded)
                    throw new DataValidationException(result.Errors.First().Description);


                if (request.Role == UserRole.Trainer)
                {
                    var user = await _appUserManager.FindByNameAsync(request.UserName);
                    if (user is null)
                        throw new DataNotFoundException("The provided entity is not found");
                    var profile = new TrainerProfile()
                    {
                        TrainerId = user.Id,
                        Description = "",
                        Experience = "",
                        Championship = "",
                    };
                    await _trainerProfileRepository.AddAsync(profile);
                }

                await transaction.CommitAsync();
            }
            

        }

        public async Task UpdateAsync(UserRequests request)
        {
            using (var transaction = await _appUserManager.BeginTransactionAsync())
            {
                if (!request.Id.HasValue)
                    throw new DataValidationException("Id must be provided");

                var entity = await _appUserManager.GetByIdAsync(request.Id.Value);
                if (entity is null)
                    throw new DataNotFoundException("The provided entity is not found");

                entity = UserMapper.ToAppUser(entity, request);
                entity.Image = await _storageService.ReplaceFileAsync(request.ImageFile, "Images\\Users", entity.Image);
                
                var result = await _appUserManager.UpdateAsync(entity);

                if (!result.Succeeded)
                    throw new DataValidationException(result.Errors.First().Description);

                var userRole = (await _appUserManager.GetRolesAsync(entity)).FirstOrDefault();

                if (userRole != request.Role)
                {
                    await _appUserManager.RemoveFromRoleAsync(entity, request.Role);


                    result = await _appUserManager.AddToRoleAsync(entity, request.Role);

                    if (!result.Succeeded)
                        throw new DataValidationException(result.Errors.First().Description);
                }

                await transaction.CommitAsync();
            }

        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _appUserManager.FindByIdAsync(id.ToString());


            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            entity.IsActive = false;
            var result = await _appUserManager.UpdateAsync(entity);
            if (!result.Succeeded)
                throw new DataValidationException(result.Errors.First().Description);

            _storageService.DeleteFileIfExists(entity.Image);
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var user = await _appUserManager.FindByNameAsync(request.UserName);
            if (user is null)
                throw new NotAuthorizedException("Invalid login attempt");

            var password = request.Password.ToString();
            var result = await _appSignManager.CheckPasswordSignInAsync(user, password, true);
            if (!result.Succeeded)
                throw new NotAuthorizedException("Invalid login attempt");

            var token = await _appSignManager.GenerateUserTokens(user);
            return token;
        }


    }
}
