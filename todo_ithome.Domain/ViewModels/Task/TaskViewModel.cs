using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace todo_ithome.Domain.ViewModels
{
    public class TaskViewModel
    {
        public long Id { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; }

        //это строка всё правильно
        [Display(Name = "Ready")]
        public string IsDone { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }

        [Display(Name = "Discription")]
        public string Description { get; set; }
        
        [Display(Name = "Created")]
        public string Created { get; set; }
    }
}
