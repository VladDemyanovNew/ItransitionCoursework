using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.WEB.Models
{
    public class UpdateViewModel
    {
        public UpdateViewModel(
            List<string> topics,
            string userId,
            string title,
            string topic,
            string tags,
            string summary,
            List<string> answers,
            string mpId)
        {
            Topics = new SelectList(topics, "Name");
            UserId = userId;
            Title = title;
            Topic = topic;
            Tags = tags;
            Summary = summary;
            Answers = answers;
            MathProblemId = mpId;
        }

        public UpdateViewModel()
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
        public string MathProblemId { get; set; }
        public List<string> Answers { get; set; }
        public SelectList Topics { get; private set; }
    }
}
