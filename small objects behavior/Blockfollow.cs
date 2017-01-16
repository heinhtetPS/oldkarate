using UnityEngine;
using System.Collections;

public class Blockfollow : MonoBehaviour {

	public GameObject karateman;
	public Player playerscript;
	
	void Start () {
		
		karateman = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)karateman.GetComponent("Player");
	
	}
	

	void Update () {
		
		if (playerscript.facingright)
		transform.position = new Vector3 (karateman.transform.position.x + 35, 
							karateman.transform.position.y + 20, -100);
		
		if (!playerscript.facingright)
		transform.position = new Vector3 (karateman.transform.position.x - 35, 
							karateman.transform.position.y + 20, -100);
	
	}
}
