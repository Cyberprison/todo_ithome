using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using todo_ithome.Domain.Entity;
using todo_ithome.Domain.Response;
using todo_ithome.Domain.ViewModels;

namespace todo_ithome.Service.Interfaces
{
    public interface ITaskService
    {
        Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model);
    }
}
