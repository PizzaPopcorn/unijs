# UniJS  - Web Interop
This library will let you interact with your Unity Web / WebGL build from JS in a more efficient way than using SendMessage. It also includes a JavaScript library for using all available unity functions.

If you are developing a web game that can't run in editor because it depends on browser stuff or you simply want to prototype fast directly in the browser, this plugin is for you.

## Setup
1. Install this repo as a package in your Unity project using the package manager. Open the package manager, click on the add button (+) and then "Install package from git URL". Paste the clone URL for this repository on the text field.
<img width="260" height="154" alt="image" src="https://github.com/user-attachments/assets/ffb3bd84-fc51-4269-8ed4-7f9576e05f06" />


2. From this point you can either jump to the *How to use* section using the sample scene or continue with step 3 if you want to do it from scratch.

3. Add a null object to your starting scene and drag the `JSInstance.cs` behavior to it.
4. Drag the `JSKeyGameObject` to each object you want to expose directly to JavaScript and set a key to identify it. You will be able to access any object down in its hierarchy from JS.
5. Recommended: To maximize the fast prototyping potencial, export all of your game content to asset bundles, that way you don't have to build the game each time, just update your bundles.
6. Open the Build window and mark your build as "Development Build" so we don't have to deal with headers and decompression for now.
7. Build your game in Web / WebGL using the "Build" option, don't use the "Publish and Play" option because we need to be able to add code later. 
8. Once finished, start the `index.html` file and you should see your start scene.
   - IMPORTANT: If you are already familiar with web builds you'll know that opening the file by double-clicking it won't work. You have to start a local server, some IDEs like Rider let you do that out of the box.
9. By default, iside the `index.html` a script tag importing a script called `unityController.js` will be added, that script doesn't exist, but it's actually there for you to create it, so create it in the same folder as the `index.html`.
   - Note: In the UniJS settings located in Unity Project Settings, you can choose whether to add that script tag or not. You can also set a different name if you want.
10. That's it, use that controller script to interact with the API. Some IDEs can fetch the CDN file so your Intellisense can auto-complete the functions.
11. If you need to embed your game in your web site and don't want to directly use the `index.html` file, go to the *Dynamic Build Instance* section to learn how to achieve it. It's important that your game is embeded directly inside the DOM, iframe won't work because you'll lose the connection with the game code.

## How to use
For a quick understanding of how to use the library you can follow this guide using the sample scene. If you have already your scene setup you can skip to the step 5.
1. Import the sample scene from the package manager.
2. Open the sample scene and you should see something like the image below
<img height="225" alt="sample scene" src="https://github.com/user-attachments/assets/eb60e783-e544-40f6-8cae-3777aa0344c6" />

3. Notice how the components are assigned:
   - At the top there is an empty object called "UniJS" with a `JSInstance` component attached.
   - The cube and the text have a `JSKeyGameObject` compoment attached with the keys "Cube" and "Text" respectively.
   - The sphere has nothing attached.
   - The UI button doesn't have a `JSKeyGameObject` attached but it has a `JSEventButton` and its OnClick event is invoking a `JSEvent` called "ButtonClicked".
5. Add the sample scene as the first one in the scenes list for build, and turn off the other scenes if any.
6. Follow steps 6 to 9 from the *Setup* section if you haven't done already.
7. Test your build, if everything is ok you should see the same image as in step 2.
8. Now go to your JS controller script and add this code:
```js
Unity.onInstanceReady(() => {
    Unity.GameObject.GetKeyGameObject("Text").SetText("UniJS is awesome!");
});
```

8. Save and hit refresh and you will see that the text now will display "UniJS is awesome!" on load.
9. The `onInstanceReady` callback helps you make sure that unity is already running and the first scene has completed loading. If you try to get a gameObject outside that callback it will probably return null, unless the whole script is loaded after the unity instance is ready.
10. There's another way of making sure an object exists and it is by using the lifecycle callbacks. Add the following code to your script:
```js
Unity.GameObject.onStart("Cube", cube => {
    cube.transform.Rotate(0, 45, 0);
});
```

11. Now save and hit refresh and you'll see that the cube appears rotated. That's how you handle start, awake and enable functions, the object might not exist yet but this tells Unity that when the `JSKeyGameObject` with the tag "Cube" exists and calls its `Start` method, it should execute that callback. This is a good method also for objects that are instanced during runtime.
12. Now, the sphere on top of the cube doesn't have a Key, but it is a child of the cube, so we can still access it. Modify the start callback as follows:
```js
Unity.GameObject.onStart("Cube", cube => {
    cube.transform.Rotate(0, 45, 0);
    cube.GetChild(0).SetActive(false);
});
```

13. Save and hit refresh and now the sphere is gone. You can go as deep as you want inside the object's hierarchy. The `GetChild` function works both with an index or a child name. Modify the start callback as follows:
```js
Unity.GameObject.onStart("Cube", cube => {
    cube.transform.Rotate(0, 45, 0);
    cube.GetChild("Sphere").SetActive(false);
});
```

14. Save and hit refresh and notice that the effect is the same.
15. Remember the UI button? Well, there are 2 ways of interacting with it, the easiest one is via the `JSEvent` (as it's currently configured), the other one is by keying it and accessing its `onClick` event, but we'll cover here the event method. Add this snippet:
```js
Unity.onEvent("ButtonClicked", () => {
    Unity.GameObject.GetKeyGameObject("Text").SetText("Hello World");
});
```
16. Notice how when you click the button, the text changes to display "Hello World".
17. Explore the API in JavaScript to look for other functions. Check the [Documentation](https://github.com/PizzaPopcorn/unijs-lib/tree/main/docs) for more details.

## Dynamic Build Instance
This section covers how to create a dynamic instance of your Web / WebGL build in case you want to embed your game in your web page. This approach is the preferred one because that way you have total control of your HTML content and don't have to do anything else each time your build is updated. This is also the only way if you are fetching your game from a server and / or if you are using a frontend framework like React or Blazor.

1. Go to your project settings and then to the UniJS section (or locate the config object in `Assets/UniJS/Editor`) and unckeck the Include CDN option. That will prevent the build pipeline to include the CDN script inside the generated `index.html` file in the WebGL template so you can add it to its wrapper instead.
   - Note: The config file is automatically created when you build your project or open the UniJS settings window for the first time, if you want to manually create it you can do it via the menu bar by clicking on `UniJS/Create or Focus Settings`. That will create the file or select it in the project window if it already exists.
2. Also, in the settings window, uncheck the Inject controller script option, for the same reason, so you can manually add it to its wrapper.
3. Build your game in Web / WebGL using the "Build" option and once finished, put the build folder inside the `wwwroot` folder in your web project (or upload it somewhere).
4. In your web page, import the JavaScript library on the html file that will load the Unity instace by adding this line:
```html
<script src="https://cdn.jsdelivr.net/npm/@pizzapopcorn/unijs/dist/unity.min.js"></script>
```

5. Then, create a div (if you don't have one already) for your game and assign an `id` to it. Something like this:
```html
<div id="unijs"></div>
```

6. Create your controller JavaScript file (name it as you want) and also add it to the html page, then in that file, call `Unity.LoadInstance("url", "elementId");` where the url can be either an external url or the folder inside `wwwroot` where your build is located, and `elementId` is the `id` of the div where the game canvas will be embeded.
