using System;
using System.Reactive.Linq;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Upload;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
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