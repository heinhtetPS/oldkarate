using UnityEngine;
using System.Collections;

public class cinematicintro : MonoBehaviour {
	
	private int picnumber = 0, linenumber = 0;
	private float timekeeper = 0, fadenum1 = 0, fadenum2 = 0;
	private Color fadecolor;
	private bool fadingin = false, fadingout = false;
	
	private Texture2D Currentpic;
	private string Currentline;
	
	public GUIStyle regularfont;
	
	private Rect picrect = new Rect(Screen.width / 2 - 160, Screen.height / 2 - 200, 320, 240);
	private Rect textrect = new Rect();
	
	public Texture2D[] cinematicpics;
	private string[] cinematiclines = new string[]
	{
		"...",
		"In a world so similar, yet so different...",
		"Zenix corporation has invented a tool that drastically changed the fate of mankind.",
		"The Gravity Manipulator, a device that activates upon detection of unstable human emotions.",
		"People could no longer live their daily lives without being bounced around against their will.",
		"Soon after, Z-corp came out with a gravity stabilizer, but only offered it to those rich enough to afford it.",
		"Ever since then, the world has been filled with chaos erupting from hordes of the bouncing malcontent.",
		"This is a tale of a victim.  A victim of this chaotic era. A soon-to-be messiah of the weak, an enemy of injustice.",
		"People called him: The Karateman."
	};
	

	void Start () {
		
		
		
		
		picnumber = 1;
		linenumber = 1;
	
	}
	

	void Update () {
		
		textrect = new Rect(picrect.x - 110, picrect.y + 280, 620, 100);
		fadecolor = new Color(255, 255, 255, 0 + fadenum1);
		
		//process for advancing
		//needs to change later on to timing based 
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (fadenum1 == 1)
			fadingout = true;
			
			if (fadenum1 == 0)
			fadingin = true;
		}
		
		//FADE mechanics
		if (fadingout)
		{
			if (fadenum1 > 0)
				fadenum1 -= 0.01f;	
				
			if (fadenum1 <= 0)
			{
				fadenum1 = 0;
				fadingout = false;
				picnumber++;
				linenumber++;
			}
		}
		
		if (fadingin)
		{
			if (fadenum1 < 1)
				fadenum1 += 0.01f;	
				
			if (fadenum1 >= 1)
			{
				fadenum1 = 1;
				fadingin = false;
				
			}
		}
		
		
		//restrictions
		if (picnumber > 8)
			picnumber = 8;
		if (linenumber > 8)
			linenumber = 8;
		
		//updates the pic and lines
		Updateframes();
		
		
	
	}//END update
	
	void Updateframes()
	{
		//setting the pic and lines
		Currentpic = cinematicpics[picnumber];
		Currentline = cinematiclines[linenumber];
		
		
		
	}
	
	void OnGUI() {
		
	GUI.Label(new Rect(0, 200, 400, 100), fadecolor.ToString());	
		
	GUI.color = fadecolor;
		
	//drawing the pic
	GUI.DrawTexture(picrect, Currentpic);
		
		
	//drawing the lines
	GUI.Label(textrect, Currentline, regularfont);
		
		
		
	}//END on GUI
}
