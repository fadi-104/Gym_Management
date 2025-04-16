using AutoMapper;
using Core.Models;
using DataAccessLayer.Repository.ExerciseCategories;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.ExerciseCategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
             _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ExerciseCategoryResponses>> GetAllAsync()
        {

            var list = await _categoryRepository.GetAllNoTrackingAsync();
            var responses = list.Select(x => _mapper.Map<ExerciseCategoryResponses>(x)).ToList();

            return responses;
        }

        public async Task<ExerciseCategoryResponses> GetAsync(int id)
        {
            
            var entity = await _categoryRepository.FindNoTrackingAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            return _mapper.Map<ExerciseCategoryResponses>(entity);
        }

        public async Task CreateAsync(ExerciseCategoryRequests request)
        {
            if (request.Id > 0)
                throw new DataValidationException("Id must no to be set");

            var entity = _mapper.Map<ExerciseCategory>(request);
            await _categoryRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(ExerciseCategoryRequests request)
        {
            if (!request.Id.HasValue)
                throw new DataValidationException("Id must be set");

            var entity = await _categoryRepository.FindNoTrackingAsync(request.Id.Value);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            entity = _mapper.Map<ExerciseCategory>(request);
            await _categoryRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _categoryRepository.FindAsync(id);
            if ( entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            await _categoryRepository.DeleteAsync(entity);    
        }
    }
}
