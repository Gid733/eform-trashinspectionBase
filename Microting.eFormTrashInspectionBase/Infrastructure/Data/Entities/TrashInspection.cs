using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities
{

    public class TrashInspection : BaseEntity
    {

        public TrashInspection()
        {
            TrashInspectionCases = new HashSet<TrashInspectionCase>();
        }

        public int Version { get; set; }

        public int WeighingNumber { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string RegistrationNumber { get; set; }

        public int TrashFraction { get; set; }

        public int Eak_Code { get; set; }

        public string Producer { get; set; }

        public string Transporter { get; set; }
        
        [ForeignKey("Installation")]
        public int InstallationId { get; set; }

        public bool MustBeInspected { get; set; }
        
        public int Status { get; set; }
        
        public virtual ICollection<TrashInspectionCase> TrashInspectionCases { get; set; }
    }
}