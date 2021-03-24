using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IList<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
