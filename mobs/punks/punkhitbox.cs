using UnityEngine;
using System.Collections;

public class punkhitbox : MonoBehaviour {
	
	public GameObject mainpunk;
	public Punk1 punkscript;
	public Punk2 punk2script;
	public Punk3 punk3script;
	
	public Collider hitbox;
	
	// Use this for initialization
	void Start () {
		
		if (mainpunk.tag == "Enemy")
		punkscript = (Punk1)mainpunk.GetComponent("Punk1");
		
		if (mainpunk.tag == "Enemy2")
		punk2script = (Punk2)mainpunk.GetComponent("Punk2");
		
		if (mainpunk.tag == "Enemy3")
		punk3script = (Punk3)mainpunk.GetComponent("Punk3");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (mainpunk.tag == "Enemy")
		{
			if (punkscript.facingleft)
			{
				hitbox.transform.position = new Vector3
				(mainpunk.transform.position.x - 50, mainpunk.transform.position.y, mainpunk.transform.position.z);
			}
			
			if (!punkscript.facingleft)
			{
				hitbox.transform.position = new Vector3
				(mainpunk.transform.position.x + 50, mainpunk.transform.position.y, mainpunk.transform.position.z);
			}
			
		}
		
		if (mainpunk.tag == "Enemy2")
		{
			if (punk2script.facingleft)
			{
				hitbox.transform.position = new Vector3
				(mainpunk.transform.position.x - 50, mainpunk.transform.position.y, mainpunk.transform.position.z);
			}
			
			if (!punk2script.facingleft)
			{
				hitbox.transform.position = new Vector3
				(mainpunk.transform.position.x + 50, mainpunk.transform.position.y, mainpunk.transform.position.z);
			}
			
		}
		
		if (mainpunk.tag == "Enemy3")
		{
			if (punk3script.facingleft)
			{
				hitbox.transform.position = new Vector3
				(mainpunk.transform.position.x - 50, mainpunk.transform.position.y, mainpunk.transform.position.z);
			}
			
			if (!punk3script.facingleft)
			{
				hitbox.transform.position = new Vector3
				(mainpunk.transform.position.x + 50, mainpunk.transform.position.y, mainpunk.transform.position.z);
			}
			
		}
	
	}
}
