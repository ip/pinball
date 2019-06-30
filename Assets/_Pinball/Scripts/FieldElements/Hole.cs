using System;
using System.Collections;
using UnityEngine;

namespace Pinball
{
    public class Hole : MonoBehaviour, IScoreAdder
    {
        public Rigidbody2D otherHole;
        public float delay = 1;
        public float kickVelocity = 100;
        public int scoreValue = 5000;

        public Action<int> OnScoreAdded { get; set; }

        private Rigidbody2D _ball;

        private void Awake()
        {
            Debug.Assert(otherHole != null);

            _ball = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            StartCoroutine(_TeleportBall());

            OnScoreAdded(scoreValue);
        }

        private IEnumerator _TeleportBall()
        {
            // Temporarily disable the other hole's trigger to avoid
            // infinite loop
            otherHole.simulated = false;

            _ball.simulated = false;
            _ball.position = otherHole.position;
            _ball.transform.position = otherHole.position;

            yield return new WaitForSeconds(delay);

            _ball.simulated = true;
            _ball.velocity = otherHole.transform.up * kickVelocity;

            yield return new WaitForSeconds(1);
            otherHole.simulated = true;
        }
    }
}
