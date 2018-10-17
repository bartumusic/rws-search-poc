using System.Configuration;
using System.Linq;
using Raven.Abstractions.Json;
using Raven.Client;
using Raven.Client.Document;
using Raven.Imports.Newtonsoft.Json;

namespace Importer.Repositories
{
    public static class RavenDb
    {
        public const int DefaultMaximumNumberOfRequests = 2000;

        public static IDocumentStore Init(string connectionStringName = "RavenDB")
        {
            return Init(ConsistencyOptions.None, connectionStringName);
        }

        public static IDocumentStore Init(
            ConsistencyOptions consistencyType,
            int? maxNumberOfRequests = null)
        {
            return Init(consistencyType, "RavenDB", maxNumberOfRequests);
        }

        public static IDocumentStore Init(
            ConsistencyOptions consistencyType,
            string connectionStringName,
            int? maxNumberOfRequests = null)
        {
            if (maxNumberOfRequests == null)
            {
                maxNumberOfRequests = DefaultMaximumNumberOfRequests;
            }

            var store = new DocumentStore
            {
                ConnectionStringName = connectionStringName,
                EnlistInDistributedTransactions = false,
                Conventions =
                {
                    DefaultQueryingConsistency = consistencyType,
                    MaxNumberOfRequestsPerSession = maxNumberOfRequests.Value,
                    CustomizeJsonSerializer = CustomizeJsonSerializer,
                    DisableProfiling = true,
                    ShouldCacheRequest = s => false,
                    ShouldAggressiveCacheTrackChanges = true
                },
            };

            store.Initialize();

            return store;
        }

        private static void CustomizeJsonSerializer(JsonSerializer serializer)
        {
            // Add in custom date serialiser in same position that the existing raven one is in else errors seem to occur.
            var converters = serializer.Converters.ToArray();
            serializer.Converters.Clear();

            foreach (var converter in converters)
            {
                if (converter is JsonDateTimeISO8601Converter)
                {
                    serializer.Converters.Add(new CustomJsonDateTimeIso8601Converter());
                }
                else
                {
                    serializer.Converters.Add(converter);
                }
            }

            // Always replace data from Raven
            serializer.ObjectCreationHandling = ObjectCreationHandling.Replace;
        }
    }
}
