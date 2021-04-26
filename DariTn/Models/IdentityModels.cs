using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DariTn.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser. Pour en savoir plus, consultez https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.AssetAdv> AssetAdvs { get; set; }

        public System.Data.Entity.DbSet<DariTN.Models.Entities.Complaint> Complaints { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.Bank> Banks { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.CreditFormula> CreditFormulas { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.InsuranceAgency> InsuranceAgencies { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.Pack> Packs { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.Credit> Credits { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.EmailViewModel> EmailViewModals { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.Insurance> Insurances { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.BoughtAsset> BoughtAssets { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.TimeSlots> TimeSlots { get; set; }

        public System.Data.Entity.DbSet<DariTn.Models.Entities.Guarantee> Guarantees { get; set; }
    }
}