using UnityEngine;
using System.Collections;
using System.Linq;

namespace Extensions
{
    public static class ExTransform
    {
        public static void X(this Transform transform, float value)
        {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        public static void Y(this Transform transform, float value)
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
        public static void Z(this Transform transform, float value)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, value);
        }
        public static void MoveTo(this Transform transform, Transform other, float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);
        }
        public static void MoveTo(this Transform transform, Vector3 vector, float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, vector, speed * Time.deltaTime);
        }
        public static bool WithinDistance(this Transform transform, Transform other, float distance)
        {
            return Vector3.Distance(transform.position, other.transform.position) < distance;
        }
        public static Transform Closest(this Transform transform, Transform[] transforms)
        {
            return transforms.OrderBy(t => (t.position - transform.position).sqrMagnitude).FirstOrDefault();
        }
    }
}