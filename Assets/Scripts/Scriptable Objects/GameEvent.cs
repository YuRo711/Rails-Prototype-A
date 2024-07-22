using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game Event")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField] private List<GameEventListener> listeners = new();

        #region Public Methods
        
        public void Raise()
        {
            for (int i = listeners.Count -1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }
        
        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }
        
        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
        #endregion
    }
}