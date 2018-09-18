using System;
using System.Reactive.Linq;
using IO.Proximax.SDK.Async;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;


namespace IO.Proximax.SDK.Utils
{
    public static class AsyncUtils
    {
        public static void ProcessFirstItem<T>(IObservable<T> observable, AsyncCallbacks<T> asyncCallbacks, AsyncTask asyncTask)
        {
            CheckParameter(observable != null, "observable is required");
            CheckParameter(asyncTask != null, "asyncTask is required");

            observable.FirstAsync()
                .Subscribe(
                    result => {
                        asyncCallbacks?.OnSuccess(result);
                        asyncTask.SetToDone();
                    }, exception => {
                        asyncCallbacks?.OnFailure(exception);
                        asyncTask.SetToDone();
                    });
        }

    }
}
