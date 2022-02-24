using UnityEngine;

namespace JGM.Patterns.ObjectPool
{
    public class ClientObjectPool : MonoBehaviour
    {
        private DroneObjectPool _pool;

        private void Awake()
        {
            _pool = gameObject.AddComponent<DroneObjectPool>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn Drones"))
            {
                _pool.SpawnPooledItemInRandomPos();
            }
        }
    }
}