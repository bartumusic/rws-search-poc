using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Importer.Repositories
{
    public interface IRavenRepository<T>
    {
        Task<T> Get(Guid id);
        Task<T> Get(string id);
        Task<IEnumerable<T>> GetAll(int page, int pageSize);
    }
}
