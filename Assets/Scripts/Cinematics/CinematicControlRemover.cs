using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{ 
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        void Start()
        {
             player = GameObject.FindWithTag("Player");

            GetComponent<PlayableDirector>().played+= DisableControls;
            GetComponent<PlayableDirector>().stopped+= EnableControls;

        }
        void DisableControls(PlayableDirector pd)
        {
            if (player != null)
            {
                player.GetComponent<ActionScheduler>().CancelCurrentAction();
                player.GetComponent<PlayerController>().enabled = false;
            }
        }

        void EnableControls(PlayableDirector pd)
        {
            if (player != null)
            {
                player.GetComponent<PlayerController>().enabled = true;
            }
        }
    }
}
