using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Address:BaseEntity
    {

        public string AppUserId { get; set; }
        [StringLength(maximumLength: 100)]
        public string Addresses { get; set; }

        [StringLength(maximumLength: 100)]
        public string Aparment { get; set; }
        [StringLength(maximumLength: 100)]
        public string City { get; set; }
        [StringLength(maximumLength: 100)]
        public string Country { get; set; }
        [StringLength(maximumLength: 100)]
        public string State { get; set; }
        public int ZipCode { get; set; }
        [StringLength(maximumLength: 35)]
        public string Phone { get; set; }
        [Range(1,3)]
        public AppUser AppUser { get; set; }
        public bool IsMain { get; set; }
    }
}
