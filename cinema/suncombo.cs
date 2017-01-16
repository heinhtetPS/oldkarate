using UnityEngine;
using System.Collections;

public class suncombo : MonoBehaviour {

	Karateoboxnew obox;
	Player playerscript;
	public GameObject sunshine;
	public spinning sunshinespin;
	
	private float yelevation;
	
	void Start () {
		
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		transform.position = new Vector3 (5, -175, 105);
		
	}
	
	// Update is called once per frame
	void Update () {
			
		//general equation forpos
		transform.position = new Vector3 (transform.position.x, -175 + yelevation, 105);
		
		//sun travel max
		if (transform.position.y > 250)
			transform.position = new Vector3 (transform.position.x, 250, transform.position.z);
		
		//during stage
		if (!playerscript.endtime)
		{
			//combo up
			if (obox.combocounter > 1)
			{
				if (yelevation < obox.combocounter * 8.5f)
				yelevation += 0.2f;
				if (yelevation > 425)
					yelevation = 425;
			}
			
			//no combo down
			if (obox.combocounter <= 1)
			{
				yelevation -= 5;	
				if (yelevation < 0)
					yelevation = 0;
			}
			
			//sunshine appearance
			if (obox.combocounter > 30)
				sunshine.active = true;
			else 
				sunshine.active = false;
		}
		
		//stage end
//		if (playerscript.endtime)
//		{
//			sunshine.active = true;
//			yelevation += 0.6f;	
//			if (yelevation > 425)
//				yelevation = 425;
//		}
		
		
		
		
	
	
	}
}
