using UnityEngine;
using System.Collections;

public class bgobjsgravity : MonoBehaviour {

	float initialpos, rotation;
	
	bool bounced = false;
	
	
	void Start () {
		
		initialpos = transform.position.y;
	
	}
	
	
	void FixedUpdate ()
	{
		if (transform.position.y > initialpos)
		{
			gameObject.rigidbody.velocity += new Vector3(0, -23, 0);
			bounced = true;
		}
		else bounced = false;
		
	}
	
	void Update () {
		
		if (transform.position.y < initialpos)
			transform.position = new Vector3 (transform.position.x, initialpos, transform.position.z);
			
		if (bounced)
		{	
			float duration = 0.5f;
   			float lerp = Mathf.PingPong (Time.time, duration) / duration;
 			rotation = Mathf.Lerp(-5, 5, lerp);

			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0 + rotation));
		}
		
		if (!bounced)
			transform.rotation = Quaternion.Euler(Vector3.zero);
	}
}
