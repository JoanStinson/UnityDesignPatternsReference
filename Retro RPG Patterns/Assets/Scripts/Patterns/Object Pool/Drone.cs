using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

namespace JGM.Patterns.ObjectPool
{
    public class Drone : MonoBehaviour
    {
        public IObjectPool<Drone> Pool { get; set; }
        public float CurrentHealth;

        [SerializeField] private float _maxHealth = 100.0f;
        [SerializeField] private float _timeToSelfDestruct = 3.0f;

        private void Awake()
        {
            CurrentHealth = _maxHealth;
        }

        private void OnEnable()
        {
            AttackPlayer();
            StartCoroutine(SelfDestruct());
        }

        public void AttackPlayer()
        {
            Debug.Log("Attack player!");
        }

        private IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(_timeToSelfDestruct);
            TakeDamage(_maxHealth);
        }

        public void TakeDamage(float amount)
        {
            CurrentHealth -= amount;

            if (CurrentHealth <= 0.0f)
            {
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            Pool.Release(this);
        }

        private void OnDisable()
        {
            ResetDrone();
        }

        private void ResetDrone()
        {
            CurrentHealth = _maxHealth;
        }
    }
}