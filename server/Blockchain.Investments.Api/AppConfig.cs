namespace Blockchain.Investments.Api
{
    public class AppConfig
    {
        public AppConfig()
        {
            // Set default value.
            MONGOLAB_URI = "mongodb://localhost:27017";
        }
        public string MONGOLAB_URI { get; set; }
    }
}