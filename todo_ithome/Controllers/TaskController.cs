using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using todo_ithome.Domain.ViewModels;
using todo_ithome.Models;
using todo_ithome.Service.Interfaces;

namespace todo_ithome.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            var response = await _taskService.Create(model);

            //почему не работает???
            //using todo_ithome.Domain.Enum;
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new
                {
                    description = response.Description
                });
            }

            return BadRequest(new
            {
                description = response.Description
            });
        }
    }
}
