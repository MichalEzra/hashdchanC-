using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;

namespace Service.Interfasces
{
    public interface IUserLinkedService<T>
    {
        Task<List<T>> GetAllByUserId(int userId);
    }
}
