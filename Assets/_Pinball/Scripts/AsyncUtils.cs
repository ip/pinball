using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    public class AsyncUtils : MonoBehaviour
    {
        public static AsyncUtils instance;

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
