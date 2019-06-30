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

        public bool IsSidePressed(InputSide side) => false;
        public bool IsLaunchStarted() => isLaunchStarted;
        public bool IsLaunchEnded() => isLaunchEnded;
    }

    [RequireComponent(typeof(InputProvider), typeof(BallLauncher))]
    public class Bot : MonoBehaviour
    {
        public float minLaunchSpeed = 47.7f;

        private BotInput _input = new BotInput();

        private bool _isActive;
        private bool _isLaunching;

        private BallLauncher _ballLauncher;

        private void Awake()
        {
            _ballLauncher = GetComponent<BallLauncher>();

            Debug.Assert(minLaunchSpeed <= _ballLauncher.maxSpeed);


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

        private void Update()
        {
            if (!_isActive)
                return;

            _HandleBallLaunch();
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
    }
}
