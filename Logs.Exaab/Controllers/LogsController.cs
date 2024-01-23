using Logs.Exaab.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Logs.Exaab.Controllers
{
	public class LogsController : Controller
	{
		private readonly MongoClient _client;
		private readonly ILogger _logger;
		private readonly string _databaseName;
		private readonly string _databaseCollection;
		public LogsController(MongoDBSetting settings, ILogger<LogsController> logger)
		{
			_logger = logger;
			_databaseName = settings.DatabaseName;
			_client = _client = new MongoClient(settings.databaseUrl);
			_databaseCollection = settings.collectionName;
		}
			
		public IActionResult Index()
		{
			var collection = _client.GetDatabase(_databaseName).GetCollection<BsonDocument>(_databaseCollection);
			var projection = Builders<BsonDocument>.Projection.
				Include("MessageTemplate").
				Include("Level").Include("UtcTimestamp").
				Include("Properties").
				Include("Exception").
				Include("SourceContext");
			var document = collection.Find(Builders<BsonDocument>.Filter.Empty).Project(projection).ToList();
			var Data = document.Select(v => BsonSerializer.Deserialize<LogsModel>(v)).ToList();

			return View(Data);
		}
	}
}
