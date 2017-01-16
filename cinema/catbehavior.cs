using UnityEngine;
using System.Collections;

public class catbehavior : MonoBehaviour {
	
	Karateoboxnew obox;
	Player playerscript;
	
	public bool walking = true, jumping = false, facingleft = true;
	
	
	//cat stuff
	public exSprite catsprite;
	public exSpriteAnimation catanim;
	
	
	void Start () {
		
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		catanim.PlayDefault();
	
	}
	
	void FixedUpdate () 
	{	
		//gravity
		if (transform.position.y > -178)
		gameObject.rigidbody.velocity += new Vector3(0, -23, 0);
		
		//jumpbounce
		if (transform.position.y <= -178 && jumping)
		{
			gameObject.rigidbody.velocity = new Vector3(0, 400, 0);	
		}
		
		
	}
	

	void Update () {
		
		if (walking)
		{
			transform.position = new Vector3(transform.position.x, -178, transform.position.z);
			
			
			//on screen & facing right
			if (transform.position.x <= 600 && !facingleft)
			{
				walkright();
			}
			
			//on screen & facing left
			if (transform.position.x >= -600 && facingleft)
			{
				walkleft();
			}
			
			//offscreen right
			if (transform.position.x > 600)
			{
				if (!facingleft)
				catsprite.HFlip();	
				facingleft = true;
			}
			
			//offscreen left
			if (transform.position.x < -600)
			{
				if (facingleft)
				catsprite.HFlip();	
				facingleft = false;
			}
			
		}
		
		if (jumping)
		{
			catanim.Stop();	
		}
		
		if (obox.combocounter == 0)
		{
			if (!catanim.IsPlaying("Catwalk"))
			catanim.Play("Catwalk");
		}
		
		if (obox.combocounter == 10)
		{
			walking = false;
			jumping = true;
		}
		
		if (obox.combocounter < 10)
		{
			walking = true;
			jumping = false;
		}
	
	}
	
	void walkleft ()
	{
		if (!facingleft)
		{
			catsprite.HFlip();	
			facingleft = true;
		}
		transform.Translate(new Vector3(-80, 0, 0) * Time.deltaTime);
		
	}
	
	void walkright ()
	{
		if (facingleft)
		{
			catsprite.HFlip();	
			facingleft = false;
		}
		transform.Translate(new Vector3(80, 0, 0) * Time.deltaTime);
		
	}
	
}
