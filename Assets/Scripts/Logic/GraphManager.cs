using System;
using Logic;
using Nodes;
using ScriptableObjects;
using UnityEngine;

namespace Graphics
{
    public class GraphManager : MonoBehaviour
    {
        #region Fields
        
        public Graph Graph = new();
        public static GraphManager Instance;

        [SerializeField] private GameEvent pathsUpdateEvent;

        #endregion

        #region MyRegion

        public void ChangeDirection(JunctionNode junctionNode)
        {
            Graph.ChangeDirection(junctionNode);
            pathsUpdateEvent.Raise();
        }

        public void Reload()
        {
            Graph.Clear();
        }

        #endregion

        #region MB Callbacks

        private void Awake()
        {
            Reload();
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        #endregion
    }
}