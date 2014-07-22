using UnityEngine;
using System.Collections;

public class AlertTrigger : MonoBehaviour {

	public AudioSource sonifyHot;
	public AudioSource sonifyMatch;
	public string matchPreference;
	public string sampleName = "";
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
			other.gameObject.GetComponent<SoundInfo>().sonify.mute = true;
			sampleName = other.gameObject.name;
			GameObject.Find("Sample Name").GetComponent<GUIText>().guiText.text = "Sample Name: " +sampleName;
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
			other.gameObject.GetComponent<SoundInfo>().sonify.mute = false;
		}
		
	}
}
