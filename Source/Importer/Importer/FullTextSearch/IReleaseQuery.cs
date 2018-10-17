using System.Collections.Generic;
using Importer.Models;

namespace Importer.FullTextSearch
{
    interface IReleaseQuery
    {
        IEnumerable<ReleaseDetailsModel> Search(SearchRequestModel model);
    }
}
