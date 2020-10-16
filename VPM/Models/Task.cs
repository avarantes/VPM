using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VPM.Models
{
    public class Task
    {
        #region Fields

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Billable Time")]
        public DateTime? BillableTime { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        [Display(Name = "Cost per Hour (€)")]
        public decimal CostPerHour { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        #endregion Fields

        [Column(TypeName = "decimal(16,4)")]
        [Display(Name = "Task Cost (€)")]
        public decimal? TaskCost { get; set; }

        #region Relations

        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        #endregion Relations

        
        
    }
}