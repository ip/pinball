using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    public class ProgressBar : MonoBehaviour
    {
        public Transform maskBar;

        private float _defaultOffset;
        private float _defaultScale;

        private float _offset
        {
            get => maskBar.localPosition.y;
            set
            {
                var v = maskBar.localPosition;
                v.y = value;
                maskBar.localPosition = v;
            }
        }

        private float _scale
        {
            get => maskBar.localScale.y;
            set
            {
                var v = maskBar.localScale;
                v.y = value;
                maskBar.localScale = v;
            }
        }

        private void Awake()
        {
            Debug.Assert(maskBar != null);

            _defaultOffset = _offset;
            _defaultScale = _scale;
        }

        // progress: [0; 1]
        public void SetProgress(float progress)
        {
            _scale = _defaultScale * (1 - progress);
            _offset = _defaultOffset + (_defaultScale / 2 * progress);
        }
    }
}
