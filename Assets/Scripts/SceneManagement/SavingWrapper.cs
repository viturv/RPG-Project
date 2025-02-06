using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defualtSaveFile = "save";
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L)){
                Load();
            }

            
        }

        private void Save()
        {
            //Call Saving system to Save
            Debug.Log("Saving");
            GetComponent<SavingSystem>().Save(defualtSaveFile);
        }

        private void Load()
        {
            //Call Saving system to load 
            Debug.Log("Loading");
            GetComponent<SavingSystem>().Load(defualtSaveFile);
        }
    }
}
