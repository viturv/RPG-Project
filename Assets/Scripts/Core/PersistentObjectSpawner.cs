using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistenObject();
            
            hasSpawned = true;
            
        }

        private void SpawnPersistenObject()
        {
            GameObject persistentObject =  Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
