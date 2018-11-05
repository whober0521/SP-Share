using System.Data.Entity;

namespace SP_Share.Models
{
    public class SPContext : DbContext
    {
        //public DbSet<Company> Company { get; set; }
        //public DbSet<Group> Group { get; set; }
        //public DbSet<Rank> Rank { get; set; }
        //public DbSet<Project> Project { get; set; }
        //public DbSet<ProjectDetail> ProjectDetail { get; set; }
        //public DbSet<Member> Member { get; set; }
        //public DbSet<CreditCard> CreditCard { get; set; }
        //public DbSet<Purchase> Purchase { get; set; }
        //public DbSet<SerialNumber> SerialNumber { get; set; }
        //public DbSet<PointHistory> PointHistory { get; set; }
        //public DbSet<BonusHistory> BonusHistory { get; set; }
        //public DbSet<PaymentHistory> PaymentHistory { get; set; }
        //public DbSet<Feedback> Feedback { get; set; }

        public SPContext()
            : base("name=DefaultConnection")
        {
        }

        //public class Group
        //{
        //    [Key, Column(Order = 0)]
        //    [Required]
        //    [DisplayName("公司代碼")]
        //    [StringLength(2)]
        //    [MaxLength(2, ErrorMessage = "長度不可超過 2")]
        //    public string Company { get; set; }

        //    [Key, Column(Order = 1)]
        //    [Required]
        //    [DisplayName("組別")]
        //    [StringLength(1)]
        //    [MaxLength(1, ErrorMessage = "長度不可超過 1")]
        //    public string Code { get; set; }

        //    [DisplayName("對應編號")]
        //    [StringLength(2)]
        //    [MaxLength(2, ErrorMessage = "長度不可超過 2")]
        //    public string SerialNo { get; set; }
        //}
    }
}