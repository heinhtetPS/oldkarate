using UnityEngine;
using System.Collections;

public class rightsupport : MonoBehaviour {
	
	public GameObject Karateman;
	public float speed = 1f;
	
	private float lifetime;
	
	// Use this for initialization
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");


//		rigidbody.velocity = Getdiffright() * speed;
		
	
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
		
		
		if (transform.position.z != -10)
			transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		
		if (lifetime >= 5)
			Destroy(this.gameObject);
		
		if (transform.position.x >= 800 || transform.position.x <= -900 || transform.position.y >= 600)
			Destroy(this.gameObject);
	}
	
	public Vector3 Getdiffright()
	{
		Vector3 diff = new Vector3(Karateman.transform.position.x, Karateman.transform.position.y + 500, Karateman.transform.position.z)
						- transform.position;
		return diff;
	}
	

		
}
