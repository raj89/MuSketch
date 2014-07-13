using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(AudioSource))]
public class AmbienceTrigger : MonoBehaviour 
{
	public AudioSource ambient_1;
	public AudioSource ambient_2;

	public bool isSoundReady = false;

	//Design Consideration: We want to yet be able to hear some what noise coming from the restaurant when exiting outside.

	// Use this for initialization
	void Start () {
		ambient_2.panLevel = 1.0f;
		if (ambient_1.clip == null || ambient_2.clip == null)
			return;	

		InitSounds();
	}
	
	// Update is called once per frame
	void Update () {
		if (ambient_1.clip == null || ambient_2.clip == null)
			return;

		else 
		{
			if (isSoundReady) {
				return;
			}

			InitSounds();
		}
		
	}


	void OnTriggerEnter(Collider other) {
		if (!isSoundReady)
			return;

		if(other.tag=="Player") 
		{
			ambient_1.volume = 0.0f;
			ambient_2.panLevel = 0.0f;	//set to 2D
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (!isSoundReady)
			return;
			
		if(other.tag=="Player")
		{
			ambient_1.volume = 1.0f;
			ambient_2.panLevel = 1.0f;	//set to 3D
		}
	}

	void InitSounds() {
		ambient_1.loop = true;
		ambient_2.loop = true;
		ambient_1.Play();
		ambient_2.Play();
		isSoundReady = true;
	}

}