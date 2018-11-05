﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;

namespace SP_Share.Models
{
    public class SPContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }

        public SPContext()
            : base("name=DefaultConnection")
        {
        }
    }

    public class User
    {
        [Key]
        [Required]
        [DisplayName("Login Name")]
        [StringLength(16)]
        [MaxLength(16, ErrorMessage = "Max Length 16")]
        [MinLength(5, ErrorMessage = "Min Length 5")]
        public string Account { get; set; }

        [DisplayName("Actual Name")]
        [StringLength(80)]
        [MaxLength(80, ErrorMessage = "Max Length 80")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} length at least {2} characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Group")]
        public int Group { get; set; }

        public bool IsAdmin { get; set; }
    }

    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Idx { get; set; }

        [StringLength(10)]
        public string Name { get; set; }
    }
}