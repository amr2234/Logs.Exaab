using Logs.Exaab.Models;
using Logs.Exaab.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Logs.Exaab.Controllers
{
	public class LogsController : Controller
	{
		private readonly ILogger _logger;
		private readonly ILogsService _logsService;

		public LogsController( ILogger<LogsController> logger, ILogsService logsService)
		{
			_logger = logger;	
			_logsService = logsService;
		
		}
        public IActionResult GetData()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetData(string ErrorType)
			{
			var data = _logsService.GetLogs(ErrorType);

            return Json(data);
		}
	}
}
