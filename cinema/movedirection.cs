using UnityEngine;
using System.Collections;

public class movedirection : MonoBehaviour {
	
	public float movespeed, starttime;
	public string direction;
	private float startdelay = 0;
	private float age;
	public float ttg;
	
	public GameObject Karateman;
	
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
	
	}
	

	void Update () {
		
		startdelay += Time.deltaTime;
		age += Time.deltaTime;
		
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
			
		if (direction == "follow")
		transform.Translate(Vector3.Normalize(Getdiff()) * Time.deltaTime * movespeed);
			
		}
		
		if (age > ttg)
			Destroy(this.gameObject);
	
	}
	
	public Vector3 Getdiff()
	{
		Vector3 diff = Karateman.transform.position - transform.position; 
		return diff;
	}
}
