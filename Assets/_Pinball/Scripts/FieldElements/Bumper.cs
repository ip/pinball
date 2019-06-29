﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    public class Bumper : MonoBehaviour
    {
        public float strength = 100;

        private void OnCollisionEnter2D(Collision2D collision)
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