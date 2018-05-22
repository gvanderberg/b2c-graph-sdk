using System;

namespace B2CGraphSDK
{
    public class B2COptions
    {
        public B2COptions()
        {
            AzureAdB2CInstance = Globals.Instance;
        }

        public string Authority => $"{AzureAdB2CInstance}/{Tenant}";

        public string AzureAdB2CInstance { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Tenant { get; set; }
    }
}