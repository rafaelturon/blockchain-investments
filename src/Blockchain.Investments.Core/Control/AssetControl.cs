using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Control
{
    public class AssetControl
    {
        public List<Asset> List() 
        {
            List<Asset> items = new List<Asset>();
            Asset asset = new Asset();
            asset.Name = "Gold";

            items.Add(asset);

            return items;
        }
    }
}