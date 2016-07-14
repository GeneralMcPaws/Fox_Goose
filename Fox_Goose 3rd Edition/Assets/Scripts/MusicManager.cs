using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] levelMusicArray;

    static MusicManager instance = null;

    private AudioSource audioSource = null;
   
  

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    

        audioSource = GetComponent<AudioSource>();
    }

    
}
