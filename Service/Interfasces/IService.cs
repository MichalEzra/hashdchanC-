using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfasces
{  //לוגיקה
    public interface IService<T>
    {
        T GetById(int id);
        List<T> GetAll();
        T AddItem(T item);
        void DeleteItem(int id);
        void UpdateItem(int id, T item);
    }
}
