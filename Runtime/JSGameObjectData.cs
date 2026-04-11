using UnityEngine;
using UniJS.Payloads;

namespace UniJS
{
    [System.Serializable]
    public class JSGameObjectData
    {
        public string name;

        public bool active;
        
        public TransformPayload transform;

        public bool hasRigidbody;
        public bool hasAnimator;

        public JSGameObjectData(GameObject go)
        {
            name = go.name;
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