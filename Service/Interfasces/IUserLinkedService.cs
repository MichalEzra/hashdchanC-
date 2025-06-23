using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfasces
{
    public interface IUserLinkedService<T>
    {
        Task<T?> GetByUserId(int userId);

    }
}
