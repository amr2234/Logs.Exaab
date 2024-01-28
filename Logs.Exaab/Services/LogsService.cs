using Logs.Exaab.Controllers;
using Logs.Exaab.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;

namespace Logs.Exaab.Services
{
    public class LogsService : ILogsService
    {
        private readonly MongoClient _client;
        private readonly string _databaseName;
        private readonly string _databaseCollection;
        public LogsService(MongoDBSetting settings)
        {
            _databaseName = settings.DatabaseName;
            _client = _client = new MongoClient(settings.databaseUrl);
            _databaseCollection = settings.collectionName;
        }

        public List<LogsModel> GetLogs(string ErrorType)
        {
            var collection = _client.GetDatabase(_databaseName).GetCollection<BsonDocument>(_databaseCollection);
            var projection = Builders<BsonDocument>.Projection.
                Include("MessageTemplate").
                Include("Level").Include("UtcTimestamp").
                Include("Properties").
                Include("Exception").
                Include("SourceContext");
            if (ErrorType != null)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("Level", ErrorType);
                var document = collection.Find(filter).Project(projection).ToList();
                var Data = document.Select(v => BsonSerializer.Deserialize<LogsModel>(v)).ToList();
                return Data;
            }
            else
            {
                var document = collection.Find(Builders<BsonDocument>.Filter.Empty).Project(projection).ToList();
                var Data = document.Select(v => BsonSerializer.Deserialize<LogsModel>(v)).ToList();
                return Data;
            }
        }


    }
}
