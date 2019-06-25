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

        private const KeyCode _launchKey = KeyCode.Space;

        private Vector2 _initialPosition;
        private bool _launchKeyReleased = false;

        private void Awake()
        {
            Debug.Assert(ball != null);
            Debug.Assert(progressBar != null);

            _initialPosition = ball.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_launchKey))
                _BeginBallLaunch();

            // Input.GetKeyUp() doesn't work from coroutines, workaround
            _launchKeyReleased = Input.GetKeyUp(_launchKey);

            // For development
            if (Application.isEditor && Input.GetKeyDown(KeyCode.Return))
                _RespawnBall();
        }

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
        }
    }
}
