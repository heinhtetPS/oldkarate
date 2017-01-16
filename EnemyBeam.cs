using UnityEngine;
using System.Collections;

public class EnemyBeam : MonoBehaviour {
	
	private float destroydelay, activedelay;


	void Start () {
		
//	obox = (KaratemanOffensivebox)GameObject.FindGameObjectWithTag("Offense").GetComponent("KaratemanOffensivebox");
	collider.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		destroydelay += Time.deltaTime;
		activedelay += Time.deltaTime;
		
		if (activedelay - Time.deltaTime > 0.25f && activedelay - Time.deltaTime < 0.75f)
			collider.enabled = true;
		
		if (destroydelay - Time.deltaTime > 1.5f)
			Destroy(this.gameObject);
	
	}
	
	void OnTriggerEnter (Collider otherObject) 
	{
		if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				enemyscript.FlyAway();
			}
			if (otherObject.tag == "Player")
			{
				Player playerscript = (Player)otherObject.gameObject.GetComponent("Player");
				playerscript.loseHealth(5);
				playerscript.Gethitfunc(transform.position.x);
			}

	}
}
