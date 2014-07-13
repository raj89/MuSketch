using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Waitress : MonoBehaviour {

	public AudioClip [] orderSounds;

	public Vector3 target1 = new Vector3(-10, 2, -25);
	public Vector3 target2 = new Vector3(-4, 2, -15);
	public float time = 1.0f;
	public int waitTime = 1;

IEnumerator Start () 
{

	Vector3 startPoint = transform.position;

	while (true) 
		{
	        yield return StartCoroutine(MoveObject(transform, startPoint, target1, time, waitTime));
	        yield return StartCoroutine(MoveObject(transform, target1, target2, time, waitTime));
	        yield return StartCoroutine(MoveObject(transform, target2, startPoint, time, waitTime));
	    }
}

IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time, int waitTime) 
{

    float fractDist = 0.0f;

    float fractSpeed = 1.0f/ time;

    while (fractDist < 1.0f) 
    {
        fractDist += Time.deltaTime * fractSpeed;
        thisTransform.position = Vector3.Lerp(startPos, endPos, fractDist);
        yield return null; 
    }
    PlayRandom();
    yield return new WaitForSeconds(waitTime);
}

void PlayRandom()
{
	if(audio.isPlaying)
		return;

	audio.clip = orderSounds[Random.Range(0,orderSounds.Length)];
	audio.Play();

	//Okay, gave up on this, couldn't think of a way to pass the the current audio clip length

	// float clipLength = audio.clip.length;
	// Debug.Log(clipLength);
	// StartCoroutine(letMeTakeMyOrder(clipLength));
}

// IEnumerator letMeTakeMyOrder(float audioLength)
// {
// 	float length = audioLength;
// 	yield return WaitForSeconds(length);
// }

}
