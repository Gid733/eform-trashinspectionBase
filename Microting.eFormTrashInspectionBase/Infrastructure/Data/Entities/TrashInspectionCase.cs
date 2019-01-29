using System.ComponentModel.DataAnnotations.Schema;

namespace Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities
{
    public class TrashInspectionCase : BaseEntity
    {                
        public int Status { get; set; }
        
        [ForeignKey("TrashInspection")]
        public int TrashInspectionId { get; set; }
        
        public string SdkCaseId { get; set; }
    }
}