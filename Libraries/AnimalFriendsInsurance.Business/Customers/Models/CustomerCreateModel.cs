﻿using AnimalFriendsInsurance.Business.Customers.DataAnnotations;
using AnimalFriendsInsurance.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnimalFriendsInsurance.Business.Customers.Models
{
    /// <summary>
    /// API request containing customer data
    /// </summary>
    public class CustomerCreateModel
    {
        public const string CUSTOMER_FIRST_NAME_REQUIRED = "First name is required";
        public const string CUSTOMER_FIRST_NAME_MIN_LENGTH = "First name must be at least 3 characters";
        public const string CUSTOMER_FIRST_NAME_MAX_LENGTH = "First name must be no more than 50 characters";

        public const string CUSTOMER_SURNAME_REQUIRED = "Surname is required";
        public const string CUSTOMER_SURNAME_MIN_LENGTH = "Surname must be at least 3 characters";
        public const string CUSTOMER_SURNAME_MAX_LENGTH = "Surname must be no more than 50 characters";

        public const string CUSTOMER_POLICY_REFERENCE_REQUIRED = "The policy reference is required";
        public const string CUSTOMER_POLICY_REFERENCE_FORMAT = "The policy reference number should have two capital letters, followed by a dash, followed by 6 numbers";

        public const string EMAIL_ADDRESS_FORMAT = "The email address is not in a valid format";
        public const string EMAIL_ADDRESS_END = "Email address must end with .co.uk, or .com";

        /// <summary>
        /// List of customer entity convert rules depending on the data supplied
        /// </summary>
        private Dictionary<Type, Predicate<CustomerCreateModel>> CustomerEntityConvertRules = new Dictionary<Type, Predicate<CustomerCreateModel>>
        {
            { typeof(CustomerEmailEntity), s => !string.IsNullOrWhiteSpace(s.Email) && !s.DateOfBirth.HasValue },
            { typeof(CustomerDateOfBirthEntity), s => s.DateOfBirth.HasValue && string.IsNullOrWhiteSpace(s.Email) }
        };

        /// <summary>
        /// Customer first name
        /// </summary>
        [Required(ErrorMessage = CUSTOMER_FIRST_NAME_REQUIRED),
          MinLength(3, ErrorMessage = CUSTOMER_FIRST_NAME_MIN_LENGTH),
         MaxLength(50, ErrorMessage = CUSTOMER_FIRST_NAME_MAX_LENGTH)]
        public string? FirstName { get; init; }

        /// <summary>
        /// Customer surname
        /// </summary>
        [Required(ErrorMessage = CUSTOMER_SURNAME_REQUIRED),
            MinLength(3, ErrorMessage = CUSTOMER_SURNAME_MIN_LENGTH),
         MaxLength(50, ErrorMessage = CUSTOMER_SURNAME_MAX_LENGTH)]
        public string? Surname { get; init; }

        /// <summary>
        /// Policy reference number
        /// </summary>
        [Required(ErrorMessage = CUSTOMER_POLICY_REFERENCE_REQUIRED),
            RegularExpression(@"^([A-Z]{2})\-([0-9]{6})$", ErrorMessage = CUSTOMER_POLICY_REFERENCE_FORMAT)]
        public string? PolicyReferenceNumber { get; init; }

        /// <summary>
        /// Customer's date of birth
        /// </summary>         
        [DataType(DataType.Date), CustomerDateOfBirthValidation, CustomerEitherDobOrEmailValidation]
        public DateTime? DateOfBirth { get; init; }

        /// <summary>
        /// Customer's email address
        /// </summary>
        [EmailAddress(ErrorMessage = EMAIL_ADDRESS_FORMAT), CustomerEitherDobOrEmailValidation, RegularExpression(@"(.*)(\.co\.uk|\.com)$", ErrorMessage = "Email address must end with .co.uk, or .com")]
        public string? Email { get; init; }

        /// <summary>
        /// Gets the entity to convert the customer to
        /// </summary>
        /// 
        [JsonIgnore]
        public Type? CustomerTypeEntity
        {
            get
            {
                foreach (var rule in CustomerEntityConvertRules)
                {
                    if (rule.Value(this))
                    {
                        // Found the type based on the data supplied, so return the type (key)
                        return rule.Key;
                    }
                }

                return null;
            }
        }

    }
}
