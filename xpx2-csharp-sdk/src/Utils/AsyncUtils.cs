using IO.Proximax.SDK.Async;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
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
                                if (asyncCallback != null)
                                {
                                    asyncCallback.OnSuccess(result);
                                }
                                asyncTask.SetToDone();
                            }, exception => {
                                if (asyncCallback != null)
                                {
                                    asyncCallback.OnFailure(exception);
                                }
                                asyncTask.SetToDone();
                            });
        }

    }
}
