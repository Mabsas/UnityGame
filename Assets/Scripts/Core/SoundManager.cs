using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {  get; private set; }
    private AudioSource source;

    private void Awake()
    {
        instance = this;

        source = GetComponent<AudioSource>();



        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //on moving to level 2 song keeps playing doesnt get destroyed
        }


        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }


    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
