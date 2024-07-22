using System;
using System.Collections.Generic;
using System.Text;
using todo_ithome.Domain.Enum;

namespace todo_ithome.Domain.Entity
{
    //по умолчанию поля и сам класс приватный
    public class TaskEntity
    {
        public long Id { get; set; }

        public string Name{ get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }
    }
}
