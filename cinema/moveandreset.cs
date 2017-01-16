using UnityEngine;
using System.Collections;

public class moveandreset : MonoBehaviour {
	
	public float movespeed, starttime, xtoresetright, xtoresetleft, ytoresetup, ytoresetdown, newyup, newydown, newxright, newxleft;
	public string direction;
	private float startdelay = 0;
	
	public exSprite thesprite;
	
	void Start () {
	
	}
	

	void Update () {
		
		startdelay += Time.deltaTime;
		
		if (startdelay > starttime)
		{
		
		if (direction == "left")
		transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * movespeed);
		
		if (direction == "right")
		transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * movespeed);
		
		if (direction == "up")
		transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * movespeed);
		
		if (direction == "down")
		transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * movespeed);
			
		}
		
		if (direction == "right")
		{
			if (transform.position.x >= xtoresetright)
			transform.position = new Vector3 (newxright, transform.position.y, transform.position.z);
		}
		
		if (direction == "left")
		{
			if (transform.position.x <= xtoresetleft)
			transform.position = new Vector3 (newxleft, transform.position.y, transform.position.z);
			
		}
		
		if (direction == "up")
		{
			if (transform.position.y >= ytoresetup)
			transform.position = new Vector3 (newyup, transform.position.y, transform.position.z);
			
		}
		
		if (direction == "down")
		{
			if (transform.position.y >= ytoresetdown)
			transform.position = new Vector3 (newydown, transform.position.y, transform.position.z);
			
		}
	
	}
}
