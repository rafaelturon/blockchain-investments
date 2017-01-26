using System;
using NBitcoin;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 1) 
            {
                BitcoinSecret secret = new BitcoinSecret(args[0], Network.Main);
                BitcoinAddress pubAddress = secret.GetAddress();
                Console.WriteLine("Public Address: " + pubAddress); 

                string uri = args[1];
                Console.WriteLine("URI: " + uri);
                
                string signature = secret.PrivateKey.SignMessage(uri);
                Console.WriteLine("Signature: " + signature);
            }
        }
    }
}
