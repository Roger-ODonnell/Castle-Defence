using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;

    public void queueSound(AudioSource clip)
    {
        clip.Play();
    }

}
