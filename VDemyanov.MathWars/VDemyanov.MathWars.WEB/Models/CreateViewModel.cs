using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Input the title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Select the topic")]
        public string Topic { get; set; }
        [Required(ErrorMessage = "Add tags")]
        public string Tags { get; set; }
        [Required(ErrorMessage = "Input the summary")]
        public string Summary { get; set; }
        public List<string> Answers { get; set; }
        public SelectList Topics { get; private set; }
    }
}
