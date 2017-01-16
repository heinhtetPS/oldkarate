using UnityEngine;
using System.Collections;

public class movementtest : MonoBehaviour {
	
	public bool Wtested = false, Stested = false, Atested = false, Dtested = false;
	public int Wnumber, Snumber, Anumber, Dnumber;
	
	bool recordedinput = false;
	float inputdelay;
	
	public GUISkin tutorial;
	private Tutorialshit tutscript;
	
	// Use this for initialization
	void Start () {
		
		tutscript = (Tutorialshit)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.W) && !recordedinput)
		{
			Wnumber ++;	
			if (Wnumber > 3)
				Wnumber = 3;
			recordedinput = true;
		}
		
		if (Input.GetKeyDown(KeyCode.S) && !recordedinput)
		{
			Snumber ++;
			if (Snumber > 3)
				Snumber = 3;
			recordedinput = true;
			
		}
		
		if (Input.GetKeyDown(KeyCode.A) && !recordedinput)
		{
			if (Anumber + Dnumber < 8)
			Anumber ++;	
			recordedinput = true;
			
		}
		
		if (Input.GetKeyDown(KeyCode.D) && !recordedinput)
		{
			if (Anumber + Dnumber < 8)
			Dnumber ++;	
			recordedinput = true;
			
		}
		
		if (Wnumber >= 3)
		Wtested = true;
		
		if (Snumber >= 3)
		Stested = true;
		
		if (Anumber + Dnumber >= 8)
		{
			Atested = true;	
			Dtested = true;
		}
		
		if (Wtested && Stested && Atested && Dtested)
		{
			tutscript.movementcleared = true;	
			StartCoroutine (Delayeddestroy () );
		}
		
		
		//cooldown
		if (recordedinput)
		{
			inputdelay += Time.deltaTime;
			if (inputdelay > 0.1f)
			{
				recordedinput = false;	
				inputdelay = 0;
			}
			
		}
	
	}
	
	void Showmovementstats()
	{
		GUI.skin = tutorial;
		GUI.Label(new Rect(0, 200, 280, 90), 
			  "Horizontal movement: " + (Anumber + Dnumber).ToString() + " / 8" +
			"\nDouble jump: " + (Wnumber).ToString() + " / 3" + 
			"\nQuick Descent: " + (Snumber).ToString() + " / 3");
		
	}
	
	IEnumerator Delayeddestroy()
	{
		yield return new WaitForSeconds(1);	
		
		Destroy(this.gameObject);
	}
	
	void OnGUI()
	{
		Showmovementstats();
		
		
		
	}
	
}
