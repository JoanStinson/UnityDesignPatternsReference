using UnityEngine;

namespace JGM.Patterns.Visitor
{
    public class BikeShieldVisitable : MonoBehaviour, IBikeElementVisitable
    {
        public float HealtPercentage = 50f;

        public float Damage(float damage)
        {
            return HealtPercentage -= damage;
        }

        public void Accept(IBikeElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(125, 0, 200, 20), "Shield Health: " + HealtPercentage);
        }
    }
}