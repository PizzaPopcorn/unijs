# JSInstance

`JSInstance` is the central component of UniJS on the Unity side. It's a singleton that manages the communication bridge, handles event registration from JS, and provides logging utilities that output directly to the browser console.

## Static Methods

### Event Subscriptions (Unity to JS)

These methods allow Unity to listen for events sent from the JavaScript side.

- **`OnEvent<TPayload>(string eventName, Action<TPayload> callback)`**
  Registers a callback for a specific event name. Returns a `Guid` used for unsubscription.
  
- **`OnEvent<TPayload, TResult>(string eventName, Func<TPayload, TResult> callback)`**
  Registers a callback that returns a result back to JS.

- **`OnEventAsync<TPayload>(string eventName, Func<TPayload, Task> callback)`**
  Registers an asynchronous callback.

- **`OnEventAsync<TPayload, TResult>(string eventName, Func<TPayload, Task<TResult>> callback)`**
  Registers an asynchronous callback that returns a result.

- **`OffEvent(string eventName, Guid callbackId)`**
  Unregisters a previously registered callback.

### Invoking Events (Unity to JS)

- **`InvokeEvent(string eventName, object payload)`**
  Sends an event with a payload to the JS side.
  
- **`InvokeEvent(string eventName)`**
  Sends an event without a payload.

### Key GameObject Registration

While the preferred way to register Key GameObjects is using the `JSKeyGameObject` component via the Inspector, `JSInstance` provides these static methods for manual registration:

- **`RegisterKeyGameObject(string key, GameObject go)`**
  Registers a `GameObject` with a unique string key, making it accessible from the JS library.
  
- **`UnregisterKeyGameObject(string key)`**
  Removes a `GameObject` registration.

### Logging

For cleaner debugging, use these methods to log messages to the browser console. Unity's Debug.Log works too, but it's not as convenient as it prints several internal stacktraces.

- **`Log(string message)`** / **`Log(string verbosity, string message)`**
- **`LogWarning(string message)`**
- **`LogError(string message)`**
- **`LogInternal(string message)`** (Used for UniJS internal debugging)

## Lifecycle

The `JSInstance` is a `MonoBehaviour` that uses `DontDestroyOnLoad`, ensuring it persists across scene loads. It initializes `JSEventHub` and registers several built-in system events during `Awake`.
