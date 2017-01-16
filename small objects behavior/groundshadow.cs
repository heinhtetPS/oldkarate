using UnityEngine;
using System.Collections;

public class groundshadow : MonoBehaviour {

	GameObject Karateman;
	public exSprite shadowsprite;
	
	float scaler;
	
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
		//position
		transform.position = new Vector3(Karateman.transform.position.x, -340, -120);
		
		//scaler increment
		if (Karateman.transform.position.y > -340)
		{
			scaler = (Karateman.transform.position.y + 340) / 1200;
			
		}
		
		//full shadow
		if (Karateman.transform.position.y <= -340)
			scaler = 0;
		
		//scale changer
		shadowsprite.scale = new Vector2(1 - scaler, 1 - scaler);
		
		//restrictions
		if (transform.rotation != Quaternion.Euler(Vector3.zero))
			transform.rotation = Quaternion.Euler(Vector3.zero);
		
		if (transform.rotation.z != 0)
			transform.rotation = Quaternion.Euler(Vector3.zero);
	
	}
}
