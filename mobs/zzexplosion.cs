using UnityEngine;
using System.Collections;

public class zzexplosion : MonoBehaviour {
	
	public GameObject zoomzoom;
	public bool diddamage = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = zoomzoom.transform.position;
	
	}
	
	void OnTriggerStay(Collider otherObject)
	{
		if (otherObject.tag == "Player" && !diddamage)
		{
			Player playerscript = (Player)otherObject.gameObject.GetComponent("Player");
			
			if (playerscript.shielded)
				{
					playerscript.shieldhit++;
					diddamage = true;
					return;
				}
				if (playerscript.blocking)
				{	
					playerscript.gothit++;
					playerscript.loseHealth(1);
					diddamage = true;
					return;
				}
			playerscript.gothit++;
			playerscript.knockback(otherObject.transform.position);
			playerscript.Gethitfunc(transform.position.x);
			playerscript.loseHealth(5);
			diddamage = true;
		}
		
	}
}
