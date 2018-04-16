﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimeBox.Models;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LimeBox.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository accountRepository;
        private readonly Repository repository;

        public AccountController(AccountRepository accountRepository, Repository repository)
        {
            this.accountRepository = accountRepository;
            this.repository = repository;
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            //await accountRepository.AddRoleAsync("Standard");
            return Content("Success!");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AccountCreateVM
            {
                ReturnUrl = Request.Headers["Referer"].ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await accountRepository.AddNewUserAsync(model.CreateForm))
            {
                await accountRepository.TryLoginAsync(new AccountLoginVM { Username = model.CreateForm.Username, Password = model.CreateForm.Password });

                if (model.ReturnUrl == null)
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                return Redirect(model.ReturnUrl);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            return View(new AccountLoginVM
            {
                ReturnUrl = ReturnUrl
            });
        }


        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM viewModel)
        {
            string referer = Request.Headers["Referer"].ToString();

            if (!ModelState.IsValid)
                return Redirect(referer);

            // Check if credentials is valid (and set auth cookie)
            if (!await accountRepository.TryLoginAsync(viewModel))
            {
                // Show login error
                ModelState.AddModelError(nameof(AccountLoginVM.Username), "Invalid credentials");
                return Redirect(referer);
            }

            if (viewModel.ReturnUrl == null)
                return Redirect(referer);
            return Redirect(viewModel.ReturnUrl);
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await accountRepository.TryLogOutAsync();

            string referer = Request.Headers["Referer"].ToString();

            return Redirect(referer);

        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(new AccountSettingsVM
                {
                    CreateForm = await repository.GetAccountSettingsVM(User),
                    ReturnUrl = Request.Headers["Referer"].ToString()
                });
            }
            else
            {
                return RedirectToAction(nameof(AccessDenied));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Settings(AccountSettingsVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await accountRepository.UpdateUser(model.CreateForm);

            return View();
        }
        [HttpGet]
        public IActionResult Order(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(repository.GetAccountOrderVM(id));
            }
            else
            {
                return RedirectToAction(nameof(AccessDenied));
            }
        }
    }
}
