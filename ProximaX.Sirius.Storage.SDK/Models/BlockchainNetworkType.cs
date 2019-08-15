using ProximaX.Sirius.Storage.SDK.Exceptions;
using ProximaX.Sirius.Chain.Sdk.Model.Blockchain;

namespace ProximaX.Sirius.Storage.SDK.Models
{
    public enum BlockchainNetworkType
    {
        MainNet = NetworkType.MAIN_NET,
        TestNet = NetworkType.TEST_NET,
        Private = NetworkType.PRIVATE,
        PrivateTest = NetworkType.PRIVATE_TEST,
        Mijin = NetworkType.MIJIN,
        MijinTest = NetworkType.MIJIN_TEST
    }

    static class BlockchainNetworkTypeMethods
    {
        public static NetworkType GetNetworkType(this BlockchainNetworkType type)
        {
            switch (type)
            {
                case BlockchainNetworkType.MainNet: return NetworkType.MAIN_NET;
                case BlockchainNetworkType.TestNet: return NetworkType.TEST_NET;
                case BlockchainNetworkType.Private: return NetworkType.PRIVATE;
                case BlockchainNetworkType.PrivateTest: return NetworkType.PRIVATE_TEST;
                case BlockchainNetworkType.Mijin: return NetworkType.MIJIN;
                case BlockchainNetworkType.MijinTest: return NetworkType.MIJIN_TEST;
                default:
                    throw new NetworkTypeInvalidException("Invalid network type");
            }
        }
    }

    public static class BlockchainNetworkTypeConverter
    {
        public static BlockchainNetworkType GetNetworkType(string network)
        {
            switch (network)
            {
                case "PUBLIC": return BlockchainNetworkType.MainNet;
                case "PUBLICTEST": return BlockchainNetworkType.TestNet;
                case "TEST_NET": return BlockchainNetworkType.TestNet;
                case "PRIVATE": return BlockchainNetworkType.Private;
                case "PRIVATE_TEST": return BlockchainNetworkType.PrivateTest;
                case "MIJIN": return BlockchainNetworkType.Mijin;
                case "MIJIN_TEST": return BlockchainNetworkType.MijinTest;
                default:
                    throw new NetworkTypeInvalidException("Invalid network");
            }
        }
    }
}