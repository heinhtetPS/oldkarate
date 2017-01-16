using UnityEngine;
using System.Collections;

public class shadowbehavior : MonoBehaviour {

	float countdown = 0;
	public float timetogo;
	public float speed;
	
	public GameObject Karateman;
	
	// Use this for initialization
	void Start () {
	
		Karateman = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		countdown += Time.deltaTime;
		
		if (countdown - Time.deltaTime > timetogo)
			Destroy(this.gameObject);
		
		transform.Translate(new Vector3(Getdiff().x, Getdiff().y, 0) * speed);
		
		if (transform.position.z != -90)
			transform.position = new Vector3(transform.position.x, transform.position.y, -90);
	
	}
	
	public Vector3 Getdiff()
	{
		Vector3 diff = Karateman.transform.position - transform.position; 
		return diff;
	}
}
