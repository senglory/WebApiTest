using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;



namespace WebApiTest.Interfaces
{
    public class Asset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual System.Guid AssetID { get; set; }

        [Required]
        public virtual string FirstName { get; set; }

        [Required]
        public virtual string LastName { get; set; }

        [Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public virtual DateTime AssetDate { get; set; }

        [Required]
        public virtual string AssetNumber { get; set; }

        [Required]
        public virtual string OrgName { get; set; }
        [Required]
        public virtual string Position { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public virtual string EMail { get; set; }
    }
}
