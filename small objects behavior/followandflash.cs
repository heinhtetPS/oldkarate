using UnityEngine;
using System.Collections;

public class followandflash : MonoBehaviour {

	private GameObject Karateman;
	private float ttg;
	
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		ttg += Time.deltaTime;
		
		transform.position = Karateman.transform.position;
		
		StartCoroutine( flicker () );
		
		if (ttg >= 0.18f)
			Destroy(this.gameObject);
	}
	
	
	IEnumerator flicker()
	{
		yield return new WaitForSeconds(0.06f);
		
		renderer.enabled = false;
		
		yield return new WaitForSeconds(0.06f);
		
		renderer.enabled = true;
		
		yield return new WaitForSeconds(0.06f);
		
		renderer.enabled = false;
		
		
	}
}
