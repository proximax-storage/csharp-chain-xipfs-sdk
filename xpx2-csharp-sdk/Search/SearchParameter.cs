using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.Search
{
    public class SearchParameter
    {
        public TransactionFilter TransactionFilter { get; }
        public int ResultSize { get; }
        public string AccountAddress { get; }
        public string AccountPublicKey { get; }
        public string AccountPrivateKey { get; }
        public string NameFilter { get; }
        public string DescriptionFilter { get; }
        public string MetadataKeyFilter { get; }
        public string MetadataValueFilter { get; }
        public string FromTransactionId { get; }

        public SearchParameter(TransactionFilter transactionFilter, int resultSize, string accountAddress, string accountPublicKey, string accountPrivateKey, string nameFilter, string descriptionFilter, string metadataKeyFilter, string metadataValueFilter, string fromTransactionId)
        {
            TransactionFilter = transactionFilter;
            ResultSize = resultSize;
            AccountAddress = accountAddress;
            AccountPublicKey = accountPublicKey;
            AccountPrivateKey = accountPrivateKey;
            NameFilter = nameFilter;
            DescriptionFilter = descriptionFilter;
            MetadataKeyFilter = metadataKeyFilter;
            MetadataValueFilter = metadataValueFilter;
            FromTransactionId = fromTransactionId;
        }

        public static SearchParameterBuilder CreateForAddress(string accountAddress) {
            return SearchParameterBuilder.CreateForAddress(accountAddress);
        }

        public static SearchParameterBuilder CreateForPublicKey(string accountPublicKey) {
            return SearchParameterBuilder.CreateForPublicKey(accountPublicKey);
        }
        
        public static SearchParameterBuilder CreateForPrivateKey(string accountPrivateKey) {
            return SearchParameterBuilder.CreateForPrivateKey(accountPrivateKey);
        }
    }
}
