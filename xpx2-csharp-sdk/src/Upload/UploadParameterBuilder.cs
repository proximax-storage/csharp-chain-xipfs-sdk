using System;
using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.PrivacyStrategies;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Upload
{
    public class UploadParameterBuilder
    {
        private string SignerPrivateKey { get; set; }
        private string RecipientPublicKey { get; set; }
        private string Description { get; set; }
        private IPrivacyStrategy PrivacyStrategy { get; set; }
        private bool ComputeDigest { get; set; }
        private IList<IUploadParameterData> DataList { get; set; }

        internal UploadParameterBuilder(string signerPrivateKey, string recipientPublicKey)
        {
            CheckParameter(signerPrivateKey != null, "signerPrivateKey is required");
            CheckParameter(recipientPublicKey != null, "recipientPublicKey is required");

            SignerPrivateKey = signerPrivateKey;
            RecipientPublicKey = recipientPublicKey;
            DataList = new List<IUploadParameterData>();
            ComputeDigest = true;
        }

        public UploadParameterBuilder AddFile(FileParameterData parameterData)
        {
            CheckParameter(parameterData != null, "parameterData is required");

            throw new NotImplementedException();
        }

        public UploadParameterBuilder AddFilesAsZip(FilesAsZipParameterData parameterData)
        {
            CheckParameter(parameterData != null, "parameterData is required");

            throw new NotImplementedException();
        }

        public UploadParameterBuilder AddByteArray(ByteArrayParameterData parameterData)
        {
            CheckParameter(parameterData != null, "parameterData is required");

            throw new NotImplementedException();
        }

        public UploadParameterBuilder AddString(StringParameterData parameterData)
        {
            CheckParameter(parameterData != null, "parameterData is required");

            throw new NotImplementedException();
        }

        public UploadParameterBuilder AddUrlResource(UrlResourceParameterData parameterData)
        {
            CheckParameter(parameterData != null, "parameterData is required");

            throw new NotImplementedException();
        }

        public UploadParameterBuilder AddPath(PathParameterData parameterData)
        {
            CheckParameter(parameterData != null, "parameterData is required");

            throw new NotImplementedException();
        }

        public UploadParameterBuilder WithDescription(string description)
        {
            CheckParameter(description == null || description.Length <= 500, "root description cannot exceed 500 characters");

            Description = description;
            return this;
        }

        public UploadParameterBuilder WithComputeDigest(Boolean computeDigest)
        {
            ComputeDigest = computeDigest;
            return this;
        }

        public UploadParameterBuilder WithPrivacyStrategy(IPrivacyStrategy privacyStrategy)
        {
            PrivacyStrategy = privacyStrategy;
            return this;
        }

        public UploadParameterBuilder WithPlainPrivacy()
        {
            PrivacyStrategy = PlainPrivacyStrategy.Create(null);
            return this;
        }

        public UploadParameter Build()
        {
            if (DataList == null && DataList.Count == 0)
                throw new UploadParameterBuildFailureException("A parameter data should be provided. Considering using one of the Add***() methods");

            if (this.PrivacyStrategy == null)
                this.PrivacyStrategy = PlainPrivacyStrategy.Create(null);

            return new UploadParameter(SignerPrivateKey, RecipientPublicKey, Description, PrivacyStrategy, ComputeDigest, DataList);
        }
    }
}
