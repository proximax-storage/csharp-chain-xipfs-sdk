using System;
using System.Reactive.Linq;
using Proximax.Storage.SDK.Async;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;


namespace Proximax.Storage.SDK.Utils
{
    public static class AsyncUtils
    {
        public static void ProcessFirstItem<T>(IObservable<T> observable, AsyncCallbacks<T> asyncCallbacks,
            AsyncTask asyncTask)
        {
            CheckParameter(observable != null, "observable is required");
            CheckParameter(asyncTask != null, "asyncTask is required");

            observable.FirstAsync()
                .Subscribe(
                    result =>
                    {
                        asyncCallbacks?.OnSuccess(result);
                        asyncTask.SetToDone();
                    }, exception =>
                    {
                        asyncCallbacks?.OnFailure(exception);
                        asyncTask.SetToDone();
                    });
        }
    }
}