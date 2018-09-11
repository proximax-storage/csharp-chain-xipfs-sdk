using io.nem2.sdk.Model.Blockchain;
using IO.Proximax.SDK.Exceptions;

namespace IO.Proximax.SDK.Models
{
    public enum BlockchainNetworkType
    {
        Main_Net = NetworkType.Types.MAIN_NET,
        Test_Net = NetworkType.Types.TEST_NET,
        Mijin = NetworkType.Types.MIJIN,
        Mijin_Test = NetworkType.Types.MIJIN_TEST
    }
    
    static class BlockchainNetworkTypeMethods
    {
        public static NetworkType.Types GetNetworkType(this BlockchainNetworkType type)
        {
            switch (type)
            {
                case BlockchainNetworkType.Main_Net: return NetworkType.Types.MAIN_NET;
                case BlockchainNetworkType.Test_Net: return NetworkType.Types.TEST_NET;
                case BlockchainNetworkType.Mijin: return NetworkType.Types.MIJIN;
                case BlockchainNetworkType.Mijin_Test: return NetworkType.Types.MIJIN_TEST;
                default:
                    throw new NetworkTypeInvalidException("Invalid network type");
            }
            
        }
    }
    
}
