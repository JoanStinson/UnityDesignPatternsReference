using UnityEngine;
using System.Collections;

namespace JGM.Patterns.Observer
{
    public abstract class Subject : MonoBehaviour
    {
        private readonly ArrayList _observers = new ArrayList();

        protected void Attach(Observer observer)
        {
            if (observer != null)
            {
                _observers.Add(observer);
            }
            else
            {
                Debug.LogWarning("Attached observer cannot be null!");
            }
        }

        protected void Detach(Observer observer)
        {
            if (observer != null)
            {
                _observers.Remove(observer);
            }
            else
            {
                Debug.LogWarning("Detached observer cannot be null!");
            }
        }

        protected void NotifyObservers()
        {
            foreach (Observer observer in _observers)
            {
                observer?.Notify(this);
            }
        }
    }
}