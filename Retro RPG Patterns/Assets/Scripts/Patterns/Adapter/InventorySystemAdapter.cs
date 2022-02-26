using UnityEngine;
using System.Collections.Generic;

namespace JGM.Patterns.Adapter
{
    public class InventorySystemAdapter : InventorySystem, IInventorySystem
    {
        private List<InventoryItem> _cloudInventory;

        public void SyncInventories()
        {
            var _cloudInventory = GetInventory();
            Debug.Log("Synchronizing local drive and cloud inventories");
        }

        public void AddItem(InventoryItem item, SaveLocation location)
        {
            if (location == SaveLocation.Cloud)
            {
                AddItem(item);
            }
            else if (location == SaveLocation.Local)
            {
                Debug.Log("Adding item to local drive");
            }
            else if (location == SaveLocation.Both)
            {
                Debug.Log("Adding item to local drive and on the cloud");
            }
        }

        public void RemoveItem(InventoryItem item, SaveLocation location)
        {
            Debug.Log("Remove item from local/cloud/both");
        }

        public List<InventoryItem> GetInventory(SaveLocation location)
        {
            Debug.Log("Get inventory from local/cloud/both");
            return new List<InventoryItem>();
        }
    }
}