using Importer.Models;

namespace Importer.FullTextSearch
{
    public interface ISearchQueryBuilder
    {
        string Build(SearchRequestModel model);
    }
}
