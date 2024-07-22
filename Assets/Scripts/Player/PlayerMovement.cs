using System;
using System.Linq;
using Graphics;
using Logic;
using Nodes;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private float speed;
        [SerializeField] private NodeEvents nodeEvents;
        
        private Vector3 _lastPoint;
        private Vector3 _nextPoint;
        private float _progressToNext;
        private bool _isNextSet;
        private Graph _graph;

        #endregion
        
        #region Unity Methods
        
        /// <summary>
        /// Sets the start point
        /// </summary>
        public void SetStartPoint(Vector3 coords)
        {
            _lastPoint = coords;
        }
        
        /// <summary>
        /// Sets the next point (first one that has a path from the last)
        /// </summary>
        public void GetNextPoint()
        {
            _nextPoint = _graph.PathsActivity
                .First(pathActivity =>
                    pathActivity.Key.Item1 == _lastPoint && pathActivity.Value)
                .Key.Item2;

            _progressToNext = 0;
            _isNextSet = true;
        }
        
        /// <summary>
        /// Moves the player along the path if user is pressing Space
        /// </summary>
        private void Move()
        {
            if (!Input.GetKey(KeyCode.Space))
                return;
            
            // we don't move in a direction, we calculate what the next position will be
            // it's more "expensive" but insignificant in a small game such as ours
            // makes programming it way easier, though
            transform.position = Vector3.Lerp(_lastPoint, _nextPoint, _progressToNext);
            _progressToNext += 0.001f * speed;
            
            // If we've reached the next point, we set a new one
            if (_progressToNext >= 1)
                UpdatePoint();
        }
        
        /// <summary>
        /// Called when the player has reached a new point
        /// </summary>
        private void UpdatePoint()
        {
            // Checks if the next node is a finish, dead end, etc/
            if (nodeEvents.IsNodeFinal(_graph.PointTypes[_nextPoint]))
            {
                enabled = false;
                return;
            }

            _lastPoint = _nextPoint;
            GetNextPoint();
        }

        #endregion
        
        #region MB Callbacks

        private void Update()
        {
            if (_graph == null) 
                _graph = GraphManager.Instance.Graph;
            if (!_isNextSet)
                GetNextPoint();
            
            Move();
        }
        #endregion
    }
}