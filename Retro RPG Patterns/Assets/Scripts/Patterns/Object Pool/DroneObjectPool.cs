using UnityEngine;
using UnityEngine.Pool;

namespace JGM.Patterns.ObjectPool
{
    public class DroneObjectPool : MonoBehaviour
    {
        [SerializeField]
        private int _poolSize = 10;

        public IObjectPool<Drone> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new ObjectPool<Drone>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, _poolSize, _poolSize);
                }
                return _pool;
            }
        }

        private IObjectPool<Drone> _pool;

        private Drone CreatedPooledItem()
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = "Drone";
            Drone drone = go.AddComponent<Drone>();
            drone.Pool = Pool;
            return drone;
        }

        private void OnReturnedToPool(Drone drone)
        {
            drone.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Drone drone)
        {
            drone.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(Drone drone)
        {
            Destroy(drone.gameObject);
        }

        public void Spawn()
        {
            var amount = Random.Range(1, 10);

            for (int i = 0; i < amount; ++i)
            {
                var drone = Pool.Get();
                drone.transform.position = Random.insideUnitSphere * 10;
            }
        }
    }
}