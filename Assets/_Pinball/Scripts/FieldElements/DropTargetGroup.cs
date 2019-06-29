using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pinball
{
    // Resets the drop target group and adds score once all its targets are hit.
    public class DropTargetGroup : MonoBehaviour, IScoreAdder
    {
        public DropTarget[] targets;
        public int scoreValue = 5000;

        public Action<int> OnScoreAdded { get; set; }

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

        private void _ResetTargetsIfNeeded()
        {
            bool areTargetsHit = targets.All(target => !target.isTargetActive);
            if (areTargetsHit)
            {
                _ResetTargets();
                OnScoreAdded(scoreValue);
            }
        }

        private void _ResetTargets()
        {
            foreach (var target in targets)
                target.isTargetActive = true;
        }

        private void _ResetOnGameStart()
        {
            EventManager.instance.OnGameStart += _ResetTargets;
        }
    }
}
