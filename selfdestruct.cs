using UnityEngine;
using System.Collections;

public class selfdestruct : MonoBehaviour {
	
	float countdown = 0;
	public float timetogo;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		countdown += Time.deltaTime;
		
		if (countdown - Time.deltaTime > timetogo)
			Destroy(this.gameObject);
	
	}
}
