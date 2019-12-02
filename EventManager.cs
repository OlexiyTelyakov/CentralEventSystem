using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KiwiKaleidoscope 
{
    public class EventManager : MonoBehaviour 
    {

        #region Singleton
        private static EventManager instance;
        public static EventManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<EventManager>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject("EventManager");
                        instance = go.AddComponent<EventManager>();
                    }
                }
                return instance;
            }
        }
        #endregion

        private Dictionary<Type, List<Callback>> eventListeners;


        /// <summary>
        /// Register a method to listen for an event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listener"></param>
        public static void RegisterListener<T>(Action<T> listener) where T : EventInfo
        {
            //Get type to pass into the dictionary searchers/contstructor.
            Type eventType = typeof(T);
            //Reflection magic.
            object target = listener.Target;
            System.Reflection.MethodInfo method = listener.Method;
            //Check if the dictionary, key and the corresponding value exist and if not, initialize them.
            if(Instance.eventListeners == null)
            {
                Instance.eventListeners = new Dictionary<Type, List<Callback>>();
            }
            if(!Instance.eventListeners.ContainsKey(eventType) || Instance.eventListeners[eventType] == null)
            {
                Instance.eventListeners[eventType] = new List<Callback>();
            }
            //Construct the callback with reflection magic and add it to the event listener.
            Callback callback = new Callback { target = target, method = method };
            Instance.eventListeners[eventType].Add(callback);
        }

        /// <summary>
        /// Stops method from listening to an event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listener"></param>
        public static void RemoveListener<T>(Action<T> listener) where T : EventInfo
        {
            //If event dictionary hasn't been initialized, return.
            if(Instance.eventListeners == null) { return; }
            //Get type to pass into the dictionary searchers/contstructor.
            Type eventType = typeof(T);
            //Initialize eventListeners if they haven't been initialized yet.
            if (Instance.eventListeners == null) Instance.eventListeners = new Dictionary<Type, List<Callback>>();
            //If there no dictionary key or corresponding value exists, return.
            if (!Instance.eventListeners.ContainsKey(eventType) || Instance.eventListeners[eventType] == null) { return; }
            //Find and remove the callback.
            Instance.eventListeners[eventType].RemoveAll(l => l.target == listener.Target && l.method == listener.Method);
        }

        /// <summary>
        /// Fires an event with event information T. All listeners will execute corresponding methods.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventInfo"></param>
        public static void Event<T>(T eventInfo) where T : EventInfo
        {
            //Get type to pass into the dictionary searchers/contstructor.
            Type eventType = typeof(T);
            //Check if the dictionary, key and the corresponding value exist and if not, initialize them.
            if (Instance.eventListeners == null)
            {
                Instance.eventListeners = new Dictionary<Type, List<Callback>>();
            }
            //If there no dictionary key or corresponding value exists, return.
            if (!Instance.eventListeners.ContainsKey(eventType) || Instance.eventListeners[eventType] == null) { return; }
            //Call all listeners about the event.
            for(int i = 0; i < Instance.eventListeners[eventType].Count; i++)
            {
                Instance.eventListeners[eventType][i].method.Invoke(Instance.eventListeners[eventType][i].target, new[] { eventInfo });
            }
            //foreach(Callback callback in Instance.eventListeners[eventType])
            //{
            //    callback.method.Invoke(callback.target, new[] { eventInfo });
            //}
        }

        private class Callback 
        {
            public object target;
            public System.Reflection.MethodInfo method;
        }
    }
}
