using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KommProv.Archiver.Server.Data.Models
{
    [Table("Rule_Archive")]
    public class RuleArchive
    {
        [Key]
        public long Id { get; set; }

        public string AboId { get; set; }
        public string AboGroupId { get; set; }
        public string AboType { get; set; }
        public string DeviceId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }

        public string ProviderId { get; set; }
        public string CustomerName { get; set; }
        public string KTId { get; set; }
        public string KTName { get; set; }

        public int ProvisionDirection { get; set; }
        public int ProvisionType { get; set; }
        public string OEN { get; set; }
        public string EmployeeType { get; set; }

        public int CalculationRelevant { get; set; }
        public string CustomerId { get; set; }
        public string Priority { get; set; }
        public int isYouthContract { get; set; }

        public string Manufacturer { get; set; }
        public string Operator { get; set; }
        public int? PeriodOfValidity { get; set; }

        public string PercentageOf { get; set; }
        public string PercentageOfProvisionType { get; set; }
        public string RetailTenderType { get; set; }
        public string RetailDiscount { get; set; }

        public string DeviceCategory { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
