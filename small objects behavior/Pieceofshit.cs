using UnityEngine;
using System.Collections;

public class Pieceofshit : MonoBehaviour {
	
	public Vector3 gravity = new Vector3(0, -23, 0);
	private bool gotrandom = false;
	
	private float lifetime;
	
	// Use this for initialization
	void Start () {

		
		rigidbody.velocity = new Vector3(Random.Range (-400, 400), 1200, 0);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		lifetime += Time.deltaTime;
		rigidbody.velocity += gravity;
		
		transform.Rotate(new Vector3(0,0,1) * 400);
		
		
		if (transform.position.y <= -325)
			rigidbody.velocity = new Vector3(Random.Range(-400,400), 500, 0);
		
		if (transform.position.z != -100)
			transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		if (lifetime >= 5)
			Destroy(this.gameObject);
		
		if (transform.position.x >= 800 || transform.position.x <= -900 || transform.position.y >= 600)
			Destroy(this.gameObject);
	}
		
}
