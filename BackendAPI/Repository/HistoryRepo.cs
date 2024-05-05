using BackendAPI.IRepository;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class HistoryRepo:IHistory
    {
        private readonly DataContext _context;
        public HistoryRepo(DataContext context)
        {
            _context = context;
        }

        public bool AddHistory(History history)
        {
            _context.History.Add(history);
            _context.SaveChanges();
            return true;
        }

        public List<History> GetHistory(int orgid)
        {
           var data=_context.History.Where(x=>x.OrgId==orgid).ToList();
            return data;
        }
    }
}
