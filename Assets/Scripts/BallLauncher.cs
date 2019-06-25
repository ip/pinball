using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    public class BallLauncher : MonoBehaviour
    {
        public Rigidbody2D ball;
        public float maxSpeed = 100;

        private Vector2 _initialPosition;

        private void Awake()
        {
            Debug.Assert(ball != null);

            _initialPosition = ball.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _LaunchBall();

            // For development
            if (Application.isEditor && Input.GetKeyDown(KeyCode.Return))
                _RespawnBall();
        }

        private void _LaunchBall()
        {
            ball.velocity = new Vector2(0, maxSpeed);
        }

        private void _RespawnBall()
        {
            ball.velocity = Vector2.zero;
            ball.position = _initialPosition;
        }
    }
}
