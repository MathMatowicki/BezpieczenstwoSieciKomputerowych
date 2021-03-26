using System.Threading;

namespace Bezpieczeństwo
{
    public interface IWorker
    {
        void DoWork(CancellationToken cancellationToken);
        ulong GetOutput();
        void SetActive(bool b);
        void SetLsfr(int size, int[] indexes);
    }
}