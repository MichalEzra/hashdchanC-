using AutoMapper;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserType = Common.Dto.UserType;

namespace Service.Services
{
    public class UserService : IService<UserDto>, IUserService<UserDto>
    {
        private readonly IRepository<User> repository;
        private readonly IUserRepository<User> userRepository;

        private readonly IMapper mapper;

        public UserService(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<UserDto> AddItem(UserDto item)
        {
            var userEntity = mapper.Map<User>(item);
            var addedUser = await repository.AddItem(userEntity);
            var resultDto = mapper.Map<UserDto>(addedUser);

            return resultDto;
        }



        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<UserDto>> GetAll()
        {
            return mapper.Map<List<User>, List<UserDto>>(await repository.GetAll());
        }

        public async Task<UserDto> GetById(int id)
        {
            return mapper.Map<User, UserDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, UserDto item)
        {
            await repository.UpdateItem(id, mapper.Map<UserDto, User>(item));
        }


        public async Task<List<UserDto>> GetUsersByType(UserType userType)
        {
            var entityType = (Repository.Entities.UserType)userType;
            var users = await userRepository.GetUsersByTypeFromRepository(entityType);
            return mapper.Map<List<UserDto>>(users);
        }
        public async Task<UserDto?> GetByEmail(string email)
        {
            var user = await userRepository.GetByEmail(email);
            return user != null ? mapper.Map<UserDto>(user) : null;
        }

    }
}
