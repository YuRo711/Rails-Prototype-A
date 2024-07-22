using System;
using Logic;
using UnityEngine;

namespace Graphics
{
    public class GraphManager : MonoBehaviour
    {
        #region Fields
        
        public readonly Graph Graph = new();

        public static GraphManager Instance;

        #endregion

        #region MB Callbacks

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        #endregion
    }
}