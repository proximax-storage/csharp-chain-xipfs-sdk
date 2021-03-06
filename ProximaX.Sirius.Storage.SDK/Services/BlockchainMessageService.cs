﻿using System;

using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Exceptions;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Services.Clients.Catapult;
using ProximaX.Sirius.Storage.SDK.Utils;
using ProximaX.Sirius.Chain.Sdk.Model.Accounts;
using ProximaX.Sirius.Chain.Sdk.Model.Blockchain;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions.Messages;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Services
{
    public class BlockchainMessageService
    {
        private NetworkType NetworkType { get; }
        private AccountClient AccountClient { get; }

        public BlockchainMessageService(BlockchainNetworkConnection blockchainNetworkConnection)
        {
            AccountClient = new AccountClient(blockchainNetworkConnection);
            NetworkType = blockchainNetworkConnection.NetworkType;
        }

        internal BlockchainMessageService(NetworkType networkType, AccountClient accountClient)
        {
            NetworkType = networkType;
            AccountClient = accountClient;
        }

        public IMessage CreateMessage(ProximaxMessagePayloadModel messagePayload, string senderPrivateKey,
            string recipientPublicKeyRaw,
            string recipientAddress, bool useBlockchainSecureMessage)
        {
            CheckParameter(messagePayload != null, "messagePayload is required");

            var jsonPayload = messagePayload.ToJson();

            if (useBlockchainSecureMessage)
            {
                var recipientPublicKey =
                    this.GetRecipientPublicKey(senderPrivateKey, recipientPublicKeyRaw, recipientAddress);
                return SecureMessage.Create(jsonPayload, senderPrivateKey, recipientPublicKey);
            }
            else
            {
                return PlainMessage.Create(jsonPayload);
            }
        }

        public string GetMessagePayload(TransferTransaction transferTransaction, string accountPrivateKey)
        {
            CheckParameter(transferTransaction != null, "transferTransaction is required");

            switch (transferTransaction.Message)
            {
                case PlainMessage plainMessage:
                    return plainMessage.GetStringPayload();
                case SecureMessage secureMessage:
                    if (accountPrivateKey == null)
                        throw new MissingPrivateKeyOnDownloadException(
                            "accountPrivateKey is required to download a secure message");
                    var retrieverKeyPair = KeyPair.CreateFromPrivateKey(accountPrivateKey);
                    
                    return secureMessage.DecryptPayload(accountPrivateKey,
                        GetTransactionOtherPartyPublicKey(retrieverKeyPair, transferTransaction));
                default:
                    throw new NotSupportedException(
                        $"Download of message type {transferTransaction.Message} is not supported");
            }
        }

        private string GetRecipientPublicKey(string senderPrivateKey, string recipientPublicKey,
            string recipientAddress)
        {
            if (recipientPublicKey != null)
            {
                // use public key if available
                return recipientPublicKey;
            }
            else if (recipientAddress != null)
            {
                // use public key from address
                var senderPublicKey = KeyPair.CreateFromPrivateKey(senderPrivateKey).PublicKeyString;

                if (IsSenderPrivateKeySameWithRecipientAddress(senderPublicKey, recipientAddress))
                {
                    // sending to self
                    return senderPublicKey;
                }
                else
                {
                    // sending to another
                    return AccountClient.GetPublicKey(recipientAddress);
                }
            }
            else
            {
                // fallback to sender private key as default
                return KeyPair.CreateFromPrivateKey(senderPrivateKey).PublicKeyString;
            }
        }

        private bool IsSenderPrivateKeySameWithRecipientAddress(string signerPublicKey, string recipientAddress)
        {
            var senderAddress = Address.CreateFromPublicKey(signerPublicKey, NetworkType);
            return senderAddress.Plain.Equals(recipientAddress);
        }

        private string GetTransactionOtherPartyPublicKey(KeyPair retrieverKeyPair,
            TransferTransaction transferTransaction)
        {
            var senderAccount = transferTransaction.Signer;
            var recipient = transferTransaction.Recipient.Address;
            var retrieverAddress = Address.CreateFromPublicKey(retrieverKeyPair.PublicKeyString, NetworkType);

            if (retrieverAddress.Plain.Equals(recipient.Plain))
            {
                // retriever is the recipient, use sender public key
                return senderAccount.PublicKey;
            }
            else if (retrieverAddress.Plain.Equals(senderAccount.Address.Plain))
            {
                // retriever is the sender, use recipient public key
                return AccountClient.GetPublicKey(recipient.Plain);
            }
            else
            {
                throw new InvalidPrivateKeyOnDownloadException(
                    "accountPrivateKey cannot be used to read secure transaction message");
            }
        }
    }
}