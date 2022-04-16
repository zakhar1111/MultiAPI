using System.Threading.Tasks;
using Time.API.Model;

namespace Time.API.Service
{
    public interface ITimeService
    {
        Task<TimeRoot> GetLocal();
        Task<TimeRoot> GetTime(string city);
    }
}
