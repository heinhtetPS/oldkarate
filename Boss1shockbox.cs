using UnityEngine;
using System.Collections;

public class Boss1shockbox : MonoBehaviour {
	
	private Player playerscript;
	
	private float dmgtimer;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (enabled)
			dmgtimer += Time.deltaTime;
	
	}
	
	void OnTriggerEnter (Collider otherObject)
	{
		if (otherObject.tag == "Player")
		{
			playerscript = (Player)otherObject.gameObject.GetComponent("Player");
			if (dmgtimer - Time.deltaTime > 0.5f)
			{
				playerscript.loseHealth(1);
				dmgtimer = 0;
			}
		}
		
	}
	
	void OnTriggerStay (Collider otherObject)
	{
		if (otherObject.tag == "Player")
		{
			playerscript = (Player)otherObject.gameObject.GetComponent("Player");
			if (dmgtimer - Time.deltaTime > 0.5f)
			{
				playerscript.loseHealth(1);
				dmgtimer = 0;
			}
		}
		
	}
	
	void OnGUI()
	{
//		GUI.Label(new Rect(0, 130, 200, 100), "dmgtimer: " + dmgtimer.ToString());
		
	}
}
