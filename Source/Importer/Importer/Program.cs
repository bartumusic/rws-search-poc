using System;
using GRM.Common.Models.Repertoire;
using Importer.Repositories;
using Raven.Client.Document;
using Raven.Client.Document.Async;
using Marten;

namespace Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            var ravenStore = RavenDb.Init(ConsistencyOptions.None, null);
            var ravenSession = ravenStore.OpenAsyncSession();

            var martenStore = Marten.DocumentStore.For(x =>
                {
                    x.Connection("host=localhost;database=StudioHub;password=admin;username=postgres");

                    x.Schema.Include<ImportRegistry>();
                });
            var martenSession = martenStore.OpenSession();

            var repo = new RavenRepository<Release>(ravenSession);

            for (int i = 200; i < 350; i++)
            {
                Console.WriteLine($"loading page {i}");
                var releases = repo.GetAll(i, 1024).GetAwaiter().GetResult();

                foreach (var release in releases)
                {
                    martenSession.Store<Release>(release);
                }

                martenSession.SaveChangesAsync().GetAwaiter().GetResult();
            }

            Console.ReadKey();
        }

        private static void CreateIndexes()
        {

        }
    }
}
