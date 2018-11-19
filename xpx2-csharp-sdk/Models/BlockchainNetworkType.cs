using io.nem2.sdk.Model.Blockchain;
using IO.Proximax.SDK.Exceptions;

namespace IO.Proximax.SDK.Models
{
    public enum BlockchainNetworkType
    {
        MainNet = NetworkType.Types.MAIN_NET,
        TestNet = NetworkType.Types.TEST_NET,
        Private = NetworkType.Types.PRIVATE,
        PrivateTest = NetworkType.Types.PRIVATE_TEST,
        Mijin = NetworkType.Types.MIJIN,
        MijinTest = NetworkType.Types.MIJIN_TEST
    }

    static class BlockchainNetworkTypeMethods
    {
        public static NetworkType.Types GetNetworkType(this BlockchainNetworkType type)
        {
            switch (type)
            {
                case BlockchainNetworkType.MainNet: return NetworkType.Types.MAIN_NET;
                case BlockchainNetworkType.TestNet: return NetworkType.Types.TEST_NET;
                case BlockchainNetworkType.Private: return NetworkType.Types.PRIVATE;
                case BlockchainNetworkType.PrivateTest: return NetworkType.Types.PRIVATE_TEST;
                case BlockchainNetworkType.Mijin: return NetworkType.Types.MIJIN;
                case BlockchainNetworkType.MijinTest: return NetworkType.Types.MIJIN_TEST;
                default:
                    throw new NetworkTypeInvalidException("Invalid network type");
            }
        }
    }
}