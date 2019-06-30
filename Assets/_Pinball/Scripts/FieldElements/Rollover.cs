using System;
using UnityEngine;

namespace Pinball
{
    // An indicator which switches when a ball rolls over it.
    public class Rollover : MonoBehaviour
    {
        public Color enabledColor;

        public Action OnTriggered;
        public bool rolloverEnabled
        {
            get => _rolloverEnabled;
            set
            {
                _rolloverEnabled = value;
                _UpdateState();
            }
        }

        private Material _material;
        private Color _disabledColor;
        private bool _rolloverEnabled;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;

            _disabledColor = _material.color;

            EventManager.instance.OnGameStart += _ =>
                rolloverEnabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            rolloverEnabled = !rolloverEnabled;

            if (OnTriggered != null)
                OnTriggered();
        }

        private void _UpdateState()
        {
            _material.color = rolloverEnabled ? enabledColor : _disabledColor;
        }
    }
}
