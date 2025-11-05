using System;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Models
{
    public class Member
    {
        [Key]
        [Required]
        public string Account { get; set; }
        public string PasswordHash { get; set; }
        public string RealName { get; set; }
        public string Phone { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public string BirthDay { get; set; }
        public string ImageName { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpire { get; set; }
    }
}