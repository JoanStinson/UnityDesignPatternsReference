using UnityEngine;
using System.Collections.Generic;

namespace JGM.Patterns.Visitor
{
    public class BikeController : MonoBehaviour, IBikeElementVisitable
    {
        private List<IBikeElementVisitable> _bikeElements = new List<IBikeElementVisitable>();

        private void Awake()
        {
            _bikeElements.Add(gameObject.AddComponent<BikeShieldVisitable>());
            _bikeElements.Add(gameObject.AddComponent<BikeWeaponVisitable>());
            _bikeElements.Add(gameObject.AddComponent<BikeEngineVisitable>());
        }

        public void Accept(IBikeElementVisitor visitor)
        {
            foreach (IBikeElementVisitable element in _bikeElements)
            {
                element.Accept(visitor);
            }
        }
    }
}