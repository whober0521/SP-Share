using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System;

namespace SP_Share.Models
{
    public class SPContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<Item> Item { get; set; }

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
        [MinLength(6, ErrorMessage = "Min Length 6")]
        public string Account { get; set; }

        [DisplayName("Actual Name")]
        [StringLength(80)]
        [MaxLength(80, ErrorMessage = "Max Length 80")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} length at least {2} characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password not matched")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Group")]
        public int? Group { get; set; }

        [Display(Name = "Is Administrator?")]
        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }
    }

    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Idx { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }

    public class UserGroup
    {
        [Key, Column(Order = 0)]
        public string User { get; set; }

        [Key, Column(Order = 1)]
        public int Group { get; set; }
    }

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Idx { get; set; }

        [DisplayName("Item Name")]
        [StringLength(68)]
        [MaxLength(68, ErrorMessage = "Max Length 68")]
        public string Name { get; set; }

        [DisplayName("Item Description")]
        [StringLength(500)]
        [MaxLength(500, ErrorMessage = "Max Length 500")]
        public string Description { get; set; }

        public byte[] Content { get; set; }

        public int Group { get; set; }

        [DisplayName("Creator's Name")]
        [StringLength(16)]
        public string Creator { get; set; }

        [DisplayName("Create Time")]
        public DateTime CreateTime { get; set; }

        [DisplayName("Access Time")]
        public DateTime AccessTime { get; set; }
    }
}