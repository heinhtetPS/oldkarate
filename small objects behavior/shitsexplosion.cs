using UnityEngine;
using System.Collections;

public class shitsexplosion : MonoBehaviour {
	
	public GameObject debris, debris2, debris3, debris4, debris5, debris6;
	
	public GameObject[] debrisbox;
	
	private float ttg;
	
	void Start () {
		
		debrisbox = new GameObject[] {debris, debris2, debris3, debris4, debris5, debris6};
		
		int debrisroll = Random.Range(1,4);
	
		if (debrisroll == 1)
			{
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
			if (debrisroll == 2)
			{
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
			if (debrisroll == 3)
			{
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
	}
	
	// Update is called once per frame
	void Update () {
		
		ttg += Time.deltaTime;
		
		if (ttg > 2)
			Destroy(this.gameObject);
	
	}
}
