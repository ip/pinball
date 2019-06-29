using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pinball
{
    // Resets the drop target group once all its targets hit.
    public class DropTargetGroup : MonoBehaviour
    {
        public DropTarget[] targets;

        private void Awake()
        {
            Debug.Assert(targets.Length > 0);

            _ResetWhenAllTargetsHit();
            _ResetOnGameStart();
        }

        private void _ResetWhenAllTargetsHit()
        {
            foreach (var target in targets)
                target.OnHit += _ResetTargetsIfNeeded;
        }

        private void _ResetOnGameStart()
        {
            EventManager.instance.OnGameStart += _ResetTargets;
        }

        private void _ResetTargetsIfNeeded()
        {
            bool areTargetsHit = targets.All(target => !target.isTargetActive);
            if (areTargetsHit)
                _ResetTargets();
        }

        private void _ResetTargets()
        {
            foreach (var target in targets)
                target.isTargetActive = true;
        }
    }
}
