# Key GameObjects

Key GameObjects are Unity `GameObject` instances that are explicitly registered to be accessible from the JavaScript side. This avoids the need for JS to search for objects in the scene by name or tag, which can be inefficient and error-prone.

## Registration

To register a GameObject, attach the `JSKeyGameObject` component to it and set the **JS Key** in the Inspector.

- **JS Key**: The unique identifier that will be used in JavaScript to access this object. If left empty, it defaults to the GameObject's name.
- **Start Enabled**: If unchecked, the GameObject will disable itself after registration (useful for objects that should be managed by JS but start inactive). IMPORTANT: This component does not work on disabled GameObjects, that's why this flag exists.

## How it Works

When a `JSKeyGameObject` component awakes:
1. It registers itself to the `JSInstance` using its key.
2. Its metadata (instance ID, name, tag, etc.) is sent to the JS side.
3. It automatically handles common events like collisions, triggers, and lifecycle methods (Update, OnEnable, etc.), forwarding them to JS.
4. On the JS side, a corresponding wrapper object is created, allowing JS developers to interact with it using the assigned key.

## Manual Registration (Advanced)

While the `JSKeyGameObject` component is the preferred method, you can also register objects via code using `JSInstance.RegisterKeyGameObject`:

```csharp
JSInstance.RegisterKeyGameObject("MainPlayer", gameObject);
```

*Note: Manual registration does not automatically set up the lifecycle and collision event forwarding that the `JSKeyGameObject` component provides.*

For more information on how to access these objects from JavaScript, see the [UniJS - Library Documentation](https://github.com/PizzaPopcorn/unijs-lib).
