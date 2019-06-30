using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pinball
{
    public class BotInput : IInput
    {
        public bool isLaunchStarted;
        public bool isLaunchEnded;
        public bool[] isSidePressed =
            new bool[Enum.GetNames(typeof(InputSide)).Length];

        public bool IsSidePressed(InputSide side) => isSidePressed[(int)side];
        public bool IsLaunchStarted() => isLaunchStarted;
        public bool IsLaunchEnded() => isLaunchEnded;
    }

    [RequireComponent(typeof(InputProvider), typeof(BallLauncher))]
    public class Bot : MonoBehaviour
    {
        public Transform ball;

        // A flipper will be triggered when the distance between the ball
        // and the flipper axis falls into this interval
        public float triggerMin = 2;
        public float triggerMax = 3.87f;

        public float minLaunchSpeed = 47.7f;

        private BallLauncher _ballLauncher;
        private BotInput _input = new BotInput();
        private bool _isActive;
        private bool _isLaunching;
        private FlipperTrigger[] _flipperTriggers;

        private void Awake()
        {
            _ballLauncher = GetComponent<BallLauncher>();
            _CacheFlipperTriggers();

            Debug.Assert(ball != null);
            Debug.Assert(minLaunchSpeed <= _ballLauncher.maxSpeed);

            _SubscribeToPlayStateChange();
        }

        private void Update()
        {
            if (!_isActive)
                return;

            _HandleBallLaunch();
        }

        private void FixedUpdate()
        {
            if (!_isActive)
                return;

            _TriggerFlippersIfNeeded();
        }

        private void _SubscribeToPlayStateChange()
        {
            var inputProvider = GetComponent<InputProvider>();
            EventManager.instance.OnGameStart += (bool isBotMode) =>
            {
                if (!isBotMode)
                    return;

                inputProvider.input = _input;
                _isActive = true;
            };

            EventManager.instance.OnGameOver += () =>
                _isActive = false;
        }

        private void _HandleBallLaunch()
        {
            bool isBallAtStart = _ballLauncher.IsBallAtStart();
            if (!_isLaunching && isBallAtStart)
                StartCoroutine(_LaunchBall());
        }

        private IEnumerator _LaunchBall()
        {
            _isLaunching = true;

            _input.isLaunchStarted = true;
            yield return new WaitForEndOfFrame();
            _input.isLaunchStarted = false;

            float launchSpeed = Random.Range(minLaunchSpeed, _ballLauncher.maxSpeed);

            while (_ballLauncher.currentLaunchSpeed < launchSpeed)
                yield return new WaitForEndOfFrame();

            _input.isLaunchEnded = true;
            yield return new WaitForEndOfFrame();
            _input.isLaunchEnded = false;

            yield return new WaitForSeconds(1);
            _isLaunching = false;
        }

        private void _CacheFlipperTriggers()
        {
            _flipperTriggers =
                MonoBehaviour.FindObjectsOfType<FlipperTrigger>();
        }

        private void _TriggerFlippersIfNeeded()
        {
            bool[] isSidePressed = _input.isSidePressed;
            Vector2 ballPos = ball.transform.position;

            for (int j = 0; j < isSidePressed.Length; ++j)
                isSidePressed[j] = false;

            for (int i = 0; i < _flipperTriggers.Length; ++i)
            {
                FlipperTrigger trigger = _flipperTriggers[i];
                bool shouldTrigger = trigger.isTriggered;

                if (shouldTrigger)
                {
                    InputSide side = trigger.flipper.inputSide;
                    isSidePressed[(int)side] = true;
                }
            }
        }
    }
}
