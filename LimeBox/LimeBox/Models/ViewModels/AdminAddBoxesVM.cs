﻿using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class AdminAddBoxesVM
    {
        [Required(ErrorMessage = "Det måste finnas ett namn!")]
        [Display(Name = "Namn på box")]
        public string BoxType { get; set; }

        [Required(ErrorMessage = "Du måste ange ett pris")]
        [Display(Name = "Pris")]
        [DataType(DataType.Currency, ErrorMessage = "Måste vara en siffra")]
        public decimal? BoxPrice { get; set; }
    }
}