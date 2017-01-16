using UnityEngine;
using System.Collections;

public class moveandanim : MonoBehaviour {
	
	public float movespeed, starttime;
	public string direction;
	private float startdelay = 0;
	private float age;
	
	public exSpriteAnimation spriteanims; 
	
	void Start () {
	
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
			
		}
		
		if (age > 30)
			Destroy(this.gameObject);
	
	}
}
