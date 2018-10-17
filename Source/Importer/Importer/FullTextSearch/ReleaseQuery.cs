using System.Collections.Generic;
using System.Linq;
using GRM.Common.Models.Repertoire;
using Importer.Models;
using Marten;

namespace Importer.FullTextSearch
{
    public class ReleaseQuery : IReleaseQuery
    {
        ISearchQueryBuilder _queryBuilder;
        IDocumentSession _martenSession;

        public ReleaseQuery()
        {
            _queryBuilder = new SearchQueryBuilder();


            var martenStore = Marten.DocumentStore.For(x =>
            {
                x.Connection("host=localhost;database=StudioHub;password=admin;username=postgres");

                x.Schema.Include<ImportRegistry>();
            });
            _martenSession = martenStore.OpenSession();
        }

        public IEnumerable<ReleaseDetailsModel> Search(SearchRequestModel model)
        {
            var query = _queryBuilder.Build(model);

            return _martenSession.Query<Release>(query)
                .Select(x => new ReleaseDetailsModel {Upc = x.Upc })
                .ToList();
        }
    }
}
