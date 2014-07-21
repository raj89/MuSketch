using UnityEngine;
using System.Collections;

public class AlertTrigger : MonoBehaviour {

	public AudioSource sonifyHot;
	public AudioSource sonifyMatch;
	public string matchPreference;
	public int hotNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<SoundInfo>()) {
			other.gameObject.GetComponent<SoundInfo>().sample.mute = false;
			if (other.gameObject.GetComponent<SoundInfo> ().numLikes >= hotNum) {
				sonifyHot.Play ();
			}
			if (other.gameObject.tag == matchPreference) {
				sonifyMatch.Play ();
			}
		}
		
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.GetComponent<SoundInfo>()) {
			other.gameObject.GetComponent<SoundInfo>().sample.mute = true;
			if (other.gameObject.GetComponent<SoundInfo> ().numLikes >= hotNum) {
				sonifyHot.Play ();
			}
			if (other.gameObject.tag == matchPreference) {
				sonifyMatch.Play ();
			}
		}
		
	}
}
