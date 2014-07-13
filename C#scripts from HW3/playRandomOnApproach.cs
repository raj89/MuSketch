using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class playRandomOnApproach : MonoBehaviour {

	public AudioClip[] randSounds;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		InitializeAudioSource();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitializeAudioSource()
	{

		if (audioSource != null) //if we have already set it up
			return;
		
		GameObject go = new GameObject();
		go.transform.parent = transform;
		go.name = "Collision-" + gameObject.name;
		audioSource = go.AddComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.tag=="Player")
		PlayRandom();
	}

	void PlayRandom()
	{
		if(audioSource.isPlaying)
			return;
			
		audioSource.clip = randSounds[Random.Range(0,randSounds.Length)];
		audioSource.Play();
	}


}

