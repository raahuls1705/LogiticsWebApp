using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;

namespace LogiticsWebApp.Repositories
{

    public class ErrorLogRepository : BaseRepository<ErrorLog>
    {
     
        private readonly LogisticDbContext _context;
        public ErrorLogRepository(LogisticDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
