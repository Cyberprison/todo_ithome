using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace todo_ithome.Domain.Enum
{
    /// <summary>
    /// Приоритет задачи
    /// </summary>
    
    //перечисление
    public enum Priority
    {
        [Display(Name = "Easy")]
        Easy = 0,

        [Display(Name = "Medium")]
        Medium = 1,

        [Display(Name = "High")]
        High = 2

    }
}
