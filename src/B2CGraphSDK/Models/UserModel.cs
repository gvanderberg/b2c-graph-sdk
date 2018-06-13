using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace B2CGraphSDK.Models
{
    public class UserModel
    {
        [JsonConstructor]
        private UserModel()
        { }

        private UserModel(bool active, string city, string country, string postalCode, string fullName, string firstName, string lastName, string emailAddress)
        {
            AccountEnabled = active;
            DisplayName = fullName;
            GivenName = firstName;
            Surname = lastName;

            SignInNames.Add(new SignInType { Value = emailAddress });
        }

        public static UserModel Create(bool active, string city, string country, string postalCode, string fullName, string firstName, string lastName, string emailAddress)
        {
            return new UserModel(active, city, country, postalCode, fullName, firstName, lastName, emailAddress);
        }

        public string ObjectId { get; set; }

        public bool AccountEnabled { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public string CreationType { get; set; } = "LocalAccount";

        public string DisplayName { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string PasswordPolicies { get; set; } = "DisablePasswordExpiration,DisableStrongPassword";

        public PasswordProfile PasswordProfile { get; } = new PasswordProfile();

        public List<SignInType> SignInNames { get; } = new List<SignInType>();
    }

    public class PasswordProfile
    {
        public bool ForceChangePasswordNextLogin { get; set; } = true;

        public string Password { get; set; } = "P@ssword!";
    }

    public class SignInType
    {
        public string Type { get; set; } = "emailAddress";

        public string Value { get; set; }
    }
}