namespace Proximax.Storage.SDK.Async
{
    public class AsyncTask
    {
        private bool Done { get; set; }
        private bool Cancelled { get; set; }

        public AsyncTask()
        {
            Done = false;
            Cancelled = false;
        }

        public void SetToDone()
        {
            if (!Cancelled)
                Done = true;
        }

        public void SetToCancelled()
        {
            if (!Done)
                Cancelled = true;
        }

        public bool IsDone()
        {
            return Done;
        }

        public bool IsCancelled()
        {
            return Cancelled;
        }
    }
}