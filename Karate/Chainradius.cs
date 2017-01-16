using UnityEngine;
using System.Collections;

public class Chainradius : MonoBehaviour {

	GameObject parentexplo;
	Explosioncollisions exploscript;
	
	void Start () {
		
		collider.enabled = false;
		
		parentexplo = gameObject.transform.parent.gameObject;
		exploscript = (Explosioncollisions)parentexplo.GetComponent("Explosioncollisions");
		
		
		if (exploscript.t2white)
		collider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = parentexplo.transform.position;
		
		if (exploscript.t2white)
		collider.enabled = true;
	
	}
	
	void OnTriggerStay(Collider otherObject)
	{
		if (otherObject.tag == "Sbomb")
		{
			SpiritBomb sbomb = (SpiritBomb)otherObject.gameObject.GetComponent("SpiritBomb");	
			sbomb.DelayedExplosion(0.4f);
		}
	}
}
