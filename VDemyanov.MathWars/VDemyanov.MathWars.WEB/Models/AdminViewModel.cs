using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.WEB.Models
{
    public class AdminViewModel
    {
        public List<IdentityUser> Users { get; set; }
    }
}
