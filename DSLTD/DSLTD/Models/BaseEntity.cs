using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        public bool IsDeleted { get; set; } = false;
    }
}
