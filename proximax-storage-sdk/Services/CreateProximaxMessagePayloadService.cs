using System;
using System.Reactive.Linq;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.Upload;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Services
{
    public class CreateProximaxMessagePayloadService
    {
        public IObservable<ProximaxMessagePayloadModel> CreateMessagePayload(UploadParameter uploadParameter,
            ProximaxDataModel uploadedData)
        {
            CheckParameter(uploadParameter != null, "uploadParameter is required");
            CheckParameter(uploadedData != null, "uploadedData is required");

            return Observable.Return(ProximaxMessagePayloadModel.Create(
                uploadParameter.PrivacyStrategy.GetPrivacyType(),
                uploadParameter.Version, uploadedData));
        }
    }
}