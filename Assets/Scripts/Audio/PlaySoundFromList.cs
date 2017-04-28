using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFromList : MonoBehaviour {

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip[] clips;

    void Start()
    {
//        source = GetComponent<AudioSource>();
    }


	public void PlayClip()
    {
//        source.clip = clips[Random.Range(0, clips.Length - 1)];
        UtilsAudio.PlayClip2D(clips[Random.Range(0, clips.Length - 1)]);
    }
}
