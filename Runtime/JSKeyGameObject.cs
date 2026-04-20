using System;
using UnityEngine;
using UniJS.InstanceTools;
using UniJS.Payloads;
using UnityEngine.Serialization;

namespace UniJS
{
    public class JSKeyGameObject : MonoBehaviour
    {
        [FormerlySerializedAs("_jsKey")]
        [Tooltip("The ID used to find this object in Javascript.")] 
        [SerializeField] private string jsKey;
        public string JSKey => jsKey;
        
        [Tooltip("Javascript can't register disabled GameObjects. Set this to false and it will disable itself after registration but before its first frame.")]
        [SerializeField] private bool startEnabled = true;

        private Guid eventGuid;

        private void Awake()
        {
            if (string.IsNullOrEmpty(jsKey))
            {
                jsKey = name;
            }
            JSInstance.LogInternal($"Registering GameObject [{jsKey}]...");
            JSInstance.RegisterKeyGameObject(jsKey, gameObject);
            eventGuid = JSInstance.OnEvent<string, ResponsePayload>($"GOEvent:{jsKey}", JSOnEventCallback);
            if (!startEnabled)
            {
                gameObject.SetActive(false);
            }
            JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(jsKey, "awake");
        }

        private void Start()
        {
            JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(jsKey, "start");
        }

        private void OnEnable()
        {
            JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(jsKey, "enable");
        }
        
        private void OnDisable()
        {
            JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(jsKey, "disable");
        }
        
        private void OnDestroy()
        {
            JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(jsKey, "destroy");
            JSInstance.OffEvent($"GOEvent:{jsKey}", eventGuid);
            JSInstance.UnregisterKeyGameObject(jsKey);
        }

        private void Update()
        {
            JSGameObjectEventHandler.SendGameObjectLifeCycleEvent(jsKey, "update");
        }

        private void OnCollisionEnter(Collision collision)
        {
            JSInstance.InvokeEvent($"CollisionEnter:{jsKey}", new JSGameObjectData(collision.gameObject));
        }

        private void OnCollisionExit(Collision collision)
        {
            JSInstance.InvokeEvent($"CollisionExit:{jsKey}", new JSGameObjectData(collision.gameObject));
        }

        private void OnTriggerEnter(Collider other)
        {
            JSInstance.InvokeEvent($"TriggerEnter:{jsKey}", new JSGameObjectData(other.gameObject));
        }

        private void OnTriggerExit(Collider other)
        {
            JSInstance.InvokeEvent($"TriggerExit:{jsKey}", new JSGameObjectData(other.gameObject));
        }

        private ResponsePayload JSOnEventCallback(string payload)
        {
            JSInstance.LogInternal( $"GameObject [{jsKey}] received event: {payload}");
            var eventPayload = JsonUtility.FromJson<GameObjectEventPayload>(payload);
            if (eventPayload == null) 
                return ResponsePayload.Error("Event payload failed to parse. Check if the payload is formatted correctly.");
              
            if(!gameObject.activeInHierarchy && !eventPayload.listenDisabled)
                return ResponsePayload.Ok("<ignored>");
            
            var target = gameObject;
            if (target == null)
                return ResponsePayload.Error($"The selected GameObject '{jsKey}' is null.");
                
            if (!string.IsNullOrEmpty(eventPayload.hierarchyPath))
            {
                var paths = eventPayload.hierarchyPath.Split('/');
                for (var i = 1; i < paths.Length; i++)
                {
                    var path = paths[i];
                    if (string.IsNullOrEmpty(path)) continue;
                    target = target.transform.Find(path)?.gameObject;
                    if (target == null)
                        return ResponsePayload.Error(
                            $"GameObject with hierarchy path '{eventPayload.hierarchyPath}' not found.");
                }
            }
            return target.InvokeJSEvent(eventPayload.eventName, eventPayload.payloadJson);
        }
    }
}
