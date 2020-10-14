using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VPM.Models
{
    public class Project
    {
        #region Fields

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Create")]
        public DateTime CreateDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "End")]
        public DateTime? EndDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Delivery")]
        public DateTime? DeliveryDate { get; set; }

        #endregion Fields

        #region Relations

        public virtual ICollection<Task> Task { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        #endregion Relations
    }
}