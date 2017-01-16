using UnityEngine;
using System.Collections;

public class ComicControls2 : MonoBehaviour {
	
	public Texture2D bottomframe;
	public GUIStyle blank;
	public GUISkin silverright, silverleft, skip;
	
	private Rect nextbutton = new Rect(Screen.width / 2 + 80, Screen.height - 80, 124, 63);
	private Rect backbutton = new Rect(Screen.width / 2 - 188, Screen.height - 80, 124, 63);
	private Rect middlebutton = new Rect (Screen.width / 2 - 40, Screen.height / 2, 125, 20);
	private Rect bottombox = new Rect(0, Screen.height - 118, Screen.width, 119);
	private Rect skipbutton = new Rect(Screen.width / 2 - 60, Screen.height - 80, 136, 75);
	
	public bool firstframe= true, lastframe = false, arrowson = false, justmoved = false;
	public int Currentframe = 1;
	
	public Texture2D quickstartguide;
	
	private bool demoon = false;
	
	public Camera main;
	public GUISkin pauseskin;
	public GameObject blackbg;
	
	public AudioClip turnpage;

	// Use this for initialization
	void Start () {
		
		main = this.camera;
		StartCoroutine ( showArrows () );
	}
	
	// Update is called once per frame
	void Update () {
		
		
	
	}
	
	IEnumerator showArrows()
	{
		yield return new WaitForSeconds(0.5f);
		
		arrowson = true;
		justmoved = false;
		
	}
	
	void MovetoNextFrame()
	{
		main.transform.position = new Vector3(
			main.transform.position.x + 600, main.transform.position.y, main.transform.position.z);
		Currentframe++;
		arrowson = false;
		justmoved = true;
		StartCoroutine ( showArrows () );
		
	}
	
	void MovetoPreviousFrame()
	{
		main.transform.position = new Vector3(
			main.transform.position.x - 600, main.transform.position.y, main.transform.position.z);
		Currentframe--;
		arrowson = false;
		justmoved = true;
		StartCoroutine ( showArrows () );
		
	}
	
	void MovetoNextRow()
	{
		main.transform.position = new Vector3(
			main.transform.position.x - 1250, main.transform.position.y - 600, main.transform.position.z);
		Currentframe++;
		arrowson = false;
		justmoved = true;
		StartCoroutine ( showArrows () );
		
	}
	
	void MovetoPreviousRow()
	{
		main.transform.position = new Vector3(
			main.transform.position.x + 1250, main.transform.position.y + 600, main.transform.position.z);
		Currentframe--;
		arrowson = false;
		justmoved = true;
		StartCoroutine ( showArrows () );
		
	}
	
	void MovetoFinal()
	{
		main.transform.position = new Vector3(
			main.transform.position.x - 760 , main.transform.position.y - 560, main.transform.position.z);
		Currentframe++;
		arrowson = false;
		justmoved = true;
		StartCoroutine ( showArrows () );
		
	}
	
	IEnumerator Dofadeout(string level)
	{
		Instantiate(blackbg, new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y, -800), Quaternion.Euler(new Vector3(270, 0, 0)));
		
		yield return new WaitForSeconds(1);
		
		
		Application.LoadLevel(level);
	}
	
	void OnGUI()
	{
//		GUI.TextArea(new Rect(0, 20, 125, 20), "CurrentFrame: " + Currentframe.ToString());
//		GUI.TextArea(new Rect(0, 40, 150, 20), "Screensize: " + Screen.width.ToString() + "x" + Screen.height.ToString());
		
		if (GameObject.FindGameObjectWithTag("Fader") == null)
		{
		
		if (arrowson)
		{
			//static bottom bar
			GUI.DrawTexture(bottombox, bottomframe); 
				
			GUI.skin = skip;
			if (GUI.Button(skipbutton, " "))
			{
				demoon = true;
			}
			GUI.skin = null;	
		
			
			
			if (Currentframe != 6)
			{
				GUI.skin = silverright;
				if (GUI.Button(nextbutton, " "))
				{
					audio.PlayOneShot(turnpage);
					if (Currentframe != 3 && Currentframe != 6 && !justmoved)
					MovetoNextFrame();
					if (Currentframe == 3 && !justmoved)
					MovetoNextRow();
					if (Currentframe == 6 && !justmoved)
					MovetoFinal();
				}
				GUI.skin = null;
			}
			
	
			GUI.skin = silverleft;
			if (GUI.Button(backbutton, " "))
			{
				audio.PlayOneShot(turnpage);
				if (Currentframe == 1)
					Application.LoadLevel("LevelSelect");
					
				
				if (Currentframe != 4 && Currentframe != 1 && !justmoved)
					MovetoPreviousFrame();
					if (Currentframe == 4 && !justmoved)
					MovetoPreviousRow();
				
			}
			GUI.skin = null;
			
			GUI.skin = silverright;
			if (Currentframe == 6)
			if (GUI.Button(nextbutton, " "))
				{
					audio.PlayOneShot(turnpage);
					StartCoroutine ( Dofadeout ("Comicpg2") );
				}
			GUI.skin = null;
		}
			
			
				if (demoon)
			{
				GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), quickstartguide);
				if (Input.anyKeyDown)
				StartCoroutine ( Dofadeout ("Level1") );
			}
		}
		
	}
}
