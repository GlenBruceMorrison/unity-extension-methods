using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TigerMoon
{
    public class Singleton<T> : MonoBehaviour where T : class
    {
        protected static T instance;
        public static T Instance => instance;

        protected virtual void Awake()
        {
            instance = this as T;
        }
    }
}