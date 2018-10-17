using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GRM.Repertoire.Raven.Indexes;
using Raven.Client;

namespace Importer.Repositories
{
    public class RavenRepository<T> : IRavenRepository<T>
    {
        private readonly IAsyncDocumentSession _documentSession;

        public RavenRepository(IAsyncDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }


        public async Task<T> Get(Guid id)
        {
            return await _documentSession.LoadAsync<T>(id);
        }

        public async Task<T> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll(int page, int pageSize)
        {
            return await _documentSession
                .Query<RepertoireReleaseSearchIndex.Definition, RepertoireReleaseSearchIndex>()
                .Skip(page * pageSize)
                .Take(1024)
                .ProjectFromIndexFieldsInto<T>()
                .ToListAsync();
        }
    }
}
