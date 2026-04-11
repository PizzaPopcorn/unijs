using UnityEngine;
using UniJS.Payloads;

namespace UniJS.Events
{
    [ExposeJSEvent("animator.setTrigger")]
    public class Event_AnimatorSetTrigger : JSEventVoid<GameObject, string>
    {
        protected override void Invoke(GameObject target, string trigger)
        {
            target.GetComponent<Animator>().SetTrigger(trigger);
        }
    }

    [ExposeJSEvent("animator.setFloat")]
    public class Event_AnimatorSetFloat : JSEventVoid<GameObject, FloatPayload>
    {
        protected override void Invoke(GameObject target, FloatPayload payload)
        {
            target.GetComponent<Animator>().SetFloat(payload.name, payload.value);
        }
    }

    [ExposeJSEvent("animator.setInteger")]
    public class Event_AnimatorSetInteger : JSEventVoid<GameObject, IntPayload>
    {
        protected override void Invoke(GameObject target, IntPayload payload)
        {
            target.GetComponent<Animator>().SetInteger(payload.name, payload.value);
        }
    }

    [ExposeJSEvent("animator.setBool")]
    public class Event_AnimatorSetBool : JSEventVoid<GameObject, BoolPayload>
    {
        protected override void Invoke(GameObject target, BoolPayload payload)
        {
            target.GetComponent<Animator>().SetBool(payload.name, payload.value);
        }
    }

    [ExposeJSEvent("animator.play")]
    public class Event_AnimatorPlay : JSEventVoid<GameObject, string>
    {
        protected override void Invoke(GameObject target, string stateName)
        {
            target.GetComponent<Animator>().Play(stateName);
        }
    }

    [ExposeJSEvent("animator.setLayerWeight")]
    public class Event_AnimatorSetLayerWeight : JSEventVoid<GameObject, LayerWeightPayload>
    {
        protected override void Invoke(GameObject target, LayerWeightPayload payload)
        {
            target.GetComponent<Animator>().SetLayerWeight(payload.layerIndex, payload.weight);
        }
    }
}