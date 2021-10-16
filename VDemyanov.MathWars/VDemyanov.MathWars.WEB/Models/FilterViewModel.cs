using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.WEB.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<string> topics, string topic)
        {
            topics.Insert(0, "All");
            Topics = new SelectList(topics, "Name");
            SelectedTopic = topic;
        }

        public SelectList Topics { get; private set; }
        public string SelectedTopic { get; private set; }
    }
}
