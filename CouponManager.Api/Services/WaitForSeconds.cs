using System.Threading.Tasks;

namespace CouponManager.Api.Services
{
    public class WaitForSeconds
    {
        public async Task WaitSeconds(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}
