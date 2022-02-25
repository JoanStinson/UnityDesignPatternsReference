using UnityEngine;

namespace JGM.Patterns.Visitor
{
    public class BikeWeaponVisitable : MonoBehaviour, IBikeElementVisitable
    {
        [Header("Range")]
        public int Range = 5;
        public int MaxRange = 25;

        [Header("Strength")]
        public float Strength = 25f;
        public float MaxStrength = 50f;

        public void Fire()
        {
            Debug.Log("Weapon fired!");
        }

        public void Accept(IBikeElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(125, 40, 200, 20), "Weapon Range: " + Range);
            GUI.Label(new Rect(125, 60, 200, 20), "Weapon Strength: " + Strength);
        }
    }
}