using UnityEngine;

namespace JGM.Patterns.Visitor
{
    [CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
    public class PowerUpVisitor : ScriptableObject, IBikeElementVisitor
    {
        public string PowerupName;
        public GameObject PowerupPrefab;
        public string PowerupDescription;

        [Tooltip("Fully heal shield")]
        public bool HealShield;

        [Range(0f, 50f)]
        [Tooltip("Boost turbo settings up to increments of 50/mph")]
        public float TurboBoost;

        [Range(0f, 25)]
        [Tooltip("Boost weapon range in increments of up to 25 units")]
        public int WeaponRange;

        [Range(0.0f, 50f)]
        [Tooltip("Boost weapon strength in increments of up to 50%")]
        public float WeaponStrength;

        public void Visit(BikeShieldVisitable bikeShield)
        {
            if (HealShield)
            {
                bikeShield.HealtPercentage = 100f;
            }
        }

        public void Visit(BikeWeaponVisitable bikeWeapon)
        {
            int range = bikeWeapon.Range += WeaponRange;
            bikeWeapon.Range = (range >= bikeWeapon.MaxRange) ? bikeWeapon.MaxRange : range;

            float strength = bikeWeapon.Strength += Mathf.Round(bikeWeapon.Strength * WeaponStrength / 100);
            bikeWeapon.Strength = (strength >= bikeWeapon.MaxStrength) ? bikeWeapon.MaxStrength : strength;
        }

        public void Visit(BikeEngineVisitable bikeEngine)
        {
            float boost = bikeEngine.TurboBoostInMph += TurboBoost;

            if (boost < 0.0f)
            {
                bikeEngine.TurboBoostInMph = 0.0f;
            }
            else if (boost >= bikeEngine.MaxTurboBoost)
            {
                bikeEngine.TurboBoostInMph = bikeEngine.MaxTurboBoost;
            }
        }
    }
}