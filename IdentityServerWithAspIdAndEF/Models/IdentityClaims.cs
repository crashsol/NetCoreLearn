using System;
using System.Collections.Generic;

namespace IdentityServerWithAspIdAndEF.Models
{
    public partial class IdentityClaims
    {
        public int Id { get; set; }
        public int IdentityResourceId { get; set; }
        public string Type { get; set; }

        public IdentityResources IdentityResource { get; set; }
    }
}
