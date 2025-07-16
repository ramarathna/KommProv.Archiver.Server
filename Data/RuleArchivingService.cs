using EFCore.BulkExtensions;
using KommProv.Archiver.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KommProv.Archiver.Server.Data
{
    public class RuleArchivingService
    {
        private readonly Context _context;

        public RuleArchivingService(Context context)
        {
            _context = context;
        }

        public async Task<string> ArchiveRulesAsync(DateTime? start, DateTime end, string providerId, string archivedBy, Guid archiveId)
        {
            var query = _context.Rules
                .Where(r => r.EndDate <= end);

            if (start.HasValue)
                query = query.Where(r => r.StartDate >= start.Value);

            if (!string.IsNullOrWhiteSpace(providerId))
                query = query.Where(r => r.ProviderId == providerId);

            // Load complete Rule entities (with tracking) for bulk deletion + archiveId setting
            var toArchive = await query.AsNoTracking().ToListAsync();

            if (!toArchive.Any())
                return "Keine Regeln zum Archivieren.";

            // Set ArchiveId
            toArchive.ForEach(r => r.ArchiveId = archiveId);

            // Map Rule → RuleArchive
            var archived = toArchive.Select(r => new RuleArchive
            {
                Id = r.Id,
                AboId = r.AboId,
                AboGroupId = r.AboGroupId,
                AboType = r.AboType,
                DeviceId = r.DeviceId,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Value = r.Value,
                Description = r.Description,
                ProviderId = r.ProviderId,
                CustomerName = r.CustomerName,
                KTId = r.KTId,
                KTName = r.KTName,
                ProvisionDirection = r.ProvisionDirection,
                ProvisionType = r.ProvisionType,
                OEN = r.OEN,
                EmployeeType = r.EmployeeType,
                CalculationRelevant = r.CalculationRelevant,
                CustomerId = r.CustomerId,
                Priority = r.Priority,
                isYouthContract = r.isYouthContract,
                Manufacturer = r.Manufacturer,
                Operator = r.Operator,
                PeriodOfValidity = r.PeriodOfValidity,
                PercentageOf = r.PercentageOf,
                PercentageOfProvisionType = r.PercentageOfProvisionType,
                RetailTenderType = r.RetailTenderType,
                RetailDiscount = r.RetailDiscount,
                DeviceCategory = r.DeviceCategory,
                ModifiedBy = r.ModifiedBy,
                LastModified = r.LastModified,
                ArchiveId = archiveId
            }).ToList();

            var history = new RuleArchiveHistory
            {
                ArchiveId = archiveId,
                StartDate = start ?? DateTime.MinValue,
                EndDate = end,
                ProviderId = providerId,
                ArchivedAt = DateTime.UtcNow,
                ArchivedBy = archivedBy,
                Action = "Archived",
                Description = $"Archiviert {archived.Count} Regeln"
            };

            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.RuleArchiveHistories.AddAsync(history);
                await _context.BulkInsertAsync(archived);
                await _context.BulkDeleteAsync(toArchive);
                await tx.CommitAsync();

                return $"Archiviert: {archived.Count} Regeln.";
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return $"Fehler: {ex.Message}";
            }
        }
    }
}
