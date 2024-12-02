
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement

{
    public class Portal : MonoBehaviour
    {
        enum DestinatioIdentifier
        {
            A, B, C, D, E
        }


        [SerializeField] int SceneToLoad = -1;
        [SerializeField] Transform SpawnPoint;
        [SerializeField] DestinatioIdentifier destination;

        [SerializeField] float FadeOutTime= 3f;
        [SerializeField] float FadeInTime = 2f;
        [SerializeField] float FadeWaitTime = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        IEnumerator Transition()
        {
            if (SceneToLoad < 0)
            {
                Debug.LogError("Scene To Load Not Set");
                yield break;
            }


            DontDestroyOnLoad(this.gameObject);
            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(FadeOutTime);
            yield return SceneManager.LoadSceneAsync(SceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(FadeWaitTime);

            yield return fader.FadeIn(FadeOutTime);


            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject Player = GameObject.FindWithTag("Player");
            Player.GetComponent<NavMeshAgent>().Warp(otherPortal.SpawnPoint.position);
            Player.transform.rotation = otherPortal.SpawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }
            return null;
        }
    }
}