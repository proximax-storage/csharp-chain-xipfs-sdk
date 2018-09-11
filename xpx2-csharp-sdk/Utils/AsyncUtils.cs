using System;
using System.Reactive.Linq;
using IO.Proximax.SDK.Async;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;


namespace IO.Proximax.SDK.Utils
{
    public static class AsyncUtils
    {
        public static void ProcessFirstItem<T>(IObservable<T> observable, AsyncCallback<T> asyncCallback, AsyncTask asyncTask)
        {
            CheckParameter(observable != null, "observable is required");
            CheckParameter(asyncTask != null, "asyncTask is required");

            observable.FirstAsync()
                .Subscribe(
                    result => {
                        asyncCallback?.OnSuccess(result);
                        asyncTask.SetToDone();
                    }, exception => {
                        asyncCallback?.OnFailure(exception);
                        asyncTask.SetToDone();
                    });
        }

    }
}
