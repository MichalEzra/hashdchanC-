using Common.Dto;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserType = Common.Dto.UserType;

namespace Service.Interfasces
{  //לוגיקה
    public interface IService<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> AddItem(T item);
        Task DeleteItem(int id);
        Task UpdateItem(int id, T item);
        //Task<bool> ExistsAsync(int idCandidate1);
    }
    
}
