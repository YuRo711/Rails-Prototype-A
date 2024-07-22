using Nodes;
using ScriptableObjects;
using UnityEngine;

namespace Logic
{
    public class NodeEvents : ScriptableObject
    {
        [SerializeField] private GameEvent winEvent;
        [SerializeField] private GameEvent loseEvent;

        /// <summary>
        /// Raises an event corresponding to the type of the reached node
        /// and returns whether it's final or not
        /// </summary>
        public bool IsNodeFinal(NodeType nodeType)
        {
            if (nodeType == NodeType.Finish)
            {
                winEvent.Raise();
                return true;
            }
            if (nodeType == NodeType.DeadEnd)
            {
                loseEvent.Raise();
                return true;
            }

            return false;
        }
    }
}