using System;
using Importer.Models;

namespace Importer.FullTextSearch
{
    public class SearchQueryBuilder :ISearchQueryBuilder
    {
        private const string INDEXNAME = "search_index";

        public string Build(SearchRequestModel model)
        {
            var selectClause = $"select * from {INDEXNAME} where ";

            var whereClause = string.Empty;
            whereClause = AddEqualTo(whereClause, "upc", model.Upc);
            whereClause = AddEqualTo(whereClause, "r2_status", model.R2Status);
            whereClause = AddTsQuery(whereClause, "title", model.Artist);
            whereClause = AddTsQuery(whereClause, "releaselabel", model.Label);

            return $"{selectClause} {whereClause}";
        }

        private string AddEqualTo(string statement, string fieldName, string phrase)
        {
            if (string.IsNullOrEmpty(phrase)) return statement;

            statement = statement.Length > 0 ? " and" : String.Empty;

            return statement + $" {fieldName} = '{phrase}'";
        }

        private string AddTsQuery(string statement, string fieldName, string phrase)
        {
            if (string.IsNullOrEmpty(phrase)) return statement;

            statement = statement.Length > 0 ? " and" : String.Empty;
            var formattedPhrase = phrase.Replace(" ", "&");

             return statement + $" {fieldName} @@ to_tsquery('{formattedPhrase}')";
        }
    }
}
