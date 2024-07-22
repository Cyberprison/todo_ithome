using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using todo_ithome.DAL.Interfaces;
using todo_ithome.DAL.Repositories;
using todo_ithome.Domain.Entity;
using todo_ithome.Service.Implementations;
using todo_ithome.Service.Interfaces;

namespace todo_ithome
{
    public static class Initializer
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<TaskEntity>, TaskRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
        }
    }
}
