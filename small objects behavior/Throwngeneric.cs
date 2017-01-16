using UnityEngine;
using System.Collections;

public class Throwngeneric : MonoBehaviour {
	
	private bool gotrandom = false;
	public GameObject Karateman, Parent;
	public exSprite bulletsprite;
	
	public bool justpaused = false;
	private float pausetimer;
	public Vector3 saved;
	
	private float lifetime;
	
	pausemenu pausescript;
	Punkthrower parentscript;
	
	// Use this for initialization
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		pausescript = (pausemenu)Camera.main.GetComponent("pausemenu");
		parentscript = (Punkthrower)Parent.GetComponent("Punkthrower");
		
		//aggro
		if (parentscript.taunted && GameObject.FindGameObjectWithTag("Fake") != null)
		{
			GameObject Fakeman = GameObject.FindGameObjectWithTag("Fake");
			rigidbody.velocity = new Vector3(Getdiff("yes", Fakeman).x, Getdiff("yes", Fakeman).y + 2, 0) * 360;
		}
		
		//real
		if (!parentscript.taunted)
		rigidbody.velocity = new Vector3(Getdiff().x, Getdiff().y + 2, 0) * 360;
		
		//in case of pause
		saved = rigidbody.velocity;
		
	}
	
	void FixedUpdate()
	{
		if (!pausescript.playerpause)
		{
			//gravity
			if (transform.position.y > -300)
				gameObject.rigidbody.velocity += new Vector3(0, -18, 0);
				
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!pausescript.playerpause)
		{
			
			lifetime += Time.deltaTime;
			
			//spinning
			transform.Rotate(new Vector3(0,0,1) * 14);
			
			if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
			
			if (lifetime >= 20)
				Destroy(this.gameObject);
			
			if (transform.position.x >= 800 || transform.position.x <= -900 || transform.position.y >= 600 || transform.position.y <= -500)
				Destroy(this.gameObject);
			
			if (justpaused)
			{
				rigidbody.velocity = saved;
				pausetimer += Time.deltaTime;	
				if (pausetimer >= 0.2f)
				{
					justpaused = false;	
					pausetimer = 0;
				}
			}
			
		}
		
		if (pausescript.playerpause)
		{
			if (!justpaused)
			saved = rigidbody.velocity;
			rigidbody.velocity = Vector3.zero;
			justpaused= true;
		}
		
	}
	
	public Vector3 Getdiff()
	{
		Vector3 diff = Karateman.transform.position - transform.position;
		
		diff = Vector3.Normalize(diff);
		
		return diff;
	}
	
	public Vector3 Getdiff(string yesorno, GameObject target)
	{
		Vector3 diff = target.transform.position - transform.position;
		
		if (yesorno == "yes")
			diff = Vector3.Normalize(diff);
		
		return diff;
	}
	
//	void OnGUI()
//	{
//
//		GUI.Label(new Rect(0, 200, 300, 100), "velo: " + rigidbody.velocity.ToString());
//		GUI.Label(new Rect(0, 220, 300, 100), "jp: " + justpaused.ToString());
//		GUI.Label(new Rect(0, 240, 300, 100), "saved: " + saved.ToString());
//		
//	}
		
}
