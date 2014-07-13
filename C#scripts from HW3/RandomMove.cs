using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class RandomMove : MonoBehaviour {

public float time = 1.0f;
public int waitTime = 1;
public float radius = 3;

	IEnumerator Start () 
	{

	Vector3 startPoint = transform.position;

	while (true) 
		{	 
			Vector3 newPosition = Random.insideUnitSphere * radius;
			newPosition.y = 0.0f;
			newPosition = newPosition + startPoint;
	        yield return StartCoroutine(MoveObject(transform, startPoint, newPosition, time, waitTime));
	        yield return StartCoroutine(MoveObject(transform, newPosition, startPoint, time, waitTime));
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

	    yield return new WaitForSeconds(waitTime);

	}


}
