using UnityEngine;
using System.Collections;

public class Beam2tip : MonoBehaviour {
	
	private float destroydelay, activedelay;
	public AudioClip hacomon;
	public GameObject Karateman;
	public Player playerscript;
	
	public BoxCollider collider;
	
	public bool blackt2 = false, whitet2 = false;
	private bool alreadydid = false;
	
	public float beamdmgdelay;
	private float mouseX, mouseY, diffX, diffY, cameraDif;
	private Vector3 mWorldPos, mainPos;

	void Start () {
		
	Karateman = GameObject.FindGameObjectWithTag("Player");
	playerscript = (Player)Karateman.GetComponent("Player");
//	audio.PlayOneShot(hacomon);
	collider.enabled = false;
		
	if (PlayerPrefs.GetString("LazerT2") == "black")
			blackt2 = true;
	if (PlayerPrefs.GetString("LazerT2") == "white")
			whitet2 = true;
		
	if (whitet2)
			transform.position = new Vector3(Karateman.transform.position.x, 380, -150);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		destroydelay += Time.deltaTime;
		activedelay += Time.deltaTime;
		
		if (blackt2)
		{
			cameraDif = Camera.main.transform.position.y - transform.position.y;
			mouseX = Input.mousePosition.x;
			mouseY = Input.mousePosition.y;
			mWorldPos = Camera.main.ScreenToWorldPoint( new Vector3(mouseX, mouseY, cameraDif));
			
			diffX = mWorldPos.x - transform.position.x;
		    diffY = mWorldPos.y  - transform.position.y;
	
			float angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
    		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
			
			if (playerscript.MousetotheRight())
			transform.position = new Vector3 (Karateman.transform.position.x + 85, Karateman.transform.position.y + 45, -100);
			
			if (!playerscript.MousetotheRight())
			transform.position = new Vector3 (Karateman.transform.position.x - 85, Karateman.transform.position.y + 45, -100);
		}
		
		if (whitet2)
		{
			transform.rotation = Quaternion.Euler(new Vector3(90, 90, 0));	
			
		}
		
		if (!whitet2 && !blackt2)
		{
			if (playerscript.facingright)
			transform.position = new Vector3 (Karateman.transform.position.x + 90, Karateman.transform.position.y + 35, -100);
			
			if (!playerscript.facingright)
			transform.position = new Vector3 (Karateman.transform.position.x - 90, Karateman.transform.position.y + 35, -100);
			
		}
		
		if (activedelay - Time.deltaTime > 0.25f && activedelay - Time.deltaTime < 1f)
			collider.enabled = true;
		
		if (destroydelay - Time.deltaTime > 4f)
			Destroy(this.gameObject);
		
		if (playerscript.yellowhealth <= 0)
			Destroy(this.gameObject);
	
	}
	
	
}
