using System.Collections;
using UnityEngine;

namespace Fsi.Gameplay
{
    public class DelayedDestroy : MonoBehaviour
    {
        [SerializeField]
        private bool startOnAwake = true;
        
        [SerializeField]
        private float time = 1;

        private void Awake()
        {
            if (startOnAwake)
            {
                StartDelay();
            }
        }
        
        public void StartDelay()
        {
            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
