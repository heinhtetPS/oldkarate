using UnityEngine;
using System.Collections;

public class moveresetobox : MonoBehaviour {
	
	public float movespeed, starttime, xtoresetright, xtoresetleft, ytoresetup, ytoresetdown, newyup, newydown, newxright, newxleft;
	public string direction;
	private float startdelay = 0;
	private float oboxmultiplier;
	
	public exSprite thesprite;
	
	Karateoboxnew obox;
	
	void Start () {
		
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
	
	}
	

	void Update () {
		
		startdelay += Time.deltaTime;
		oboxmultiplier = obox.combocounter * 1.2f;
		
		if (startdelay > starttime)
		{
		
		if (direction == "left")
		transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * (movespeed + oboxmultiplier));
		
		if (direction == "right")
		transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * (movespeed + oboxmultiplier));
		
		if (direction == "up")
		transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * (movespeed + oboxmultiplier));
		
		if (direction == "down")
		transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * (movespeed + oboxmultiplier));
			
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
