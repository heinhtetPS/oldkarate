using UnityEngine;
using System.Collections;

public class Shurikengeneric : MonoBehaviour {
	
	public GameObject Karateman;
	public float speed = 1.5f;
	
	private float lifetime;
	
	// Use this for initialization
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		lifetime += Time.deltaTime;
		
		if (transform.position.y > -325)
		transform.Rotate(new Vector3(0,0,1) * 400);
		
		
		if (transform.position.y <= -325)
		{
			rigidbody.velocity = Vector3.zero;
			collider.enabled = false;	
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
		
}
