using System;
using System.Collections.Generic;
using System.IO;
using Proximax.Storage.SDK.PrivacyStrategies;
using ProximaX.Sirius.Chain.Sdk.Model.Mosaics;
using static Proximax.Storage.SDK.Models.Constants;

namespace Proximax.Storage.SDK.Upload
{
    public class UploadParameter
    {
        public IUploadParameterData Data { get; }
        public string SignerPrivateKey { get; }
        public string RecipientPublicKey { get; }
        public string RecipientAddress { get; }
        public bool ComputeDigest { get; }
        public bool DetectContentType { get; }
        public int TransactionDeadline { get; }
        public List<Mosaic> TransactionMosaics { get; }
        public bool UseBlockchainSecureMessage { get; }
        public IPrivacyStrategy PrivacyStrategy { get; }
        public string Version { get; }

        public UploadParameter(IUploadParameterData data, string signerPrivateKey, string recipientPublicKey,
            string recipientAddress, bool computeDigest, bool detectContentType, int transactionDeadline,
            List<Mosaic> transactionMosaics, bool useBlockchainSecureMessage, IPrivacyStrategy privacyStrategy)
        {
            Data = data;
            SignerPrivateKey = signerPrivateKey;
            RecipientPublicKey = recipientPublicKey;
            RecipientAddress = recipientAddress;
            ComputeDigest = computeDigest;
            DetectContentType = detectContentType;
            TransactionDeadline = transactionDeadline;
            TransactionMosaics = transactionMosaics;
            UseBlockchainSecureMessage = useBlockchainSecureMessage;
            PrivacyStrategy = privacyStrategy;
            Version = SchemaVersion;
        }

        public static UploadParameterBuilder CreateForFileUpload(string file, string signerPrivateKey)
        {
            return CreateForFileUpload(FileParameterData.Create(file), signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForFileUpload(FileParameterData parameterData,
            string signerPrivateKey)
        {
            return new UploadParameterBuilder(parameterData, signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForByteArrayUpload(byte[] bytes, string signerPrivateKey)
        {
            return CreateForByteArrayUpload(ByteArrayParameterData.Create(bytes), signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForByteArrayUpload(ByteArrayParameterData parameterData,
            string signerPrivateKey)
        {
            return new UploadParameterBuilder(parameterData, signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForStringUpload(string @string, string signerPrivateKey)
        {
            return CreateForStringUpload(StringParameterData.Create(@string), signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForStringUpload(StringParameterData parameterData,
            string signerPrivateKey)
        {
            return new UploadParameterBuilder(parameterData, signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForUrlResourceUpload(string url, string signerPrivateKey)
        {
            return CreateForUrlResourceUpload(UrlResourceParameterData.Create(url), signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForUrlResourceUpload(UrlResourceParameterData parameterData,
            string signerPrivateKey)
        {
            return new UploadParameterBuilder(parameterData, signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForStreamUpload(Func<Stream> streamSupplier, string signerPrivateKey)
        {
            return CreateForStreamUpload(StreamParameterData.Create(streamSupplier), signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForStreamUpload(StreamParameterData parameterData,
            string signerPrivateKey)
        {
            return new UploadParameterBuilder(parameterData, signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForFilesAsZipUpload(IEnumerable<string> files,
            string signerPrivateKey)
        {
            return CreateForFilesAsZipUpload(FilesAsZipParameterData.Create(files), signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForFilesAsZipUpload(FilesAsZipParameterData parameterData,
            string signerPrivateKey)
        {
            return new UploadParameterBuilder(parameterData, signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForPathUpload(string path, string signerPrivateKey)
        {
            return CreateForPathUpload(PathParameterData.Create(path), signerPrivateKey);
        }

        public static UploadParameterBuilder CreateForPathUpload(PathParameterData parameterData,
            string signerPrivateKey)
        {
            return new UploadParameterBuilder(parameterData, signerPrivateKey);
        }
    }
}