using Proximax.Storage.SDK.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.StorageConnection
{
    [TestClass]
    public class ConnectionConfigIntegrationTests
    {
        [TestMethod, Timeout(30000)]
        public void ShouldInitializeBlockchainNetworkConnectionWithJustStorageConnection()
        {
            var connectionConfig = ConnectionConfig.CreateWithStorageConnection(
                new Proximax.Storage.SDK.Connections.StorageConnection(
                    StorageNodeApiHost,
                    StorageNodeApiPort,
                    StorageNodeApiProtocol,
                    StorageNodeApiBearer,
                    StorageNodeApiNemAddress));

            Assert.IsNotNull(connectionConfig);
            Assert.IsNotNull(connectionConfig.BlockchainNetworkConnection);
            Assert.IsNotNull(connectionConfig.BlockchainNetworkConnection.HttpProtocol);
            Assert.IsNotNull(connectionConfig.BlockchainNetworkConnection.ApiPort);
            Assert.IsNotNull(connectionConfig.BlockchainNetworkConnection.ApiHost);
            Assert.IsNotNull(connectionConfig.BlockchainNetworkConnection.NetworkType);
            Assert.IsNotNull(connectionConfig.BlockchainNetworkConnection.RestApiUrl);
        }
    }
}