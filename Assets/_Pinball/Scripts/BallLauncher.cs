using System.Collections;
using UnityEngine;

namespace Pinball
{
    [RequireComponent(typeof(InputProvider))]
    public class BallLauncher : MonoBehaviour
    {
        public ProgressBar progressBar;
        public float maxSpeed = 100;
        public float maxChargeTime = 1;

        public float currentLaunchSpeed { get; private set; }

        private Rigidbody2D _ball;
        private InputProvider _input;
        private const float _ballRadius = 1;
        private Vector2 _initialPosition;
        private bool _launchKeyReleased = false;

        private void Awake()
        {
            Debug.Assert(progressBar != null);

            _ball = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>();
            _input = GetComponent<InputProvider>();

            _initialPosition = _ball.position;

            EventManager.instance.OnGameStart += _ => _RespawnBall();
        }

        private void Update()
        {
            if (_input.IsLaunchStarted() && IsBallAtStart())
                _BeginBallLaunch();

            // This is a workaround for the fact that Input.GetKeyUp()
            // doesn't work from coroutines - so we remember its result here
            _launchKeyReleased = _input.IsLaunchEnded();
        }

        public bool IsBallAtStart() =>
            Vector2.Distance(_initialPosition, _ball.position) < _ballRadius;

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
                currentLaunchSpeed = progress * maxSpeed;

                yield return new WaitForEndOfFrame();
            }

            _LaunchBall(currentLaunchSpeed);

            progressBar.SetProgress(0);
        }

        private void _LaunchBall(float speed)
        {
            _ball.velocity = new Vector2(0, speed);
        }

        private void _RespawnBall()
        {
            _ball.velocity = Vector2.zero;
            _ball.position = _initialPosition;

            // Update Transform's position as well, otherwise game over
            // will be triggered in the next frame
            _ball.transform.position = _initialPosition;
        }
    }
}
