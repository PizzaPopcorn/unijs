# JSEventHub and Event System

The `JSEventHub` is a static registry that maps event names to specific C# logic. It allows the JavaScript side to execute methods on Unity GameObjects by sending an event name and a JSON payload.

> [!TIP]
> **Recommendation:** Use `JSEventHub` for actions that target specific GameObjects (e.g., changing a transform, playing an animation). For global events, or events that don't depend on a specific `GameObject` instance, use [JSInstance Events](./api-jsinstance.md#event-subscriptions-unity-to-js).

## JSEventHub

- **`InvokeJSEvent(this GameObject target, string eventName, string payload)`**
  This is an extension method for `GameObject`. It looks up the event by name in the internal registry and executes it on the provided `target`.

## Defining Custom Events

To create a new event that can be called from JS, you must:

1. Create a class that implements `IJSEvent<GameObject>`.
2. Decorate the class with the `[ExposeJSEvent("EventName")]` attribute.
3. (Optional but recommended) Inherit from `JSEvent<TTarget, TPayload, TResult>` or `JSEventVoid<TTarget, TPayload>` for automatic payload parsing.

### Example: Custom Event

```csharp
using UniJS;
using UniJS.Events;
using UnityEngine;

[ExposeJSEvent("SetPlayerHealth")]
public class SetPlayerHealthEvent : JSEventVoid<GameObject, HealthPayload>
{
    protected override void Invoke(GameObject target, HealthPayload payload)
    {
        var healthComponent = target.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.CurrentHealth = payload.amount;
        }
    }
}

[System.Serializable]
public class HealthPayload
{
    public float amount;
}
```

## Built-in Events

UniJS comes with several built-in events categorized by functionality:

- **Transform**: `SetPosition`, `SetRotation`, `SetScale`, `Translate`, `Rotate`.
- **GameObject**: `SetActive`, `SetName`, `Destroy`.
- **Physics**: `AddForce`, `SetVelocity`, `SetAngularVelocity`.
- **Animator**: `SetTrigger`, `SetFloat`, `SetBool`, `SetInteger`.
- **Text**: `SetText`.

These events are automatically registered by `JSEventHub` at startup using reflection to find all types with the `[ExposeJSEvent]` attribute.
