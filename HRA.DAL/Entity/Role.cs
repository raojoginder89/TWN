using Microsoft.AspNetCore.Identity;

namespace HRA.DAL.Entity
{
    public sealed class Role : IdentityRole
    {
        public Role() : base()
        { }

        public Role(string roleName) : base(roleName)
        {
            NormalizedName = roleName?.ToUpper();
        }
    }
}
