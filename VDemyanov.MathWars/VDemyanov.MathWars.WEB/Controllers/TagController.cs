﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.Service.Interfaces;

namespace VDemyanov.MathWars.WEB.Controllers
{
    public class TagController : Controller
    {
        ITagService _tagService;
        UserManager<IdentityUser> _userManager;
        public IConfiguration Configuration { get; }

        public TagController(ITagService tagService,
                             UserManager<IdentityUser> userManager,
                             IConfiguration configuration)
        {
            _tagService = tagService;
            _userManager = userManager;
            Configuration = configuration;
        }
        [HttpGet]
        public JsonResult GetTagNames()
        {
            List<string> tags = _tagService.GetAll().Select(tag => tag.Name).ToList();
            return Json(tags);
        }
    }
}
