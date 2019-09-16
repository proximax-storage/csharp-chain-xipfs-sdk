using System;
using System.Reactive.Linq;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Upload;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Services
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