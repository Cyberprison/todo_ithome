using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using todo_ithome.Domain.Entity;
using todo_ithome.Domain.Response;
using todo_ithome.Domain.ViewModels;
using todo_ithome.Service.Interfaces;
using Microsoft.Extensions.Logging;
using todo_ithome.DAL.Interfaces;
using System.Linq;
using todo_ithome.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace todo_ithome.Service.Implementations
{
    public class TaskService : ITaskService
    {
        private ILogger<TaskService> _logger;

        private readonly IBaseRepository<TaskEntity> _taskRepository;

        public TaskService(ILogger<TaskService> logger, IBaseRepository<TaskEntity> taskRepository)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model)
        {
            try
            {
                _logger.LogInformation($"Request on create task - {model.Name}");

                //чтобы тут LINQ работало для IQueryable, не забудь подключить
                //using Microsoft.EntityFrameworkCore;
                //using System.Linq;
                var task = await _taskRepository.GetAll()
                    .Where(x => x.Created == DateTime.Today)
                    .FirstOrDefaultAsync(x => x.Name == model.Name);

                if (task != null)
                {
                    return new BaseResponse<TaskEntity>()
                    {
                        Description = "Task is has already",
                        StatusCode = StatusCode.InternalServerError
                    };
                }

                task = new TaskEntity()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Priority = model.Priority,
                    Created = DateTime.Now
                };

                await _taskRepository.Create(task);

                _logger.LogInformation($"Task added: {task.Name} {task.Created}");

                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task created",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.Create]: {ex.Message}");

                return new BaseResponse<TaskEntity>()
                {
                    Description = "Internal Error",
                    StatusCode = StatusCode.InternalServerError
                };
            }





        }








    }





}

