if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
    // Mobile device style: fill the whole browser client area with the game canvas:
    var meta = document.createElement('meta');
    meta.name = 'viewport';
    meta.content = 'width=device-width, height=device-height, initial-scale=1.0, user-scalable=no, shrink-to-fit=yes';
    document.getElementsByTagName('head')[0].appendChild(meta);

    var canvas = document.querySelector("#unity-canvas");
    canvas.style.width = "100%";
    canvas.style.height = "100%";
    canvas.style.position = "fixed";

    document.body.style.textAlign = "left";

    #if SHOW_DIAGNOSTICS
    // position the diagnostics icon in the corner on the canvas
    let diagnostics_icon = document.getElementById("diagnostics-icon");
    diagnostics_icon.style.position = "fixed";
    diagnostics_icon.style.bottom = "10px";
    diagnostics_icon.style.right = "0px";
    canvas.after(diagnostics_icon);
    #endif
}

    createUnityInstance(document.querySelector("#unity-canvas"), {
    arguments: [],
    dataUrl: "Build/{{{ DATA_FILENAME }}}",
    frameworkUrl: "Build/{{{ FRAMEWORK_FILENAME }}}",
    #if USE_THREADS
    workerUrl: "Build/{{{ WORKER_FILENAME }}}",
    #endif
    #if USE_WASM
    codeUrl: "Build/{{{ CODE_FILENAME }}}",
    #endif
    #if SYMBOLS_FILENAME
    symbolsUrl: "Build/{{{ SYMBOLS_FILENAME }}}",
    #endif
    streamingAssetsUrl: "StreamingAssets",
    companyName: {{{ JSON.stringify(COMPANY_NAME) }}},
    productName: {{{ JSON.stringify(PRODUCT_NAME) }}},
    productVersion: {{{ JSON.stringify(PRODUCT_VERSION) }}},
    // matchWebGLToCanvasSize: false, // Uncomment this to separately control WebGL canvas render size and DOM element size.
    // devicePixelRatio: 1, // Uncomment this to override low DPI rendering on high DPI displays.
}).then((unityInstance) => {
    #if SHOW_DIAGNOSTICS
    document.getElementById("diagnostics-icon").onclick = () => {
    unityDiagnostics.openDiagnosticsDiv(unityInstance.GetMetricsInfo);
};
    #endif
}).catch((message) => {
    alert(message);
});