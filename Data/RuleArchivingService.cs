using System.Diagnostics;
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

        // Required for Stopwatch
        public async Task<string> ArchiveRulesAsync(DateTime? start, DateTime end, string providerId, string archivedBy, Guid archiveId)
        {
            var stopwatch = Stopwatch.StartNew(); // Start timing

            try
            {
                var idQuery = _context.Rules
                    .Where(r => r.EndDate <= end);

                if (start.HasValue)
                    idQuery = idQuery.Where(r => r.StartDate >= start);

                if (!string.IsNullOrWhiteSpace(providerId))
                    idQuery = idQuery.Where(r => r.ProviderId == providerId);
               
                var dummyRules = await idQuery.ToListAsync();


                if (!dummyRules.Any())
                    return "Keine Regeln zum Archivieren.";

                var archived = dummyRules.Select(r => new RuleArchive
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

                var safeStartDate = start.HasValue && start.Value >= new DateTime(1753, 1, 1)
                    ? start.Value
                    : (DateTime?)null;

                var history = new RuleArchiveHistory
                {
                    ArchiveId = archiveId,
                    StartDate = safeStartDate,
                    EndDate = end,
                    ProviderId = providerId,
                    ArchivedAt = DateTime.UtcNow,
                    ArchivedBy = archivedBy,
                    Action = "Archived",
                    Description = $"Archiviert {archived.Count} Regeln"
                };

                using var tx = await _context.Database.BeginTransactionAsync();

                await _context.RuleArchiveHistories.AddAsync(history);
                await _context.SaveChangesAsync(); // 👈 required to persist `AddAsync`

                var bulkConfigbatch = new BulkConfig
                {
                    BatchSize = 10000,
                    BulkCopyTimeout = 300 // in seconds (5 minutes)
                };

                await _context.BulkInsertAsync(archived, bulkConfigbatch);

                // ✅ Confirm insertion before deletion
                var insertedCount = await _context.Rule_Archive
                    .CountAsync(r => r.ArchiveId == archiveId);

                if (insertedCount != archived.Count)
                {
                    await tx.RollbackAsync();
                    return $"❌ Nur {insertedCount} von {archived.Count} Regeln archiviert – keine Löschung durchgeführt!";
                }

                await _context.BulkDeleteAsync(dummyRules, bulkConfigbatch);
                await tx.CommitAsync();

                stopwatch.Stop();
                Console.WriteLine($"✅ ArchiveRulesAsync completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds. Archived {archived.Count} records.");

                return $"Archiviert: {archived.Count} Regeln.\n✅ ArchiveRulesAsync completed in {stopwatch.Elapsed.TotalSeconds} seconds";
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"❌ ArchiveRulesAsync failed after {stopwatch.Elapsed.TotalSeconds:F2} seconds: {ex.Message}");
                return $"Fehler: {ex.Message}";
            }
        }

        public async Task<string> UndoArchiveAsync(Guid archiveId)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var archivedRules = await _context.Rule_Archive
                    .Where(r => r.ArchiveId == archiveId)
                    .ToListAsync();

                if (!archivedRules.Any())
                    return $"⚠️ Kein Archiv-Eintrag mit ArchiveId {archiveId} gefunden.";

                var rulesToRestore = archivedRules.Select(r => new Rule
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
                    LastModified = r.LastModified
                }).ToList();

                var bulkConfig = new BulkConfig
                {
                    BatchSize = 10000,
                    BulkCopyTimeout = 300
                };

                using var tx = await _context.Database.BeginTransactionAsync();

                await _context.BulkInsertAsync(rulesToRestore, bulkConfig);
                await _context.BulkDeleteAsync(archivedRules, bulkConfig);

                await tx.CommitAsync();

                stopwatch.Stop();
                return $"✅ Archivierung rückgängig gemacht für {rulesToRestore.Count} Regeln (ArchiveId: {archiveId}) in {stopwatch.Elapsed.TotalSeconds:F2} Sekunden.";
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                return $"❌ Fehler beim Undo: {ex.Message}";
            }
        }


    }
}
