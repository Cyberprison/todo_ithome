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
using todo_ithome.Domain.Extensions;

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
                
                var task = await _taskRepository.GetAll()
                    .Where(x => x.Created.Date == DateTime.Today)
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

        //need async???
        //warning
        public async Task<IBaseResponse<IEnumerable<TaskViewModel>>> GetTasks()
        {
            try
            {
                var tasks = _taskRepository.GetAll()
                    .Select(x => new TaskViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsDone = x.IsDone == true ? "Ready" : "No ready",
                        Description = x.Description,
                        Priority = x.Priority.GetDisplayName(),
                        Created = x.Created.ToLongDateString()
                    });

                _logger.LogInformation($"[TaskService.GetTask] gets count elements {tasks.Count()}");

                return new BaseResponse<IEnumerable<TaskViewModel>>()
                {
                    Data = tasks,
                    StatusCode = StatusCode.OK
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.GetTask]: {ex.Message}");

                return new BaseResponse<IEnumerable<TaskViewModel>> ()
                {
                    Description = "Internal Error",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<TaskViewModel>> GetTask(long id)
        {
            try
            {
                //получение задачи
                var task = await _taskRepository.GetAll()
                       .FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                {
                    return new BaseResponse<TaskViewModel>()
                    {
                        Description = "Task not found",
                        StatusCode = StatusCode.TaskNotFound
                    };
                }

                var data = new TaskViewModel()
                {
                    Id = task.Id,
                    Name = task.Name,
                    IsDone = task.IsDone == true ? "Ready" : "No ready",
                    Description = task.Description,
                    Priority = task.Priority.GetDisplayName(),
                    Created = task.Created.ToLongDateString()
                };

                return new BaseResponse<TaskViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.GetTask]: {ex.Message}");

                return new BaseResponse<TaskViewModel>()
                {
                    Description = "Internal Error",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> EndTask(long id)
        {
            try
            {
                var task = await _taskRepository.GetAll()
                       .FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Task not found",
                        StatusCode = StatusCode.TaskNotFound
                    };
                }

                task.IsDone = true;

                await _taskRepository.Update(task);

                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Description = "Task is finally"
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.EndTask]: {ex.Message}");

                return new BaseResponse<bool>()
                {
                    Description = "Internal Error",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }





}

