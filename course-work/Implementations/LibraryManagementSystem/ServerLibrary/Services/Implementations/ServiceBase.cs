using AutoMapper;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using ServerLibrary.Repositories.Implementations;
using ServerLibrary.Repositories.Interfaces;
using ServerLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Implementations
{
    public class ServicesBase<TEntity, TDto> : IServiceBase<TDto> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        private IUserRepository userRepository;
        private IReservationRepository reservationRepository;
        private IMapper mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public IAuthorRepository AuthorRepository { get; }

        public ServicesBase(IRepository<TEntity> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ServicesBase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ServicesBase(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public ServicesBase(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ServicesBase(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
        }

        public async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto?>(entity);
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> UpdateAsync(int id, TDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Not found");
            _mapper.Map(dto, entity);
            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            _repository.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (entities == null) return Enumerable.Empty<TDto>();
            return _mapper.Map<IEnumerable<TDto>>(entities);

        }
    }
}
