using UnityEngine;
using System.Collections;

public class AnalyzeSound : MonoBehaviour
{
    public int qSamples = 1024; // array size
    public float refValue = 0.1f; // RMS value for 0 dB
    public float threshold = 0.02f; // minimum amplitude to extract pitch
    public float rmsValue; // sound level - RMS
    public float dbValue; // sound level - dB
    public float pitchValue; // sound pitch - Hz
    
	private AudioSource audio;
	private float[] samples;
	private float[] spectrum;
	private float fSample;
	public GUIText display;
	
    void Start () 
	{
		audio = GameObject.Find ("SampleA").GetComponent<SoundInfo> ().sample;
    	samples = new float[qSamples];
    	spectrum = new float[qSamples];
    	fSample = AudioSettings.outputSampleRate;
		StartCoroutine(UpdateGUI());
    }
     
    void SoundAnalysis()
	{
	    audio.GetOutputData(samples, 0); // fill array with samples
		float sum = 0f;
	    for (int i=0; i < qSamples; i++)
		{
	    	sum += samples[i]*samples[i]; // sum squared samples
    	}
	    rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
	    dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
	    if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
	    // get sound spectrum
	    audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		float maxV = 0f;
		float maxN = 0f;
	    for (int i=0; i < qSamples; i++)
		{ // find max
		    if (spectrum[i] > maxV && spectrum[i] > threshold)
			{
			    maxV = spectrum[i];
			    maxN = i; // maxN is the index of max
	    	}
   		 }
		float freqN = maxN; // pass the index to a float variable
	    if (maxN > 0f && maxN < (float)(qSamples-1))
		{ // interpolate index using neighbours
			float dL = (float)(spectrum[(int)(maxN)-1]/spectrum[(int)(maxN)]);
		    float dR = (float)(spectrum[(int)(maxN)+1]/spectrum[(int)maxN]);
		    freqN += (float)(0.5*(dR*dR - dL*dL));
	    }
    	pitchValue = (float)(freqN*(fSample/2)/qSamples); // convert index to frequency
    }
     

     
	IEnumerator UpdateGUI()
	{
		while (true)
		{
			SoundAnalysis();
			display.text = "RMS: "+rmsValue.ToString("F2")+
					" ("+dbValue.ToString("F1")+" dB)\n"+
						"Pitch: "+pitchValue.ToString("F0")+" Hz";
			yield return new WaitForSeconds(0.1f);
		}
	}
}