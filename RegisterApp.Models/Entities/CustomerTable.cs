using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegisterApp.Models.Entities
{
    /// <summary>
    /// Customer table in database, Renamed in customer table to distinguish from API models.
    /// </summary>
    [Table("Customers")]
    public class CustomerTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public int CustomerID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string SurName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(10)]
        public string PolicyReferenceNumber { get; set; }

        public DateTime? DOB { get; set; }


        //Audit Fields - Could be added in base class
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }


        public int UpdatedByID { get; set; }

        public int CreatedByID { get; set; }
    }
}
