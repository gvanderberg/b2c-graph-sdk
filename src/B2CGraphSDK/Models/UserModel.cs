using System;
using System.Collections.Generic;

namespace B2CGraphSDK.Models
{
    public class UserModel
    {
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

        public bool AccountEnabled { get; }

        public string City { get; }

        public string Country { get; }

        public string PostalCode { get; }

        public string CreationType { get; } = "LocalAccount";

        public string DisplayName { get; }

        public string GivenName { get; }

        public string Surname { get; }

        public string PasswordPolicies { get; } = "DisablePasswordExpiration,DisableStrongPassword";

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