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
        public System.Guid AssetID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime AssetDate { get; set; }

        [Required]
        public string AssetNumber { get; set; }

        [Required]
        public string OrgName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EMail { get; set; }
    }
}
