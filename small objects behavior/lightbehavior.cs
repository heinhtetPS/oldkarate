using UnityEngine;
using System.Collections;

public class lightbehavior : MonoBehaviour {

	private bool rotating = true;
	
	void Start () {
	
	}
	

	void Update () {
		
	if (rotating)
//		float rotationspeed = currentRotate * Time.deltaTime;
		transform.Rotate(new Vector3(-0.01f,0,0));
		
		if (transform.rotation.x <= -60f)
			rotating = false;
	
	}
}
