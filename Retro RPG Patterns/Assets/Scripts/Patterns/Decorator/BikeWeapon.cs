using UnityEngine;
using System.Collections;

namespace JGM.Patterns.Decorator
{
    public class BikeWeapon : MonoBehaviour
    {
        public WeaponConfig WeaponConfig;
        public WeaponAttachment MainAttachment;
        public WeaponAttachment SecondaryAttachment;

        private IWeapon _weapon;
        private bool _isFiring;
        private bool _isDecorated;

        private void Awake()
        {
            _weapon = new Weapon(WeaponConfig);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(5, 50, 150, 100), "Range: " + _weapon.Range);
            GUI.Label(new Rect(5, 70, 150, 100), "Strength: " + _weapon.Strength);
            GUI.Label(new Rect(5, 90, 150, 100), "Cooldown: " + _weapon.Cooldown);
            GUI.Label(new Rect(5, 110, 150, 100), "Firing Rate: " + _weapon.Rate);
            GUI.Label(new Rect(5, 130, 150, 100), "Weapon Firing: " + _isFiring);

            if (MainAttachment && _isDecorated)
            {
                GUI.Label(new Rect(5, 150, 150, 100), "Main Attachment: " + MainAttachment.name);
            }

            if (SecondaryAttachment && _isDecorated)
            {
                GUI.Label(new Rect(5, 170, 200, 100), "Secondary Attachment: " + SecondaryAttachment.name);
            }
        }

        public void ToggleFire()
        {
            _isFiring = !_isFiring;

            if (_isFiring)
            {
                StartCoroutine(FireWeapon());
            }
        }

        private IEnumerator FireWeapon()
        {
            float firingRate = 1.0f / _weapon.Rate;

            while (_isFiring)
            {
                yield return new WaitForSeconds(firingRate);
                Debug.Log("fire");
            }
        }

        public void Reset()
        {
            _weapon = new Weapon(WeaponConfig);
            _isDecorated = !_isDecorated;
        }

        public void Decorate()
        {
            if (MainAttachment && !SecondaryAttachment)
            {
                _weapon = new WeaponDecorator(_weapon, MainAttachment);
            }

            if (MainAttachment && SecondaryAttachment)
            {
                _weapon = new WeaponDecorator(new WeaponDecorator(_weapon, MainAttachment), SecondaryAttachment);
            }

            _isDecorated = !_isDecorated;
        }
    }
}