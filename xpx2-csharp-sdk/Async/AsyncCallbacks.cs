using System;

namespace IO.Proximax.SDK.Async
{
    public class AsyncCallbacks<T>
    {
        private Action<T> SuccessCallback { get; set; }
        private Action<Exception> FailureCallback { get; set; }

        public AsyncCallbacks(Action<T> successCallback, Action<Exception> failureCallback)
        {
            SuccessCallback = successCallback;
            FailureCallback = failureCallback;
        }

        public static AsyncCallbacks<A> Create<A>(Action<A> successCallback, Action<Exception> failureCallback)
        {
            return new AsyncCallbacks<A>(successCallback, failureCallback);
        }        

        public void OnSuccess(T result)
        {
            SuccessCallback?.Invoke(result);
        }

        public void OnFailure(Exception ex)
        {
            FailureCallback?.Invoke(ex);
        }

    }
}
