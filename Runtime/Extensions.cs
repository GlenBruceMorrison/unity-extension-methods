using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Rendering;
using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting;

namespace TigerMoon
{
    public static class Extensions
    {
        #region Rigidbody2D Extensions
        public static void Knockback(this Rigidbody2D rigidbody, Vector3 origin, float force)
        {
            rigidbody.AddForce((origin - rigidbody.transform.position).normalized * force);
        }
        #endregion

        #region MonoBehaviour Extensions
        public static void AfterTime(this MonoBehaviour owner, float delay, System.Action action)
        {
            owner.StartCoroutine(AfterTimeAsync(delay, action));
        }

        private static IEnumerator AfterTimeAsync(float delay, System.Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        public static void TransitionFloat(this MonoBehaviour owner, float startValue, float endValue, float duration, float delay, System.Action<float> updateAction, System.Action OnComplete = null)
        {
            owner.StartCoroutine(TransitionFloatAsync(startValue, endValue, duration, delay, updateAction, OnComplete));
        }

        private static IEnumerator TransitionFloatAsync(float startValue, float endValue, float duration, float delay, System.Action<float> updateAction, System.Action OnComplete = null)
        {
            if (delay > 0)
            {
                updateAction?.Invoke(startValue);
                yield return new WaitForSeconds(delay);
            }

            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                var t = elapsedTime / duration;
                var currentValue = Mathf.Lerp(startValue, endValue, t);

                updateAction?.Invoke(currentValue);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final value is set
            updateAction?.Invoke(endValue);

            OnComplete?.Invoke();
        }

        public static void Repeat(this MonoBehaviour owner, float delay, float timeBetweenActions, System.Action<int> action)
        {
            owner.StartCoroutine(RepeatAsync(delay, timeBetweenActions, action));
        }

        private static IEnumerator RepeatAsync(float delay, float timeBetweenActions, System.Action<int> action)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }

            var cycle = 0;

            while (true)
            {
                yield return new WaitForSeconds(timeBetweenActions);
                action?.Invoke(cycle);
                cycle++;
            }
        }
        #endregion
    }
}