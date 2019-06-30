using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
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
