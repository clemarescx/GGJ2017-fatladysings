using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioMixer))]
public class UtilsAudio : MonoBehaviour {

	public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos, AudioMixerGroup output, float volume = 1f)
	{
		var tmpObject = new GameObject("TempAudio"); // create the temp object
		tmpObject.transform.position = pos; // set its position
		var aSource = tmpObject.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.outputAudioMixerGroup = output;
		aSource.volume = volume;
		aSource.spatialBlend = 1; //always play 3D sound

		// set other aSource properties here, if desired
		aSource.Play(); // start the sound
		Destroy(tmpObject, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}

    public static AudioSource PlayClip2D(AudioClip clip, float volume = 1f)
    {
        var tmpObject = new GameObject("TempAudio"); // create the temp object
        var aSource = tmpObject.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        aSource.spatialBlend = 0;
        aSource.volume = volume;

        // set other aSource properties here, if desired
        aSource.Play(); // start the sound
        Destroy(tmpObject, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }
}
