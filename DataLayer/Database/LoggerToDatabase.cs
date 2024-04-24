using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;

namespace DataLayer.Database
{
    public class LoggerToDatabase
    {
        private readonly DatabaseContext _context;

        public LoggerToDatabase(DatabaseContext context)
        {
            _context = context;
        }

        public void LogMessage(string message)
        {
            var logEntry = new LogEntry { Message = message };
            _context.LogEntries.Add(logEntry);
            _context.SaveChanges();
        }
    }
}
