using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.WEB.Models
{
    public class CreateViewModel
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Tags { get; set; }
        public string Summary { get; set; }
        public string Answers { get; set; }
    }
}
