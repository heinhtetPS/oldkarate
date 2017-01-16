using UnityEngine;
using System.Collections;

public class creeperhead : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
		void OnTriggerEnter(Collider otherObject)
	{
		
		if (otherObject.tag == "Sushi" ||
			otherObject.tag == "Money" ||
			otherObject.tag == "Chi")
		{
			otherObject.rigidbody.velocity = new Vector3(
				otherObject.rigidbody.velocity.x * -1,
				otherObject.rigidbody.velocity.y * -1,
				 otherObject.rigidbody.velocity.z);
		}
	}
}
