using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel saveViewModel)
        {
            Entity entity = _mapper.Map<Entity>(saveViewModel);
            SaveViewModel saveVm = _mapper.Map<SaveViewModel>(await _repository.AddAsync(entity));
            return saveVm;
        }

        public virtual async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task Delete(params int[] id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            List<ViewModel> viewModelList = _mapper.Map<List<ViewModel>>(await _repository.GetAllAsync());
            return viewModelList;
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            SaveViewModel saveVm = _mapper.Map<SaveViewModel>(await _repository.GetByIdAsync(id));
            return saveVm;
        }

        public virtual async Task Update(SaveViewModel saveViewModel, int id)
        {
            Entity entity = _mapper.Map<Entity>(saveViewModel);
            await _repository.UpdateAsync(entity, id);
        }
    }
}