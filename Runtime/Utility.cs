using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TigerMoon
{
    public static class Utility
    {
        public static void SaveJSON(string fileName, object data)
        {
            var filePath = Application.persistentDataPath + $"/{fileName}.json";
            var jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, jsonData);

            Debug.Log($"Saved data to {filePath}");
        }

        public static T LoadJSON<T>(string fileName)
        {
            var path = Application.persistentDataPath + $"/{fileName}.json";
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
                return data;
            }

            Debug.Log("No saved data found.");
            return default;
        }

        public static bool TryGetClosestTag(string tag, Vector2 position, out GameObject target)
        {
            GameObject closestObject = null;
            float closestDistance = Mathf.Infinity;

            var objects = GameObject.FindGameObjectsWithTag(tag);

            foreach (var obj in objects)
            {
                float distance = Vector2.Distance(position, obj.transform.position);
                if (distance < closestDistance)
                {
                    closestObject = obj;
                    closestDistance = distance;
                }
            }

            target = closestObject;
            return closestObject != null;
        }

        public static bool TryGetClosest<T>(List<T> objects, Vector2 position, out T target) where T : MonoBehaviour
        {
            T closestObject = null;
            float closestDistance = Mathf.Infinity;

            foreach (T obj in objects)
            {
                float distance = Vector2.Distance(position, obj.transform.position);
                if (distance < closestDistance)
                {
                    closestObject = obj;
                    closestDistance = distance;
                }
            }

            target = closestObject;
            return closestObject != null;
        }

        public static Vector2 PointerPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public static Vector2 PointerDirection(Vector2 from)
        {
            return (PointerPosition() - from).normalized;
        }
    }
}