using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KommProv.Archiver.Server.Data.Models
{
    public class RuleArchiveHistory
    {
        public int Id { get; set; }

        public Guid ArchiveId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string ProviderId { get; set; }
        public DateTime ArchivedAt { get; set; }
        public string ArchivedBy { get; set; }

        public string Action { get; set; }
        public string Description { get; set; }
    }
}
