using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IHistory
    {
        List<History>GetHistory(int orgid);
        bool AddHistory(History history);
    }
}
