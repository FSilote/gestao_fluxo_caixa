namespace CodeChallenger.Sdk.WebApi.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorizationAttribute : Attribute
    {
        public AuthorizationAttribute(string permission)
        {
            this.Permission = permission;
        }

        public AuthorizationAttribute(params string[] permissions)
        {
            if (permissions?.Any() ?? false)
            {
                this.Permission = string.Join(",", permissions);
            }
        }

        public string Permission { get; private set; }

        public IList<string> Permissions
        {
            get => this.Permission?
                .Split(',')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Trim())
                .ToList()
                ?? [];
        }
    }
}
