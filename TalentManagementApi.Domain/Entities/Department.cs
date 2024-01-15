using TalentManagementApi.Domain.Common;
using System.Collections.Generic;

namespace TalentManagementApi.Domain.Entities
{
    public class Department : AuditableBaseEntity
    {
        // Department Name
        public string Name { get; set; }

        // Navigation Property for related Positions
        public virtual ICollection<Position> Positions { get; set; }

        public Department()
        {
            Positions = new HashSet<Position>();
        }

        // Additional properties (e.g., Description, ManagerId, etc.) can be added here
    }
}