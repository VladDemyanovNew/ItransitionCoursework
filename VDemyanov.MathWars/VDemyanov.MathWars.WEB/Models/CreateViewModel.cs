using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.WEB.Models
{
    public class CreateViewModel
    {
        public CreateViewModel(List<string> topics, string userId)
        {
            Topics = new SelectList(topics, "Name");
            UserId = userId;
        }

        public CreateViewModel()
        {

        }

        public string UserId { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Tags { get; set; }
        public string Summary { get; set; }
        public List<string> Answers { get; set; }
        public SelectList Topics { get; private set; }
    }
}
