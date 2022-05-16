using System.Threading.Tasks;
using Time.API.Model;

namespace Time.API.Service
{
    public interface ITimeService
    {
        Task<DtoTime> GetLocal();
        Task<DtoTime> GetTime(string city);
    }
}
