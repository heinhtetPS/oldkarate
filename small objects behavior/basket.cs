using UnityEngine;
using System.Collections;

public class basket : MonoBehaviour {

	public creeper creepscript;
	private float eatdelay;
	private bool eated;
	
	// Use this for initialization
	void Start () {
		
		creepscript = (creeper)transform.parent.GetComponent("creeper");
	
	}
	
	void FixedUpdate ()
	{
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (eated)
		{
				eatdelay += Time.deltaTime;
		}
	
	}
	
	void OnTriggerEnter(Collider otherObject)
	{
		
		if (otherObject.tag == "Sushi" ||
			otherObject.tag == "Speedball" ||
			otherObject.tag == "Money" ||
			otherObject.tag == "Chi")
		{
			eated = true;
			if (otherObject.tag == "Money")
			{
				if (eatdelay - Time.deltaTime > 0.1f)
				{
					Destroy(otherObject.gameObject);
					creepscript.smileroutine(Random.Range (1,2));
					eated = false;
					eatdelay = 0;
				}
				return;
			}
			if (eatdelay - Time.deltaTime > 0.75f)
			{
				Destroy(otherObject.gameObject);
				creepscript.smileroutine(Random.Range (1,3));
				eated = false;
				eatdelay = 0;
			}
			else 
				otherObject.rigidbody.velocity += new Vector3(170, 612, 0);
		}
	}
	
	void OnTriggerStay(Collider otherObject)
	{
		if (otherObject.tag == "Sushi" ||
			otherObject.tag == "Speedball" ||
			otherObject.tag == "Chi")
		{
			if (eated == false)
				otherObject.rigidbody.velocity += new Vector3(170, 612, 0);
			
		}
		
	}
}
