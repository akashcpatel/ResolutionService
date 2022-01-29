using Publisher.Message.Data;
using System.Threading.Tasks;

namespace Publisher
{
    public interface IUserChangedReceiver
    {
        Task<UserChangedData> Receive();
    }
}
