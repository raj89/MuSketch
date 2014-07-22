using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (GUITexture))]
public class StaticWaveform : MonoBehaviour
{
	public int width = 500;
	public int height = 100;
	public Color backgroundColor = Color.clear;
	public Color waveformColor = Color.yellow;
	public int size;
	public AudioSource audio;
	public GUITexture gui;
	
	Color[] blank;
	Texture2D texture;
	float[] samples;
	
	
	// Use this for initialization
	void Start()
	{
		audio = GameObject.Find ("SampleA").GetComponent<SoundInfo> ().sample;
		size = audio.clip.samples * audio.clip.channels;
		samples = new float[size];
		
		// create the texture and assign to the guiTexture:
		texture = new Texture2D(width, height);
		gui.texture = texture;
		
		// create a 'blank screen' image
		blank = new Color[width * height];
		
		for(var i = 0; i<blank.Length; i++)
		{
			blank[i] = backgroundColor;
		}

		GetWaveForm();
	}

	void UpdateAudio() {
		string sampleName = GameObject.Find ("AlertSphere").GetComponent<AlertTrigger> ().sampleName;
		if (sampleName != "") {
			audio = GameObject.Find(sampleName).GetComponent<SoundInfo>().sample;
		}
	}


	IEnumerator UpdateWaveForm()
	{
		while (true)
		{
			GetWaveForm();
			yield return new WaitForSeconds(0.1f);
		}
	}

	void GetWaveForm()
	{

		UpdateAudio();
		size = audio.clip.samples * audio.clip.channels;
		samples = new float[size];
		// clear the texture
		texture.SetPixels(blank, 0);
		
		// get samples from channel 0 (left)
		audio.clip.GetData (samples, 0);
		
		// draw the waveform
		for(var i = 0; i < size; i++)
		{
			texture.SetPixel( (int)((width * i) / size), (int)(height * (samples[i]+1f)/2), waveformColor);
		}
		// upload to the graphics card
		texture.Apply();
	}
}	