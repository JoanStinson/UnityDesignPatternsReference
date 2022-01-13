using UnityEngine;

namespace JGM.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 10f;

        private void Start()
        {

        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(horizontal, vertical).normalized * _moveSpeed * Time.deltaTime;
        }
    }
}