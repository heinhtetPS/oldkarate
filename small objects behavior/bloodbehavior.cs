using UnityEngine;
using System.Collections;

public class bloodbehavior : MonoBehaviour {
	
	float age = 0;
	public bool usettg = false;
	public float ttg;
	private float timer;
	
	// Use this for initialization
	void Start () {
		
		rigidbody.velocity = new Vector3 (Random.Range(-300, 300), Random.Range(-300, 600), 0);
	
	}
	
	
	void FixedUpdate () {
		
		if (transform.position.y > -325)
			gameObject.rigidbody.velocity += new Vector3(0, -18, 0);
		
		
	}
	
	void Update () {
		
		age += Time.deltaTime;
		
		if (transform.position.y > -325)
		transform.Rotate(new Vector3(0,0,1) * 500);
		
		if (transform.position.y <= -325)
			rigidbody.velocity = Vector3.zero;

		if (usettg)
		{
			timer += Time.deltaTime;
			if (timer > ttg)
				Destroy (this.gameObject);
			
		}
		
		if (age > 20)
			Destroy(this.gameObject);
		
		
	}
}
