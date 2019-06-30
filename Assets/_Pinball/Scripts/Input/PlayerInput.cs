using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    [Serializable]
    public enum InputSide
    {
        Left,
        Right,
    }

    // Provides player controls - uses keyboard in the editor and touch screen
    // on mobile.
    [RequireComponent(typeof(InputProvider))]
    public class PlayerInput : MonoBehaviour, IInput
    {
        public StatefulButton leftSide;
        public StatefulButton rightSide;

        private const KeyCode _launchKey = KeyCode.Space;

        private void Awake()
        {
            Debug.Assert(leftSide != null);
            Debug.Assert(rightSide != null);

            var inputProvider = GetComponent<InputProvider>();
            EventManager.instance.OnGameStart += () =>
                inputProvider.input = this;
        }

        public bool IsSidePressed(InputSide side)
        {
            var guiSide = side == InputSide.Left ? leftSide : rightSide;
            var keyboardSide = side == InputSide.Left ? KeyCode.LeftArrow :
                KeyCode.RightArrow;

            return Input.GetKey(keyboardSide) || guiSide.isPressed;
        }

        public bool IsLaunchStarted()
            => Input.GetKeyDown(_launchKey) || leftSide.isPressStarted
                || rightSide.isPressStarted;

        public bool IsLaunchEnded()
            => Input.GetKeyUp(_launchKey) || leftSide.isPressEnded
                || rightSide.isPressEnded;
    }
}
