using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Logs.Exaab.Models
{
	[BsonIgnoreExtraElements]
	public class LogsModel
	{
		public ObjectId Id { get; set; }
		public DateTime Timestamp { get; set; }

		public string Level { get; set; }
		public string MessageTemplate { get; set; }
		public string RenderedMessage { get; set; }
		public Properties Properties { get; set; }
		public string UtcTimestamp { get; set; }
		public string Exception { get; set; }


	}
	[BsonIgnoreExtraElements]
	public class Properties
	{
		public string RequestPath { get; set; }
		public string ApplicationName { get; set; }
		public string SourceContext { get; set; }

	}
}
