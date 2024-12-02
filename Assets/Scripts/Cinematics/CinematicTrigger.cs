using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        public bool CinematicTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            
            if (!CinematicTriggered && other.gameObject.tag=="Player")
            {
                CinematicTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
            
            
        }

    }
}
