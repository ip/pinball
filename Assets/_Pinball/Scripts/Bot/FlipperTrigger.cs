using UnityEngine;

namespace Pinball
{
    public class FlipperTrigger : MonoBehaviour
    {
        public Flipper flipper;
        public bool isTriggered { get; private set; }

        private void Awake()
        {
            Debug.Assert(flipper != null);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            isTriggered = true;
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            isTriggered = false;
        }
    }
}
