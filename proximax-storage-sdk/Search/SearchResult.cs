using System.Collections.Generic;

namespace Proximax.Storage.SDK.Search
{
    public class SearchResult
    {
        public List<SearchResultItem> Results { get; }
        public string FromTransactionId { get; }
        public string ToTransactionId { get; }

        public SearchResult(List<SearchResultItem> results, string fromTransactionId, string transactionId)
        {
            Results = results;
            FromTransactionId = fromTransactionId;
            ToTransactionId = transactionId;
        }
    }
}