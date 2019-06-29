using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    public class BallLauncher : MonoBehaviour
    {
        public Rigidbody2D ball;
        public ProgressBar progressBar;
        public float maxSpeed = 100;
        public float maxChargeTime = 1;

        private const float _ballRadius = 1;
        private Vector2 _initialPosition;
        private bool _launchKeyReleased = false;

        private void Awake()
        {
            Debug.Assert(ball != null);
            Debug.Assert(progressBar != null);

            _initialPosition = ball.position;

            EventManager.instance.OnGameStart += _RespawnBall;
        }

        private void Update()
        {
            if (InputManager.instance.IsLaunchStarted() && _IsBallAtStart())
                _BeginBallLaunch();

            // Input.GetKeyUp() doesn't work from coroutines, workaround
            // TODO: move the workaround into InputManager
            _launchKeyReleased = InputManager.instance.IsLaunchEnded();
        }

        private bool _IsBallAtStart() =>
            Vector2.Distance(_initialPosition, ball.position) < _ballRadius;

        private void _BeginBallLaunch()
        {
            StartCoroutine(_BeginBallLaunchCoroutine());
        }

        private IEnumerator _BeginBallLaunchCoroutine()
        {
            float startTime = Time.time;

            float progress = 0;

            for (bool shouldStop = false; !shouldStop;)
            {
                float timePassed = Time.time - startTime;
                progress = Mathf.Clamp01(timePassed / maxChargeTime);

                shouldStop = progress == 1 || _launchKeyReleased;

                progressBar.SetProgress(progress);

                yield return new WaitForEndOfFrame();
            }

            _LaunchBall(speed: progress * maxSpeed);

            progressBar.SetProgress(0);
        }

        private void _LaunchBall(float speed)
        {
            ball.velocity = new Vector2(0, speed);
        }

        private void _RespawnBall()
        {
            ball.velocity = Vector2.zero;
            ball.position = _initialPosition;

            // Update Transform's position as well, otherwise game over
            // will be triggered in the next frame
            ball.transform.position = _initialPosition;
        }
    }
}
