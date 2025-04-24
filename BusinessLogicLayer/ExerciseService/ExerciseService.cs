using AutoMapper;
using BusinessLogicLayer.Storage;
using Core.Models;
using DataAccessLayer.Repository.Exercises;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using Microsoft.Extensions.Options;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.ExerciseService
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExercisRepository _exercisRepository;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public ExerciseService(IExercisRepository exercisRepository, IMapper mapper, IStorageService storageService)
        {
            _exercisRepository = exercisRepository;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<PagedResponse<List<ExerciseResponses>>> GetAllAsync(TableOptions options)
        {
            var totalCount = await _exercisRepository.CountAsync();

            var list = await _exercisRepository.GetAllNoTrackingAsync(options.Skip,options.PageSize,options.OrderBy,options.OrderByDirection);
            var responses = list.Select(x => _mapper.Map<ExerciseResponses>(x)).ToList();

            return new PagedResponse<List<ExerciseResponses>>
            {
                Data = responses,
                TotalCount = totalCount,
            };
        }

        public async Task<List<ExerciseResponses>> GetAllByCategoryAsync(string categoryName)
        {
            var list = await _exercisRepository.GetAllByCategoryAsync(categoryName);
            var responses = list.Select(x => _mapper.Map<ExerciseResponses>(x)).ToList();

            return responses;
        }
        public async Task<ExerciseResponses> GetAsync(int id)
        {
            var entity = await _exercisRepository.FindNoTrackingAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<ExerciseResponses>(entity);
            return response;
        }

        public async Task CreateAsync(ExerciseRequests request)
        {
            if (request.Id > 0)
                throw new DataValidationException("Id must no to be set");

            if (request is null)
                throw new DataNotFoundException("The provided entity is not found");

            var entity = _mapper.Map<Exercise>(request);

            entity.Image = await _storageService.SaveFileAsync(request.ImageFile, "Gym_Management\\Gym_Management\\Images\\Exercise");
            await _exercisRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(ExerciseRequests request)
        {
            if (!request.Id.HasValue)
                throw new DataValidationException("Id must be set");

            var entity = await _exercisRepository.FindNoTrackingAsync(request.Id.Value);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            entity = _mapper.Map<Exercise>(entity);

            entity.Image = await _storageService.ReplaceFileAsync(request.ImageFile, "Gym_Management\\Gym_Management\\Images\\Exercise", entity.Image);
            await _exercisRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _exercisRepository.FindAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            await _exercisRepository.DeleteAsync(entity);
            _storageService.DeleteFileIfExists(entity.Image);
        }
    }
}
