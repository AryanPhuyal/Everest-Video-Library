using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Everest_Video_Library
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }

        public override IQueryable<IdentityRole> Roles => base.Roles;

        public override Task<IdentityResult> CreateAsync(IdentityRole role)
        {
            return base.CreateAsync(role);
        }

        public override Task<IdentityResult> DeleteAsync(IdentityRole role)
        {
            return base.DeleteAsync(role);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override Task<IdentityRole> FindByIdAsync(string roleId)
        {
            return base.FindByIdAsync(roleId);
        }

        public override Task<IdentityRole> FindByNameAsync(string roleName)
        {
            return base.FindByNameAsync(roleName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task<bool> RoleExistsAsync(string roleName)
        {
            return base.RoleExistsAsync(roleName);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override Task<IdentityResult> UpdateAsync(IdentityRole role)
        {
            return base.UpdateAsync(role);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}