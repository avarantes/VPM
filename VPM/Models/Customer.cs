using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VPM.Models
{
    public class Customer
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        
        [Required]
        [Display(Name="VAT Number")]
        [StringLength(250)]
        public string VatNumber { get; set; }
        
        [StringLength(500)]
        public string Address { get; set; }

        [Display(Name ="ZIP")]
        [StringLength(20)]
        public string ZipCode { get; set; }
        #endregion
    }
}
