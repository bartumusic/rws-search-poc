using System.Linq;
using GRM.Common.Models.Repertoire;
using GRM.Common.Models.Repertoire.Primitives;
using Marten;

namespace Importer
{
    public class ImportRegistry : MartenRegistry
    {
        public ImportRegistry()
        {
            For<Release>().Duplicate(x => x.Upc);
            For<Release>().Duplicate(x => x.FormalTitle.Default.Title, configure: x => {
                x.IndexName = "FormalTitle";
            });
            For<Release>().Duplicate(x => x.Format.R2ConfigurationName, configure: x => {
                x.IndexName = "R2ConfigurationName";
            });

            For<Release>().Duplicate(x => x.SensitivityType);
            For<Release>().Duplicate(x => x.ReleaseLabel);
            For<Release>().Duplicate(x => x.R2Status);
            For<Release>().Duplicate(x => x.ReleaseDate);
            For<Release>().Duplicate(x => x.EarliestPreOrderDate);
            For<Release>().Duplicate(x => x.TimedRelease);
            //For<Release>().Duplicate(x => x.Milestones);
            For<Release>().Duplicate(x => x.IsReleaseHidden, configure: x=> {
                x.IndexName = "IsReleaseHidden";
            });
            For<Release>().Duplicate(x => x.State);
            For<Release>().Duplicate(x => x.HasTimedDeals);
        }
    }
}
