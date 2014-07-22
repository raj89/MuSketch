using UnityEngine;
using System.Collections;

public class AmbientTrigger : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<SoundInfo>()) {
			other.gameObject.GetComponent<SoundInfo>().sonify.mute = false;
			other.gameObject.GetComponent<SoundInfo>().sonify.Play();
		}
		
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.GetComponent<SoundInfo>()) {
			other.gameObject.GetComponent<SoundInfo>().sonify.mute = true;
		}
	}
}
