using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    // A group of rollovers which shifts when a flipper is activated
    // and adds score once all its rollovers enabled.
    public class RolloverGroup : MonoBehaviour
    {
        public Rollover[] rollovers;

        private InputProvider _inputProvider;

        private void Awake()
        {
            Debug.Assert(rollovers.Length > 0);

            _inputProvider = MonoBehaviour.FindObjectOfType<InputProvider>();
        }

        private void Update()
        {
            if (_inputProvider.isScreenDown)
                _ShiftLeft();
        }

        private void _ShiftLeft()
        {
            bool first = rollovers[0].rolloverEnabled;

            for (int i = 0; i < rollovers.Length - 1; ++i)
                rollovers[i].rolloverEnabled = rollovers[i + 1].rolloverEnabled;

            rollovers[rollovers.Length - 1].rolloverEnabled = first;
        }
    }
}
