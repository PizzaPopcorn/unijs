using System;
using UnityEngine;
using UniJS.Payloads;

namespace UniJS
{
    [System.Serializable]
    public class JSGameObjectData
    {
        public string name;
        public string trackingId;
        public bool active;
        
        public TransformPayload transform;

        public bool hasRigidbody;
        public bool hasAnimator;

        public JSGameObjectData(GameObject go)
        {
            if (go == null) return;
            
            name = go.name;
            
            // Handle tracking for lifecycle synchronization
            if (go.TryGetComponent<JSKeyGameObject>(out var keyGo))
            {
                trackingId = keyGo.JSKey;
                if (string.IsNullOrEmpty(trackingId)) trackingId = go.name;
            }
            else
            {
                // If it doesn't have a JSKey, we ensure it has a LifecycleTracker
                if (!go.TryGetComponent<JSLifecycleTracker>(out var tracker))
                {
                    tracker = go.AddComponent<JSLifecycleTracker>();
                    // Generate a unique ID for this instance if it doesn't have a JSKey
                    var id = Guid.NewGuid().ToString();
                    tracker.Initialize(id);
                    trackingId = id;
                    // Register it in JSInstance so JS can find it by this ID if needed
                    JSInstance.RegisterKeyGameObject(id, go);
                }
                else
                {
                    trackingId = tracker.TrackingId;
                }
            }

            active = go.activeSelf;
            hasRigidbody = go.GetComponent<Rigidbody>() != null;
            hasAnimator = go.GetComponent<Animator>() != null;
            transform = new TransformPayload
            {
                position = new Vector3Payload
                    { x = go.transform.position.x, y = go.transform.position.y, z = go.transform.position.z },
                rotation = new QuaternionPayload
                {
                    x = go.transform.rotation.x, y = go.transform.rotation.y, z = go.transform.rotation.z,
                    w = go.transform.rotation.w
                },
                scale = new Vector3Payload
                    { x = go.transform.localScale.x, y = go.transform.localScale.y, z = go.transform.localScale.z }
            };
        }
    }
}