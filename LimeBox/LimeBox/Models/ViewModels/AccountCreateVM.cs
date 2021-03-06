﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class AccountCreateVM
    {
        public string ReturnUrl { get; set; }
        public CreateFormVM CreateForm { get; set; }

        [Bind(Prefix = nameof(AccountCreateVM.CreateForm))]
        public class CreateFormVM
        {
            [Display(Name = "Användarnamn")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i användarnamn")]
            public string Username { get; set; }

            [Display(Name = "Lösenord")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,255}$", ErrorMessage = "<span style=\"color:red\">*</span>Otillåtet lösenord")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i lösenord")]
            public string Password { get; set; }

            [Display(Name = "Förnamn")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i förnamn")]
            public string FirstName { get; set; }

            [Display(Name = "Efternamn")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i efternamn")]
            public string LastName { get; set; }

            [Display(Name = "Adress")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i adress")]
            public string Address { get; set; }

            [Display(Name = "Postnummer")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i postnummer")]
            public int PostalCode { get; set; }

            [Display(Name = "Ort")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i ort")]
            public string City { get; set; }

            [Display(Name = "E-post")]
            [EmailAddress]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i mejladressen")]
            public string Email { get; set; }

            [Display(Name = "Telefonnummer")]
            [Phone]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i telefonnummer")]
            public string PhoneNumber { get; set; }

        }
    }
}
