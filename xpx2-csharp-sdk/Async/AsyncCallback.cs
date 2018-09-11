using System;

namespace IO.Proximax.SDK.Async
{
    public class AsyncCallback<T>
    {
        private Action<T> SuccessCallback { get; set; }
        private Action<Exception> FailureCallback { get; set; }

        public AsyncCallback(Action<T> successCallback, Action<Exception> failureCallback)
        {
            SuccessCallback = successCallback;
            FailureCallback = failureCallback;
        }

        public static AsyncCallback<A> Create<A>(Action<A> successCallback, Action<Exception> failureCallback)
        {
            return new AsyncCallback<A>(successCallback, failureCallback);
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
