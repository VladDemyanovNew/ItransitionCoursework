using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.WEB.Models
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState CreationDateSort { get; private set; }
        public SortState LastEditDateSort { get; private set; }
        public SortState TopicSort { get; private set; }
        public SortState Current { get; private set; }
        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            CreationDateSort = sortOrder == SortState.CreationDateAsc ? SortState.CreationDateDesc : SortState.CreationDateAsc;
            LastEditDateSort = sortOrder == SortState.LastEditDateAsc ? SortState.LastEditDateDesc : SortState.LastEditDateAsc;
            TopicSort = sortOrder == SortState.TopicAsc ? SortState.TopicDesc : SortState.TopicAsc;

            Current = sortOrder;
        }
    }
}
