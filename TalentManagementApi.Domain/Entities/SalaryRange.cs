using TalentManagementApi.Domain.Common;
using System.Collections.Generic;

namespace TalentManagementApi.Domain.Entities
{
    public class SalaryRange : AuditableBaseEntity
    {
        // Minimum salary value
        public decimal MinSalary { get; set; }

        // Maximum salary value
        public decimal MaxSalary { get; set; }

        // Description or additional details
        public string Description { get; set; }

        // Navigation property back to Position
        public virtual ICollection<Position> Positions { get; set; }

        public SalaryRange()
        {
            Positions = new HashSet<Position>();
        }


        // Additional properties or methods can be added here
    }
}