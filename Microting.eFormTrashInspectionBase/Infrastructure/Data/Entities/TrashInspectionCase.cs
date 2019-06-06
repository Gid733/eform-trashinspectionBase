using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using eFormShared;

namespace Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities
{
    public class TrashInspectionCase : BaseTrashInspectionEntity
    {                
        public int Status { get; set; }
        
        [ForeignKey("TrashInspection")]
        public int TrashInspectionId { get; set; }
        
        public string SdkCaseId { get; set; }
        
        public int SdkSiteId { get; set; }
        
        public int Version { get; set; }        
        
        [ForeignKey("Segment")]
        public int SegmentId { get; set; }
        
        public void Create(TrashInspectionPnDbContext dbContext)
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Version = 1;
            WorkflowState = Constants.WorkflowStates.Created;
            
            dbContext.TrashInspectionCases.Add(this);
            dbContext.SaveChanges();

            dbContext.TrashInspectionCaseVersions.Add(MapVersions(this));
            dbContext.SaveChanges();
        }

        public void Update(TrashInspectionPnDbContext dbContext)
        {
            TrashInspectionCase trashInspection = dbContext.TrashInspectionCases.FirstOrDefault(x => x.Id == Id);

            if (trashInspection == null)
            {
                throw new NullReferenceException($"Could not find TrashInspectionCase with id: {Id}");
            }

            trashInspection.Status = Status;
            trashInspection.TrashInspectionId = TrashInspectionId;
            trashInspection.SdkCaseId = SdkCaseId;
            trashInspection.SdkSiteId = SdkSiteId;
            trashInspection.Version = Version;
            trashInspection.SegmentId = SegmentId;

            if (dbContext.ChangeTracker.HasChanges())
            {
                trashInspection.UpdatedAt = DateTime.UtcNow;
                trashInspection.Version += 1;

                dbContext.TrashInspectionCaseVersions.Add(MapVersions(trashInspection));
                dbContext.SaveChanges();
            }                       
        }

        public void Delete(TrashInspectionPnDbContext dbContext)
        {
            TrashInspectionCase trashInspection = dbContext.TrashInspectionCases.FirstOrDefault(x => x.Id == Id);

            if (trashInspection == null)
            {
                throw new NullReferenceException($"Could not find TrashInspectionCase with id: {Id}");
            }

            trashInspection.WorkflowState = Constants.WorkflowStates.Removed;
            
            if (dbContext.ChangeTracker.HasChanges())
            {
                trashInspection.UpdatedAt = DateTime.UtcNow;
                trashInspection.Version += 1;

                dbContext.TrashInspectionCaseVersions.Add(MapVersions(trashInspection));
                dbContext.SaveChanges();
            }  
        }

        private TrashInspectionCaseVersion MapVersions(TrashInspectionCase trashInspection)
        {
            TrashInspectionCaseVersion trashInspectionVersion = new TrashInspectionCaseVersion
            {
                Status = trashInspection.Status,
                TrashInspectionId = trashInspection.TrashInspectionId,
                SdkCaseId = trashInspection.SdkCaseId,
                SdkSiteId = trashInspection.SdkSiteId,
                Version = trashInspection.Version,
                SegmentId = trashInspection.SegmentId,
                TrashInspectionCaseId = trashInspection.Id
            };

            return trashInspectionVersion;
        }
    }
}