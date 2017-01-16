using UnityEngine;
using System.Collections;

public class Shitshooter : MonoBehaviour {
	
	public GameObject parentshit, pieceofshit;
	
	private int randomforshoot;
	private bool gotrandomforshoot = false;
	private float shootCD;
	
	// Use this for initialization
	void Start () {
		
	
	
	}
	
	// Update is called once per frame
	void Update () {
		
		shootCD += Time.deltaTime;
		
		if (!gotrandomforshoot)
			getrandomforshoot();
		if (shootCD > randomforshoot)
		{
			shootshit();	
			shootCD = 0;
			gotrandomforshoot = false;
		}
	
	}
	
	void getrandomforshoot()
	{
		randomforshoot = Random.Range(5, 12);
		gotrandomforshoot = true;
	}
	
	void shootshit()
	{
		Instantiate(pieceofshit, new Vector3(transform.position.x, transform.position.y + 30, transform.position.z), transform.rotation);
	}
}
