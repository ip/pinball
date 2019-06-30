using System;
using System.Linq;
using UnityEngine;

namespace Pinball
{
    // A group of rollovers which shifts when a flipper is activated
    // and adds score once all its rollovers enabled.
    public class RolloverGroup : MonoBehaviour, IScoreAdder
    {
        public Rollover[] rollovers;
        public int scoreValue = 20000;

        public Action<int> OnScoreAdded { get; set; }

        private InputProvider _inputProvider;

        private void Awake()
        {
            Debug.Assert(rollovers.Length > 0);

            _inputProvider = MonoBehaviour.FindObjectOfType<InputProvider>();

            _HandleGroupActivation();
        }

        private void Update()
        {
            if (_inputProvider.IsSideDown(InputSide.Left))
                _ShiftLeft();

            if (_inputProvider.IsSideDown(InputSide.Right))
                _ShiftRight();
        }

        private void _ShiftLeft()
        {
            bool first = rollovers[0].rolloverEnabled;

            for (int i = 0; i < rollovers.Length - 1; ++i)
                rollovers[i].rolloverEnabled = rollovers[i + 1].rolloverEnabled;

            rollovers[rollovers.Length - 1].rolloverEnabled = first;
        }

        private void _ShiftRight()
        {
            _ShiftLeft();
            _ShiftLeft();
        }

        private void _HandleGroupActivation()
        {
            foreach (var rollover in rollovers)
                rollover.OnTriggered += _HandleChildTrigger;
        }

        private void _HandleChildTrigger()
        {
            bool wholeGroupActive = rollovers.All(
                rollover => rollover.rolloverEnabled);

            if (wholeGroupActive)
            {
                _ResetGroup();
                _AddScore();
            }
        }

        private void _ResetGroup()
        {
            foreach (var rollover in rollovers)
                rollover.rolloverEnabled = false;
        }

        private void _AddScore()
        {
            OnScoreAdded(scoreValue);
        }
    }
}
