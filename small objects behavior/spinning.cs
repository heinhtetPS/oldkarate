using UnityEngine;
using System.Collections;

public class spinning : MonoBehaviour {
	
	public float direction, speed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(new Vector3(0,0, direction) * speed);
	
	}
}
