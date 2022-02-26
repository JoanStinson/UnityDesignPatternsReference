using UnityEngine;

namespace JGM.Patterns.Adapter
{
    public class ClientAdapter : MonoBehaviour
    {
        [SerializeField]
        private InventoryItem _item;
        private InventorySystem _inventorySystem;
        private IInventorySystem _inventorySystemAdapter;

        private void Awake()
        {
            _inventorySystem = new InventorySystem();
            _inventorySystemAdapter = new InventorySystemAdapter();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Add item (no adapter)"))
            {
                _inventorySystem.AddItem(_item);
            }

            if (GUILayout.Button("Add item (with adapter)"))
            {
                _inventorySystemAdapter.AddItem(_item, SaveLocation.Both);
            }
        }
    }
}