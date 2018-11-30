using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Upload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestSupport.Constants;
using static IntegrationTests.TestDataRepository;

namespace IntegrationTests.StorageConnection
{
	[TestClass]
	public class ConnectionConfigIntegrationTests
	{

		[TestMethod, Timeout(30000)]
		public void ShouldInitializeBlockchainNetworkConnectionWithJustStorageConnection() {
			var connectionConfig = ConnectionConfig.CreateWithStorageConnection(
				new IO.Proximax.SDK.Connections.StorageConnection(
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