using System;
using UnityEngine;
using UniJS.InstanceTools;

namespace UniJS
{
    /// <summary>
    /// This component is automatically added to GameObjects when they are proxied to JavaScript
    /// if they don't already have a JSKeyGameObject component. 
    /// Its main purpose is to notify JS when the Unity GameObject is destroyed.
    /// </summary>
    [AddComponentMenu("")] // Hide from Add Component menu
    public class JSLifecycleTracker : MonoBehaviour
    {
        [SerializeField] private string _trackingId;
        public string TrackingId => _trackingId;
        private bool _isInitialized = false;

        public void Initialize(string trackingId)
        {
            _trackingId = trackingId;
            _isInitialized = true;
            
            // We only care about the 'destroy' event for tracking purposes.
            // Other events like 'awake' or 'start' have usually already happened.
            JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(_trackingId, "awake");
        }

        private void OnEnable()
        {
            if (_isInitialized)
                JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(_trackingId, "enable");
        }

        private void OnDisable()
        {
            if (_isInitialized)
                JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(_trackingId, "disable");
        }

        private void OnDestroy()
        {
            if (_isInitialized)
            {
                JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(_trackingId, "destroy");
                // Cleanup JS-side references
                JSInstance.UnregisterKeyGameObject(_trackingId);
            }
        }

        private void Update()
        {
            // Optional: JS might want to subscribe to updates for specific non-key objects.
            // For now, we only send if requested (could be optimized).
        }
    }
}
