using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {
	
	private float timenow;
	private float gravity, jumpbounce;
	private string type = "Powerup";
	private bool flickeron = false;
	
	float ttg = 0;
	
	private GameObject Karateman;
	private Player playerscript;

	void Start () 
	{
		collider.enabled = false;
		gravity = -10;
		jumpbounce = 322;
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)Karateman.GetComponent("Player");
		
		if (tag == "Money" || tag == "redscroll" || tag == "greenscroll" || tag == "bluescroll")
			rigidbody.velocity = new Vector3(Random.Range(-200, 200), 500, 0);
	
	}
	
	void FixedUpdate ()
	{
		//bounce
		if (tag != "Money")
		{
			if (transform.position.y <= -340)
			gameObject.rigidbody.velocity = new Vector3(0, jumpbounce, 0);
		}
		if (tag == "Money")
		{
			if (transform.position.y <= -340)
			gameObject.rigidbody.velocity = new Vector3(Random.Range(-100, 100), jumpbounce + 100, 0);
		}
			
		//gravity
		if (transform.position.y > -340)
			gameObject.rigidbody.velocity += new Vector3(0 , gravity, 0);
		
		//gbounce effect
		if (playerscript.itembounce && transform.position.y < 0)
			rigidbody.velocity = new Vector3(Getdiff().x * 0.3f, 400, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//variables
		timenow += Time.deltaTime;
		
		//enable collision
		if (timenow - Time.deltaTime > 0.75)
		collider.enabled = true;	
		
		
		
		//fuck 3d
			if (transform.position.z != -110)
				transform.position = new Vector3(transform.position.x, transform.position.y, -110);
		
		//restriction
		if (transform.position.x <= -750)
				Destroy(this.gameObject);
		if (transform.position.x >= 750)
				Destroy(this.gameObject);
		
		
		//ttg for coins
		if (tag == "Money")
		{
			ttg += Time.deltaTime;
			if (ttg >= 8f)
			{
				if (!flickeron)	
				StartCoroutine ( flicker () );
			}
			if (ttg > 10f)
			Destroy(this.gameObject);
			
		}
	
	}
	
	public Vector3 Getdiff()
	{
		Vector3 diff = Karateman.transform.position - transform.position; 
		return diff;
	}
	
	IEnumerator flicker()
	{
		flickeron = true;
		
		while (ttg < 10)
		{
			renderer.enabled = !renderer.enabled;	
			
			yield return new WaitForSeconds(0.1f);
		}
		
		
	}
	
}
