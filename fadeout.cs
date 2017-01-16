using UnityEngine;
using System.Collections;

public class fadeout : MonoBehaviour {

	private Color fade;
	private float fadenum;
	
	private float timer;
	public float ttf, ttg, rate;
	public GameObject blackscreen;
	
	void Start () {
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		fade = new Color(0,0,0, 1f - fadenum);
		blackscreen.renderer.material.color = fade;
		
		if (timer >= ttf)
		{
			timer+= Time.deltaTime;
			if (fadenum <= 1)
			fadenum += rate;	
			if (timer >= ttg)
				Destroy(this.gameObject);
		}
	
	}
}
