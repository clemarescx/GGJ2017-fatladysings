using UnityEngine;
using System.Collections;

public class DirectionalAudioScript : MonoBehaviour {

	private AudioListener listener;
	new private AudioSource audio;

	public float maxAngle = 35;
	public float maxVolume = 1;
	private float audioScale;

	// Use this for initialization
	void Start() {
		listener = FindObjectOfType<AudioListener>();
		audio = GetComponent<AudioSource>();
		audioScale = 1 / maxVolume;
	}
	
	// Update is called once per frame
	void Update() {
		var listenerPos = listener.transform.position;
		var targetDir = listenerPos - transform.position;
		var angle = Vector3.Angle(targetDir, transform.forward);

		if(angle < maxAngle) {
			audio.volume = maxVolume - ((angle / maxAngle) / audioScale);
		} else if(angle > maxAngle) {
			audio.volume = 0;	
		}
	}
}
