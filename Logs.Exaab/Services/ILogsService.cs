using Logs.Exaab.Models;

namespace Logs.Exaab.Services
{
    public interface ILogsService
    {
        public List<LogsModel> GetLogs(string ErrorType);
    }
}
