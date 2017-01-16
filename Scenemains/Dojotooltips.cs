using UnityEngine;
using System.Collections;

public class Dojotooltips : MonoBehaviour {
	
	public Dojomain dojoscript;
	public GUISkin dojoskin;
	public GUIStyle comic, heavy;
	
	Rect firstoption = new Rect(-10, 115, 280, 70);
	Rect secondoption = new Rect(-10, 215, 280, 70);
	Rect thirdoption = new Rect(-10, 315, 280, 70);
	Rect fourthoption = new Rect(-10, 415, 280, 70);
	
	private Rect Dojoline1 = new Rect();
	
	private Rect tvmainrect = new Rect(Screen.width / 2 - 213, 115, 468, 360);
	
	private Rect storetextbox = new Rect();
	private Rect storetextsmall = new Rect(Screen.width / 2, Screen.height / 2 - 150, 198, 116);
	private Rect storeafrotext = new Rect(Screen.width / 2, Screen.height / 2, 190, 116);
	
	private Rect Storeline1 = new Rect();
	private Rect Storeline2 = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 110, 500, 100);
	
	private Rect scrollboxmain = new Rect(Screen.width - 210, 115, 210, 616);
	private Rect scrollbox1 = new Rect(Screen.width - 210, 118, 210, 140);
	private Rect scrollbox2 = new Rect(Screen.width - 210, 270, 210, 140);
	private Rect scrollbox3 = new Rect(Screen.width - 210, 422, 210, 140);
	private Rect scrollbox4 = new Rect(Screen.width - 210, 574, 210, 140);
	
	private Rect mouseRect;
	private Vector2 mousePos;
	
	public Texture2D bubblebig, bubble, afrobubble, tv, gsmashicon;
	
	
	// Use this for initialization
	void Start () {
		
		//relative placements
		Dojoline1 = new Rect(tvmainrect.x + 100, tvmainrect.y + 80, 290, 200);
		Storeline1 = new Rect(tvmainrect.x + 100, tvmainrect.y + 95, 300, 200);
		storetextbox = new Rect(tvmainrect.x + 45, tvmainrect.y + 45, 360, 172);
		
			
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		mousePos = new Vector2(Input.mousePosition.x - 5, Screen.height - Input.mousePosition.y - 5);
	
	}
	
	Rect GetmouseRect ()
	{
		if (Input.mousePosition.x < Screen.width / 2)
		mouseRect = new Rect(Input.mousePosition.x + 30, Screen.height - Input.mousePosition.y - 60, 320, 120);	
		
		if (Input.mousePosition.x > Screen.width / 2)
		mouseRect = new Rect(Input.mousePosition.x - 200, Screen.height - Input.mousePosition.y - 60, 320, 120);
		
		return mouseRect;
	}
	
	void OnGUI()
	{
		GUI.depth = -200;	
		GUI.skin = dojoskin;
		
//		GUI.Label (new Rect(100, 100, 100, 100), "Screensize: \n" + Screen.width.ToString() + "\n" + Screen.height.ToString());
		
		
		if (!dojoscript.RunesUI && !dojoscript.StoreUI)
		{
			if (dojoscript.runetooltipon)
			{
				GUI.skin = null;
				//purchase option
				if (firstoption.Contains(mousePos))
				{
					Debug.Log("Where is the text");
					GUI.Label(new Rect(Dojoline1.x, Dojoline1.y + 10, Dojoline1.width, Dojoline1.height), 
						dojoscript.currenttooltip, comic);
				}
				//customize option
				if (secondoption.Contains(mousePos))
				{
					GUI.Label(new Rect(Dojoline1.x, Dojoline1.y + 20, Dojoline1.width, Dojoline1.height), 
						dojoscript.currenttooltip, comic);
				}
				//endlessoption
				if (thirdoption.Contains(mousePos))
				{
					GUI.Label(new Rect(Dojoline1.x, Dojoline1.y + 10, Dojoline1.width, Dojoline1.height), 
						dojoscript.currenttooltip, comic);
				}
				//training option
				if (fourthoption.Contains(mousePos))
				{
					GUI.Label(new Rect(Dojoline1.x, Dojoline1.y + 10, Dojoline1.width, Dojoline1.height), 
						dojoscript.currenttooltip, comic);
				}
				
			}
			
			if (dojoscript.Enterbattlepopup)
			{
				GUI.Label(new Rect(Storeline1.x + 172, Storeline1.y + 20, 180, 55), "Hmph, so you want to test your skills? Lets see how far you go.", comic);
			}
			
		}
		
		if (dojoscript.runetooltipon && dojoscript.RunesUI)
		{
			GUI.skin = dojoskin;
			dojoskin.label.normal.background = tv;
			
			GUI.Label(GetmouseRect(), dojoscript.currenttooltip);
			
			GUI.skin = null;
			dojoskin.label.normal.background = null;
		}
		
		if (dojoscript.runetooltipon)
		{
			if (dojoscript.StoreUI)
			{
				GUI.skin = null;
				
				//general category tabs
				if (firstoption.Contains(mousePos) || secondoption.Contains(mousePos) || thirdoption.Contains(mousePos))
				{
					GUI.DrawTexture(storetextbox, bubblebig);
					GUI.Label(Storeline1, dojoscript.currenttooltip + "\nMake it quick, I must have my afternoon tea.", comic);
				}
				
				
				//inside skills section
				if (dojoscript.storeskills)
				{
					if (dojoscript.bodytrain.Contains(mousePos))
					{
						GUI.Label(new Rect(
							dojoscript.bodytrain.x, dojoscript.bodytrain.y - 20, dojoscript.bodytrain.width, dojoscript.bodytrain.height), 
							dojoscript.currenttooltip, heavy);
						GUI.Label(Storeline1, "Body skills require no chi and typically aid with comboing.", comic);
					}
					
					if (dojoscript.mindtrain.Contains(mousePos))
					{
						GUI.Label(new Rect(
							dojoscript.mindtrain.x, dojoscript.mindtrain.y - 20, dojoscript.mindtrain.width, dojoscript.mindtrain.height), 
							dojoscript.currenttooltip, heavy);
						GUI.Label(Storeline1, "Mind skills require the most chi and have devastating effects.", comic);
					}
					
					if (dojoscript.techtrain.Contains(mousePos))
					{
						GUI.Label(new Rect(
							dojoscript.techtrain.x, dojoscript.techtrain.y - 20, dojoscript.techtrain.width, dojoscript.techtrain.height), 
							dojoscript.currenttooltip, heavy);
						GUI.Label(Storeline1, "Tech skills require moderate chi and typically require strategic usage.", comic);
					}
				}
				
				
				
				
			}
			
			
			
		}
		
		//inside scrolls section
				if (dojoscript.StoreUI && !dojoscript.purchasepopup && !dojoscript.amountchanger && !dojoscript.storeskills && !dojoscript.storerunes)
				{
					if (scrollbox1.Contains(mousePos))
					{
						GUI.DrawTexture(storetextbox, bubblebig);
						GUI.Label(new Rect(scrollbox1.x + 50, scrollbox1.y - 20, 200, 50), "Daily Special", heavy);
						if (!dojoscript.dailybought)
						GUI.Label(new Rect(Storeline1.x + 60, Storeline1.y - 20, Storeline1.width, Storeline1.height), "Today's special is:\n- " + dojoscript.daily1 + " x" + dojoscript.dailyamt1 + "\n- "  
						 																														+ dojoscript.daily2 + " x" + dojoscript.dailyamt2 + "\n- "
																																				+ dojoscript.daily3 + " x" + dojoscript.dailyamt3, comic);
						if (dojoscript.dailybought)
							GUI.Label(Storeline1, "Thank you, check back again tommorrow!", comic);
					}
					
					 
					if (scrollbox2.Contains(mousePos))
					{
						GUI.DrawTexture(storetextbox, bubblebig);
						if (dojoscript.scrolllayout == 1)
						GUI.Label(new Rect(scrollbox2.x + 50, scrollbox2.y - 5, 200, 50), "Red Scroll", heavy);
						if (dojoscript.scrolllayout == 2)
						GUI.Label(new Rect(scrollbox2.x + 50, scrollbox2.y - 5, 200, 50), "Green Scroll", heavy);
						if (dojoscript.scrolllayout == 3)
						GUI.Label(new Rect(scrollbox2.x + 50, scrollbox2.y - 5, 200, 50), "Blue Scroll", heavy);
				

						
						if (dojoscript.scrolllayout == 1)
						GUI.Label(new Rect(Storeline1.x + 10, Storeline1.y + 10, Storeline1.width, Storeline1.height), 
							"Used to craft Body Category skills.", comic);
						if (dojoscript.scrolllayout == 2)
						GUI.Label(new Rect(Storeline1.x + 10, Storeline1.y + 10, Storeline1.width, Storeline1.height), 
							"Used to craft Technique Category skills.", comic);
						if (dojoscript.scrolllayout == 3)
						GUI.Label(new Rect(Storeline1.x + 10, Storeline1.y + 10, Storeline1.width, Storeline1.height), 
							"Used to craft Mind Category skills.", comic);
					}
					
					if (scrollbox3.Contains(mousePos))
					{
						GUI.DrawTexture(storetextbox, bubblebig);
						GUI.Label(new Rect(scrollbox3.x + 50, scrollbox3.y - 5, 200, 50), "Black Scroll", heavy);
						GUI.Label(new Rect(Storeline1.x + 10, Storeline1.y + 10, Storeline1.width, Storeline1.height), 
							"Used to craft a black rune to customize skills.", comic);
					}
					
					if (scrollbox4.Contains(mousePos))
					{
						GUI.DrawTexture(storetextbox, bubblebig);
						GUI.Label(new Rect(scrollbox4.x + 50, scrollbox4.y - 5, 200, 50), "White Scroll", heavy);
						GUI.Label(new Rect(Storeline1.x + 10, Storeline1.y + 10, Storeline1.width, Storeline1.height), 
							"Used to craft a white rune to customize skills.", comic);
					}
					
					
					
				}
		
//		GUI.DrawTexture(scrollbox1, gsmashicon);
//		GUI.DrawTexture(scrollbox2, gsmashicon);
//		GUI.DrawTexture(scrollbox3, gsmashicon);
//		GUI.DrawTexture(scrollbox4, gsmashicon);
		
	}//END ONGUI
}
