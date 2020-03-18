using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "Save01";
        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}

