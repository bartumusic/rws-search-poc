using System;
using System.Collections.Generic;
using GRM.Common.Models.Milestones;

namespace Importer.Models
{
    public class ReleaseDetailsModel
    {
        public Guid Id { get; set; }
        public string Upc { get; set; }
        public string Label { get; set; }
        public string State { get; set; }
        public string R2Status { get; set; }
        public string ReleaseType { get; set; }
        public string ReleaseDate { get; set; }
        public string EarliestPreOrderDate { get; set; }
        public bool EarliestPreOrderDateTimed { get; set; }
        public string FormalTitle { get; set; }
        public string Contributors { get; set; }
        public string VersionTitles { get; set; }
        public string ParentalAdvisory { get; set; }
        public string TechnicalFormat { get; set; }
        public string R2ConfigurationName { get; set; }
        public bool CanBePublished { get; set; }
        public DateTime? LastPublish { get; set; }
        public ICollection<Milestone<ReleaseMilestone>> Milestones { get; set; }
        public bool RequiresClearance { get; set; }
        public bool Recalculating { get; set; }
        public bool HasPublishButtonBeenClicked { get; set; }
        public bool IsDelivered { get; set; }
    }
}
