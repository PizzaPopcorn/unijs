using UniJS.Payloads;
using UnityEngine;

namespace UniJS.Events
{
    [ExposeJSEvent("transform.getPosition")]
    public class Event_TransformGetPosition : JSEvent<GameObject, string, Vector3Payload>
    {
        protected override Vector3Payload Invoke(GameObject target, string _)
        {
            return new Vector3Payload(target.transform.position);
        }
    }
    
    [ExposeJSEvent("transform.setPosition")]
    public class Event_TransformSetPosition : JSEventVoid<GameObject, Vector3Payload>
    {
        protected override void Invoke(GameObject target, Vector3Payload position)
        {
            target.transform.position = new Vector3(position.x, position.y, position.z);
        }
    }

    [ExposeJSEvent("transform.getRotation")]
    public class Event_TransformGetRotation : JSEvent<GameObject, string, QuaternionPayload>
    {
        protected override QuaternionPayload Invoke(GameObject target, string _)
        {
            return new QuaternionPayload(target.transform.rotation);
        }
    }
    
    [ExposeJSEvent("transform.setRotation")]
    public class Event_TransformSetRotation : JSEventVoid<GameObject, QuaternionPayload>
    {
        protected override void Invoke(GameObject target, QuaternionPayload rotation)
        {
            target.transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
        }
    }

    [ExposeJSEvent("transform.getEulerAngles")]
    public class Event_TransformGetEulerAngles : JSEvent<GameObject, string, Vector3Payload>
    {
        protected override Vector3Payload Invoke(GameObject target, string _)
        {
            return new Vector3Payload(target.transform.eulerAngles);
        }
    }

    [ExposeJSEvent("transform.getLocalPosition")]
    public class Event_TransformGetLocalPosition : JSEvent<GameObject, string, Vector3Payload>
    {
        protected override Vector3Payload Invoke(GameObject target, string _)
        {
            return new Vector3Payload(target.transform.localPosition);
        }
    }

    [ExposeJSEvent("transform.setLocalPosition")]
    public class Event_TransformSetLocalPosition : JSEventVoid<GameObject, Vector3Payload>
    {
        protected override void Invoke(GameObject target, Vector3Payload position)
        {
            target.transform.localPosition = new Vector3(position.x, position.y, position.z);
        }
    }

    [ExposeJSEvent("transform.getLocalScale")]
    public class Event_TransformGetLocalScale : JSEvent<GameObject, string, Vector3Payload>
    {
        protected override Vector3Payload Invoke(GameObject target, string _)
        {
            return new Vector3Payload(target.transform.localScale);
        }
    }

    [ExposeJSEvent("transform.setLocalScale")]
    public class Event_TransformSetLocalScale : JSEventVoid<GameObject, Vector3Payload>
    {
        protected override void Invoke(GameObject target, Vector3Payload scale)
        {
            target.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }

    [ExposeJSEvent("transform.getLossyScale")]
    public class Event_TransformGetLossyScale : JSEvent<GameObject, string, Vector3Payload>
    {
        protected override Vector3Payload Invoke(GameObject target, string _)
        {
            return new Vector3Payload(target.transform.lossyScale);
        }
    }

    [ExposeJSEvent("transform.translate")]
    public class Event_TransformTranslate : JSEvent<GameObject, Vector3Payload, Vector3Payload>
    {
        protected override Vector3Payload Invoke(GameObject target, Vector3Payload vector)
        {
            target.transform.Translate(vector.x, vector.y, vector.z);
            var newPos = target.transform.position;
            return new Vector3Payload(newPos);
        }
    }

    [ExposeJSEvent("transform.rotate")]
    public class Event_TransformRotate : JSEvent<GameObject, Vector3Payload, Vector3Payload>
    {
        protected override Vector3Payload Invoke(GameObject target, Vector3Payload euler)
        {
            target.transform.Rotate(euler.x, euler.y, euler.z);
            var newRot = target.transform.eulerAngles;
            return new Vector3Payload(newRot);
        }
    }
}