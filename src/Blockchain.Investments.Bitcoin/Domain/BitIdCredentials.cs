using System;
using Blockchain.Investments.Core.Infrastructure;
using NBitcoin;

namespace Blockchain.Investments.Bitcoin.Domain
{
    public class BitIdCredentials
    {
        public string Address { get; set; }
        public string Uri { get; set; }
        public string Signature { get; set; }
        public BitIdCredentials() {}
        public BitIdCredentials(string address, string uri, string signature) 
        {
            Address = address;
            Uri = uri;
            Signature = signature;
        }
        public BitIdResponse VerifyMessage() 
        {
            BitIdResponse response = new BitIdResponse();
            response.Success = false;

            if (!string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Uri) && !string.IsNullOrEmpty(Signature)) 
            {
                string nonce = Uri.Split('=')[1].Replace("&u", "");
                string hexTicks = nonce.Substring(32, 15);
                long ticks = Convert.ToInt64(hexTicks, 16);
                double spanSeconds = DateTime.UtcNow.Subtract(new DateTime(ticks)).TotalSeconds;
                if (spanSeconds > Constants.SpanTimeInSeconds) 
                {
                    response.Message = "Request timeout!";
                    return response;
                }
                
                BitcoinPubKeyAddress testAddress = new BitcoinPubKeyAddress(Address);
                response.Success = testAddress.VerifyMessage(Uri, Signature);
                if (response.Success) 
                {
                    response.Address = Address;
                    response.Signature = Signature;
                }
                else
                {
                    response.Message = "Invalid signature!";
                }
            }
            else 
            {
                response.Message = "Missing parameters!";
            }

            return response;
        }
    }
}