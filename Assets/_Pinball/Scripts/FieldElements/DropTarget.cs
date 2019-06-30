using System;
using UnityEngine;

namespace Pinball
{
    // See https://en.wikipedia.org/wiki/Pinball#Targets.
    [RequireComponent(typeof(Rigidbody2D))]
    public class DropTarget : MonoBehaviour
    {
        public float disabledZPosition = 0.7f;

        public Action OnHit;

        public bool isTargetActive
        {
            get => _isTargetActive;
            set
            {
                _isTargetActive = value;
                _rigidbody.simulated = value;
                _UpdateZPosition();
            }
        }

        private Rigidbody2D _rigidbody;
        private bool _isTargetActive = true;
        private float _enabledZPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _enabledZPosition = transform.position.z;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            isTargetActive = false;

            if (OnHit != null)
                OnHit();
        }

        private void _UpdateZPosition()
        {
            float z = isTargetActive ? _enabledZPosition : disabledZPosition;
            var pos = transform.position;
            pos.z = z;
            transform.position = pos;
        }
    }
}
