﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdvantagePlatform.Data;
using Microsoft.AspNetCore.Identity;

namespace AdvantagePlatform.Pages.Deployments
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AdvantagePlatformUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<AdvantagePlatformUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Deployment> Deployments { get;set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            Deployments = await _context.Deployments
                .Where(d => d.UserId == user.Id)
                .Include(d => d.Tool)
                .Include(d => d.Client)
                .ToListAsync();
        }
    }
}