using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct AudioPresset
    {
        public float minSize;
        public float maxSize;
        public AudioClip clip;
    }

    public AudioPresset[] audioPressets;

    private static AudioManager instance;
    public static AudioManager Instance()
    {

        // If instance is not initialized
        if (!instance)
        {

            // Is there an ObjectPoolCollection somewhere in the scene
            instance = FindObjectOfType<AudioManager>();

            // If not, then the designer forgot to put one
            if (!instance)
            {
                Debug.LogError("There needs to be at least one active ObjectPoolCollection on the scene");
            }

            // This won't work because we can't guarantee that T has an Initialize method
        }

        return instance;
    }
}
