using UnityEngine;
using System.Collections;

public class Targetbox : MonoBehaviour {
	
	public GameObject karateman;
	public Player playerscript;

	void Start () {
		
		playerscript = (Player)karateman.gameObject.GetComponent("Player");
	
	}
	

	void Update () {
		
		transform.position = karateman.transform.position;
	
	}
	
	void OnTriggerStay(Collider otherObject)
	{
		if (playerscript.hurricane && PlayerPrefs.GetString("HurricaneT1") == "black")
		{
			if (otherObject.tag == "Enemy" || otherObject.tag == "Enemy2" || otherObject.tag == "Enemy3" ||
				otherObject.tag == "Ground" || otherObject.tag == "Ground2" || otherObject.tag == "Ground3" ||
				otherObject.tag == "Hardcore" || otherObject.tag == "Hardcore2" || otherObject.tag == "Hardcore3" ||
				otherObject.tag == "Ninja1" || otherObject.tag == "Ninja2" ||
				otherObject.tag == "Bomb")
			{
//				if (PlayerPrefs.GetString("HurricaneT2") == "black")
//				{
//				
//					otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position) * -2f;	
//					return;
//				}
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position) * 4f;	
				
			}
			
			
		}
		
		if (playerscript.hurricane && PlayerPrefs.GetString("HurricaneT1") == "white")
		{
			if (otherObject.tag == "Sushi" || otherObject.tag == "Chi" || otherObject.tag == "Money")
			{
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position) * 4;	
				
			}
			
			
		}
		
		
	}
	
	public Vector3 Getdiff(Vector3 enemyposition)
	{
		Vector3 diff = karateman.transform.position - enemyposition; 
		return diff;
	}
}
