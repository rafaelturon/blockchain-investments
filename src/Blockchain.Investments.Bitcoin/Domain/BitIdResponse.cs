namespace Blockchain.Investments.Bitcoin.Domain
{
    public class BitIdResponse 
    {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string Address { get; set; }
            public string Signature { get; set; }
    }
}