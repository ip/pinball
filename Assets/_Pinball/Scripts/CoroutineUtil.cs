using System;
using System.Collections;
using UnityEngine;

namespace Pinball
{
    public class CoroutineUtil : MonoBehaviour
    {
        public static CoroutineUtil instance;

        private void Awake()
        {
            instance = this;
        }

        public void WaitForEndOfFrame(Action action)
        {
            StartCoroutine(_WaitForEndOfFrameCoroutine(action));
        }

        private IEnumerator _WaitForEndOfFrameCoroutine(Action action)
        {
            yield return new WaitForEndOfFrame();
            action();
        }
    }
}
