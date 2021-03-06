﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
        public DbSet<ItemLimit> ItemLimit { get; set; }

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

        public string Salt { get; set; }

        [NotMapped]
        [Display(Name = "Group")]
        public Guid? Group { get; set; }

        public int Limit { get; set; }
        public string Size { get; set; }

        [Display(Name = "Is Administrator?")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }

    public class Group
    {
        [Key]
        public Guid Idx { get; set; }

        [Required]
        [StringLength(16)]
        [MaxLength(16, ErrorMessage = "Max Length 16")]
        public string Name { get; set; }

        public int Limit { get; set; }
        public string Size { get; set; }

        public bool IsActive { get; set; }
    }

    public class UserGroup
    {
        [Key, Column(Order = 0)]
        public string User { get; set; }

        [Key, Column(Order = 1)]
        public Guid Group { get; set; }
    }

    public class ItemLimit
    {
        [Key]
        [Required]
        [DisplayName("Extension Name")]
        [StringLength(16)]
        [MaxLength(16, ErrorMessage = "Max Length 16")]
        public string Name { get; set; }

        public int Limit { get; set; }
        public string Size { get; set; }
    }

    public class Item
    {
        [Key]
        public Guid Idx { get; set; }

        [DisplayName("Item Name")]
        [StringLength(68)]
        [MaxLength(68, ErrorMessage = "Max Length 68")]
        public string Name { get; set; }

        [DisplayName("Item Description")]
        [StringLength(500)]
        [MaxLength(500, ErrorMessage = "Max Length 500")]
        public string Description { get; set; }

        public byte[] Content { get; set; }
        public int Length { get; set; }

        public Guid Group { get; set; }

        [DisplayName("Creator's Name")]
        [StringLength(16)]
        public string Creator { get; set; }

        [DisplayName("Create Time")]
        public DateTime CreateTime { get; set; }

        [DisplayName("Access Time")]
        public DateTime AccessTime { get; set; }

        [ForeignKey("Group")]
        public virtual Group GroupDetail { get; set; }

        [ForeignKey("Creator")]
        public virtual User CreatorDetail { get; set; }
    }
}