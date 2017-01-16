using UnityEngine;
using System.Collections;

public class slomocontrols2 : MonoBehaviour {
	
	public movedirection objectscript1;
	public GameObject karatemankick;
	
	public moveandanim karatebust, cyrusbust;
	
	private bool logoon = false, titlenew = false;
	public Texture2D logo, titlescreen;
	private Rect logorect = new Rect(120, 30, 885, 212);
	public Color fade;
	public Color fade2;
	public Color Redbg = new Color(70, 0 ,0 , 1);
	public float fadenum = 0f, fadenum2 = 0f, logonum = 0f;
	
	public float bgchange = 0;
	
	public AudioClip gsmash;
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		fade = new Color(255,255,255, 0.1f + fadenum);
		fade2 = new Color(255, 255, 255, 0.1f + fadenum2);
		bgchange += Time.deltaTime;
		
		//slomo
		if (Input.GetKey(KeyCode.Q))
			Time.timeScale = 0.3f;	
			
		
		//stop fighters
		if (karatemankick.transform.position.x >= -391)
			objectscript1.movespeed = 0;	
		
		
		//karateman mouth
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			karatebust.spriteanims.Play("afroface");
		}
		
		//cyrus mouth
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			cyrusbust.spriteanims.Play("cyrusface");
		}
		
		//fade in logo
		if (Input.GetKey(KeyCode.F) && fade.a <= 1)
		{
			logoon = true;
			fadenum += 0.1f;
		}
		
		//size of logo
		if (Input.GetKey(KeyCode.E))
		{
			logoon = true;
			logonum += 1f;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			titlenew = true;
			fadenum2 += 1f;
			
		}
		
		//fade out screen
		if (Input.GetKey(KeyCode.D) && fade2.a <= 255)
		{
			titlenew = true;
			fadenum2 += 0.01f;
		}
		
		if (titlenew && Input.GetKeyDown(KeyCode.Space))
			Application.LoadLevel("LevelSelect");
	
	}
	
	void OnGUI()
	{
		if (logoon)
		{
		GUI.color = fade;
		GUI.DrawTexture(new Rect(40, 20, 885, 218), logo, ScaleMode.StretchToFill);	
		}
		
		if (titlenew)
		{
			GUI.color = fade2;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), titlescreen);		
		}
		
//		GUI.Label(new Rect(20,20,200,200), logonum.ToString());
		
	}
}
