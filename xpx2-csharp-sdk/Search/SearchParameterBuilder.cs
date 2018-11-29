using IO.Proximax.SDK.Models;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Search
{
    public class SearchParameterBuilder
    {
        
        private TransactionFilter? TransactionFilter { get; set; }
        private int? ResultSize { get; set; }
        private string AccountAddress { get; set; }
        private string AccountPublicKey { get; set; }
        private string AccountPrivateKey { get; set; }
        private string NameFilter { get; set; }
        private string DescriptionFilter { get; set; }
        private string MetadataKeyFilter { get; set; }
        private string MetadataValueFilter { get; set; }
        private string FromTransactionId { get; set; }

        private SearchParameterBuilder() {}
        
        public static SearchParameterBuilder CreateForAddress(string accountAddress)
        {
            return new SearchParameterBuilder {AccountAddress = accountAddress};
        }
        
        public static SearchParameterBuilder CreateForPublicKey(string accountPublicKey)
        {
            return new SearchParameterBuilder {AccountPublicKey = accountPublicKey};
        }
        
        public static SearchParameterBuilder CreateForPrivateKey(string accountPrivateKey)
        {
            return new SearchParameterBuilder {AccountPrivateKey = accountPrivateKey};
        }
        
        public SearchParameterBuilder WithTransactionFilter(TransactionFilter? transactionFilter)
        {
            TransactionFilter = transactionFilter;
            return this;
        }

        public SearchParameterBuilder WithResultSize(int? resultSize)
        {
            CheckParameter(resultSize == null || (resultSize >= 1 && resultSize <= 20),
                "result size should be between 1 and 20");
            
            ResultSize = resultSize;
            return this;
        }

        public SearchParameterBuilder WithNameFilter(string nameFilter)
        {
            NameFilter = nameFilter;
            return this;
        }

        public SearchParameterBuilder WithDescriptionFilter(string descriptionFilter)
        {
            DescriptionFilter = descriptionFilter;
            return this;
        }

        public SearchParameterBuilder WithMetadataKeyFilter(string metadataKeyFilter)
        {
            MetadataKeyFilter = metadataKeyFilter;
            return this;
        }

        public SearchParameterBuilder WithMetadataValueFilter(string metadataValueFilter)
        {
            MetadataValueFilter = metadataValueFilter;
            return this;
        }

        public SearchParameterBuilder WithFromTransactionId(string fromTransactionId)
        {
            FromTransactionId = fromTransactionId;
            return this;
        }

        public SearchParameter Build()
        {
            return new SearchParameter(
                TransactionFilter ?? Models.TransactionFilter.Outgoing,
                ResultSize ?? 10,
                AccountAddress,
                AccountPublicKey,
                AccountPrivateKey,
                NameFilter,
                DescriptionFilter,
                MetadataKeyFilter,
                MetadataValueFilter,
                FromTransactionId
            );
        }
    }
}
