using UnityEngine;
using System.Collections;

public class Carlbehavior : MonoBehaviour {

	Karateoboxnew obox;
	Player playerscript;
	
	bool doingregular = true;
	
	public exSprite carlsprite;
	public exSpriteAnimation carlanim;
	
	void Start () {
		
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
	
			StartCoroutine ( regularcarlactions () );
	}
	
	// Update is called once per frame
	void Update () {
		
		if (obox.combocounter > 20)
		{
			doingregular = false;
			if (!carlanim.IsPlaying("carlcheer"))
			carlanim.Play("carlcheer");
		}
		
		if (obox.combocounter == 0)
		{
			if (!carlanim.IsPlaying("carlflip") && carlanim.IsPlaying("carlcheer"))	
			carlanim.Play("carlflip");
		}
		
		if (!doingregular && obox.combocounter <= 20 && !carlanim.IsPlaying("carlflip"))
		{
			StartCoroutine ( regularcarlactions () );
			
		}
	
	}
	
	IEnumerator regularcarlactions()
	{
		doingregular = true;
		
		if (!carlanim.IsPlaying("carljerk"))
		carlanim.Play("carljerk");
		
		yield return new WaitForSeconds(3);
		
		if (carlanim.IsPlaying("carljerk"))
		carlanim.Play("carlopen");
		
		yield return new WaitForSeconds(2);
		
		doingregular = false;
	}
}
