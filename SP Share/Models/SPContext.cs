using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;

namespace SP_Share.Models
{
    public class SPContext : DbContext
    {
        public DbSet<User> User { get; set; }

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
        [StringLength(100, ErrorMessage = "{0} length at least {2} characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Group")]
        public int Group { get; set; }

        public bool IsAdmin { get; set; }
    }

    //public class Groupa
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