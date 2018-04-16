﻿using LimeBox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Views.Shared.Components.BoughtBoxes
{
    public class BoughtBoxesViewComponent : ViewComponent
    {
        private Repository repository;

        public BoughtBoxesViewComponent(Repository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.GetBoughtBoxesVM(User.Identity.Name));
        }
    }
}
