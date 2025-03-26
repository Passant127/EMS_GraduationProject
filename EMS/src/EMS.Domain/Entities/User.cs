using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace EMS.Entities
{
    public class AppUsers : IdentityUser
    {
        private Guid guid;
        private DateTime? bOD;
        private object value;

        public AppUsers(Guid id, string userName, string email, Guid? tenantId = null)
            : base(id, userName, email, tenantId)
        {
        }

        public AppUsers(Guid guid, string userName, string email, float? height, float? weight, DateTime? bOD, object value , string? address)
        {
            this.guid = guid;
            UserName = userName;
            Email = email;
            Height = height;
            Weight = weight;
            BOD = DateOnly.FromDateTime((DateTime)bOD);
            Address = address;
        }
        public new string? PasswordHash
        {
            get => base.PasswordHash;
            set => base.PasswordHash = value;
        }

        public float? Weight { get; set; }
        public float? Height { get; set; }
        public DateOnly? BOD { get; set; }
        public string? Address { get; set; }
    
    }


}
