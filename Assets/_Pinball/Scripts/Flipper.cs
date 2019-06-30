using UnityEngine;

namespace Pinball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Flipper : MonoBehaviour
    {
        public InputProvider input;

        public InputSide inputSide;

        [Tooltip("Rotation in activated state")]
        public float activeRotation;
        [Tooltip("Rotation speed (degrees per second)")]
        public float rotationSpeed = 30;

        private float _inactiveRotation;
        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();

            _inactiveRotation = _rigidBody.rotation;
        }

        private void FixedUpdate()
        {
            float deltaAngleAbs = rotationSpeed * Time.fixedDeltaTime;

            bool isActive = input.IsSidePressed(inputSide);
            float targetAngle = isActive ? activeRotation : _inactiveRotation;
            float currentAngle = _rigidBody.rotation;
            float angleDiff = targetAngle - currentAngle;
            float sign = Mathf.Sign(angleDiff);
            float angleDiffAbs = Mathf.Abs(angleDiff);
            float deltaAngle = sign * Mathf.Min(angleDiffAbs, deltaAngleAbs);

            _rigidBody.MoveRotation(currentAngle + deltaAngle);
        }
    }
}
