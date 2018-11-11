﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AdvantagePlatform.Data;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdvantagePlatform.Pages.Tools
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _appContext;
        private readonly IConfigurationDbContext _identityContext;
        private readonly UserManager<AdvantagePlatformUser> _userManager;

        public DetailsModel(
            ApplicationDbContext appContext,
            IConfigurationDbContext identityContext, 
            UserManager<AdvantagePlatformUser> userManager)
        {
            _appContext = appContext;
            _identityContext = identityContext;
            _userManager = userManager;
        }

        [Display(Name = "Issuer", Description = "This is the Issuer for all launch messages from the platform.")]
        public string PlatformIssuer { get; set; }

        public ToolModel Tool { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var tool = await _appContext.Tools.FindAsync(id);
            if (tool == null || tool.UserId != user.Id)
            {
                return NotFound();
            }

            var client = await _identityContext.Clients
                .Include(c => c.ClientSecrets)
                .Include(c => c.Properties)
                .SingleOrDefaultAsync(c => c.Id == tool.IdentityServerClientId);
            if (client == null)
            {
                return NotFound();
            }

            Tool = new ToolModel(tool, client);

            PlatformIssuer = HttpContext.GetIdentityServerIssuerUri();

            return Page();
        }
    }
}
