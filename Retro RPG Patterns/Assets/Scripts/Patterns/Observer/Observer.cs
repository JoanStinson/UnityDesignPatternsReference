using UnityEngine;

namespace JGM.Patterns.Observer
{
    public abstract class Observer : MonoBehaviour
    {
        public abstract void Notify(Subject subject);
    }
}