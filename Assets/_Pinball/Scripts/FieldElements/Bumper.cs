using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    // In the collision, kicks the ball in the opposite direction.
    // This script is also used for slingshots.
    public class Bumper : MonoBehaviour, IScoreAdder
    {
        public float strength = 40;
        public int scoreValue = 100;

        public Action<int> OnScoreAdded { get; set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _KickBallBack(collision);

            OnScoreAdded(scoreValue);
        }

        private void _KickBallBack(Collision2D collision)
        {
            Vector2 normal = _GetAverageNormal(collision);
            Vector2 impulse = -normal * strength;
            collision.rigidbody.AddForce(impulse, ForceMode2D.Impulse);
        }

        private static Vector2 _GetAverageNormal(Collision2D collision)
        {
            var normal = new Vector2();

            for (int i = 0; i < collision.contactCount; ++i)
                normal += collision.GetContact(i).normal;

            normal /= collision.contactCount;

            return normal;
        }
    }
}
