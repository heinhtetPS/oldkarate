using UnityEngine;
using System.Collections;

public class Searchrange : MonoBehaviour {

	public GameObject mainpunk;
	public Punk1 punkscript;
	public Punk2 p2script;
	public Punk3 p3script;
	
	pausemenu pausescript;
	
	private bool chaseready = true, gotrandom = false;
	private float chaseCD = 0;
	int chasedelay;
	
	
	void Start () {
		
		if (mainpunk.tag == "Enemy")
		punkscript = (Punk1)mainpunk.GetComponent("Punk1");
		
		if (mainpunk.tag == "Enemy2")
		p2script = (Punk2)mainpunk.GetComponent("Punk2");
		
		if (mainpunk.tag == "Enemy3")
		p3script = (Punk3)mainpunk.GetComponent("Punk3");
		
		pausescript = (pausemenu)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("pausemenu");
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = mainpunk.transform.position;
		
		if (!pausescript.playerpause)
		{
			if (!chaseready)
			{
				if (!gotrandom)
					Getrandom();
				chaseCD += Time.deltaTime;
				if (chaseCD >= chasedelay)
				{
					chaseready = true;	
					chaseCD = 0;
				}
				
				
			}
		}
	
	}
	
	
	void OnTriggerEnter(Collider otherObject)
	{
		if (!pausescript.playerpause)
		{
			if (otherObject.tag == "Player" || otherObject.tag == "Fake")
			{
				if (mainpunk.tag == "Enemy")
				{
					if (punkscript.canhityou && !punkscript.getknocked && !punkscript.falling && !punkscript.tackled
						&& !punkscript.stunned && !punkscript.grabbed && !punkscript.gravitypulled && chaseready)
					punkscript.normalattack();
					gotrandom = false;
					chaseready = false;
					return;
				}
				
				if (mainpunk.tag == "Enemy2")
				{
					if (p2script.canhityou && !p2script.getknocked && !p2script.falling && !p2script.tackled 
						&& !p2script.stunned && !p2script.grabbed && !p2script.gravitypulled && chaseready)
					p2script.normalattack();
					gotrandom = false;
					chaseready = false;
					return;
				}
				
				if (mainpunk.tag == "Enemy3")
				{
					if (p3script.canhityou && !p3script.getknocked && !p3script.falling && !p3script.tackled 
						&& !p3script.stunned && !p3script.grabbed && !p3script.gravitypulled && chaseready)
					{
						if (!p3script.gsmashready)
						p3script.normalattack();
						
						if (p3script.gsmashready)
						p3script.gsmashattack();
						
					}
					gotrandom = false;
					chaseready = false;
					return;
				}
				
			}
		
		}
	}
	
	void Getrandom()
	{
		chasedelay = Random.Range(1, 3);			
		gotrandom = true;	
				
	}
	
//	void OnGUI()
//	{
//		GUI.Label(new Rect(0, 160, 200, 100), "chaseready == " + chaseready.ToString());
//		GUI.Label(new Rect(0, 180, 200, 100), "delay == " + chasedelay.ToString());
//	}

}
