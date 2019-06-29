using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pinball
{
    // A GUI button which allows to poll its state like Input.GetKeyDown(),
    // Input.GetKey() and Input.GetKeyUp().
    public class StatefulButton : MonoBehaviour, IPointerDownHandler,
        IPointerUpHandler
    {
        public bool isPressStarted { get; private set; }
        public bool isPressed { get; private set; }
        public bool isPressEnded { get; private set; }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            isPressed = true;
            isPressStarted = true;
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            isPressed = false;
            isPressEnded = true;
        }

        private void Update()
        {
            if (isPressStarted)
                CoroutineUtil.instance.WaitForEndOfFrame(
                    () => isPressStarted = false);

            if (isPressEnded)
                CoroutineUtil.instance.WaitForEndOfFrame(
                    () => isPressEnded = false);
        }
    }
}
