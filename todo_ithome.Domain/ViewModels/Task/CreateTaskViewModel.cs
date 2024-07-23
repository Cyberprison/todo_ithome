using System;
using System.Collections.Generic;
using System.Text;
using todo_ithome.Domain.Enum;

namespace todo_ithome.Domain.ViewModels
{
    public class CreateTaskViewModel
    {
        public string Name { get; set; }

        public Priority Priority { get; set; }

        public string Description { get; set; }
    }
}
