using System.Collections;
using UnityEngine;

public class GameplayMusicManager : MonoBehaviour
{
    public AudioClip music1, music2, music3;
    private AudioSource source;
    public float musicVolume = 0.2f;
    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(StartMusic());
    }
    IEnumerator StartMusic()
    {
        source.PlayOneShot(music1, musicVolume);
        yield return new WaitForSeconds(music1.length);
        source.PlayOneShot(music2, musicVolume);
        yield return new WaitForSeconds(music2.length);
        source.PlayOneShot(music3, musicVolume);
        yield return new WaitForSeconds(music3.length);
        StartCoroutine(StartMusic());
    }
}
