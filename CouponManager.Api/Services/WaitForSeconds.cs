using System.Threading;
using System.Threading.Tasks;

namespace CouponManager.Api.Services
{
    public class WaitForSeconds
    {
        public async Task WaitSecondsAsync(int milliseconds, CancellationToken cancellationToken = default)
        {
            await Task.Delay(milliseconds, cancellationToken);
        }
    }
}
