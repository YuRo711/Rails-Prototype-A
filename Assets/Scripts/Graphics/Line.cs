using System;
using UnityEngine;

namespace Graphics
{
    public class Line : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Vector3 startPoint;
        [SerializeField] private Vector3 endPoint;
        
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
        
        #endregion

        #region Methods
        
        /// <summary>
        /// Connecting two points with a line
        /// </summary>
        public void Connect(Vector3 start, Vector3 end)
        {
            lineRenderer.SetPositions(new[] {start, end});
            
            startPoint = start;
            endPoint = end;
        }
        
        /// <summary>
        /// Sets the color according to local activity
        /// </summary>
        public void SetPathActive(bool isActive)
        {
            lineRenderer.startColor = isActive ? activeColor : inactiveColor;
            lineRenderer.endColor = isActive ? activeColor : inactiveColor;
        }
        
        /// <summary>
        /// Sets the activity according to graph
        /// </summary>
        public void UpdateActivity()
        {
            var isActive = GraphManager.Instance.Graph.PathsActivity[Tuple.Create(startPoint, endPoint)];
            SetPathActive(isActive);
        }

        #endregion

        #region MB Callbacks

        private void Awake()
        {
            // Connects line renderer (prevents human error on creation)
            if (lineRenderer is null)
                lineRenderer = GetComponent<LineRenderer>();
            
            // If either of the points isn't default (0, 0, 0), connect them
            if (startPoint != default || endPoint != default)
                Connect(startPoint, endPoint);
            
            SetPathActive(false);
        }

        #endregion
    }
}