using System;
using System.Collections.Generic;
using System.Text;
using todo_ithome.Domain.Enum;

namespace todo_ithome.Domain.Filters.Task
{
    public class TaskFilter
    {
        public string Name { get; set; }

        public Priority? Priority { get; set; }
    }
}
