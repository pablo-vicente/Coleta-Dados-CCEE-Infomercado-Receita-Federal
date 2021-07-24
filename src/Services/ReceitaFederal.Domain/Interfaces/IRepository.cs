using System.Collections.Generic;

namespace ReceitaFederal.Domain.Interfaces
{
    public interface IRepository<T,K> {
        IEnumerable<T> ReadAll();
        T Read(K id);
        void Create(params T[] entity);
        void Update(params T[] entity);
        void Delete(params T[] entity);
    }
}