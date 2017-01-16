using UnityEngine;
using System.Collections;

public class Naltoknife : MonoBehaviour {
	
	public exSprite kunai;
	private bool gotrandom = false;
	public GameObject Karateman;
	public float speed = 1.5f;
	public GameObject parent; 
	Ninja2 ninscript;
	
	
	bool facingleft = true;
	
	private float lifetime;
	
	// Use this for initialization
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		ninscript = (Ninja2)parent.GetComponent("Ninja2");
		
		if (ninscript.taunted && GameObject.FindGameObjectWithTag("Fake") != null)
		{
			GameObject Fakeman = GameObject.FindGameObjectWithTag("Fake");
			rigidbody.velocity = Getdiff("yes", Fakeman) * speed;
			
		}
		
		if (!ninscript.taunted)
		rigidbody.velocity = Getdiff("yes", Karateman) * speed;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		lifetime += Time.deltaTime;
		
		if (rigidbody.velocity.x > 0 && facingleft)
		{
			kunai.HFlip();	
			facingleft = false;
		}
		if (rigidbody.velocity.x <= 0 && !facingleft)
		{
			kunai.HFlip();	
			facingleft = true;
		}
		
		
		if (transform.position.z != -100)
			transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		if (lifetime >= 5)
			Destroy(this.gameObject);
		
		if (transform.position.x >= 800 || transform.position.x <= -900 || transform.position.y >= 600)
			Destroy(this.gameObject);
	}
	
	public Vector3 Getdiff()
	{
		Vector3 diff = Karateman.transform.position - transform.position; 
		return diff;
	}
		
	public Vector3 Getdiff(string yesorno, GameObject target)
	{
		Vector3 diff = target.transform.position - transform.position;
		
		if (yesorno == "yes")
			diff = Vector3.Normalize(diff);
		
		return diff;
	}
}
