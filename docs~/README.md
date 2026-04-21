# UniJS - Unity API Documentation

Welcome to the Unity-side documentation for **UniJS**, a seamless interop layer to control Unity WebGL builds from the browser's JavaScript context.

This submodule focuses on the Unity implementation. For the JavaScript-side library, please refer to the [UniJS - Library Repository](https://github.com/PizzaPopcorn/unijs-lib).

## Table of Contents

- [JSInstance](./api-jsinstance.md): The core singleton managing the bridge between Unity and JS.
- [JSEventHub & Event System](./api-jseventhub.md): How to invoke events on GameObjects and extend the event system.
- [Key GameObjects](./key-gameobjects.md): Registering Unity GameObjects to be accessible from JS.

## Getting Started

To use UniJS in your Unity project:
1. Ensure the `JSInstance` prefab (or a GameObject with the `JSInstance` component) is present in your initial scene.
2. The `JSInstance` will automatically initialize the bridge when the WebGL build starts.
3. Attach the `JSKeyGameObject` component to any GameObjects you want to expose to the JS context.

For more information on how to get started, follow the steps in the repository's [README File](https://github.com/PizzaPopcorn/unijs).
