using Microsoft.EntityFrameworkCore;

namespace medienVerwaltungDbSolution.Services
{
    public class RetryStrat : SqlServerRetryingExecutionStrategy
    {
        private readonly List<TimeSpan> _retryDelays;
        private int _currentRetryCount = 0;

        public RetryStrat(DatabaseContext context)
            : base(context, 3, TimeSpan.FromSeconds(30), null)
        {
            _retryDelays =
            [
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(15),
                TimeSpan.FromSeconds(30)
            ];
        }

        protected override TimeSpan? GetNextDelay(Exception lastException)
        {

            if (_currentRetryCount < _retryDelays.Count)
            {
                return _retryDelays[_currentRetryCount];
            }

            return null;
        }

        protected void OnRetry(Exception e, TimeSpan delay, int retryCount, DatabaseContext context)
        {
            _currentRetryCount++;
        }
    }
}