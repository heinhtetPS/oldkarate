using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dojomain : MonoBehaviour {

	
	public bool RunesUI = false;
	public bool StoreUI = false;
	public bool Enterbattlepopup = false, choosecheckpoint = false;
	private bool endlesschoices = false, skillquestion = false, runesquestion = false;
	private int runetier = 0, runetype = 0;
	private Rect mouseRect = new Rect();
	private Vector2 mousePos = new Vector2();
	public GameObject blackbg;
	public Font aesmat, comicfont;
	public GUIStyle empty, comic, heavy, newfont, newfont2;
	public GUISkin dojoskin, dojo2, wood, woodright, returnbutton, justqmark;
	private bool help1 = false, help2 = false, help3 = false;
	
	//static UI
	private Rect RunesWindow = new Rect(0, 0, Screen.width, Screen.height);
	private Rect StoreWindow = new Rect(0, 0, Screen.width, Screen.height);
	private Rect sensuipop = new Rect(Screen.width / 2 - 10, Screen.height / 2 - 130, 198, 116);
	private Rect afropop = new Rect();
	private Rect helpbox = new Rect(360, 40, 55, 54);
	private Rect scrollscostbox = new Rect (Screen.width / 2 + 250, Screen.height / 2 - 210, 160, 180);
	private Rect smallboxleftside = new Rect (Screen.width / 2 - 335, Screen.height / 2 - 200, 130, 150);
	private Rect amountbox = new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 300, 150);
	private Rect topleftplate = new Rect(0, 0, 400, 80);
	private Rect statsplate = new Rect(Screen.width - 572, 0, 572, 80);
	
	private Rect devrect = new Rect(Screen.width / 2 - 300, Screen.height - 150, 150, 25);
	
	private Rect tvmainrect = new Rect(Screen.width / 2 - 213, 115, 468, 360);
	float tvoffset = 0;
	private Rect tipscrollrect = new Rect(Screen.width - 260, 115, 220, 494);
	private Rect chatbox = new Rect(Screen.width / 2 - 248, Screen.height / 2 + 165, 538, 150);
	private Rect skillstvbox = new Rect(Screen.width / 2 - 560, 115, 468, 360);
	
	Rect firstoption = new Rect(-10, 115, 280, 70);
	Rect secondoption = new Rect(-10, 215, 280, 70);
	Rect thirdoption = new Rect(-10, 315, 280, 70);
	Rect fourthoption = new Rect(-10, 415, 280, 70);
	float xoffset = 0;
	
	private Rect dojoline1 = new Rect();
	private Rect dojoline2 = new Rect();
	private Rect dojoline3 = new Rect();
	
	private Rect enterbuttonrect = new Rect();
	
	private Rect Storeline1 = new Rect();
	
	//generic rune circles
	private Rect leftcircle = new Rect(Screen.width / 2 - 300, 380, 176, 180);
	private Rect rightcircle = new Rect(Screen.width / 2 + 130, 380, 176, 180);
	private Rect middlecircle = new Rect(Screen.width / 2 - 127, 130, 254, 256);
	
	//ying yangs
	private Rect smallblackleft = new Rect(Screen.width / 2 - 249, 392, 114, 152);
	private Rect smallwhiteleft = new Rect(Screen.width / 2 - 288, 392, 114, 152);
	
	private Rect smallblackright = new Rect(Screen.width / 2 + 181, 392, 114, 152);
	private Rect smallwhiteright = new Rect(Screen.width / 2 + 142, 392, 114, 152);
	
	private Rect bigleft = new Rect(Screen.width / 2 - 109, 145, 164, 222);
	private Rect bigright = new Rect(Screen.width / 2 - 54, 145, 164, 222);
	private Rect bigmain = new Rect (Screen.width / 2 - 70, 227, 167, 166);
	private Rect bigsstrike = new Rect (Screen.width / 2 - 57, 239, 140, 141);
//	private Rect T1tooltipleft = new Rect();
//	private Rect T1tooltipright = new Rect();
//	private Rect T2tooltipleft = new Rect();
//	private Rect T2tooltipright = new Rect();
	private Rect skilllabel = new Rect(Screen.width / 2 - 120, Screen.height / 2 + 60, 230, 200); 
	private Rect Runetooltip = new Rect(Screen.width / 2 - 80, 360, 300, 100);
	
	//runeslot invisible boxes
	private Rect Bigleftbutton = new Rect(Screen.width / 2 - 120, 160, 156, 90);
	private Rect Bigrightbutton = new Rect(Screen.width / 2 - 40, 265, 150, 92);
	private Rect T1left = new Rect(Screen.width / 2 - 290, 400, 92, 70);
	private Rect T1right = new Rect(Screen.width / 2 - 260, 469, 96, 70);
	private Rect T2left = new Rect(Screen.width / 2 + 165, 400, 92, 70);
	private Rect T2right = new Rect(Screen.width / 2 + 191, 469, 96, 70);
	
	//sapecial runeslots
	private Rect Sstrikebox = new Rect(Screen.width / 2 - 250, Screen.height - 80, 510, 60);
	private float gembarYoffset = 0;
	private Rect sapecial1 = new Rect(Screen.width / 2 - 120, Screen.height - 140, 46, 46);
	private Rect sapecial2 = new Rect(Screen.width / 2 - 200, Screen.height - 140, 46, 46);
	private Rect sapecial3 = new Rect(Screen.width / 2 + 60, Screen.height - 140, 46, 46);
	private Rect sapecial4 = new Rect(Screen.width / 2 + 140, Screen.height - 140, 46, 46);
	// -240 and + 180
	
	//skillbar slots
	private Rect Currentbar = new Rect(Screen.width / 2 - 280, Screen.height - 114, 588, 114);
	private Rect firstbarslot = new Rect(Screen.width / 2 - 172, Screen.height - 73, 60, 60);
	private Rect secondbarslot = new Rect(Screen.width / 2 - 82, Screen.height - 73, 60, 60);
	private Rect thirdbarslot = new Rect(Screen.width / 2 + 52, Screen.height - 73, 60, 60);
	private Rect fourthbarslot = new Rect(Screen.width / 2 + 142, Screen.height - 73, 60, 60);
	private Rect sapecialslot = new Rect(Screen.width / 2 - 30, Screen.height - 115, 58, 58);
	
	//rune option buttons
	private Rect t1first = new Rect(Screen.width - 155, 70, 125, 25); 
	private Rect t1second = new Rect(Screen.width - 155, 100, 125, 25); 
	private Rect t2first = new Rect(Screen.width - 155, 160, 125, 25); 
	private Rect t2second = new Rect(Screen.width - 155, 190, 125, 25); 
	private Rect removebox = new Rect(Screen.width / 2 + 234, Screen.height - 80, 125, 25); 
	private Rect backbox = new Rect(20, Screen.height - 60, 125, 25);
	private Rect Storeback = new Rect (20, Screen.height - 60, 162, 51);
	
	//#&#& tag is for replacing shit with new abilities
	
	//skill inventory
	private Rect bodyinventory = new Rect(224, 8, 100, 78);
	private Rect mindinventory = new Rect(9, 8, 100, 78);
	private Rect sakillinventory = new Rect(116, 8, 100, 78);
	public bool binventory = true, minventory = false, skinventory = false;
	private Rect invboxmain = new Rect(0, 100, 200, 100);
	private Rect inventory1 = new Rect();
	private Rect inventory2 = new Rect();
	private Rect inventory3 = new Rect();
	private Rect inventory4 = new Rect();
	private Rect inventory5 = new Rect();
	private Rect inventorynewlabel = new Rect(10, 130, 80, 25);
	
	//Learnskills tv icons
	private Rect bodyiconstv = new Rect();
	private Rect techiconstv = new Rect();
	private Rect mindiconstv = new Rect();
	
	
	//inv rune visuals, empty so that they can follow general inv rect
	//inv1
	private Rect inv1bigleft = new Rect();
	private Rect inv1bigright = new Rect();
	
	private Rect inv1t1left = new Rect();
	private Rect inv1t1right = new Rect();
	private Rect inv1t2left = new Rect();
	private Rect inv1t2right = new Rect();
	
	//inv 2
	private Rect inv2bigleft = new Rect();
	private Rect inv2bigright = new Rect();
	
	private Rect inv2t1left = new Rect();
	private Rect inv2t1right = new Rect();
	private Rect inv2t2left = new Rect();
	private Rect inv2t2right = new Rect();
	
	//inv 3
	private Rect inv3bigleft = new Rect();
	private Rect inv3bigright = new Rect();
	
	private Rect inv3t1left = new Rect();
	private Rect inv3t1right = new Rect();
	private Rect inv3t2left = new Rect();
	private Rect inv3t2right = new Rect();
	
	//inv 4
	private Rect inv4bigleft = new Rect();
	private Rect inv4bigright = new Rect();
	
	private Rect inv4t1left = new Rect();
	private Rect inv4t1right = new Rect();
	private Rect inv4t2left = new Rect();
	private Rect inv4t2right = new Rect();
	
	//inv 5
	private Rect inv5bigleft = new Rect();
	private Rect inv5bigright = new Rect();
	
	private Rect inv5t1left = new Rect();
	private Rect inv5t1right = new Rect();
	private Rect inv5t2left = new Rect();
	private Rect inv5t2right = new Rect();
	
	
	//STORE STUFF
	
	//Learn skills
	public Rect bodytrain = new Rect(70, 280, 500, 60);
	public Rect mindtrain = new Rect(70, 400, 500, 60);
	public Rect techtrain = new Rect(70, 520, 500, 60);
	
	private bool individualskill = false;
	private bool gsmashstats = false, tacklestats = false, forcestats = false, hfiststats = false, finalbodystats = false, spiritbstats = false, tdoorstats = false,
	wallstats = false, clonestats = false, vplatestats = false, hurricanestats = false, lazerstats = false, thirdmindstats = false, serenitystats = false, warudostats = false;
	
	//scrolls
	private Rect scrollboxmain = new Rect(Screen.width - 210, 115, 210, 616);
	private Rect scrollbox1 = new Rect(Screen.width - 155, 140, 102, 48);
	private Rect scrollbox2 = new Rect(Screen.width - 155, 295, 102, 48);
	private Rect scrollbox3 = new Rect(Screen.width - 155, 450, 102, 48);
	private Rect scrollbox4 = new Rect(Screen.width - 155, 605, 102, 48);
	
	//scrollbox layout
	//1 = R & G
	//2 = R & B
	//3 = B & G
	public int scrolllayout;
	
	public bool storeskills = false, storerunes = false, storegambling = false, purchasepopup = false, scrollcost = false, scrollscostwindow = false;
	public bool dailybought = false;
	public string daily1, daily2, daily3;
	public int dailyamt1, dailyamt2, dailyamt3;
	string purchaseitem, learnedability, message;
	int purchaseprice, scrollprice;
	int redcost, greencost, blucost, blackcost, whitecost;
	
	//moving buttons
	private Rect yesbox = new Rect(20, 30, 64, 28);
	private Rect yesdetection = new Rect(Screen.width / 2 - 200, 200, 70, 70);
	private Rect nobox = new Rect(120, 30, 46, 28);
	private Rect nodetection = new Rect(Screen.width / 2 - 100, 200, 50, 70);
	private bool movingon = false, goinup = false, goindown = true;
	private float Yoffset = 0;
	
	//new chatbox
	private int numberofmessages = 0;
	private Rect message1 = new Rect(10, 15, 460, 50);
	private string[] oldmessages = new string[9];
	private Color graycolor = new Color(0.8f, 0.8f, 0.8f, 1);
	private Color almostblack = new Color(0.1f, 0.1f, 0.1f, 1);
	
	//For typewriter effecgt
	private string fullstring;
	private string currentstring;
	
	private int stringcounter = 0, tutorialindex = 0;
	private float stringtimer;
	
	
	//other
	public Texture2D equippedbar, skillsboximg, mainslot, minislot, Sstrikebarimg, moonspeak, gsmashicon, hurricon, beamicon, SPred, SPgreen, SPyellow, SPblue,
	spiritbicon, sereniticon, tdooricon, cloneicon, wallicon, tackleicon, forceicon, sapecialicon, ying, yang, yingalt, yangalt, blank, tv, screenshade, tvbg,
	platebigleftblack, platebigleftwhite, platebigrightblack, platebigrightwhite, platet1white, platet1black, plt2w, plt2b, shinyblue, afroman, tipscroll,
	shinyred, shinygreen, sensui, bubble, bubblesmall, helpbut, helpic1, yestext, notext, scrollssale, redscroll, greenscroll, bluescroll, blackscroll, lefttop, righttop,
	whitescroll, tinyscroll, tinyred, tinygreen, tinyblue, tinyblack, tinywhite, tinycash, leftarrow, chatboxtexture, tolvlselect, shinybox, shinynew, rightarrow,
	hfisticon, finalbodyicon, thirdmindicon, warudoicon, vplateicon, trainskillstv, individualtv, redball, greenball, blueballs, fakebutton;
	//platet1white and platet1black variables are switched, textures are accurate
	
	private int rainbownumber;
	
	public Texture2D purchasetext, customtext, endlesstext, craftbtext, craftwtext, craftsptext, bodytext, mindtext, techtext,
	skillstabtext, runestabtext, scrollstabtext, returntext, enterimg, enterimg2, afropic, rb1, rb2, rb3, rb4, rb5, rb6;
	
	public AudioClip cash;
	
	//ability options for customize
	public bool gsmashoptions = false, tackleoptions = false, forceoptions = false, hfistoptions = false, hurricaneoptions = false, vplateoptions = false, lazeroptions = false, 
	serenityoptions = false, thirdmind = false, finalbodyoptions = false, warudooptions = false, sbomboptions = false, tdooroptions = false, cloneoptions = false, walloptions = false; 
	
	public bool sapecialoptions = false, skillinventory = false, sapecialinventory = false, amountchanger = false, dojotutorial = false, 
	pressenter = false, flashing = false, rainbowon = false;
	
	private float flashtimer = 0, rainbowtimer = 0;
	private int tutorialnumber = 1;
	
	public bool runetooltipon = false, storemouseover = false, dojomouseover = false;
	
	public string Slot1content, Slot2content, Slot3content, Slot4content, Slot5content, CurrentSlot, currenttip;
	
	//tooltip arrays
	public string currenttooltip, tool2, tool3;
	private int tooltipindex, purchasenumber = 1;
	
	
	private string[] generaltips = new string[]
	{
		"Lesson #0:\nGG easy, nubs.",
		"Lesson #1:\nLanding normal (LMB) attacks on enemies is the most reliable way to build your soul meter.",
		"Lesson #2:\nTo avoid negative damage scaling on normal (LMB) attack damage, mix heavy (RMB) attacks into your combos.\nButton-mashing is for babies!",
		"Lesson #3:\nParrying an incoming enemy attack will grant you 1 full bar of your soul meter.",
		"Lesson #4:\nWhen an Axe-kicked enemy collides with other enemies on the ground, both units take damage.",
		"Lesson #5:\nCan't find the scroll you need? The shop may not be fully stocked all the time, so try to remember which enemies drop scrolls.",
		"Lesson #6:\nThe reset for the side dash works the same way as your heavy attack reset; It is refreshed upon hitting an enemy 3 times, or bouncing off the ground.",
		"Lesson #7:\nMaximizing your combo points is most often the best way of improving your stage scores."
	};
	

	private string[] abilitydescriptions = new string[]
	{
		"Smash the ground. Damages goos and nearby enemies.",
		"Use your body to smash enemies against the side of the screen.",
		"Unleash a blast of force, pushing back enemies.",
		"Unleash a lightning-fast barrage of blows hitting all enemies in front.",
		"WIP. Coming Soon!.",
		"An orb of concentrated energy that explodes upon contact with enemies.",
		"A door that traverses planes. Lets you teleport to its location when active.",
		"Summon a shaolin spear from above. Damages enemies upon impact, blocks projectiles.",
		"Creates a clone of yourself can act as a decoy and also damages enemies.",
		"WIP. Coming Soon!!",
		"Become a whirling gale of chi, leaving destruction in your wake.",
		"Gather your chi into a beam of energy that damages enemies.",
		"WIP. Coming soon!!",
		"Train your mind to ignore damage done. Completely blocks the next 3 attacks.",
		"WIP. Coming soon!!"
	};
	
	private string[] gsmashtips = new string[]
	{
		"Groundsmash Rune\nTier 1 Black\nEffect: Increases the radius of Groundsmash by 100%",
		"Groundsmash Rune\nTier 1 White\nEffect: Also will bounce items into the air, towards you.",
		"Groundsmash Rune\nTier 2 Black\nEffect: You learn Piledriver: Grab a nearby enemy and slam them into the ground" +
		" causing massive damage to the target and surrounding area.",
		"Groundsmash Rune\nTier 2 White\nEffect: A tremendous quake unbalances foes, knocking down enemies who are near the ground."
	};
	
	private string[] gsmashtips2 = new string[]
	{
		"Increased radius of damage.",
		"Bounces items towards you.",
		"Learn Piledriver.",
		"Knocks down enemies near the ground."
	};
	
	private string[] tackletips = new string[]
	{
		"Tackle Rune\nTier 1 Black\nEffect: Tackle cannot be stopped by enemies but it does no damage.", 
		"Tackle Rune\nTier 1 White\nEffect: While on cooldown, hitting enemies will make it refresh faster.",
		"Tackle Rune\nTier 2 Black\nEffect: Puts the enemy into a state in which they drop more money.", 
		"Tackle Rune\nTier 2 White\nEffect: Bouncing an enemy off the wall causes them to do damage to other enemies as they return." 
	};
	
	private string[] tackletips2 = new string[]
	{
		"Can't be stopped, but does no damage.", 
		"Hitting enemies lowers cooldown.",
		"Make enemies drop more money.", 
		"Enemies that hit each other receive damage." 
	};
	
	private string[] forcetips = new string[]
	{
		"Forcepush Rune\nTier 1 Black\nEffect: Knocks back less, but deals light damage to enemies.", 
		"Forcepush Rune\nTier 1 White\nEffect: Increases the radius of effect.", 
		"Forcepush Rune\nTier 2 Black\nEffect: Instead of knockback, briefly paralyzes all surrounding enemies.", 
		"Forcepush Rune\nTier 2 White\nEffect: In addition to pushback, conveys a 50% chance to knockdown enemies in the radius."
	};
	
	private string[] forcetips2 = new string[]
	{
		"Knocks back less, deals light damage.", 
		"Increases the radius of effect.", 
		"No knockback but briefly paralyzes enemies.", 
		"50% chance to knockdown enemies."
	};
	
	private string[] hfisttips = new string[]
	{
		"Hfist Rune\nTier 1 Black\nEffect: Summons a weapon with increased size and strength.",
		"Hfist Rune\nTier 1 White\nEffect: Touching the weapon will bless you with increased battle prowess.",
		"Hfist Rune\nTier 2 Black\nEffect: Instead of a shaolin spear, summons a brutal brutal morningstar that damages enemies who touch it.",
		"Hfist Rune\nTier 2 White\nEffect: Appears at mouselocation & allows you to choose its angular rotation. Will launch itself once confirmed."
	};
	
	private string[] finalbodytips = new string[]
	{
		"Hfist Rune\nTier 1 Black\nEffect: Summons a weapon with increased size and strength.",
		"Hfist Rune\nTier 1 White\nEffect: Touching the weapon will bless you with increased battle prowess.",
		"Hfist Rune\nTier 2 Black\nEffect: Instead of a shaolin spear, summons a brutal brutal morningstar that damages enemies who touch it.",
		"Hfist Rune\nTier 2 White\nEffect: Appears at mouselocation & allows you to choose its angular rotation. Will launch itself once confirmed."
	};
	
	private string[] hurritips = new string[]
	{
		"Hurricane Rune\nTier 1 Black\nEffect: Pulls enemies towards you.",
		"Hurricane Rune\nTier 1 White\nEffect: Pulls items towards you.",
		"Hurricane Rune\nTier 2 Black\nEffect: Does less dmg, makes enemies into tornadoes that dmg their friends.",
		"Hurricane Rune\nTier 2 White\nEffect: Slows enemies hit by hurricane."
	};
	
	private string[] hurritips2 = new string[]
	{
		"Pulls enemies towards you.",
		"Pulls items towards you.",
		"Turns enemies into tornados that damage others.",
		"Slows enemies hit."
	};
	
	private string[] lazertips = new string[]
	{
		"Spiritbeam Rune\nTier 1 Black\nEffect: Increased beam size.",
		"Spiritbeam Rune\nTier 1 White\nEffect: Lower cooldown.",
		"Spiritbeam Rune\nTier 2 Black\nEffect: The ability to control the direction of the beam via mouse cursor.",
		"Spiritbeam Rune\nTier 2 White\nEffect: Summon a pillar of light that deals damage to enemies while healing you."
	};
	
	private string[] lazertips2 = new string[]
	{
		"Increased beam size.",
		"Lower cooldown.",
		"Beam can be rotated.",
		"A vertical beam heals you and damages enemies."
	};
	
	private string[] thirdmindtips = new string[]
	{
		"Warudo Rune\nTier 1 Black\nEffect: Summons a weapon with increased size and strength.",
		"Warudo Rune\nTier 1 White\nEffect: Touching the weapon will bless you with increased battle prowess.",
		"Warudo Rune\nTier 2 Black\nEffect: Instead of a shaolin spear, summons a brutal brutal morningstar that damages enemies who touch it.",
		"Warudo Rune\nTier 2 White\nEffect: Appears at mouselocation & allows you to choose its angular rotation. Will launch itself once confirmed."
	};
	
	private string[] serentips = new string[]
	{
		"Serenity Rune\nTier 1 Black\nEffect: blocks more attacks.",
		"Serenity Rune\nTier 1 White\nEffect: Time based instead of hit based.",
		"Serenity Rune\nTier 2 Black\nEffect: searing wind around your body.",
		"Serenity Rune\nTier 2 White\nEffect: heals some health upon use. "
	};
	
	private string[] serentips2 = new string[]
	{
		"Blocks more attacks.",
		"Time-based instead of hit-based.",
		"A damaging aura around your body.",
		"Heals some health upon use. "
	};
	
	private string[] warudotips = new string[]
	{
		"Warudo Rune\nTier 1 Black\nEffect: Summons a weapon with increased size and strength.",
		"Warudo Rune\nTier 1 White\nEffect: Touching the weapon will bless you with increased battle prowess.",
		"Warudo Rune\nTier 2 Black\nEffect: Instead of a shaolin spear, summons a brutal brutal morningstar that damages enemies who touch it.",
		"Warudo Rune\nTier 2 White\nEffect: Appears at mouselocation & allows you to choose its angular rotation. Will launch itself once confirmed."
	};
	
	private string[] spiritbtips = new string[]
	{
		"Spiritbomb Rune\nTier 1 Black\nEffect: larger explo radius.",
		"Spiritbomb Rune\nTier 1 White\nEffect: kick to explode. it will heal you.",
		"Spiritbomb Rune\nTier 2 Black\nEffect: press again to detonate. Gravity effect and halves regular enemies HP. Can't put more than 1 on screen.",
		"Spiritbomb Rune\nTier 2 White\nEffect: kick to explode. it will chain other bombs."
	};
	
	private string[] spiritbtips2 = new string[]
	{
		"Larger explosion radius.",
		"Kick to explode. It will heal you.",
		"Learn Gravity Bomb.",
		"Kick to explode. It will chain other bombs."
	};
	
	private string[] tdoortips = new string[]
	{
		"Teledoor Rune\nTier 1 Black\nEffect: CC guys that come close to it.",
		"Teledoor Rune\nTier 1 White\nEffect: summon on mouselocation. Bring back souvenirs.",
		"Teledoor Rune\nTier 2 Black\nEffect: Eat guys that come close to it.",
		"Teledoor Rune\nTier 2 White\nEffect: Summon on mouselocation. Atk buff for some seconds."
	};
	
	private string[] tdoortips2 = new string[]
	{
		"Stuns a nearby enemy.",
		"Summon on mouselocation. Generates an item.",
		"Eats a nearby enemy.",
		"Summon on mouselocation. Short attack buff."
	};
	
	private string[] clonetips = new string[]
	{
		"Mirror Image Rune\nTier 1 Black\nEffect: no death. Stays until timeout.",
		"Mirror Image Rune\nTier 1 White\nEffect: no timeout. Stays until death.",
		"Mirror Image Rune\nTier 2 Black\nEffect: Make 2 minions.",
		"Mirror Image Rune\nTier 2 White\nEffect: Clone mirrors your actions."
	};
	
	private string[] clonetips2 = new string[]
	{
		"No death. Stays until timeout.",
		"No timeout. Stays until death.",
		"Makes 2 minions.",
		"Clone mirrors your actions."
	};
	
	private string[] walltips = new string[]
	{
		"Wall Rune\nTier 1 Black\nEffect: Summons a weapon with increased size and strength.",
		"Wall Rune\nTier 1 White\nEffect: Touching the weapon will bless you with increased battle prowess.",
		"Wall Rune\nTier 2 Black\nEffect: Instead of a shaolin spear, summons a brutal brutal morningstar that damages enemies who touch it.",
		"Wall Rune\nTier 2 White\nEffect: Appears at mouselocation & allows you to choose its angular rotation. Will launch itself once confirmed."
	};
	
	private string[] walltips2 = new string[]
	{
		"Weapon has increased size and strength.",
		"Weapon blesses you with attack buff.",
		"Summon weapon: Brutal Morningstar.",
		"Summon on mouselocation and launch."
	};

	
	private string[] vplatetips = new string[]
	{
		"Vector Plate Rune\nTier 1 Black\nEffect: Summons a weapon with increased size and strength.",
		"Vector Plate Rune\nTier 1 White\nEffect: Touching the weapon will bless you with increased battle prowess.",
		"Vector Plate Rune\nTier 2 Black\nEffect: Instead of a shaolin spear, summons a brutal brutal morningstar that damages enemies who touch it.",
		"Vector Plate Rune\nTier 2 White\nEffect: Appears at mouselocation & allows you to choose its angular rotation. Will launch itself once confirmed."
	};
	
	private string[] Sstriketips = new string[]
	{
		"Sapeciel Strike Rune\nYellow\nEffect: Explosive strike, Enemy explodes upon death. Cast it twice on the same guy to make him die.",
		"Sapeciel Strike Rune\nGreen\nEffect: Cobra Strike. Poisons the enemy. 0 Damage/sec for 0 seconds. Chance to interrupt their actions.",
		"Sapeciel Strike Rune\nBlue\nEffect: Hadwoken. Ranged attack that deals the same damage as your attack. Small aoe upon impact.",
		"Sapeciel Strike Rune\nRed\nEffect: Vamp. Steal health equivalent to your attack damage."
	};
	
	private string[] tutorialtext = new string[]
	{
		"Ah, its you, again...\nWelcome to my dojo, neophyte. This is a place of peace.\nTry not to break anything.",
		"As you can see, you will be able to train here in order to improve your pitiful skills. But, of course, I do not run these services for free...",
		"Here, you can buy scrolls to learn the ancient secrets of the Karate. Once you have the knowledge, I can teach you new skills.",
		"Here, you will choose which skills to take to battle. Even masters of the Karate cannot know every skill. Customize your skill loadout before embarking on another adventure.",
		"Here, the pride of my dojo: The Endless Tower.\nIt is an illusionary battleground filled with fearsome foes.\nYou may challenge it to test your skills.",
		"Finally, there is the training chamber.\nWhile it won't give you a year's worth of training in a day, It is available to test your rune or skill loadouts."
	};
	
	void Start () {
		
		if (PlayerPrefs.GetInt("Sstrikeabilitystate") > 0)
		{
			PlayerPrefs.SetString("Slot5", "Sstrike");
		}
		
		
		Slot1content = PlayerPrefs.GetString("Slot1");
		Slot2content = PlayerPrefs.GetString("Slot2");
		Slot3content = PlayerPrefs.GetString("Slot3");
		Slot4content = PlayerPrefs.GetString("Slot4");
		Slot5content = PlayerPrefs.GetString("Slot5");
		
		if (PlayerPrefs.GetInt("Dojotutorial") == 0)
			dojotutorial = true;
		else dojotutorial = false;
		
		if (dojotutorial)
		{
			stringcounter = 0;	
			fullstring = tutorialtext[tutorialindex];
			WriteString();
		}
		
		
		//relative placements
		//Afrotv main
		dojoline1 = new Rect(tvmainrect.x + 100, tvmainrect.y + 80, 300, 200);
		Storeline1 = new Rect(tvmainrect.x + 100, tvmainrect.y + 95, 300, 200);
		enterbuttonrect = new Rect(tvmainrect.x + 270, tvmainrect.y + 160, 47, 18);
		afropop = new Rect(tvmainrect.x + 20, tvmainrect.y + 90, 190, 116);
		sensuipop = new Rect(tvmainrect.x + 245, tvmainrect.y + 90, 198, 116);
		
		//learnskillstv
		bodyiconstv = new Rect(skillstvbox.x + 87, skillstvbox.y + 100, 58, 58);
		techiconstv = new Rect(skillstvbox.x + 87, skillstvbox.y + 182, 58, 58);
		mindiconstv = new Rect(skillstvbox.x + 87, skillstvbox.y + 264, 58, 58);
		
		//left side inventory slots
		inventory1 = new Rect(21, invboxmain.y + 20, 58, 58);
		inventory2 = new Rect(21, invboxmain.y + 140, 58, 58);
		inventory3 = new Rect(21, invboxmain.y + 260, 58, 58);
		inventory4 = new Rect(21, invboxmain.y + 380, 58, 58);
		inventory5 = new Rect(21, invboxmain.y + 500, 58, 58);
		
		//inventory content
		//slot 1
		inv1bigleft = new Rect(invboxmain.x + 96, invboxmain.y + 29, 20, 40);
		inv1bigright = new Rect(invboxmain.x + 113, invboxmain.y + 29, 20, 40);
		
		inv1t1left = new Rect(invboxmain.x + 143, invboxmain.y + 30, 18, 14);
		inv1t1right = new Rect(invboxmain.x + 163, invboxmain.y + 30, 18, 14);
		inv1t2left = new Rect(invboxmain.x + 142, invboxmain.y + 52, 22, 20);
		inv1t2right = new Rect(invboxmain.x + 162, invboxmain.y + 52, 22, 20);
		
		//slot 2
		inv2bigleft = new Rect(invboxmain.x + 96, invboxmain.y + 149, 20, 40);
		inv2bigright = new Rect(invboxmain.x + 113, invboxmain.y + 149, 20, 40);
		
		inv2t1left = new Rect(invboxmain.x + 143, invboxmain.y + 150, 18, 14);
		inv2t1right = new Rect(invboxmain.x + 163, invboxmain.y + 150, 18, 14);
		inv2t2left = new Rect(invboxmain.x + 142, invboxmain.y + 172, 22, 20);
		inv2t2right = new Rect(invboxmain.x + 162, invboxmain.y + 172, 22, 20);
		
		//slot 3
		inv3bigleft = new Rect(invboxmain.x + 96, invboxmain.y + 269, 20, 40);
		inv3bigright = new Rect(invboxmain.x + 113, invboxmain.y + 269, 20, 40);
		
		inv3t1left = new Rect(invboxmain.x + 143, invboxmain.y + 270, 18, 14);
		inv3t1right = new Rect(invboxmain.x + 163, invboxmain.y + 270, 18, 14);
		inv3t2left = new Rect(invboxmain.x + 142, invboxmain.y + 292, 22, 20);
		inv3t2right = new Rect(invboxmain.x + 162, invboxmain.y + 292, 22, 20);
		
		//slot 4
		inv4bigleft = new Rect(invboxmain.x + 96, invboxmain.y + 389, 20, 40);
		inv4bigright = new Rect(invboxmain.x + 113, invboxmain.y + 389, 20, 40);
		
		inv4t1left = new Rect(invboxmain.x + 143, invboxmain.y + 390, 18, 14);
		inv4t1right = new Rect(invboxmain.x + 163, invboxmain.y + 390, 18, 14);
		inv4t2left = new Rect(invboxmain.x + 142, invboxmain.y + 412, 22, 20);
		inv4t2right = new Rect(invboxmain.x + 162, invboxmain.y + 412, 22, 20);
		
		//slot 5
		inv5bigleft = new Rect(invboxmain.x + 96, invboxmain.y + 509, 20, 40);
		inv5bigright = new Rect(invboxmain.x + 113, invboxmain.y + 509, 20, 40);
		
		inv5t1left = new Rect(invboxmain.x + 143, invboxmain.y + 510, 18, 14);
		inv5t1right = new Rect(invboxmain.x + 163, invboxmain.y + 510, 18, 14);
		inv5t2left = new Rect(invboxmain.x + 142, invboxmain.y + 532, 22, 20);
		inv5t2right = new Rect(invboxmain.x + 162, invboxmain.y + 532, 22, 20);
	
		//randomized stuff
		currenttip = generaltips[Random.Range(0, generaltips.Length)];
		scrolllayout = Random.Range(1, 4);
		RandomizeDaily();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		mousePos = new Vector2(Input.mousePosition.x - 5, Screen.height - Input.mousePosition.y - 5);
		
		//flashing image
		if (pressenter)
		{
			flashtimer += Time.deltaTime;
			
			if (flashtimer > 0.5f)
			{
				flashing = !flashing;	
				flashtimer = 0;
			}
		}
		
		
		//pressing enter stuff
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (pressenter)
			{
				if (stringcounter >= fullstring.Length)
				{
					tutorialnumber++;
					tutorialindex++;
					stringcounter = 0;
					fullstring = tutorialtext[tutorialindex];
				}
				else
				{
					stringcounter = fullstring.Length;
					currentstring = fullstring;
				}
			}
		}
		
		//rainbow activation
		if (PlayerPrefs.GetInt("Newabilitynotice") > 0)
			rainbowon = true;
		else rainbowon = false;
		
		//rainbow
		if (rainbowon)
		{
			rainbowtimer += Time.deltaTime;
			
			if (rainbowtimer > 0.02f)
			{
				rainbownumber++;
				if (rainbownumber > 6)
					rainbownumber = 1;
				rainbowtimer = 0;
			}
		}
		
		//moving boxes for storeskills and runes
		if (storeskills || storerunes)
		{
			if (xoffset > -270)
				xoffset -= 6;
			
			if (tvoffset < 140)
				tvoffset += 4;
		}
		
		//moving for gembar
		if (sapecialoptions)
		{
			if (gembarYoffset < 60)
			{
				gembarYoffset++;	
			}
			
		}
		if (!sapecialoptions)
		{
			if (gembarYoffset > 0)
				gembarYoffset--;
			
		}
		
			//side bar options
			firstoption = new Rect(-10 + xoffset, firstoption.y, firstoption.width, firstoption.height);	
			secondoption = new Rect(-10 + xoffset, secondoption.y, secondoption.width, secondoption.height);	
			thirdoption = new Rect(-10 + xoffset, thirdoption.y, thirdoption.width, thirdoption.height);	
			fourthoption = new Rect(-10 + xoffset, fourthoption.y, fourthoption.width, fourthoption.height);	
			
			//afrotv and chatbox
			tvmainrect = new Rect(Screen.width / 2 - 213 + tvoffset, tvmainrect.y, tvmainrect.width, tvmainrect.height);
			afropop = new Rect(Screen.width / 2 - 193 + tvoffset, afropop.y, afropop.width, afropop.height);
			sensuipop = new Rect(Screen.width / 2 + 32 + tvoffset, sensuipop.y, sensuipop.width, sensuipop.height);
			chatbox = new Rect(Screen.width / 2 - 248 + tvoffset, chatbox.y, chatbox.width, chatbox.height);
			yesdetection = new Rect(Screen.width / 2 - 180 + tvoffset, yesdetection.y, yesdetection.width, yesdetection.height);
			nodetection = new Rect(Screen.width / 2 - 80 + tvoffset, nodetection.y, nodetection.width, nodetection.height);
		
			//gembar
			Sstrikebox = new Rect(Screen.width / 2 - 250, Screen.height - 80 - gembarYoffset, 510, 60);
			
		
		if (!storeskills && !storerunes)
		{
			if (xoffset < 0)
				xoffset += 6;
			if (tvoffset > 0)
				tvoffset -= 4;
			
		}
		
		//TOOLTIP STUFF FOR DJTP
		#region Tooltips
		runetooltipon = false;
		
		//Main dojo options
		if (!RunesUI && !StoreUI && !dojotutorial && !Enterbattlepopup)
		{
			if (firstoption.Contains(mousePos) || secondoption.Contains(mousePos) || thirdoption.Contains(mousePos) || fourthoption.Contains(mousePos))
				dojomouseover = true;
			else dojomouseover = false;
			
			if (firstoption.Contains(mousePos))
			{
				currenttooltip = "Purchase scrolls, runes or discover your hidden talents. All these ethereal gifts in exchange for mere common currency.";
				runetooltipon = true;
			}
			
			if (secondoption.Contains(mousePos))
			{
				currenttooltip = "Runecraft is an intricate art used to craft wonders. Customize your newfound abilities to suit your playstyle.";
				runetooltipon = true;
			}
			
			if (thirdoption.Contains(mousePos) && !Enterbattlepopup)
			{
				currenttooltip = "The endless tower, an illusionary training ground. Though many have tried, none have conquered its heights. You wish to challenge the tower with your meager skills?";
				runetooltipon = true;
			}
			
			if (fourthoption.Contains(mousePos))
			{
				currenttooltip = "Training is the route to mastery of any craft. Use the facilities here to test your skill or rune loadouts. Click on the help option once inside to see even more options.";
				runetooltipon = true;
			}
			
//			if (Enterbattlepopup)
//			{
//				currenttooltip = "Are you certain? Hmph, so you want to test your skills? Let's see how far you can go.";
//				runetooltipon = true;
//			}
			
		}
		
		//for rune customization section
		if (RunesUI)
		{
			dojoskin.label.fontSize = 16;
			
			//topleft moontab
			if (bodyinventory.Contains(mousePos))
			{
				currenttooltip = "Body\nA warrior's bread and butter. These abilities do not cost chi, for they utilize your raw power.";
				runetooltipon = true;
			}
	
			if (mindinventory.Contains(mousePos))
			{
				currenttooltip = "Mind\nThese abilities cost the most chi and often have devastating effects.";
				runetooltipon = true;
			}
	
			if (sakillinventory.Contains(mousePos))
			{
				currenttooltip = "Tech\nThese abilities cost some chi and can be used in strategic ways.";
				runetooltipon = true;
			}
	
			//inventory
			if (skillinventory)
			{
				if (binventory)
				{
					if (inventory1.Contains(mousePos) && PlayerPrefs.GetInt("Groundsmashabilitystate") > 0)
					{
						currenttooltip = "Groundsmash\nCost: no cost\nCooldown: 2 Seconds\n" + abilitydescriptions[0];
						runetooltipon = true;
					}
					if (inventory2.Contains(mousePos) && PlayerPrefs.GetInt("Tackleabilitystate") > 0)
					{
						currenttooltip = "Tackle\nCost: no cost\nCooldown: 2 seconds\n" + abilitydescriptions[1];
						runetooltipon = true;
					}
					if (inventory3.Contains(mousePos) && PlayerPrefs.GetInt("Forceabilitystate") > 0)
					{
						currenttooltip = "Force Push\nCost: no cost\nCooldown: 3 seconds\n" + abilitydescriptions[2];
						runetooltipon = true;
					}
					if (inventory4.Contains(mousePos) && PlayerPrefs.GetInt("Hfistabilitystate") > 0)
					{
						currenttooltip = "Hundred Fist\nCost: no cost\nCooldown: 5 seconds\n" + abilitydescriptions[3];
						runetooltipon = true;
					}
					if (inventory5.Contains(mousePos) && PlayerPrefs.GetInt("Finalbodyabilitystate") > 0)
					{
						currenttooltip = "Final Body\nCost: no cost\nCooldown: 5 seconds\n" + abilitydescriptions[4]; 
						runetooltipon = true;
					}

				}
				
				if (minventory)
				{
					if (inventory1.Contains(mousePos) && PlayerPrefs.GetInt("Hurricaneabilitystate") > 0)
					{
						currenttooltip = "Hurricane\nCost: 3 Chi\nCooldown: 3 Seconds\n" + abilitydescriptions[10];
						runetooltipon = true;
					}
					if (inventory2.Contains(mousePos) && PlayerPrefs.GetInt("Lazerabilitystate") > 0)
					{
						
						currenttooltip = "Spiritbeam\nCost: 4 Chi\nCooldown: 15 Seconds\n" + abilitydescriptions[11];
						runetooltipon = true;
					}
					

					if (inventory3.Contains(mousePos) && PlayerPrefs.GetInt("Thirdmindabilitystate") > 0)
					{
						
						currenttooltip = "Third Mind\nCost: all Chi\nCooldown: 15 Seconds\n" + abilitydescriptions[12];
						runetooltipon = true;
					}
					
					if (inventory4.Contains(mousePos) && PlayerPrefs.GetInt("Serenityabilitystate") > 0)
					{
						
						currenttooltip = "Serenity\nCost: 4 Chi\nCooldown: 15 Seconds\n" + abilitydescriptions[13];
						runetooltipon = true;
					}
					
					if (inventory5.Contains(mousePos) && PlayerPrefs.GetInt("Warudoabilitystate") > 0)
					{
						
						currenttooltip = "Time Stop\nCost: some hp/sec\nCooldown: N/A\n" + abilitydescriptions[14];
						runetooltipon = true;
					}
					
				}
				
				if (skinventory)
				{
					if (inventory1.Contains(mousePos) && PlayerPrefs.GetInt("Spiritbombabilitystate") > 0)
					{
						currenttooltip = "Spiritbomb\nCost: 1 Chi\nCooldown: None\n" + abilitydescriptions[5];
						runetooltipon = true;
					}
					
					if (inventory2.Contains(mousePos) && PlayerPrefs.GetInt("Cloneabilitystate") > 0)
					{
						currenttooltip = "Mirror Image\nCost: 1 chi\nCooldown: 1 Seconds\n" + abilitydescriptions[6];
						runetooltipon = true;
					}
					
					if (inventory3.Contains(mousePos) && PlayerPrefs.GetInt("Teledoorabilitystate") > 0)
					{
						currenttooltip = "Teledoor\nCost: 1 Chi\nCooldown: None.\n" + abilitydescriptions[7];
						runetooltipon = true;
					}
					
					if (inventory4.Contains(mousePos) && PlayerPrefs.GetInt("Wallabilitystate") > 0)
					{
						currenttooltip = "Aegis Weapon\nCost: 1 chi\nCooldown: none\n" + abilitydescriptions[8];
						runetooltipon = true;
					}
					
					if (inventory5.Contains(mousePos) && PlayerPrefs.GetInt("Vplateabilitystate") > 0)
					{
						currenttooltip = "Vector Plate\nCost: 1 chi\nCooldown: none\n" + abilitydescriptions[9];
						runetooltipon = true;
					}
					
				}
			}
	
			// Equipped runes
			if (gsmashoptions || hurricaneoptions || lazeroptions || sbomboptions || serenityoptions || tdooroptions || thirdmind ||
				cloneoptions || walloptions || forceoptions || tackleoptions || hfistoptions || warudooptions || vplateoptions || finalbodyoptions)
			{
				if (Bigleftbutton.Contains(mousePos))
				{
					if (IsT1runethere())
					{
						currenttooltip = "Remove Rune";
						runetooltipon = true;
					}
				}
				
				if (Bigrightbutton.Contains(mousePos))
				{
					if (IsT2runethere())
					{
						currenttooltip = "Remove Rune";
						runetooltipon = true;
					}
				}
				
			}
	
			//Available runes
			if (gsmashoptions)
			{
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") >= 2 && PlayerPrefs.GetInt("Groundsmashabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = gsmashtips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = gsmashtips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") >= 4 && PlayerPrefs.GetInt("Groundsmashabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = gsmashtips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = gsmashtips[3];
						runetooltipon = true;
					}
				}
			}
			
			//2
			if (tackleoptions)
			{
				if (PlayerPrefs.GetInt("Tackleabilitystate") >= 2 && PlayerPrefs.GetInt("Tackleabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = tackletips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Tackleabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = tackletips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Tackleabilitystate") >= 4 && PlayerPrefs.GetInt("Tackleabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = tackletips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Tackleabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = tackletips[3];
						runetooltipon = true;
					}
				}
			}
			
			//3
			if (forceoptions)
			{
				if (PlayerPrefs.GetInt("Forceabilitystate") >= 2 && PlayerPrefs.GetInt("Forceabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = forcetips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Forceabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = forcetips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Forceabilitystate") >= 4 && PlayerPrefs.GetInt("Forceabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = forcetips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Forceabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = forcetips[3];
						runetooltipon = true;
					}
				}
			}
			
			//4
			if (hfistoptions)
			{
				if (PlayerPrefs.GetInt("Hfistabilitystate") >= 2 && PlayerPrefs.GetInt("Hfistabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = hfisttips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Hfistabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = hfisttips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Hfistabilitystate") >= 4 && PlayerPrefs.GetInt("Hfistabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = hfisttips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Hfistabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = hfisttips[3];
						runetooltipon = true;
					}
				}
			}
			
			//5 
			if (finalbodyoptions)
			{
				if (PlayerPrefs.GetInt("Finalbodyabilitystate") >= 2 && PlayerPrefs.GetInt("Finalbodyabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = finalbodytips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Finalbodyabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = finalbodytips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Finalbodyabilitystate") >= 4 && PlayerPrefs.GetInt("Finalbodyabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = finalbodytips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Finalbodyabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = finalbodytips[3];
						runetooltipon = true;
					}
				}
			}
			
			//6
			if (hurricaneoptions)
			{
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") >= 2 && PlayerPrefs.GetInt("Hurricaneabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = hurritips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = hurritips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") >= 4 && PlayerPrefs.GetInt("Hurricaneabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = hurritips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = hurritips[3];
						runetooltipon = true;
					}
				}
			}
	
			//7
			if (lazeroptions)
			{
				if (PlayerPrefs.GetInt("Lazerabilitystate") >= 2 && PlayerPrefs.GetInt("Lazerabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = lazertips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Lazerabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = lazertips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Lazerabilitystate") >= 4 && PlayerPrefs.GetInt("Lazerabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = lazertips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Lazerabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = lazertips[3];
						runetooltipon = true;
					}
				}
			}
			
			//8 
			if (thirdmind)
			{
				if (PlayerPrefs.GetInt("Thirdmindabilitystate") >= 2 && PlayerPrefs.GetInt("Thirdmindabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = thirdmindtips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Thirdmindabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = thirdmindtips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Thirdmindabilitystate") >= 4 && PlayerPrefs.GetInt("Thirdmindabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = thirdmindtips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Thirdmindabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = thirdmindtips[3];
						runetooltipon = true;
					}
				}
			}
			
			//9
			if (serenityoptions)
			{
				if (PlayerPrefs.GetInt("Serenityabilitystate") >= 2 && PlayerPrefs.GetInt("Serenityabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = serentips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Serenityabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = serentips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Serenityabilitystate") >= 4 && PlayerPrefs.GetInt("Serenityabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = serentips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Serenityabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = serentips[3];
						runetooltipon = true;
					}
				}
			}
			
			//10
			if (warudooptions)
			{
				if (PlayerPrefs.GetInt("Warudoabilitystate") >= 2 && PlayerPrefs.GetInt("Warudoabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = warudotips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Warudoabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = warudotips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Warudoabilitystate") >= 4 && PlayerPrefs.GetInt("Warudoabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = warudotips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Warudoabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = warudotips[3];
						runetooltipon = true;
					}
				}
			}
	
			//11
			if (sbomboptions)
			{
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") >= 2 && PlayerPrefs.GetInt("Spiritbombabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = spiritbtips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = spiritbtips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") >= 4 && PlayerPrefs.GetInt("Spiritbombabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = spiritbtips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = spiritbtips[3];
						runetooltipon = true;
					}
				}
			}
	
			//12
			if (tdooroptions)
			{
				if (PlayerPrefs.GetInt("Teledoorabilitystate") >= 2 && PlayerPrefs.GetInt("Teledoorabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = tdoortips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Teledoorabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = tdoortips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Teledoorabilitystate") >= 4 && PlayerPrefs.GetInt("Teledoorabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = tdoortips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Teledoorabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = tdoortips[3];
						runetooltipon = true;
					}
				}
			}
	
			//13
			if (cloneoptions)
			{
				if (PlayerPrefs.GetInt("Cloneabilitystate") >= 2 && PlayerPrefs.GetInt("Cloneabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = clonetips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Cloneabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = clonetips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Cloneabilitystate") >= 4 && PlayerPrefs.GetInt("Cloneabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = clonetips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Cloneabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = clonetips[3];
						runetooltipon = true;
					}
				}
			}
	
			//14
			if (walloptions)
			{
				if (PlayerPrefs.GetInt("Wallabilitystate") >= 2 && PlayerPrefs.GetInt("Wallabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = walltips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Wallabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = walltips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Wallabilitystate") >= 4 && PlayerPrefs.GetInt("Wallabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = walltips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Wallabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = walltips[3];
						runetooltipon = true;
					}
				}
			}
			
			//15 
			if (vplateoptions)
			{
				if (PlayerPrefs.GetInt("Vplateabilitystate") >= 2 && PlayerPrefs.GetInt("Vplateabilitystate") != 3)	
				{
					if (T1right.Contains(mousePos))
					{
						currenttooltip = vplatetips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Vplateabilitystate") >= 3)	
				{
					if (T1left.Contains(mousePos))
					{
						currenttooltip = vplatetips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Vplateabilitystate") >= 4 && PlayerPrefs.GetInt("Vplateabilitystate") != 6)	
				{
					if (T2right.Contains(mousePos))
					{
						currenttooltip = vplatetips[2];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Vplateabilitystate") >= 5)	
				{
					if (T2left.Contains(mousePos))
					{
						currenttooltip = vplatetips[3];
						runetooltipon = true;
					}
				}
			}
	
			if (sapecialoptions)
			{
				
				if (PlayerPrefs.GetInt("Sstrikeabilitystate") == 1)	
				{
					if (sapecial2.Contains(mousePos))
					{
						currenttooltip = Sstriketips[1];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Sstrikeabilitystate") == 2)	
				{
					if (sapecial2.Contains(mousePos))
					{
						currenttooltip = Sstriketips[1];
						runetooltipon = true;
					}
					
					if (sapecial1.Contains(mousePos))
					{
						currenttooltip = Sstriketips[0];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Sstrikeabilitystate") == 3)	
				{
					if (sapecial2.Contains(mousePos))
					{
						currenttooltip = Sstriketips[1];
						runetooltipon = true;
					}
					
					if (sapecial1.Contains(mousePos))
					{
						currenttooltip = Sstriketips[0];
						runetooltipon = true;
					}
					
					if (sapecial4.Contains(mousePos))
					{
						currenttooltip = Sstriketips[3];
						runetooltipon = true;
					}
				}
				
				if (PlayerPrefs.GetInt("Sstrikeabilitystate") >= 4)	
				{
					if (sapecial2.Contains(mousePos))
					{
						currenttooltip = Sstriketips[1];
						runetooltipon = true;
					}
					
					if (sapecial1.Contains(mousePos))
					{
						currenttooltip = Sstriketips[0];
						runetooltipon = true;
					}
					
					if (sapecial4.Contains(mousePos))
					{
						currenttooltip = Sstriketips[3];
						runetooltipon = true;
					}
					
					if (sapecial3.Contains(mousePos))
					{
						currenttooltip = Sstriketips[2];
						runetooltipon = true;
					}
				}
			}
			

				
		}//END RUNES UI tooltips
		
		//BEGIN store tooltips
		if (StoreUI && !storeskills && !storerunes)
		{
			if (firstoption.Contains(mousePos) || secondoption.Contains(mousePos) || thirdoption.Contains(mousePos)||
					scrollbox1.Contains(mousePos) || scrollbox2.Contains(mousePos) || scrollbox3.Contains(mousePos) || scrollbox4.Contains(mousePos))
				storemouseover = true;
			else storemouseover = false;
			
			if (firstoption.Contains(mousePos))
			{
				currenttooltip = "You wish to learn new abilities?";
				runetooltipon = true;
			}
			
			if (secondoption.Contains(mousePos))
			{
				currenttooltip = "You wish to discover ways to refine your abilities?";
				runetooltipon = true;
			}
			
			if (thirdoption.Contains(mousePos))
			{
				currenttooltip = "You wish to test your luck against me?";
				runetooltipon = true;
			}
			
//			//scrolls stuff
//				if (scrollbox1.Contains(mousePos))
//				{
//					currenttooltip = "Daily Special";
//					runetooltipon = true;
//				}
//				
//				if (scrollbox2.Contains(mousePos))
//				{
//					if (scrolllayout == 1)
//					currenttooltip = "Red Scroll";
//					if (scrolllayout == 2)
//					currenttooltip = "Green Scroll";
//					if (scrolllayout == 3)
//					currenttooltip = "Blue Scroll";
//					runetooltipon = true;
//					
//				}
//				
//				if (scrollbox3.Contains(mousePos))
//				{
//					currenttooltip = "Black Scroll";
//					runetooltipon = true;
//					
//				}
//				
//				if (scrollbox4.Contains(mousePos))
//				{
//					currenttooltip = "White Scroll";
//					runetooltipon = true;
//					
//				}
			
			
		}//store UI
			
			
			
		#endregion
		
		//floaty choices
		if (purchasepopup || Enterbattlepopup || skillquestion || runesquestion)
		{
			if (yesdetection.Contains(mousePos))
			{
				movingon = true;
			}
			
			if (nodetection.Contains(mousePos))
			{
				movingon = true;
			}
			
			if (!yesdetection.Contains(mousePos) && !nodetection.Contains(mousePos))
				movingon = false;
			
			if (movingon)
			{
				if (goinup)
				{
					Yoffset -= 0.6f;
					if (Yoffset < -6)
					{
						goinup = false;
						goindown = true;
					}
				}
					
				if (goindown)
				{
					Yoffset += 0.6f;
					if (Yoffset > 6)
					{
						goindown = false;
						goinup = true;
					}
				}
			}
			if (!movingon)
			{
				Yoffset = 0;
			}
		}
		
		//make sure these numbers make sense
		if (PlayerPrefs.GetInt("Money") < 0)
			PlayerPrefs.SetInt("Money", 0);
		if (PlayerPrefs.GetInt("Redscroll") < 0)
			PlayerPrefs.SetInt("Redscroll", 0);
		if (PlayerPrefs.GetInt("Greenscroll") < 0)
			PlayerPrefs.SetInt("Greenscroll", 0);
		if (PlayerPrefs.GetInt("Bluescroll") < 0)
			PlayerPrefs.SetInt("Bluescroll", 0);
		if (PlayerPrefs.GetInt("Blackscroll") < 0)
			PlayerPrefs.SetInt("Blackscroll", 0);
		if (PlayerPrefs.GetInt("Whitescroll") < 0)
			PlayerPrefs.SetInt("Whitescroll", 0);
		
		
		

	}//end update
	
	Rect GetmouseRect ()
	{
		mouseRect = new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 60, 300, 100);	
		return mouseRect;
	}
	
	//The thing that determines what runes to equip when you click on the runes
	//needs no customization per ability
	void Checkabilitystate(string abilitystate, string[] tooltip)
	{
		if (PlayerPrefs.GetInt(abilitystate) == 2)
		{
			GUI.DrawTexture(smallblackleft, ying);
			if (GUI.Button(T1right, new GUIContent (" ", newRunetext(0)), empty))
			{
				equipT1b();
			}
		}

		
		if (PlayerPrefs.GetInt(abilitystate) == 3)
		{
			GUI.DrawTexture(smallwhiteleft, yang);
			if (GUI.Button(T1left, new GUIContent (" ", newRunetext(1)), empty))
			{
				equipT1w();
			}
		}
		
		if (PlayerPrefs.GetInt(abilitystate) == 4)
		{
			GUI.DrawTexture(smallblackleft, ying);
			GUI.DrawTexture(smallwhiteleft, yang);
			if (GUI.Button(T1right, new GUIContent (" ", newRunetext(0)), empty))
			{
				equipT1b();
			}	
			if (GUI.Button(T1left, new GUIContent (" ", newRunetext(1)), empty))
			{
				equipT1w();
			}
		}
		if (PlayerPrefs.GetInt(abilitystate) == 5)
		{
			GUI.DrawTexture(smallblackleft, ying);
			GUI.DrawTexture(smallwhiteleft, yang);
			GUI.DrawTexture(smallblackright, ying);
			if (GUI.Button(T1right, new GUIContent (" ", newRunetext(0)), empty))
			{
				equipT1b();
			}
			if (GUI.Button(T1left, new GUIContent (" ", newRunetext(1)), empty))
			{
				equipT1w();
			}
			if (GUI.Button(T2right, new GUIContent (" ", newRunetext(2)), empty))
			{
				equipT2b();
			}
		}
		if (PlayerPrefs.GetInt(abilitystate) == 6)
		{
			GUI.DrawTexture(smallblackleft, ying);
			GUI.DrawTexture(smallwhiteleft, yang);
			GUI.DrawTexture(smallwhiteright, yang);
			if (GUI.Button(T1right, new GUIContent (" ", newRunetext(0)), empty))
			{
				equipT1b();
			}	
			if (GUI.Button(T1left, new GUIContent (" ", newRunetext(1)), empty))
			{
				equipT1w();
			}
			if (GUI.Button(T2left, new GUIContent (" ", newRunetext(3)), empty))
			{
				
				equipT2w();
			}
		}
		if (PlayerPrefs.GetInt(abilitystate) == 7)
		{
			GUI.DrawTexture(smallblackleft, ying);
			GUI.DrawTexture(smallwhiteleft, yang);
			GUI.DrawTexture(smallblackright, ying);
			GUI.DrawTexture(smallwhiteright, yang);
			if (GUI.Button(T1right, new GUIContent (blank, newRunetext(0)), empty))
			{
				equipT1b();
			}
		
			if (GUI.Button(T1left, new GUIContent (blank, newRunetext(1)), empty))
			{
				equipT1w();
			}
			
			if (GUI.Button(T2right, new GUIContent (blank, newRunetext(2)), empty))
			{
				equipT2b();
			}
			
			if (GUI.Button(T2left, new GUIContent (blank, newRunetext(3)), empty))
			{
				equipT2w();
			}
			
		}
		
	}
	
	//the thing that checks which sstrike rune to equip when clicked
	void Checksapecial(string abilitystate, string[] tooltip)
	{
		if (PlayerPrefs.GetInt(abilitystate) == 1)
		{
				
			if (GUI.Button(new Rect(sapecial2.x + 6, sapecial2.y + 6, sapecial2.width, sapecial2.height), new GUIContent (SPgreen, newRunetext(1)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "green");
			}
		 
			
		}
		
		
		if (PlayerPrefs.GetInt(abilitystate) == 2)
		{
			if (GUI.Button(new Rect(sapecial1.x + 6, sapecial1.y + 6, sapecial1.width, sapecial1.height), new GUIContent (SPyellow, newRunetext(0)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "yellow");
			}
				
			if (GUI.Button(new Rect(sapecial2.x + 6, sapecial2.y + 6, sapecial2.width, sapecial2.height), new GUIContent (SPgreen, newRunetext(1)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "green");
			}
		
		}
		
		
		
		if (PlayerPrefs.GetInt(abilitystate) == 3)
		{
			if (GUI.Button(new Rect(sapecial1.x + 6, sapecial1.y + 6, sapecial1.width, sapecial1.height), new GUIContent (SPyellow, newRunetext(0)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "yellow");
			}
			
			if (GUI.Button(new Rect(sapecial2.x + 6, sapecial2.y + 6, sapecial2.width, sapecial2.height), new GUIContent (SPgreen, newRunetext(1)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "green");
			}
			
			if (GUI.Button(new Rect(sapecial4.x + 6, sapecial4.y + 6, sapecial4.width, sapecial4.height), new GUIContent (SPred, newRunetext(3)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "red");
			}
			
		}
		
		
		
		if (PlayerPrefs.GetInt(abilitystate) == 4)
		{
			if (GUI.Button(new Rect(sapecial1.x + 6, sapecial1.y + 6, sapecial1.width, sapecial1.height), new GUIContent (SPyellow, newRunetext(0)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "yellow");
			}

			if (GUI.Button(new Rect(sapecial2.x + 6, sapecial2.y + 6, sapecial2.width, sapecial2.height), new GUIContent (SPgreen, newRunetext(1)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "green");
			}
	
			if (GUI.Button(new Rect(sapecial3.x + 6, sapecial3.y + 6, sapecial3.width, sapecial3.height), new GUIContent (SPblue, newRunetext(2)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "blue");
			}
		
			if (GUI.Button(new Rect(sapecial4.x + 6, sapecial4.y + 6, sapecial4.width, sapecial4.height), new GUIContent (SPred, newRunetext(3)), empty))
			{
				PlayerPrefs.SetString("Sstrikerune", "red");
			}

		}
		
	}
	
	//bools to check if runes are equipped
	//needs replacements for new abilities for each one
	bool IsT1runethere()
	{
		//BODY
		if (gsmashoptions && PlayerPrefs.GetString("GroundsmashT1") == "null")
			return false;
		if (tackleoptions && PlayerPrefs.GetString("TackleT1") == "null")
			return false;
		if (forceoptions && PlayerPrefs.GetString("ForceT1") == "null")
			return false;
		if (hfistoptions && PlayerPrefs.GetString("HfistT1") == "null")
			return false;
		if (finalbodyoptions && PlayerPrefs.GetString("FinalbodyT1") == "null")
			return false;
		//MIND
		if (hurricaneoptions && PlayerPrefs.GetString("HurricaneT1") == "null")
			return false;
		if (lazeroptions && PlayerPrefs.GetString("LazerT1") == "null")
			return false;
		if (thirdmind && PlayerPrefs.GetString("ThirdmindT1") == "null")
			return false;
		if (serenityoptions && PlayerPrefs.GetString("SerenityT1") == "null")
			return false;
		if (warudooptions && PlayerPrefs.GetString("WarudoT1") == "null")
			return false;
		//TECH
		if (sbomboptions && PlayerPrefs.GetString("SpiritbombT1") == "null")
			return false;
		if (tdooroptions && PlayerPrefs.GetString("TeledoorT1") == "null")
			return false;
		if (cloneoptions && PlayerPrefs.GetString("CloneT1") == "null")
			return false;
		if (walloptions && PlayerPrefs.GetString("WallT1") == "null")
			return false;
		if (vplateoptions && PlayerPrefs.GetString("VplateT1") == "null")
			return false;
		
		
		return true;
	}
	
	bool IsT2runethere()
	{
		//BODY
		if (gsmashoptions && PlayerPrefs.GetString("GroundsmashT2") == "null")
			return false;
		if (tackleoptions && PlayerPrefs.GetString("TackleT2") == "null")
			return false;
		if (forceoptions && PlayerPrefs.GetString("ForceT2") == "null")
			return false;
		if (hfistoptions && PlayerPrefs.GetString("HfistT2") == "null")
			return false;
		if (finalbodyoptions && PlayerPrefs.GetString("FinalbodyT2") == "null")
			return false;
		//MIND
		if (hurricaneoptions && PlayerPrefs.GetString("HurricaneT2") == "null")
			return false;
		if (lazeroptions && PlayerPrefs.GetString("LazerT2") == "null")
			return false;
		if (thirdmind && PlayerPrefs.GetString("ThirdmindT2") == "null")
			return false;
		if (serenityoptions && PlayerPrefs.GetString("SerenityT2") == "null")
			return false;
		if (warudooptions && PlayerPrefs.GetString("WarudoT2") == "null")
			return false;
		//TECH
		if (sbomboptions && PlayerPrefs.GetString("SpiritbombT2") == "null")
			return false;
		if (tdooroptions && PlayerPrefs.GetString("TeledoorT2") == "null")
			return false;
		if (cloneoptions && PlayerPrefs.GetString("CloneT2") == "null")
			return false;
		if (walloptions && PlayerPrefs.GetString("WallT2") == "null")
			return false;
		if (vplateoptions && PlayerPrefs.GetString("VplateT2") == "null")
			return false;
		
		return true;
	}
	
	string newRunetext(int number)
	{
		//BODY
		if (gsmashoptions)
			return gsmashtips[number];
		if (tackleoptions)
			return tackletips[number];
		if (forceoptions)
			return forcetips[number];
		if (hfistoptions)
			return hfisttips[number];
		if (finalbodyoptions)
			return hfisttips[number];//needs replacement
		//MIND
		if (hurricaneoptions)
			return hurritips[number];
		if (lazeroptions)
			return lazertips[number];
		if (thirdmind)
			return lazertips[number];
		if (serenityoptions)
			return serentips[number];
		if (warudooptions)
			return warudotips[number];
		//TECH
		if (sbomboptions)
			return spiritbtips[number];
		if (tdooroptions)
			return tdoortips[number];
		if (cloneoptions)
			return clonetips[number];
		if (walloptions)
			return walltips[number];
		if (vplateoptions)
			return vplatetips[number];
		
		if (sapecialoptions)
			return Sstriketips[number];
		
		return null;
	}
	
	//functions to equip specific runes. Maybe a bit inefficient right now. Not sure how to improve though.
	void equipT1b()
	{
		//BODY
		if (gsmashoptions)
			PlayerPrefs.SetString("GroundsmashT1", "black");
		if (tackleoptions)
			PlayerPrefs.SetString("TackleT1", "black");
		if (forceoptions)
			PlayerPrefs.SetString("ForceT1", "black");
		if (hfistoptions)
			PlayerPrefs.SetString("HfistT1", "black");
		if (finalbodyoptions)
			PlayerPrefs.SetString("FinalbodyT1", "black");
		//MIND
		if (hurricaneoptions)
			PlayerPrefs.SetString("HurricaneT1", "black");
		if (lazeroptions)
			PlayerPrefs.SetString("LazerT1", "black");
		if (thirdmind)
			PlayerPrefs.SetString("ThirdmindT1", "black");
		if (serenityoptions)
			PlayerPrefs.SetString("SerenityT1", "black");
		if (warudooptions)
			PlayerPrefs.SetString("WarudoT1", "black");
		//TECH
		if (sbomboptions)
			PlayerPrefs.SetString("SpiritbombT1", "black");
		if (tdooroptions)
			PlayerPrefs.SetString("TeledoorT1", "black");
		if (cloneoptions)
			PlayerPrefs.SetString("CloneT1", "black");
		if (walloptions)
			PlayerPrefs.SetString("WallT1", "black");
		if (vplateoptions)
			PlayerPrefs.SetString("VplateT1", "black");
		
		
	}
	
	void equipT1w()
	{
		//BODY
		if (gsmashoptions)
			PlayerPrefs.SetString("GroundsmashT1", "white");
		if (tackleoptions)
			PlayerPrefs.SetString("TackleT1", "white");
		if (forceoptions)
			PlayerPrefs.SetString("ForceT1", "white");
		if (hfistoptions)
			PlayerPrefs.SetString("HfistT1", "white");
		if (finalbodyoptions)
			PlayerPrefs.SetString("FinalbodyT1", "white");
		//MIND
		if (hurricaneoptions)
			PlayerPrefs.SetString("HurricaneT1", "white");
		if (lazeroptions)
			PlayerPrefs.SetString("LazerT1", "white");
		if (thirdmind)
			PlayerPrefs.SetString("ThirdmindT1", "white");
		if (serenityoptions)
			PlayerPrefs.SetString("SerenityT1", "white");
		if (warudooptions)
			PlayerPrefs.SetString("WarudoT1", "white");
		//TECH
		if (sbomboptions)
			PlayerPrefs.SetString("SpiritbombT1", "white");
		if (tdooroptions)
			PlayerPrefs.SetString("TeledoorT1", "white");
		if (cloneoptions)
			PlayerPrefs.SetString("CloneT1", "white");
		if (walloptions)
			PlayerPrefs.SetString("WallT1", "white");
		if (vplateoptions)
			PlayerPrefs.SetString("VplateT1", "white");
		
	}
	
	void equipT2b()
	{
		//BODY
		if (gsmashoptions)
			PlayerPrefs.SetString("GroundsmashT2", "black");
		if (tackleoptions)
			PlayerPrefs.SetString("TackleT2", "black");
		if (forceoptions)
			PlayerPrefs.SetString("ForceT2", "black");
		if (hfistoptions)
			PlayerPrefs.SetString("HfistT2", "black");
		if (finalbodyoptions)
			PlayerPrefs.SetString("FinalbodyT2", "black");
		//MIND
		if (hurricaneoptions)
			PlayerPrefs.SetString("HurricaneT2", "black");
		if (lazeroptions)
			PlayerPrefs.SetString("LazerT2", "black");
		if (thirdmind)
			PlayerPrefs.SetString("ThirdmindT2", "black");
		if (serenityoptions)
			PlayerPrefs.SetString("SerenityT2", "black");
		if (warudooptions)
			PlayerPrefs.SetString("WarudoT2", "black");
		//TECH
		if (sbomboptions)
			PlayerPrefs.SetString("SpiritbombT2", "black");
		if (tdooroptions)
			PlayerPrefs.SetString("TeledoorT2", "black");
		if (cloneoptions)
			PlayerPrefs.SetString("CloneT2", "black");
		if (walloptions)
			PlayerPrefs.SetString("WallT2", "black");
		if (vplateoptions)
			PlayerPrefs.SetString("VplateT2", "black");
		
	}
	
	void equipT2w()
	{
		//BODY
		if (gsmashoptions)
			PlayerPrefs.SetString("GroundsmashT2", "white");
		if (tackleoptions)
			PlayerPrefs.SetString("TackleT2", "white");
		if (forceoptions)
			PlayerPrefs.SetString("ForceT2", "white");
		if (hfistoptions)
			PlayerPrefs.SetString("HfistT2", "white");
		if (finalbodyoptions)
			PlayerPrefs.SetString("FinalbodyT2", "white");
		//MIND
		if (hurricaneoptions)
			PlayerPrefs.SetString("HurricaneT2", "white");
		if (lazeroptions)
			PlayerPrefs.SetString("LazerT2", "white");
		if (thirdmind)
			PlayerPrefs.SetString("ThirdmindT2", "white");
		if (serenityoptions)
			PlayerPrefs.SetString("SerenityT2", "white");
		if (warudooptions)
			PlayerPrefs.SetString("WarudoT2", "white");
		//TECH
		if (sbomboptions)
			PlayerPrefs.SetString("SpiritbombT2", "white");
		if (tdooroptions)
			PlayerPrefs.SetString("TeledoorT2", "white");
		if (cloneoptions)
			PlayerPrefs.SetString("CloneT2", "white");
		if (walloptions)
			PlayerPrefs.SetString("WallT2", "white");
		if (vplateoptions)
			PlayerPrefs.SetString("VplateT2", "white");
	}
	
	void OnGUI ()
	{
		if (GameObject.FindGameObjectWithTag("Fader") == null)
		{
			

//			GUI.Button(helpbox, helpbut, empty);
			
		if (!RunesUI && !StoreUI)
		{
			#region Ability bools for playerprefs----------------------------------------------------------
		
			//Gsmash
			if (GUI.Button(devrect, "Groundsmash: " + PlayerPrefs.GetInt("Groundsmashabilitystate")))
			{
				PlayerPrefs.SetInt("Groundsmashabilitystate", PlayerPrefs.GetInt("Groundsmashabilitystate") + 1);
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") > 7)
					PlayerPrefs.SetInt("Groundsmashabilitystate", 0);
			}
				
			//Tackle
			if (GUI.Button(new Rect(devrect.x, devrect.y + 30, 150, 25), "Tackle: " + PlayerPrefs.GetInt("Tackleabilitystate")))
				{
					PlayerPrefs.SetInt("Tackleabilitystate", PlayerPrefs.GetInt("Tackleabilitystate") + 1);
					if (PlayerPrefs.GetInt("Tackleabilitystate") > 7)
						PlayerPrefs.SetInt("Tackleabilitystate", 0);
				}
				
			//Force
			if (GUI.Button(new Rect(devrect.x, devrect.y + 60, 150, 25), "Force: " + PlayerPrefs.GetInt("Forceabilitystate")))
				{
					PlayerPrefs.SetInt("Forceabilitystate", PlayerPrefs.GetInt("Forceabilitystate") + 1);
					if (PlayerPrefs.GetInt("Forceabilitystate") > 7)
						PlayerPrefs.SetInt("Forceabilitystate", 0);
				}
				
			//Hfist
			if (GUI.Button(new Rect(devrect.x, devrect.y + 90, 150, 25), "Hfist: " + PlayerPrefs.GetInt("Hfistabilitystate")))
				{
					PlayerPrefs.SetInt("Hfistabilitystate", PlayerPrefs.GetInt("Hfistabilitystate") + 1);
					if (PlayerPrefs.GetInt("Hfistabilitystate") > 7)
						PlayerPrefs.SetInt("Hfistabilitystate", 0);
				}
				
			//Final body 
			if (GUI.Button(new Rect(devrect.x, devrect.y + 120, 150, 25), "Final Body: " + PlayerPrefs.GetInt("Finalbodyabilitystate")))
				{
					PlayerPrefs.SetInt("Finalbodyabilitystate", PlayerPrefs.GetInt("Finalbodyabilitystate") + 1);
					if (PlayerPrefs.GetInt("Finalbodyabilitystate") > 7)
						PlayerPrefs.SetInt("Finalbodyabilitystate", 0);
				}
			
			//Hurricane
			if (GUI.Button(new Rect(devrect.x  + 150, devrect.y, 150, 25), "Hurricane: " + PlayerPrefs.GetInt("Hurricaneabilitystate")))
				{
					PlayerPrefs.SetInt("Hurricaneabilitystate", PlayerPrefs.GetInt("Hurricaneabilitystate") + 1);
					if (PlayerPrefs.GetInt("Hurricaneabilitystate") > 7)
						PlayerPrefs.SetInt("Hurricaneabilitystate", 0);
				}
			
			//Lazer
			if (GUI.Button(new Rect(devrect.x + 150, devrect.y + 30, 150, 25), "Lazer: " + PlayerPrefs.GetInt("Lazerabilitystate")))
				{
					PlayerPrefs.SetInt("Lazerabilitystate", PlayerPrefs.GetInt("Lazerabilitystate") + 1);
					if (PlayerPrefs.GetInt("Lazerabilitystate") > 7)
						PlayerPrefs.SetInt("Lazerabilitystate", 0);
				}
				
			//third mind 
			if (GUI.Button(new Rect(devrect.x + 150, devrect.y + 60, 150, 25), "Thirdmind: " + PlayerPrefs.GetInt("Thirdmindabilitystate")))
				{
					PlayerPrefs.SetInt("Thirdmindabilitystate", PlayerPrefs.GetInt("Thirdmindabilitystate") + 1);
					if (PlayerPrefs.GetInt("Thirdmindabilitystate") > 7)
						PlayerPrefs.SetInt("Thirdmindabilitystate", 0);
				}
				
			//Serenity
			if (GUI.Button(new Rect(devrect.x + 150, devrect.y + 90, 150, 25), "Serenity: " + PlayerPrefs.GetInt("Serenityabilitystate")))
				{
					PlayerPrefs.SetInt("Serenityabilitystate", PlayerPrefs.GetInt("Serenityabilitystate") + 1);
					if (PlayerPrefs.GetInt("Serenityabilitystate") > 7)
						PlayerPrefs.SetInt("Serenityabilitystate", 0);
				}
				
			//Warudo
			if (GUI.Button(new Rect(devrect.x + 150, devrect.y + 120, 150, 25), "Warudo: " + PlayerPrefs.GetInt("Warudoabilitystate")))
				{
					PlayerPrefs.SetInt("Warudoabilitystate", PlayerPrefs.GetInt("Warudoabilitystate") + 1);
					if (PlayerPrefs.GetInt("Warudoabilitystate") > 7)
						PlayerPrefs.SetInt("Warudoabilitystate", 0);
				}
			
			//Spiritbomb
			if (GUI.Button(new Rect(devrect.x + 300, devrect.y, 150, 25), "Spiritb: " + PlayerPrefs.GetInt("Spiritbombabilitystate")))
				{
					PlayerPrefs.SetInt("Spiritbombabilitystate", PlayerPrefs.GetInt("Spiritbombabilitystate") + 1);
					if (PlayerPrefs.GetInt("Spiritbombabilitystate") > 7)
						PlayerPrefs.SetInt("Spiritbombabilitystate", 0);
				}
			
			
			
			//Teledoor
			if (GUI.Button(new Rect(devrect.x + 300, devrect.y + 30, 150, 25), "Teledoor: " + PlayerPrefs.GetInt("Teledoorabilitystate")))
				{
					PlayerPrefs.SetInt("Teledoorabilitystate", PlayerPrefs.GetInt("Teledoorabilitystate") + 1);
					if (PlayerPrefs.GetInt("Teledoorabilitystate") > 7)
						PlayerPrefs.SetInt("Teledoorabilitystate", 0);
				}
			
			//Clone
			if (GUI.Button(new Rect(devrect.x + 300, devrect.y + 60, 150, 25), "Clone: " + PlayerPrefs.GetInt("Cloneabilitystate")))
				{
					PlayerPrefs.SetInt("Cloneabilitystate", PlayerPrefs.GetInt("Cloneabilitystate") + 1);
					if (PlayerPrefs.GetInt("Cloneabilitystate") > 7)
						PlayerPrefs.SetInt("Cloneabilitystate", 0);
				}
			
			//WALL
			if (GUI.Button(new Rect(devrect.x + 300, devrect.y + 90, 150, 25), "Summon Weapon: " + PlayerPrefs.GetInt("Wallabilitystate")))
				{
					PlayerPrefs.SetInt("Wallabilitystate", PlayerPrefs.GetInt("Wallabilitystate") + 1);
					if (PlayerPrefs.GetInt("Wallabilitystate") > 7)
						PlayerPrefs.SetInt("Wallabilitystate", 0);
				}
				
			//Vplate
			if (GUI.Button(new Rect(devrect.x + 300, devrect.y + 120, 150, 25), "Vplate: " + PlayerPrefs.GetInt("Vplateabilitystate")))
				{
					PlayerPrefs.SetInt("Vplateabilitystate", PlayerPrefs.GetInt("Vplateabilitystate") + 1);
					if (PlayerPrefs.GetInt("Vplateabilitystate") > 7)
						PlayerPrefs.SetInt("Vplateabilitystate", 0);
				}
			
			//Sstrike
			if (GUI.Button(new Rect(devrect.x + 450, devrect.y , 150, 25), "SapecialStrike: " + PlayerPrefs.GetInt("Sstrikeabilitystate")))
				{
					PlayerPrefs.SetInt("Sstrikeabilitystate", PlayerPrefs.GetInt("Sstrikeabilitystate") + 1);
					if (PlayerPrefs.GetInt("Sstrikeabilitystate") > 4)
						PlayerPrefs.SetInt("Sstrikeabilitystate", 0);
				}
				
			//checkpoints
			if (GUI.Button(new Rect(devrect.x + 450, devrect.y + 30, 150, 25), "EndlessMax: " + PlayerPrefs.GetInt("EndlessMax")))
				{
					PlayerPrefs.SetInt("EndlessMax", PlayerPrefs.GetInt("EndlessMax") + 1);
					if (PlayerPrefs.GetInt("EndlessMax") > 2)
						PlayerPrefs.SetInt("EndlessMax", 0);
				}
			
			//reset	
			if (GUI.Button(new Rect(devrect.x + 450, devrect.y + 60, 150, 25), "!Reset Game!"))
				{
					newreset();
				}
		
		
		#endregion	
				
			//top side plates
			GUI.DrawTexture(topleftplate, lefttop);
			if (!RunesUI)
			GUI.DrawTexture(statsplate, righttop);
			GUI.Label(new Rect(topleftplate.x + 40, topleftplate.y + 10, 300, 100), "Dojo Store", newfont);
			
			//Afrotv on main
			GUI.DrawTexture(tvmainrect, tv);
			GUI.DrawTexture(new Rect(tvmainrect.x + 18, tvmainrect.y + 18, 430, 320), tvbg);
			GUI.DrawTexture(new Rect(tvmainrect.x + 18, tvmainrect.y + 18, 430, 320), screenshade);
				
			GUI.DrawTexture(new Rect(tvmainrect.x + 320, tvmainrect.y + 210, 82, 108), sensui);
			if (!Enterbattlepopup)
			GUI.DrawTexture(new Rect(tvmainrect.x + 45, tvmainrect.y + 45, 360, 172), bubble);
			if (Enterbattlepopup)
			GUI.DrawTexture(sensuipop, bubblesmall);		
			GUI.DrawTexture(new Rect(tvmainrect.x + 60, tvmainrect.y + 200, 90, 120), afroman);	
				
			//Tipscroll
			GUI.DrawTexture(tipscrollrect, tipscroll);
			newfont2.normal.textColor = Color.red;
			GUI.Label(new Rect(tipscrollrect.x + 38, tipscrollrect.y + 30, 140, 60), "Lesson of the Day", newfont2);
			newfont2.normal.textColor = Color.black;
			newfont2.fontSize = 20;
			GUI.Label(new Rect(tipscrollrect.x + 38, tipscrollrect.y + 120, 140, 340), currenttip, newfont2);
			newfont2.fontSize = 24;
				
			if (!dojotutorial)
			{
				if (!dojomouseover && !Enterbattlepopup && !choosecheckpoint)
				{
					GUI.color = Color.black;
					comic.fontSize = 18;
					GUI.Label(new Rect(dojoline1.x + 40, dojoline1.y + 5, dojoline1.width, dojoline1.height), 
							"Ah, its you, again...\nAlright, maybe I'll teach you a thing or two,\n...for a nominal fee, of course...", comic);
					comic.fontSize = 16;
					GUI.color = Color.white;
				}
					
				if (!dojomouseover && !Enterbattlepopup && choosecheckpoint)
				{
					GUI.color = Color.black;
					comic.fontSize = 18;
					GUI.Label(dojoline1, "It seems like you have been there already.\nYou can choose to begin anew or continue your journey.\nLet's see how far you can go.", comic);
					comic.fontSize = 16;
					GUI.color = Color.white;
				}
				
			}
		}
			
		
		

		

		
		if (!RunesUI && !StoreUI)
		{
			GUI.skin = dojoskin;
				
			//store button	
			if (GUI.Button(firstoption, "Purchase Skills") && !dojotutorial)
			{
				StoreUI = !StoreUI;
				Enterbattlepopup = false;
			}
			
			//customize button
			if (GUI.Button(secondoption, "Customize Skills") && !dojotutorial)
			{
				RunesUI = !RunesUI;
				Enterbattlepopup = false;
				if (PlayerPrefs.GetInt("Newabilitynotice") >= 21)
					PlayerPrefs.SetInt("Newabilitynotice", 0);
			}
			
			if (rainbowon)
			{
				shinynewtext(new Rect(secondoption.x + 240, secondoption.y + 10, 52, 17));
			}
			
			//endless button
			if (GUI.Button(thirdoption, "The Endless Tower") && !dojotutorial)
			{
				Enterbattlepopup = true;
				
				
			}
				
			//training button	
			if (GUI.Button(fourthoption, "Training Mode") && !dojotutorial)
			{
				StartCoroutine ( Dofadeout ("Devmode") );
			}
			
			GUI.skin = null;
		}
		
		if (Enterbattlepopup)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			Enterbattlepopup = false;	
			
			GUI.skin = dojo2;
				
			GUI.Window(8, afropop, Afropopup, " ");
			GUI.BringWindowToFront(8);	

		}
			
		if (choosecheckpoint)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			choosecheckpoint = false;	
			
			GUI.skin = dojoskin;	
			GUI.Window(11, smallboxleftside, Endlesschoose, "Endless");		
			GUI.BringWindowToFront(11);
			GUI.skin = null;
			
		}
		
		if (StoreUI)
		{
			GUI.color = Color.white;
			GUI.Window(2, StoreWindow, StoreShit, "Runic Mysteries Inc.", empty);
			GUI.Window(12, chatbox, Chatboxcontent, " ", empty);
			GUI.BringWindowToFront(12);
			
			if (storeskills && !individualskill)
			{	
				GUI.skin = dojo2;
				dojo2.label.normal.textColor = Color.black;
				GUI.Window(4, sensuipop, sensuicomment, " ");
				GUI.BringWindowToFront(4);
				GUI.skin = null;
				
			}
				
			if (storerunes && !individualskill)
			{	
				GUI.skin = dojo2;
				dojo2.label.normal.textColor = Color.black;
				GUI.Window(4, sensuipop, sensuicomment, " ");
				GUI.BringWindowToFront(4);
				GUI.skin = null;
				
			}
				
			if (skillquestion)
			{
				GUI.skin = dojo2;
				dojo2.label.normal.textColor = Color.black;
				GUI.Window(4, sensuipop, Skillquestion, " ");
				GUI.Window(8, afropop, Afropopup, " ");
				GUI.BringWindowToFront(4);
				GUI.BringWindowToFront(8);
				GUI.skin = null;
			}
				
			if (runesquestion)
			{
				GUI.skin = dojo2;
				dojo2.label.normal.textColor = Color.black;
				GUI.Window(4, sensuipop, runecrafting, " ");
				GUI.Window(8, afropop, Afropopup, " ");
				GUI.BringWindowToFront(4);
				GUI.BringWindowToFront(8);
				GUI.skin = null;
			}
				
			if (purchasepopup)
			{
				GUI.skin = dojo2;
				dojo2.label.normal.textColor = Color.black;
				GUI.Window(7, sensuipop, Popupshit, " ");
				GUI.skin = woodright;
				GUI.Window(8, afropop, Afropopup, " ");
				GUI.BringWindowToFront(7);
				GUI.BringWindowToFront(8);
				GUI.skin = null;
			}
				
			if (amountchanger)
			{
				GUI.skin = dojo2;
				dojo2.label.normal.textColor = Color.black;
				GUI.Window(9, amountbox, purchaseamount, "Purchase Confirmation");
				GUI.BringWindowToFront(9);
				GUI.skin = null;	
			}
				
			if (scrollscostwindow)
			{
				GUI.skin = dojoskin;
				dojo2.label.normal.textColor = Color.black;
				GUI.Window(10, scrollscostbox, Scrollscostui, "Scroll Cost");
				GUI.BringWindowToFront(10);
				GUI.skin = null;
			}
				
		}
			
		if (RunesUI)
		{
			GUI.color = Color.white;
			GUI.Window(1, RunesWindow, RunesShit, " ", empty);
		}
		
		
			//rest of the UI-----------------------------------------------------------------------
			if (!RunesUI && !StoreUI)
			{
				GUI.skin = dojoskin;
					
				if (dojotutorial)
				{
					GUI.color = Color.black;
					comic.fontSize = 18;
					WriteString();
						
					if (tutorialnumber == 1)
					{
						GUI.Label(new Rect(dojoline1.x, dojoline1.y + 5, dojoline1.width, dojoline1.height), 
							currentstring, comic);
						pressenter = true;
					}
						
					if (tutorialnumber == 2)
					{
						GUI.Label(new Rect(dojoline1.x, dojoline1.y + 5, dojoline1.width, dojoline1.height), 
							currentstring, comic);
						pressenter = true;
					}
						
					if (tutorialnumber == 3)
					{
						GUI.Label(new Rect(dojoline1.x, dojoline1.y + 5, dojoline1.width, dojoline1.height), 
							currentstring, comic);
						pressenter = true;
					}
						
					if (tutorialnumber == 4)
					{
						GUI.Label(new Rect(dojoline1.x, dojoline1.y + 5, dojoline1.width, dojoline1.height), 
							currentstring, comic);
						pressenter = true;
					}
						
					if (tutorialnumber == 5)
					{
						GUI.Label(new Rect(dojoline1.x, dojoline1.y + 5, dojoline1.width, dojoline1.height), 
							currentstring, comic);
						pressenter = true;
					}
						
					if (tutorialnumber == 6)
					{
						GUI.Label(new Rect(dojoline1.x, dojoline1.y + 5, dojoline1.width, dojoline1.height), 
							currentstring, comic);
						pressenter = true;
					}
						
					if (tutorialnumber == 7)
					{
						tutorialnumber = 0;
						tutorialindex = 0;
						pressenter = false;
						flashing = false;
						dojotutorial = false;
						PlayerPrefs.SetInt("Dojotutorial", 1);
					}
				
					comic.fontSize = 16;
					GUI.color = Color.white;			
						
					if (pressenter)
					{
						if (flashing)
						{
							GUI.DrawTexture(enterbuttonrect, enterimg);
								
							if (tutorialnumber == 3)
							GUI.DrawTexture(new Rect(firstoption.x + 300, firstoption.y, 48, 48), leftarrow);
							if (tutorialnumber == 4)		
							GUI.DrawTexture(new Rect(secondoption.x + 300, secondoption.y, 48, 48), leftarrow);
							if (tutorialnumber == 5)
							GUI.DrawTexture(new Rect(thirdoption.x + 300, thirdoption.y, 48, 48), leftarrow);	
							if (tutorialnumber == 6)
							GUI.DrawTexture(new Rect(fourthoption.x + 300, fourthoption.y, 48, 48), leftarrow);		
						}
								
						if (!flashing)
						GUI.DrawTexture(enterbuttonrect, enterimg2);
								
								
						
							
					}
							
				}
					
				if (GUI.Button(new Rect(10, Screen.height - 100, 170, 25), "This button don't do shit"))
				{
					checkshit();
				}
				
				if (GUI.Button(new Rect(-80, Screen.height - 60, 280, 70), "     Return"))
				{
					StartCoroutine ( Dofadeout ("LevelSelect") );
				}

				
				
				GUI.skin = null;
			}
			

			GUI.Label(new Rect(0, Screen.height / 2 + 160, 280, 50), "index: " + tutorialindex);
			GUI.Label(new Rect(0, Screen.height / 2 + 190, 680, 50), "current: " + currentstring);
			GUI.Label(new Rect(0, Screen.height / 2 + 210, 280, 50), "full: " + fullstring);
//			
//			GUI.depth = -100;
//			GUI.DrawTexture(yesdetection, gsmashicon);
//			GUI.DrawTexture(nodetection, gsmashicon);

			
			
		}// end fader
		

	}// END ON GUI
	
	void RunesShit(int windowID)
	{
		//halp
		if (GUI.Button(helpbox, helpbut, empty))
		help1 = true;
		
		
		GUI.skin = dojoskin;
		//GTFO button
		if (Input.GetKeyDown(KeyCode.Escape) && !help1 || GUI.Button(new Rect(0, Screen.height - 40, 140, 35), "Return") && !help1)
		{
			RunesUI = false;	
		}
		GUI.skin = null;
		
		//sapecialstrike bar
		if (sapecialoptions)
		{
			GUI.DrawTexture(Sstrikebox, Sstrikebarimg);	
			
		}
		
		//Static UI elementz
		GUI.DrawTexture(new Rect(2, 0, 358, 92), moonspeak);
		GUI.DrawTexture(Currentbar, equippedbar);

//		if (minventory)
//			GUI.DrawTexture(new Rect(58, 36, 56, 56), shinyblue);
//		if (skinventory)
//			GUI.DrawTexture(new Rect(168, 41, 46, 46), shinygreen);
//		if (binventory)
//			GUI.DrawTexture(new Rect(267, 36, 56, 56), shinyred);
		
		//leftside inventory
		GUI.DrawTexture(invboxmain, skillsboximg);
		GUI.DrawTexture(new Rect(0, invboxmain.y + 120, 200, 100), skillsboximg);
		GUI.DrawTexture(new Rect(0, invboxmain.y + 240, 200, 100), skillsboximg);
		GUI.DrawTexture(new Rect(0, invboxmain.y + 360, 200, 100), skillsboximg);
		GUI.DrawTexture(new Rect(0, invboxmain.y + 480, 200, 100), skillsboximg);
		

		
		//body ability notice
		if (PlayerPrefs.GetInt("Newabilitynotice") == 1 || PlayerPrefs.GetInt("Newabilitynotice") == 2 || PlayerPrefs.GetInt("Newabilitynotice") == 9 ||
			PlayerPrefs.GetInt("Newabilitynotice") == 10)
			shinynewtext( new Rect(bodyinventory.x + 50, bodyinventory.y, 52, 17));	
		//tech ability notice
		if (PlayerPrefs.GetInt("Newabilitynotice") == 3 || PlayerPrefs.GetInt("Newabilitynotice") == 6 || PlayerPrefs.GetInt("Newabilitynotice") == 7
			|| PlayerPrefs.GetInt("Newabilitynotice") == 8 || PlayerPrefs.GetInt("Newabilitynotice") == 13)
			shinynewtext( new Rect(sakillinventory.x + 50, sakillinventory.y, 52, 17));	
		//mind ability notice
		if (PlayerPrefs.GetInt("Newabilitynotice") == 4 || PlayerPrefs.GetInt("Newabilitynotice") == 5 || PlayerPrefs.GetInt("Newabilitynotice") == 11 ||
			PlayerPrefs.GetInt("Newabilitynotice") == 12)
			shinynewtext( new Rect(mindinventory.x + 50, mindinventory.y, 52, 17));	
		
		//rune circles
		GUI.DrawTexture(middlecircle, mainslot);
		GUI.DrawTexture(leftcircle, minislot);
		GUI.DrawTexture(rightcircle, minislot);
		
		
		
		
//picking a skill in your bar-----------------------------------------------------------------------------
		
		#region Slot1options-----------------------------------------
		
		//The button null state
		if (Slot1content == "null")
		{
			if (GUI.Button(firstbarslot, " "))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				skillinventory = true;
			}
		}
		
		//BODY SKILLS 
		if (Slot1content == "Groundsmash")
		{
			if (GUI.Button(firstbarslot, gsmashicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				gsmashoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Tackle")
		{
			if (GUI.Button(firstbarslot, tackleicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				tackleoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Force")
		{
			if (GUI.Button(firstbarslot, forceicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Hfist")
		{
			if (GUI.Button(firstbarslot, forceicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//final body needs replacement
		if (Slot1content == "Finalb")
		{
			if (GUI.Button(firstbarslot, forceicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				finalbodyoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		
		//MIND SKILLS
		if (Slot1content == "Hurricane")
		{
			if (GUI.Button(firstbarslot, hurricon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				hurricaneoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Lazer")
		{
			if (GUI.Button(firstbarslot, beamicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				lazeroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//3rdmind needs replacement
		if (Slot1content == "Thirdmind")
		{
			if (GUI.Button(firstbarslot, beamicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				thirdmind = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Serenity")
		{
			if (GUI.Button(firstbarslot, sereniticon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Warudo")
		{
			if (GUI.Button(firstbarslot, sereniticon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		//TECH SKILLS
		if (Slot1content == "Spiritbomb")
		{
			if (GUI.Button(firstbarslot, spiritbicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				sbomboptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Teledoor")
		{
			if (GUI.Button(firstbarslot, tdooricon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				tdooroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Clone")
		{
			if (GUI.Button(firstbarslot, cloneicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				cloneoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot1content == "Wall")
		{
			if (GUI.Button(firstbarslot, wallicon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				walloptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//icone needs replacement
		if (Slot1content == "Vplate")
		{
			if (GUI.Button(firstbarslot, sereniticon))
			{
				CurrentSlot = "Slot1";
				Toggleoffoptions();
				vplateoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		#endregion
		
		#region Slot2options------------------------------------------------
		
		//The button null state
		if (Slot2content == "null")
		{
			if (GUI.Button(secondbarslot, " "))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				skillinventory = true;
			}
		}
		
		//BODY SKILLS 
		if (Slot2content == "Groundsmash")
		{
			if (GUI.Button(secondbarslot, gsmashicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				gsmashoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Tackle")
		{
			if (GUI.Button(secondbarslot, tackleicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				tackleoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Force")
		{
			if (GUI.Button(secondbarslot, forceicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Hfist")
		{
			if (GUI.Button(secondbarslot, forceicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//final body needs replacement
		if (Slot2content == "Finalb")
		{
			if (GUI.Button(secondbarslot, forceicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		
		//MIND SKILLS
		if (Slot2content == "Hurricane")
		{
			if (GUI.Button(secondbarslot, hurricon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				hurricaneoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Lazer")
		{
			if (GUI.Button(secondbarslot, beamicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				lazeroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//3rdmind needs replacement
		if (Slot2content == "Thirdmind")
		{
			if (GUI.Button(secondbarslot, beamicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				lazeroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Serenity")
		{
			if (GUI.Button(secondbarslot, sereniticon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Warudo")
		{
			if (GUI.Button(secondbarslot, sereniticon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		//TECH SKILLS
		if (Slot2content == "Spiritbomb")
		{
			if (GUI.Button(secondbarslot, spiritbicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				sbomboptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Teledoor")
		{
			if (GUI.Button(secondbarslot, tdooricon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				tdooroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Clone")
		{
			if (GUI.Button(secondbarslot, cloneicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				cloneoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot2content == "Wall")
		{
			if (GUI.Button(secondbarslot, wallicon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				walloptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//icone needs replacement
		if (Slot2content == "Vplate")
		{
			if (GUI.Button(secondbarslot, sereniticon))
			{
				CurrentSlot = "Slot2";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		#endregion
		
		#region Slot3options---------------------------------
		
		//The button null state
		if (Slot3content == "null")
		{
			if (GUI.Button(thirdbarslot, " "))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				skillinventory = true;
			}
		}
		
		//BODY SKILLS 
		if (Slot3content == "Groundsmash")
		{
			if (GUI.Button(thirdbarslot, gsmashicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				gsmashoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Tackle")
		{
			if (GUI.Button(thirdbarslot, tackleicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				tackleoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Force")
		{
			if (GUI.Button(thirdbarslot, forceicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Hfist")
		{
			if (GUI.Button(thirdbarslot, forceicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//final body needs replacement
		if (Slot3content == "Finalb")
		{
			if (GUI.Button(thirdbarslot, forceicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		
		//MIND SKILLS
		if (Slot3content == "Hurricane")
		{
			if (GUI.Button(thirdbarslot, hurricon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				hurricaneoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Lazer")
		{
			if (GUI.Button(thirdbarslot, beamicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				lazeroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//3rdmind needs replacement
		if (Slot3content == "Thirdmind")
		{
			if (GUI.Button(thirdbarslot, beamicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				lazeroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Serenity")
		{
			if (GUI.Button(thirdbarslot, sereniticon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Warudo")
		{
			if (GUI.Button(thirdbarslot, sereniticon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		//TECH SKILLS
		if (Slot3content == "Spiritbomb")
		{
			if (GUI.Button(thirdbarslot, spiritbicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				sbomboptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Teledoor")
		{
			if (GUI.Button(thirdbarslot, tdooricon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				tdooroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Clone")
		{
			if (GUI.Button(thirdbarslot, cloneicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				cloneoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot3content == "Wall")
		{
			if (GUI.Button(thirdbarslot, wallicon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				walloptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//icone needs replacement
		if (Slot3content == "Vplate")
		{
			if (GUI.Button(thirdbarslot, sereniticon))
			{
				CurrentSlot = "Slot3";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		#endregion
		
		#region Slot4options-----------------------------------------------------------
		
		//The button null state
		if (Slot4content == "null")
		{
			if (GUI.Button(fourthbarslot, " "))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				skillinventory = true;
			}
		}
		
		//BODY SKILLS 
		if (Slot4content == "Groundsmash")
		{
			if (GUI.Button(fourthbarslot, gsmashicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				gsmashoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Tackle")
		{
			if (GUI.Button(fourthbarslot, tackleicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				tackleoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Force")
		{
			if (GUI.Button(fourthbarslot, forceicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Hfist")
		{
			if (GUI.Button(fourthbarslot, forceicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//final body needs replacement
		if (Slot4content == "Finalb")
		{
			if (GUI.Button(fourthbarslot, forceicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				forceoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		
		//MIND SKILLS
		if (Slot4content == "Hurricane")
		{
			if (GUI.Button(fourthbarslot, hurricon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				hurricaneoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Lazer")
		{
			if (GUI.Button(fourthbarslot, beamicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				lazeroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//3rdmind needs replacement
		if (Slot4content == "Thirdmind")
		{
			if (GUI.Button(fourthbarslot, beamicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				lazeroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Serenity")
		{
			if (GUI.Button(fourthbarslot, sereniticon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Warudo")
		{
			if (GUI.Button(fourthbarslot, sereniticon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		//TECH SKILLS
		if (Slot4content == "Spiritbomb")
		{
			if (GUI.Button(fourthbarslot, spiritbicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				sbomboptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Teledoor")
		{
			if (GUI.Button(fourthbarslot, tdooricon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				tdooroptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Clone")
		{
			if (GUI.Button(fourthbarslot, cloneicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				cloneoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		if (Slot4content == "Wall")
		{
			if (GUI.Button(fourthbarslot, wallicon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				walloptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		//icone needs replacement
		if (Slot4content == "Vplate")
		{
			if (GUI.Button(fourthbarslot, sereniticon))
			{
				CurrentSlot = "Slot4";
				Toggleoffoptions();
				serenityoptions = true;
				if (runetooltipon)
					runetooltipon = false;
				skillinventory = true;
			}
		}
		
		#endregion
		//END SLOTS REGION
		
		
		//Sstrike has its own thing
		if (PlayerPrefs.GetInt("Sstrikeabilitystate") > 0)
		{
		
			Slot5content = "Sstrike";
			
				if (GUI.Button(sapecialslot, blank, empty))
				{
					CurrentSlot = "Slot5";
					Toggleoffoptions();
					sapecialoptions = !sapecialoptions;
					if (runetooltipon)
						runetooltipon = false;
					if (skillinventory)
					skillinventory = false;
				}
				if (PlayerPrefs.GetString("Sstrikerune") == "red")
					GUI.DrawTexture(new Rect(sapecialslot.x + 6, sapecialslot.y + 6, 49, 49), SPred);
				if (PlayerPrefs.GetString("Sstrikerune") == "green")
					GUI.DrawTexture(new Rect(sapecialslot.x + 6, sapecialslot.y + 6, 49, 49), SPgreen);
				if (PlayerPrefs.GetString("Sstrikerune") == "blue")
					GUI.DrawTexture(new Rect(sapecialslot.x + 6, sapecialslot.y + 6, 49, 49), SPblue);
				if (PlayerPrefs.GetString("Sstrikerune") == "yellow")
					GUI.DrawTexture(new Rect(sapecialslot.x + 6, sapecialslot.y + 6, 49, 49), SPyellow);
			
			}
			
		
		
//Rune options fo skills--------------------------------------------------------------------------------------------
		
		#region Gsmashoptions------------------------------------------
		if (gsmashoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "GroundSmash");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				gsmashoptions = false;
			}
			Checkabilitystate("Groundsmashabilitystate", gsmashtips);
			
			
			//GSMASH T1 IS NULL
			if (PlayerPrefs.GetString("GroundsmashT1") == "null")
			{

			}
			
			//GSMASH T1 IS BLACK
			if (PlayerPrefs.GetString("GroundsmashT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove")))
				{
					PlayerPrefs.SetString("GroundsmashT1", "null");
				}
				

				GUI.DrawTexture(bigleft, yingalt);
				
			}
			
			//GSMASH T1 IS WHITE
			if (PlayerPrefs.GetString("GroundsmashT1") == "white")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove")))
				{
					PlayerPrefs.SetString("GroundsmashT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			
			//------------------------------------------------
			
			//GSMASH T2 IS NULL
			if (PlayerPrefs.GetString("GroundsmashT2") == "null")
			{

			}
			
			//GSMASH T2 IS BLACK
			if (PlayerPrefs.GetString("GroundsmashT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove")))
				{
					PlayerPrefs.SetString("GroundsmashT2", "null");
				}

				GUI.DrawTexture(bigright, ying);
			}
			
			//GSMASH T2 IS WHITE
			if (PlayerPrefs.GetString("GroundsmashT2") == "white")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove")))
				{
					PlayerPrefs.SetString("GroundsmashT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		
		#endregion
		
		#region Forceoptions--------------------------------------------------------
		if (forceoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Force Push");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				forceoptions = false;
			}
			
			Checkabilitystate("Forceabilitystate", forcetips);
			
			
			//Force T1 IS NULL
			if (PlayerPrefs.GetString("ForceT1") == "null")
			{

			}
			
			//Force T1 IS BLACK
			if (PlayerPrefs.GetString("ForceT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("ForceT1", "null");
				}

				GUI.DrawTexture(bigleft, yingalt);
				
			}
			
			//Force T1 IS WHITE
			if (PlayerPrefs.GetString("ForceT1") == "white")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("ForceT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			
			//------------------------------------------------
			
			//Force T2 IS NULL
			if (PlayerPrefs.GetString("ForceT2") == "null")
			{

			}
			
			//Force T2 IS BLACK
			if (PlayerPrefs.GetString("ForceT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("ForceT2", "null");
				}

				GUI.DrawTexture(bigright, ying);
			}
			
			//Force T2 IS WHITE
			if (PlayerPrefs.GetString("ForceT2") == "white")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("ForceT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		
		#endregion
		
		#region Tackleoptions------------------------------------------
		if (tackleoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Something Buster");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				tackleoptions = false;
			}
			
			Checkabilitystate("Tackleabilitystate", tackletips);
			
			
			//Tackle T1 IS NULL
			if (PlayerPrefs.GetString("TackleT1") == "null")
			{

			}
			
			//Tackle T1 IS BLACK
			if (PlayerPrefs.GetString("TackleT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("TackleT1", "null");
				}
				

				GUI.DrawTexture(bigleft, yingalt);
				
			}
			
			//Tackle T1 IS WHITE
			if (PlayerPrefs.GetString("TackleT1") == "white")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("TackleT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			
			//------------------------------------------------
			
			//Tackle T2 IS NULL
			if (PlayerPrefs.GetString("TackleT2") == "null")
			{

			}
			
			//Tackle T2 IS BLACK
			if (PlayerPrefs.GetString("TackleT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("TackleT2", "null");
				}

				GUI.DrawTexture(bigright, ying);
			}
			
			//Tackle T2 IS WHITE
			if (PlayerPrefs.GetString("TackleT2") == "white")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("TackleT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		
		#endregion
		
		#region Hfistoptions------------------------------------------
		if (hfistoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Hundred Fists");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				hfistoptions = false;
			}
			
			Checkabilitystate("Hfistabilitystate", hfisttips);
			
			
			//Hfist T1 IS NULL
			if (PlayerPrefs.GetString("HfistT1") == "null")
			{

			}
			
			//Hfist T1 IS BLACK
			if (PlayerPrefs.GetString("HfistT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("HfistT1", "null");
				}
				GUI.DrawTexture(bigleft, yingalt);
				
			}
			
			//Hfist T1 IS WHITE
			if (PlayerPrefs.GetString("HfistT1") == "white")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("HfistT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			
			//------------------------------------------------
			
			//Hfist T2 IS NULL
			if (PlayerPrefs.GetString("HfistT2") == "null")
			{

			}
			
			//Hfist T2 IS BLACK
			if (PlayerPrefs.GetString("HfistT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("HfistT2", "null");
				}
				GUI.DrawTexture(bigright, ying);
			}
			
			//Hfist T2 IS WHITE
			if (PlayerPrefs.GetString("HfistT2") == "white")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("HfistT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		
		#endregion
		
		//needs replacement
		#region Finalbody------------------------------------------
		if (hfistoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Final Body");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				hfistoptions = false;
			}
			
			Checkabilitystate("Finalbodyabilitystate", hfisttips);
			
			
			//Hfist T1 IS NULL
			if (PlayerPrefs.GetString("FinalbodyT1") == "null")
			{

			}
			
			//Hfist T1 IS BLACK
			if (PlayerPrefs.GetString("FinalbodyT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("FinalbodyT1", "null");
				}
				GUI.DrawTexture(bigleft, yingalt);
				
			}
			
			//Hfist T1 IS WHITE
			if (PlayerPrefs.GetString("FinalbodyT1") == "white")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("FinalbodyT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			
			//------------------------------------------------
			
			//Hfist T2 IS NULL
			if (PlayerPrefs.GetString("FinalbodyT2") == "null")
			{

			}
			
			//Hfist T2 IS BLACK
			if (PlayerPrefs.GetString("FinalbodyT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("FinalbodyT2", "null");
				}
				GUI.DrawTexture(bigright, ying);
			}
			
			//Hfist T2 IS WHITE
			if (PlayerPrefs.GetString("FinalbodyT2") == "white")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("FinalbodyT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		
		#endregion
		
		#region HUrricane options--------------------------------------------
		if (hurricaneoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Hurricane");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				hurricaneoptions = false;
			}
			Checkabilitystate("Hurricaneabilitystate", hurritips);
			
			
			//Hurricane T1 IS NULL
			if (PlayerPrefs.GetString("HurricaneT1") == "null")
			{	
				
			}
		
			//Hurricane T1 IS BLACK
			if (PlayerPrefs.GetString("HurricaneT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("HurricaneT1", "null");
				}
				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//Hurricane T1 IS WHITE
			if (PlayerPrefs.GetString("HurricaneT1") == "white")
			{
				
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("HurricaneT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//Hurricane T2 IS NULL
			if (PlayerPrefs.GetString("HurricaneT2") == "null")
			{
				
			}	
			
			//Hurricane T2 IS BLACK
			if (PlayerPrefs.GetString("HurricaneT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("HurricaneT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//Hurricane T2 IS WHITE
			if (PlayerPrefs.GetString("HurricaneT2") == "white")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("HurricaneT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		#region Lazer options---------------------------------------------------
		if (lazeroptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Spirit Beam");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				lazeroptions = false;
			}
			Checkabilitystate("Lazerabilitystate", lazertips);
			
			
			//Lazer T1 IS NULL
			if (PlayerPrefs.GetString("LazerT1") == "null")
			{
				
			}
		
			//Lazer T1 IS BLACK
			if (PlayerPrefs.GetString("LazerT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("LazerT1", "null");
				}

				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//Lazer T1 IS WHITE
			if (PlayerPrefs.GetString("LazerT1") == "white")
			{
				
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("LazerT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//Lazer T2 IS NULL
			if (PlayerPrefs.GetString("LazerT2") == "null")
			{
				
			}	
			
			//Lazer T2 IS BLACK
			if (PlayerPrefs.GetString("LazerT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("LazerT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//Lazer T2 IS WHITE
			if (PlayerPrefs.GetString("LazerT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("LazerT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		//needs replacement
		#region 3rd s---------------mind------------------------------------
		if (lazeroptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Third Mind");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				lazeroptions = false;
			}
			Checkabilitystate("Thirdmindabilitystate", lazertips);
			
			
			//Lazer T1 IS NULL
			if (PlayerPrefs.GetString("ThirdmindT1") == "null")
			{
				
			}
		
			//Lazer T1 IS BLACK
			if (PlayerPrefs.GetString("ThirdmindT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("ThirdmindT1", "null");
				}

				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//Lazer T1 IS WHITE
			if (PlayerPrefs.GetString("ThirdmindT1") == "white")
			{
				
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("ThirdmindT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//Lazer T2 IS NULL
			if (PlayerPrefs.GetString("ThirdmindT2") == "null")
			{
				
			}	
			
			//Lazer T2 IS BLACK
			if (PlayerPrefs.GetString("ThirdmindT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("ThirdmindT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//Lazer T2 IS WHITE
			if (PlayerPrefs.GetString("ThirdmindT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("ThirdmindT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		
		#region Serenity Options----------------------------------
		if (serenityoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Serenity");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				serenityoptions = false;
			}
			
			Checkabilitystate("Serenityabilitystate", serentips);
//			if (runetooltipon)
//				GUI.Label (Runetooltip, currenttooltip[tooltipindex]);
			
			//Serenity T1 IS NULL
			if (PlayerPrefs.GetString("SerenityT1") == "null")
			{
				
			}
		
			//Serenity T1 IS BLACK
			if (PlayerPrefs.GetString("SerenityT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("SerenityT1", "null");
				}
				
				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//Serenity T1 IS WHITE
			if (PlayerPrefs.GetString("SerenityT1") == "white")
			{
				
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("SerenityT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//Serenity T2 IS NULL
			if (PlayerPrefs.GetString("SerenityT2") == "null")
			{
				
			}	
			
			//Serenity T2 IS BLACK
			if (PlayerPrefs.GetString("SerenityT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("SerenityT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//Serenity T2 IS WHITE
			if (PlayerPrefs.GetString("SerenityT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("SerenityT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		
		#region Warudo Options----------------------------------
		if (warudooptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Za Warudo");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				warudooptions = false;
			}
			Checkabilitystate("Warudoabilitystate", warudotips);

			
			//Warudo T1 IS NULL
			if (PlayerPrefs.GetString("WarudoT1") == "null")
			{
				
			}
		
			//Warudo T1 IS BLACK
			if (PlayerPrefs.GetString("WarudoT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("WarudoT1", "null");
				}
				
				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//Warudo T1 IS WHITE
			if (PlayerPrefs.GetString("WarudoT1") == "white")
			{
				
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("WarudoT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//Warudo T2 IS NULL
			if (PlayerPrefs.GetString("WarudoT2") == "null")
			{
				
			}	
			
			//Warudo T2 IS BLACK
			if (PlayerPrefs.GetString("WarudoT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("WarudoT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//Warudo T2 IS WHITE
			if (PlayerPrefs.GetString("WarudoT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("WarudoT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		#region Spirit Bomb options-------------------------------------
		if (sbomboptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Spirit Bomb");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				sbomboptions = false;
			}
			
			Checkabilitystate("Spiritbombabilitystate", spiritbtips);
//			if (runetooltipon)
//				GUI.Label (Runetooltip, currenttooltip[tooltipindex]);
			
			//Spiritbomb T1 IS NULL
			if (PlayerPrefs.GetString("SpiritbombT1") == "null")
			{
				
			}
		
			//Spiritbomb T1 IS BLACK
			if (PlayerPrefs.GetString("SpiritbombT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("SpiritbombT1", "null");
				}
				
				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//Spiritbomb T1 IS WHITE
			if (PlayerPrefs.GetString("SpiritbombT1") == "white")
			{
				
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("SpiritbombT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//Spiritbomb T2 IS NULL
			if (PlayerPrefs.GetString("SpiritbombT2") == "null")
			{
				
			}	
			
			//Spiritbomb T2 IS BLACK
			if (PlayerPrefs.GetString("SpiritbombT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("SpiritbombT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//Spiritbomb T2 IS WHITE
			if (PlayerPrefs.GetString("SpiritbombT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("SpiritbombT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
	
		
		#region Teledoor Options----------------------------------
		if (tdooroptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Teledoor");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				tdooroptions = false;
			}
			
			Checkabilitystate("Teledoorabilitystate", tdoortips);
//			if (runetooltipon)
//				GUI.Label (Runetooltip, currenttooltip[tooltipindex]);
			
			//Tdoor T1 IS NULL
			if (PlayerPrefs.GetString("TeledoorT1") == "null")
			{
				
			}
		
			//Serenity T1 IS BLACK
			if (PlayerPrefs.GetString("TeledoorT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("TeledoorT1", "null");
				}
				
				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//Serenity T1 IS WHITE
			if (PlayerPrefs.GetString("TeledoorT1") == "white")
			{
				
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("TeledoorT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//Serenity T2 IS NULL
			if (PlayerPrefs.GetString("TeledoorT2") == "null")
			{
				
			}	
			
			//Serenity T2 IS BLACK
			if (PlayerPrefs.GetString("TeledoorT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("TeledoorT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//Serenity T2 IS WHITE
			if (PlayerPrefs.GetString("TeledoorT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("TeledoorT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		#region Clone Options----------------------------------
		if (cloneoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Mirror Image");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				cloneoptions = false;
			}
			
			Checkabilitystate("Cloneabilitystate", clonetips);
//			if (runetooltipon)
//				GUI.Label (Runetooltip, currenttooltip[tooltipindex]);
			
			//clone T1 IS NULL
			if (PlayerPrefs.GetString("CloneT1") == "null")
			{
				
			}
		
			//clone T1 IS BLACK
			if (PlayerPrefs.GetString("CloneT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("CloneT1", "null");
				}
				
				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//clone T1 IS WHITE
			if (PlayerPrefs.GetString("CloneT1") == "white")
			{
				GUI.color = Color.black;
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("CloneT1", "null");
				}
				GUI.color = Color.white;
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//clone T2 IS NULL
			if (PlayerPrefs.GetString("CloneT2") == "null")
			{
				
			}	
			
			//clone T2 IS BLACK
			if (PlayerPrefs.GetString("CloneT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("CloneT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//clone T2 IS WHITE
			if (PlayerPrefs.GetString("CloneT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("CloneT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		
		#region Wall Options----------------------------------
		if (walloptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Summon Aegis Weapon");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				cloneoptions = false;
			}
			Checkabilitystate("Wallabilitystate", walltips);

			
			//wall T1 IS NULL
			if (PlayerPrefs.GetString("WallT1") == "null")
			{
				
			}
		
			//wall T1 IS BLACK
			if (PlayerPrefs.GetString("WallT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("WallT1", "null");
				}
				
				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//wall T1 IS WHITE
			if (PlayerPrefs.GetString("WallT1") == "white")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("WallT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//wall T2 IS NULL
			if (PlayerPrefs.GetString("WallT2") == "null")
			{
				
			}	
			
			//wall T2 IS BLACK
			if (PlayerPrefs.GetString("WallT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("WallT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//wall T2 IS WHITE
			if (PlayerPrefs.GetString("WallT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("WallT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		
		#region Vplate Options----------------------------------
		if (vplateoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(skilllabel, "Vector Plate");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			//GTFO
			if (GUI.Button(removebox, "Unequip skill"))
			{
				PlayerPrefs.SetString(CurrentSlot, "null");
				if (CurrentSlot == "Slot1")
				Slot1content = "null";
				if (CurrentSlot == "Slot2")
				Slot2content = "null";
				if (CurrentSlot == "Slot3")
				Slot3content = "null";
				if (CurrentSlot == "Slot4")
				Slot4content = "null";
				vplateoptions = false;
			}
			Checkabilitystate("Vplateabilitystate", vplatetips);

			
			//Vplate T1 IS NULL
			if (PlayerPrefs.GetString("VplateT1") == "null")
			{
				
			}
		
			//Vplate T1 IS BLACK
			if (PlayerPrefs.GetString("VplateT1") == "black")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("VplateT1", "null");
				}
				
				GUI.DrawTexture(bigleft, yingalt);
			}
			
			//Vplate T1 IS WHITE
			if (PlayerPrefs.GetString("VplateT1") == "white")
			{
				if (GUI.Button(Bigleftbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("VplateT1", "null");
				}
				GUI.DrawTexture(bigleft, yang);
			}
			
			//TIER 2---------------------
			
			//Vplate T2 IS NULL
			if (PlayerPrefs.GetString("VplateT2") == "null")
			{
				
			}	
			
			//Vplate T2 IS BLACK
			if (PlayerPrefs.GetString("VplateT2") == "black")
			{
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("VplateT2", "null");
				}
				
				GUI.DrawTexture(bigright, ying);
			}
			
			//Vplate T2 IS WHITE
			if (PlayerPrefs.GetString("VplateT2") == "white")
			{
				
				if (GUI.Button(Bigrightbutton, new GUIContent(blank, "Remove"), empty))
				{
					PlayerPrefs.SetString("VplateT2", "null");
				}
				GUI.DrawTexture(bigright, yangalt);
			}
			
		}
		#endregion
		
		
		
		#region Sapecial Options----------------------------------
		if (sapecialoptions)
		{
			GUI.skin = dojoskin;
			dojoskin.label.fontSize = 28;
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			GUI.Label(new Rect(skilllabel.x, skilllabel.y + 168, skilllabel.width, skilllabel.height), "Special Strike");
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin = null;
			
			
			Checksapecial("Sstrikeabilitystate", Sstriketips);

			
			//Sstrike rune IS NULL
			if (PlayerPrefs.GetString("Sstrikerune") == "null")
			{

			}
		
			//Sstrike rune is yellow
			if (PlayerPrefs.GetString("Sstrikerune") != "null")
			{
				if (GUI.Button(new Rect(Sstrikebox.x + 500, Sstrikebox.y, 100, 25), "Remove Gem"))
				{
					PlayerPrefs.SetString("Sstrikerune", "null");
				}
			}
			
		}
		#endregion
		
		
				
//skill inventory------------------------------------------------------------------------
		
		#region Skill inventory------------------
		if (skillinventory)
			{
				if (CurrentSlot == null)
				GUI.Label(skilllabel, "Choose an ability to place in the slot.");
			
				if (GUI.Button(bodyinventory, blank))
				{
					minventory = false;
					skinventory = false;
					binventory = true;
				}
			
				if (GUI.Button(mindinventory, blank))
				{
					minventory = true;
					skinventory = false;
					binventory = false;
				}
			
				if (GUI.Button(sakillinventory, blank))
				{
					minventory = false;
					skinventory = true;
					binventory = false;
				}
				
				
				if (binventory)
				{
					if (PlayerPrefs.GetInt("Groundsmashabilitystate") > 0)
					{
						//GSMASH
						if (GUI.Button(inventory1, gsmashicon ))
						{
							PlayerPrefs.SetString(CurrentSlot, "Groundsmash");
							if (CurrentSlot == "Slot1")
							Slot1content = "Groundsmash";
							if (CurrentSlot == "Slot2")
							Slot2content = "Groundsmash";
							if (CurrentSlot == "Slot3")
							Slot3content = "Groundsmash";
							if (CurrentSlot == "Slot4")
							Slot4content = "Groundsmash";
							Toggleoffoptions();
							skillinventory = true;
							gsmashoptions = true;
						}
						InventoryCircles("GroundsmashT1", "GroundsmashT2", "Groundsmashabilitystate", 1);
						
					}
				
				if (PlayerPrefs.GetInt("Tackleabilitystate") > 0)
					{
						//Tackle
						if (GUI.Button(inventory2, tackleicon ))
						{
							PlayerPrefs.SetString(CurrentSlot, "Tackle");
							if (CurrentSlot == "Slot1")
							Slot1content = "Tackle";
							if (CurrentSlot == "Slot2")
							Slot2content = "Tackle";
							if (CurrentSlot == "Slot3")
							Slot3content = "Tackle";
							if (CurrentSlot == "Slot4")
							Slot4content = "Tackle";
							Toggleoffoptions();
							skillinventory = true;
							tackleoptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 1)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
						}
						
						InventoryCircles("TackleT1", "TackleT2", "Tackleabilitystate", 2);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 1 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 100, 52, 17));
					}
				
					
				if (PlayerPrefs.GetInt("Forceabilitystate") > 0)
					{	
						//Force
						if (GUI.Button(inventory3, forceicon ))
						{
							PlayerPrefs.SetString(CurrentSlot, "Force");
							if (CurrentSlot == "Slot1")
							Slot1content = "Force";
							if (CurrentSlot == "Slot2")
							Slot2content = "Force";
							if (CurrentSlot == "Slot3")
							Slot3content = "Force";
							if (CurrentSlot == "Slot4")
							Slot4content = "Force";
							Toggleoffoptions();
							skillinventory = true;
							forceoptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 2)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
						}
						
						
						InventoryCircles("ForceT1", "ForceT2", "Forceabilitystate", 3);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 2 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 200, 52, 17));
					}
				
					if (PlayerPrefs.GetInt("Hfistabilitystate") > 0)
					{	
						//HFIST
						if (GUI.Button(inventory4, hfisticon ))
						{
							PlayerPrefs.SetString(CurrentSlot, "Hfist");
							if (CurrentSlot == "Slot1")
							Slot1content = "Hfist";
							if (CurrentSlot == "Slot2")
							Slot2content = "Hfist";
							if (CurrentSlot == "Slot3")
							Slot3content = "Hfist";
							if (CurrentSlot == "Slot4")
							Slot4content = "Hfist";
							Toggleoffoptions();
							skillinventory = true;
							hfistoptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 9)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
						}
						
						InventoryCircles("HfistT1", "HfistT2", "Hfistabilitystate", 4);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 9 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 200, 52, 17));
					}
					
					//final body needs a replacement
					if (PlayerPrefs.GetInt("Finalbodyabilitystate") > 0)
					{	
						//Fbody
						if (GUI.Button(inventory5, finalbodyicon ))
						{
							PlayerPrefs.SetString(CurrentSlot, "Finalb");
							if (CurrentSlot == "Slot1")
							Slot1content = "Finalb";
							if (CurrentSlot == "Slot2")
							Slot2content = "Finalb";
							if (CurrentSlot == "Slot3")
							Slot3content = "Finalb";
							if (CurrentSlot == "Slot4")
							Slot4content = "Finalb";
							Toggleoffoptions();
							skillinventory = true;
							forceoptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 10)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
						}
						
						
						InventoryCircles("FinalbodyT1", "FinalbodyT2", "Finalbodyabilitystate", 5);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 10 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 200, 52, 17));
					}
				
				}//end bINVENTORY
			
				if (minventory)
				{
				
					if (PlayerPrefs.GetInt("Hurricaneabilitystate") > 0)
					{
						//HURRICANE
						if (GUI.Button(inventory1, hurricon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Hurricane");
							if (CurrentSlot == "Slot1")
							Slot1content = "Hurricane";
							if (CurrentSlot == "Slot2")
							Slot2content = "Hurricane";
							if (CurrentSlot == "Slot3")
							Slot3content = "Hurricane";
							if (CurrentSlot == "Slot4")
							Slot4content = "Hurricane";
							Toggleoffoptions();
							skillinventory = true;
							hurricaneoptions = true;
						}
						
						InventoryCircles("HurricaneT1", "HurricaneT2", "Hurricaneabilitystate", 1);
					}
						
				
					if (PlayerPrefs.GetInt("Lazerabilitystate") > 0)
					{
						//LAZER
						if (GUI.Button(inventory2, beamicon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Lazer");
							if (CurrentSlot == "Slot1")
							Slot1content = "Lazer";
							if (CurrentSlot == "Slot2")
							Slot2content = "Lazer";
							if (CurrentSlot == "Slot3")
							Slot3content = "Lazer";
							if (CurrentSlot == "Slot4")
							Slot4content = "Lazer";
							Toggleoffoptions();
							skillinventory = true;
							lazeroptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 4)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
						};
					
						InventoryCircles("LazerT1", "LazerT2", "Lazerabilitystate", 2);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 4 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 100, 52, 17));		
					}
				
					//3rd mind ability needs replacement
					if (PlayerPrefs.GetInt("Thirdmindabilitystate") > 0)
					{
						//LAZER
						if (GUI.Button(inventory3, thirdmindicon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Thirdmind");
							if (CurrentSlot == "Slot1")
							Slot1content = "Thirdmind";
							if (CurrentSlot == "Slot2")
							Slot2content = "Thirdmind";
							if (CurrentSlot == "Slot3")
							Slot3content = "Thirdmind";
							if (CurrentSlot == "Slot4")
							Slot4content = "Thirdmind";
							Toggleoffoptions();
							skillinventory = true;
							lazeroptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 11)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
						};
					
						InventoryCircles("ThirdmindT1", "ThirdmindT2", "Thirdmindabilitystate", 3);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 11 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 100, 52, 17));		
					}
				

				if (PlayerPrefs.GetInt("Serenityabilitystate") > 0)
					{
						//SERENITY
						if (GUI.Button(inventory4, sereniticon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Serenity");
							if (CurrentSlot == "Slot1")
							Slot1content = "Serenity";
							if (CurrentSlot == "Slot2")
							Slot2content = "Serenity";
							if (CurrentSlot == "Slot3")
							Slot3content = "Serenity";
							if (CurrentSlot == "Slot4")
							Slot4content = "Serenity";
							Toggleoffoptions();
							skillinventory = true;	
							serenityoptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 5)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
							
						}
					
						InventoryCircles("SerenityT1", "SerenityT2", "Serenityabilitystate", 4);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 5 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 200, 52, 17));	
					}
				
				if (PlayerPrefs.GetInt("Warudoabilitystate") > 0)
					{
						//Warudo 
						if (GUI.Button(inventory5, warudoicon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Warudo");
							if (CurrentSlot == "Slot1")
							Slot1content = "Warudo";
							if (CurrentSlot == "Slot2")
							Slot2content = "Warudo";
							if (CurrentSlot == "Slot3")
							Slot3content = "Warudo";
							if (CurrentSlot == "Slot4")
							Slot4content = "Warudo";
							Toggleoffoptions();
							skillinventory = true;	
							warudooptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 12)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
							
						}
					
						InventoryCircles("WarudoT1", "WarudoT2", "Warudoabilitystate", 5);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 12 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 200, 52, 17));	
					}
								
								
				}//end of minventory
				
				if (skinventory)
				{
				
					if (PlayerPrefs.GetInt("Spiritbombabilitystate") > 0)
					{
						//SPIRITB
						if (GUI.Button(inventory1, spiritbicon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Spiritbomb");
							if (CurrentSlot == "Slot1")
							Slot1content = "Spiritbomb";
							if (CurrentSlot == "Slot2")
							Slot2content = "Spiritbomb";
							if (CurrentSlot == "Slot3")
							Slot3content = "Spiritbomb";
							if (CurrentSlot == "Slot4")
							Slot4content = "Spiritbomb";
							Toggleoffoptions();
							skillinventory = true;	
							sbomboptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 3)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
							
						}
					
						InventoryCircles("SpiritbombT1", "SpiritBombT2", "Spiritbombabilitystate", 1);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 3 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y, 52, 17));			
					}			
										
					if (PlayerPrefs.GetInt("Cloneabilitystate") > 0)
					{
						//Mirri Image
						if (GUI.Button(inventory2, cloneicon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Clone");
							if (CurrentSlot == "Slot1")
							Slot1content = "Clone";
							if (CurrentSlot == "Slot2")
							Slot2content = "Clone";
							if (CurrentSlot == "Slot3")
							Slot3content = "Clone";
							if (CurrentSlot == "Slot4")
							Slot4content = "Clone";
							Toggleoffoptions();
							skillinventory = true;	
							cloneoptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 8)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
						}
	//					GUI.Label(GetmouseRect(), GUI.tooltip);
						
						InventoryCircles("CloneT1", "CloneT2", "Cloneabilitystate", 2);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 8 && rainbowon)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 100, 52, 17));		
					}
				
					if (PlayerPrefs.GetInt("Teledoorabilitystate") > 0)
					{
						//TELEDOOR
						if (GUI.Button(inventory3, tdooricon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Teledoor");
							if (CurrentSlot == "Slot1")
							Slot1content = "Teledoor";
							if (CurrentSlot == "Slot2")
							Slot2content = "Teledoor";
							if (CurrentSlot == "Slot3")
							Slot3content = "Teledoor";
							if (CurrentSlot == "Slot4")
							Slot4content = "Teledoor";
							Toggleoffoptions();
							skillinventory = true;
							tdooroptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 6)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
							
						}
	//					GUI.Label(GetmouseRect(), GUI.tooltip);
					
						InventoryCircles("TeledoorT1", "TeledoorT2", "Teledoorabilitystate", 3);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 6)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 200, 52, 17));						
					}
				
				if (PlayerPrefs.GetInt("Wallabilitystate") > 0)
					{
						//Wall
						if (GUI.Button(inventory4, wallicon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Wall");
							if (CurrentSlot == "Slot1")
							Slot1content = "Wall";
							if (CurrentSlot == "Slot2")
							Slot2content = "Wall";
							if (CurrentSlot == "Slot3")
							Slot3content = "Wall";
							if (CurrentSlot == "Slot4")
							Slot4content = "Wall";
							Toggleoffoptions();
							skillinventory = true;
							walloptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 7)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
							
						}
	//					GUI.Label(GetmouseRect(), GUI.tooltip);
				
						InventoryCircles("WallT1", "WallT2", "Wallabilitystate", 4);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 7)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 300, 52, 17));	
					}
				
					if (PlayerPrefs.GetInt("Vplateabilitystate") > 0)
					{
						//Vector plate
						if (GUI.Button(inventory4, vplateicon))
						{
							PlayerPrefs.SetString(CurrentSlot, "Vplate");
							if (CurrentSlot == "Slot1")
							Slot1content = "Vplate";
							if (CurrentSlot == "Slot2")
							Slot2content = "Vplate";
							if (CurrentSlot == "Slot3")
							Slot3content = "Vplate";
							if (CurrentSlot == "Slot4")
							Slot4content = "Vplate";
							Toggleoffoptions();
							skillinventory = true;
							vplateoptions = true;
							if (PlayerPrefs.GetInt("Newabilitynotice") == 13)
								PlayerPrefs.SetInt("Newabilitynotice", 0);
							
						}
	//					GUI.Label(GetmouseRect(), GUI.tooltip);
				
						InventoryCircles("VplateT1", "VplateT2", "Vplateabilitystate", 5);
						if (PlayerPrefs.GetInt("Newabilitynotice") == 13)
						shinynewtext( new Rect(inventorynewlabel.x, inventorynewlabel.y + 300, 52, 17));	
					}
												
												
				}//end SKinventory
			
			}//end Skill inventory
		
	#endregion
		
		
		// This is for the help pic
		if (help1)
		{
			GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), helpic1);
			
			GUI.skin = returnbutton;
			if (GUI.Button(Storeback, " ") || Input.GetKeyDown(KeyCode.Escape))
				help1 = false;
			GUI.DrawTexture(Storeback, returntext);
			GUI.skin = null;
		}
		
	} //END OF RUNES SHIT
	
	
	void StoreShit(int windowID)
	{
		//secret get money cheat
			if (Input.GetKeyDown(KeyCode.RightBracket))
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 500);
		
			if (Input.GetKeyDown(KeyCode.LeftBracket))
				PlayerPrefs.SetInt("Playerlevel", PlayerPrefs.GetInt("Playerlevel") + 1);
		
		//GTFO
		GUI.skin = dojoskin;
		if (Input.GetKeyDown(KeyCode.Escape) || GUI.Button(new Rect(-80, Screen.height - 60, 280, 70), "     Return"))
		{
			StoreUI = false;
			cancelallstatscreens();
			individualskill = false;
			if (storeskills)
				storeskills = false;
			if (storerunes)
				storerunes = false;
			skillquestion = false;
		}
		
		//top side plates
		GUI.DrawTexture(topleftplate, lefttop);
		GUI.DrawTexture(statsplate, righttop);
		GUI.Label(new Rect(topleftplate.x + 40, topleftplate.y + 10, 300, 100), "Dojo Store", newfont);
			
		//Afrotv in store
		GUI.DrawTexture(tvmainrect, tv);
		GUI.DrawTexture(new Rect(tvmainrect.x + 18, tvmainrect.y + 18, 430, 320), tvbg);
		GUI.DrawTexture(new Rect(tvmainrect.x + 18, tvmainrect.y + 18, 430, 320), screenshade);
				
		GUI.DrawTexture(new Rect(tvmainrect.x + 320, tvmainrect.y + 210, 82, 108), sensui);
		GUI.DrawTexture(new Rect(tvmainrect.x + 60, tvmainrect.y + 200, 90, 120), afroman);
		
		//Chatbox at bottom
		GUI.DrawTexture(chatbox, chatboxtexture);
		
		
		
		//Leftside choices
		if (GUI.Button(firstoption, "Train Skill"))
		{
			storerunes = false;
			storegambling = false;
			storeskills = true;
		}
		
		if (GUI.Button(secondoption, "Craft Runes"))
		{
			storerunes = true;
			storegambling = false;
			storeskills = false;
		}
		
		if (GUI.Button(thirdoption, "Gambling"))
		{
			storerunes = false;
			storegambling = true;
			storeskills = false;
			message = "Gambling is not available in the demo version of this game.";
			printmessage();
		}
		 
		//Store skills
		if (storeskills)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				storeskills = false;
				individualskill = false;
				skillquestion = false;
				cancelallstatscreens();
			}
			
			GUI.skin = dojoskin;	
			
			
			if (!individualskill)
			{
				GUI.DrawTexture(skillstvbox, trainskillstv);
				Queryavailables();
				
			}
			
			if (individualskill)
			{
				GUI.DrawTexture(skillstvbox, individualtv);
				Skillscreens();
				
			}

		}//END STORE SKILLS
		
		//store runes
		if (storerunes)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				storerunes = false;
				individualskill = false;
				skillquestion = false;
				cancelallstatscreens();
				
			}
			
			if (!individualskill)
			{
				GUI.DrawTexture(skillstvbox, trainskillstv);
				Queryavailables();
				
			}
			
			if (individualskill)
			{
				GUI.DrawTexture(skillstvbox, individualtv);
				Skillscreens();
				
			}

		}
		
		//SCROLLS STUFF
		GUI.DrawTexture(scrollboxmain, scrollssale);
		
		GUI.skin = justqmark;
		GUI.Label(new Rect(scrollbox1.x + 15, scrollbox1.y, 100, 50), " ? ");
		GUI.skin = dojoskin;
		
		//random scroll availability
		if (scrolllayout == 1)
		{
			GUI.DrawTexture(scrollbox2, redscroll);
		}
		if (scrolllayout == 2)
		{
			GUI.DrawTexture(scrollbox2, greenscroll);
		}
		if (scrolllayout == 3)
		{
			GUI.DrawTexture(scrollbox2, bluescroll);
		}
		GUI.DrawTexture(scrollbox3, blackscroll);
		GUI.DrawTexture(scrollbox4, whitescroll);
		
			//scroll purchase buttons
			GUI.skin = wood;
		
			//first scrollbox
			if (GUI.Button(new Rect(scrollboxmain.x + 10, scrollboxmain.y + 105, 190, 40), "Buy it! - $100") && !dailybought)
			{
				Purchaseinfo("Daily Special", 100, false, 0);
				
			}
			
			//2nd scrollbox
			if (GUI.Button(new Rect(scrollboxmain.x + 10, scrollboxmain.y + 257, 190, 20), "Buy 1 scroll - $15"))
			{
				if (scrolllayout == 1)
				Purchaseinfo("Red Scroll", 15, false, 0);
			
				if (scrolllayout == 2)
				Purchaseinfo("Green Scroll", 15, false, 0);
			
				if (scrolllayout == 3)
				Purchaseinfo("Blue Scroll", 15, false, 0);
			}
			if (GUI.Button(new Rect(scrollboxmain.x + 10, scrollboxmain.y + 277, 190, 20), "Buy 5 scrolls - $75"))
			{
				if (scrolllayout == 1)
				Purchaseinfo("Red Bundle", 75, false, 0);
			
				if (scrolllayout == 2)
				Purchaseinfo("Green Bundle", 75, false, 0);
			
				if (scrolllayout == 3)
				Purchaseinfo("Blue Bundle", 75, false, 0);
			}
			
			//blackscroll box
			if (GUI.Button(new Rect(scrollboxmain.x + 10, scrollboxmain.y + 410, 190, 20), "Buy with cash - $50"))
			{
				Purchaseinfo("Black Scroll", 50, false, 0);
			}
			if (GUI.Button(new Rect(scrollboxmain.x + 10, scrollboxmain.y + 431, 190, 20), "Buy with scrolls - 3"))
			{
				Purchaseinfo("Black Scroll", 0, true, 3);
					
			}
			GUI.DrawTexture(new Rect(scrollboxmain.x + 185, scrollboxmain.y + 430, 6, 20), tinyscroll);
			
			//whitescroll box
			if (GUI.Button(new Rect(scrollboxmain.x + 10, scrollboxmain.y + 564, 190, 20), "Buy with cash - $50"))
			{
				Purchaseinfo("White Scroll", 50, false, 0);
				
			}
			if (GUI.Button(new Rect(scrollboxmain.x + 10, scrollboxmain.y + 585, 190, 20), "Buy with scrolls - 3"))
			{
				Purchaseinfo("White Scroll", 0, true, 3);
					
			}
			GUI.DrawTexture(new Rect(scrollboxmain.x + 185, scrollboxmain.y + 585, 6, 22), tinyscroll);
			
			
			GUI.skin = null;
		
		//generic message for neutral screen
		if (!storemouseover && !storeskills && !storerunes && !purchasepopup && !Enterbattlepopup)
		{
			GUI.DrawTexture(new Rect(tvmainrect.x + 45, tvmainrect.y + 45, 360, 172), bubble);
			GUI.color = Color.black;
			comic.fontSize = 18;
			GUI.Label(Storeline1, "What is it you are looking for, plebian?\nMake it quick, I must have my afternoon tea.", comic);
			comic.fontSize = 16;
			GUI.color = Color.white;
		}
		
	}//end of Store shit
	
	
	
	void Purchaseinfo(string item, int price, bool scrolls, int scrolprice)
	{
		purchaseitem = item;
		purchaseprice = price;
		scrollcost = scrolls;
		scrollprice = scrolprice;
		purchasepopup = true;
	}
	
	void Purchaseinfo(string item, int price, bool scrolls, int scrolprice, bool amountchange)
	{
		purchaseitem = item;
		purchaseprice = price;
		scrollcost = scrolls;
		scrollprice = scrolprice;
		amountchanger = true;
	}
	
	void Clearpurchaseinfo()
	{
		purchaseitem = null;
		purchasenumber = 1;
	}
	
	void Popupshit(int windowID)
	{
		//GTFO
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			purchasepopup = false;	
			scrollscostwindow = false;	
			amountchanger = false;
		}
		
		
		//normal purchase shit tier scrolls
		if (!scrollcost && scrollprice == 0)
		{
			GUI.Label(new Rect(40, 20, 120, 50), "Purchase a " + purchaseitem + " for $" + purchaseprice.ToString() + "?");
		}
		
		//buying scrolls with scrolls
		if (scrollcost)
		{
			GUI.Label(new Rect(40, 20, 120, 50), "Purchase " + purchaseitem + " for ");	
			GUI.Label(new Rect(114, 40, 40, 50), scrollprice.ToString() + "   ?");
			GUI.DrawTexture(new Rect(126, 42, 6, 22), tinyscroll);
		}
		
	}
					
	void Afropopup(int windowID)
	{
		//GTFO
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			storeskills = false;
			purchasepopup = false;
			scrollscostwindow = false;
			Enterbattlepopup = false;
			if (choosecheckpoint)
				choosecheckpoint = false;
		}
		
		if (yesdetection.Contains(mousePos) || yesbox.Contains(mousePos))
		{
			if (GUI.Button(new Rect(yesbox.x, yesbox.y + Yoffset, yesbox.width, yesbox.height), yestext, empty))
			{
				//purchasing scrolls with money
				if (!scrollcost && scrollprice == 0)
				{
					if (purchaseprice > PlayerPrefs.GetInt("Money"))
					{
						message = "You do not have enough, fool.";
						printmessage();
						purchasepopup = false;
						return;
					}
					Buyitem();
					audio.PlayOneShot(cash);
					purchasepopup = false;
					amountchanger = false;
					scrollscostwindow = false;
				}
				
				//purchasing scrolls with scrolls
				if (scrollcost)
				{
					scrollscostwindow = true;
				}
				
				//purchasing skills
				if (skillquestion)
				{
					//FAIL. not enough money
					if (!Enoughmoneys())
					{
						message = "You do not have enough, fool.";
						printmessage();
						skillquestion = false;
						storeskills = false;
						individualskill = false;
						cancelallstatscreens();
						
						return;
					}
					
					//success
					else
					{
						newskilllearn();
						
						audio.PlayOneShot(cash);
						
						skillquestion = false;
//						storeskills = false;
//						individualskill = false;
//						skillquestion = false;
//						cancelallstatscreens();
					}

				}
				
				//purchasing runes
				if (runesquestion)
				{
					if (PlayerPrefs.GetInt("Money") < 100)
					{
						message = "You do not have enough, fool.";
						printmessage();
						runesquestion = false;
						storerunes = false;
						runetier = 0;
						individualskill = false;
						cancelallstatscreens();
						
						return;
					}
					
					if (runetype == 1)
					newruneslearnblack();
					
					if (runetype == 2)
					newruneslearnwhite();
					
					audio.PlayOneShot(cash);
					
					runesquestion = false;
					runetier = 0;
					runetype = 0;
//					cancelallstatscreens();
				}
				
				//Endless mode question
				if (Enterbattlepopup)
				{
					if (!choosecheckpoint)
					{
						if (PlayerPrefs.GetInt("EndlessMax") == 0)
						{
							PlayerPrefs.SetString("Startfromcheckpoint", "n");
							StartCoroutine ( Dofadeout("Endless") );
						}
						
						if (PlayerPrefs.GetInt("EndlessMax") > 0)
						{	
							choosecheckpoint = true;
						}
					}
					
					if (choosecheckpoint)
					{
						
						
					}
					
				}
				
			}
			
		}
		
		if (!yesdetection.Contains(mousePos))
		{
			if (GUI.Button(yesbox, yestext, empty))
			{
				
			}
			
		}
		
		if (nodetection.Contains(mousePos) || nobox.Contains(mousePos))
		{
			if (GUI.Button(new Rect(nobox.x, nobox.y + Yoffset, nobox.width, nobox.height), notext, empty))
			{
				purchasepopup = false;
				amountchanger = false;
				scrollscostwindow = false;	
				storeskills = false;
				storerunes = false;
				skillquestion = false;
				runesquestion = false;
				individualskill = false;
				cancelallstatscreens();
				Enterbattlepopup = false;
				if (choosecheckpoint)
					choosecheckpoint = false;
			}
			
		}
		
		if (!nodetection.Contains(mousePos))
		{
			if (GUI.Button(nobox, notext, empty))
			{
				purchasepopup = false;
				amountchanger = false;
				scrollscostwindow = false;	
				skillquestion = false;
				storeskills = false;
				individualskill = false;
				cancelallstatscreens();
				Enterbattlepopup = false;
				if (choosecheckpoint)
					choosecheckpoint = false;
			}
			
		}
		
		
	}//end afropop
	
	void Buyitem()
	{
		//scrolls
		//single scrolls
		if (purchaseitem == "Red Scroll")
		{
			PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") + 1);
			message = "Purchased Red Scroll x 1!";
			printmessage();
		}
		if (purchaseitem == "Green Scroll")
		{
			PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + 1);
			message = "Purchased Green Scroll x 1!";
			printmessage();
		}
		if (purchaseitem == "Blue Scroll")
		{
			PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") + 1);
			message = "Purchased Blue Scroll x 1!";
			printmessage();
		}
		
		//bundles
		if (purchaseitem == "Red Bundle")
		{
			PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") + 5);
			message = "Purchased Red Scroll x 5!";
			printmessage();
		}
		if (purchaseitem == "Green Bundle")
		{
			PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + 5);
			message = "Purchased Green Scroll x 5!";
			printmessage();
		}
		if (purchaseitem == "Blue Bundle")
		{
			PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") + 5);
			message = "Purchased Blue Scroll x 5!";
			printmessage();
		}
		
		if (purchaseitem == "Black Scroll" && !scrollcost)
		{
			PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") + 1);
			message = "Purchased Black Scroll x 1!";
			printmessage();
		}
		
		if (purchaseitem == "Black Scroll" && scrollcost)
		{
			PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") + ((redcost + greencost + blucost) / 3));
			message = "Purchased Black Scroll x 1!";
			printmessage();
		}
		
		if (purchaseitem == "White Scroll" && !scrollcost)
		{
			PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") + 1);
			message = "Purchased White Scroll x 1!";
			printmessage();
		}
		
		if (purchaseitem == "White Scroll" && scrollcost)
		{
			PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") + ((redcost + greencost + blucost) / 3));
			message = "Purchased White Scroll x 1!";
			printmessage();
		}
		
		//Random deals
		if (purchaseitem == "Daily Special")
		{
			//First Item
			if (daily1 == "Red Scroll")
			{
				PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") + dailyamt1);
				message = "Purchased Red Scroll x" + dailyamt1.ToString() + "!";
				printmessage();
			}
			if (daily1 == "Green Scroll")
			{
				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + dailyamt1);
				message = "Purchased Green Scroll x" + dailyamt1.ToString() + "!";
				printmessage();
			}
			if (daily1 == "Blue Scroll")
			{
				PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") + dailyamt1);
				message = "Purchased Blue Scroll x" + dailyamt1.ToString() + "!";
				printmessage();
			}
			
			//Second item
			if (daily2 == "Red Scroll")
			{
				PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") + dailyamt2);
				message = "Purchased Red Scroll x" + dailyamt2.ToString() + "!";
				printmessage();
			}
			if (daily2 == "Green Scroll")
			{
				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + dailyamt2);
				message = "Purchased Green Scroll x" + dailyamt2.ToString() + "!";
				printmessage();
			}
			if (daily2 == "Blue Scroll")
			{
				PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") + dailyamt2);
				message = "Purchased Blue Scroll x" + dailyamt2.ToString() + "!";
				printmessage();
			}
			
			//final item
			if (daily3 == "Red Scroll")
			{
				PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") + dailyamt3);
				message = "Purchased Red Scroll x" + dailyamt3.ToString() + "!";
				printmessage();
			}
			if (daily3 == "Green Scroll")
			{
				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + dailyamt3);
				message = "Purchased Green Scroll x" + dailyamt3.ToString() + "!";
				printmessage();
			}
			if (daily3 == "Blue Scroll")
			{
				PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") + dailyamt3);
				message = "Purchased Blue Scroll x" + dailyamt3.ToString() + "!";
				printmessage();
			}
			if (daily3 == "Black Scroll")
			{
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") + dailyamt3);
				message = "Purchased Black Scroll x" + dailyamt3.ToString() + "!";
				printmessage();
			}
			if (daily3 == "White Scroll")
			{
				PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") + dailyamt3);
				message = "Purchased White Scroll x" + dailyamt3.ToString() + "!";
				printmessage();
			}
			
			dailybought = true;
		}
	
		
		if (!scrollcost)
		Usemoney();
		
		if (scrollcost)
		Usescroll();
		
		
	}
	
	bool Enoughmoneys()
	{
		if (learnedability == "Spirit Bomb" || learnedability == "Ground Smash" || learnedability == "Hurricane" )
		{
			if (PlayerPrefs.GetInt("Money") < 100)
				return false;
			
		}
		
		if (learnedability == "Tackle" || learnedability == "Teledoor" || learnedability == "Spirit Beam")
		{
			if (PlayerPrefs.GetInt("Money") < 300)
				return false;
			
		}
		
		if (learnedability == "Force Push" || learnedability == "Aegis Weapon" || learnedability == "Third Mind")
		{
			if (PlayerPrefs.GetInt("Money") < 500)
				return false;
			
		}
		
		if (learnedability == "Hundred Fist" || learnedability == "Mirror Image" || learnedability == "Serenity")
		{
			if (PlayerPrefs.GetInt("Money") < 750)
				return false;
			
		}
		
		if (learnedability == "Final Body" || learnedability == "Vector Plate" || learnedability == "Za Warudo")
		{
			if (PlayerPrefs.GetInt("Money") < 1200)
				return false;
			
		}	
		
		return true;
	}
	
	void Usemoney()
	{
		PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - purchaseprice);
		
		if (PlayerPrefs.GetInt("Money") < 0)
			PlayerPrefs.SetInt("Money", 0);
		
	}
	
	void Usescroll()
	{
		PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") - redcost);
		if (PlayerPrefs.GetInt("Redscroll") < 0)
			PlayerPrefs.SetInt("Redscroll", 0);
		PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") - greencost);
		if (PlayerPrefs.GetInt("Greenscroll") < 0)
			PlayerPrefs.SetInt("Greenscroll", 0);
		PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") - blucost);
		if (PlayerPrefs.GetInt("Bluescroll") < 0)
			PlayerPrefs.SetInt("Bluescroll", 0);
		PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - blackcost);
		if (PlayerPrefs.GetInt("Blackscroll") < 0)
			PlayerPrefs.SetInt("Blackscroll", 0);
		PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - whitecost);
		if (PlayerPrefs.GetInt("Whitescroll") < 0)
			PlayerPrefs.SetInt("Whitescroll", 0);
			
		redcost = 0;
		greencost = 0;
		blucost = 0;
		blackcost = 0;
		whitecost = 0;
	}
					
	void Scrollscostui(int windowID)
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			scrollscostwindow = false;	
	
		GUI.DrawTexture(new Rect(10, 30, 6, 22), tinyred);	
		GUI.DrawTexture(new Rect(10, 50, 6, 22), tinygreen);
		GUI.DrawTexture(new Rect(10, 70, 6, 22), tinyblue);
		GUI.DrawTexture(new Rect(10, 90, 6, 22), tinyblack);
		GUI.DrawTexture(new Rect(10, 110, 6, 22), tinywhite);
		
		
			if (GUI.Button(new Rect(30, 30, 22, 22), leftarrow))
				redcost--;
			if (GUI.Button(new Rect(90, 30, 22, 22), rightarrow))
				redcost++;
			if (redcost > PlayerPrefs.GetInt("Redscroll"))
				redcost = PlayerPrefs.GetInt("Redscroll");
			if (redcost < 0)
				redcost = 0;
			
			if (GUI.Button(new Rect(30, 50, 22, 22), leftarrow))
				greencost--;
			if (GUI.Button(new Rect(90, 50, 22, 22), rightarrow))
				greencost++;
			if (greencost > PlayerPrefs.GetInt("Greenscroll"))
				greencost = PlayerPrefs.GetInt("Greenscroll");
			if (greencost < 0)
				greencost = 0;
			
			if (GUI.Button(new Rect(30, 70, 22, 22), leftarrow))
				blucost--;
			if (GUI.Button(new Rect(90, 70, 22, 22), rightarrow))
				blucost++;
			if (blucost > PlayerPrefs.GetInt("Bluescroll"))
				blucost = PlayerPrefs.GetInt("Bluescroll");
			if (blucost < 0)
				blucost = 0;
			
			if (GUI.Button(new Rect(30, 90, 22, 22), leftarrow))
				blackcost--;
			if (GUI.Button(new Rect(90, 90, 22, 22), rightarrow))
				blackcost++;
			if (blackcost > PlayerPrefs.GetInt("Blackscroll"))
				blackcost = PlayerPrefs.GetInt("Blackscroll");
			if (blackcost < 0)
				blackcost = 0;
			
			if (GUI.Button(new Rect(30, 110, 22, 22), leftarrow))
				whitecost--;
			if (GUI.Button(new Rect(90, 110, 22, 22), rightarrow))
				whitecost++;
			if (whitecost > PlayerPrefs.GetInt("Whitescroll"))
				whitecost = PlayerPrefs.GetInt("Whitescroll");
			if (whitecost < 0)
				whitecost = 0;
		
		
		GUI.Label(new Rect(60, 30, 50, 25), redcost.ToString());
		GUI.Label(new Rect(60, 50, 50, 25), greencost.ToString());
		GUI.Label(new Rect(60, 70, 50, 25), blucost.ToString());
		GUI.Label(new Rect(60, 90, 50, 25), blackcost.ToString());
		GUI.Label(new Rect(60, 110, 50, 25), whitecost.ToString());
	
		dojoskin.button.fontSize = 14;
		if (GUI.Button(new Rect(30, 140, 100, 25), "PURCHASE") && redcost+greencost+blucost+blackcost+whitecost >= scrollprice)
		{
			Buyitem();
			audio.PlayOneShot(cash);
			scrollscostwindow = false;
			purchasepopup = false;
		}
		dojoskin.button.fontSize = 24;
	}
	
	void purchaseamount(int windowID)
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			amountchanger = false;	
			purchasepopup = false;
		}
		
		//question
		GUI.Label(new Rect(30, 20, 120, 25), "Purchase " + purchaseitem.ToString() + " for $" + purchaseprice.ToString() + "?");
		
		//cost label
		GUI.Label(new Rect(170, 60, 120, 25), "Total Cost");
		GUI.Label(new Rect(190, 80, 50, 25), "$" + (purchaseprice * purchasenumber).ToString());
		
		//amount changerz
		GUI.Label(new Rect(30, 60, 60, 25), "Amount");
		if (GUI.Button(new Rect(30, 85, 22, 22), leftarrow))
		{
			purchasenumber--;
			if (purchasenumber < 1)
				purchasenumber = 1;
		}
		GUI.Label(new Rect(60, 80, 50, 25), purchasenumber.ToString());
		if (GUI.Button(new Rect(80, 85, 22, 22), chatboxtexture))
		{
			if ((purchasenumber * purchaseprice) <= PlayerPrefs.GetInt("Money"))
			purchasenumber++;
		}
		
		if (GUI.Button(new Rect(30, 110, 100, 25), "PURCHASE")) 
		{
			for (int g = 0; g < purchasenumber; g++)
			{
				if (purchaseprice * purchasenumber > PlayerPrefs.GetInt("Money"))
				{
					endlesschoices = true;	
					message = "You do not have enough, fool.";
					printmessage();
					amountchanger = false;
					purchasepopup = false;
					return;
				}
				
				else
				Buyitem();
				
			}
			audio.PlayOneShot(cash);
			scrollscostwindow = false;
			purchasepopup = false;
			purchasenumber = 1;
			amountchanger =  false;
		}
		
		if (GUI.Button(new Rect(170, 110, 100, 25), "CANCEL")) 
		{
			amountchanger = false;
			purchasepopup = false;
			Clearpurchaseinfo();
		}
	}
	
	void Skillquestion(int windowID)
	{
		//GTFO
		if (Input.GetKeyDown(KeyCode.Escape))
			storeskills = false;	
		
		dojo2.label.fontSize = 20;
		//distinguishing price
		if (learnedability == "Spirit Bomb" || learnedability == "Ground Smash" || learnedability == "Hurricane" )
		GUI.Label(new Rect(40, 20, 120, 50), "Learn this skill for $100  &  5     ?");
		
		if (learnedability == "Tackle" || learnedability == "Teledoor" || learnedability == "Spirit Beam")
		GUI.Label(new Rect(40, 20, 120, 50), "Learn this skill for $300  &  5     ?");
		
		if (learnedability == "Force Push" || learnedability == "Aegis Weapon" || learnedability == "Third Mind")
		GUI.Label(new Rect(40, 20, 120, 50), "Learn this skill for $500  &  5     ?");
		
		if (learnedability == "Hundred Fist" || learnedability == "Mirror Image" || learnedability == "Serenity")
		GUI.Label(new Rect(40, 20, 120, 50), "Learn this skill for $750  &  5     ?");
		
		if (learnedability == "Final Body" || learnedability == "Vector Plate" || learnedability == "Za Warudo")
		GUI.Label(new Rect(40, 20, 120, 50), "Learn this skill for $1200  &  5     ?");
		
		//distinguishing scrolls
		if (learnedability == "Ground Smash" || learnedability == "Tackle" || learnedability == "Force Push" ||
			learnedability == "Hundred Fist" || learnedability == "Final Body")
		GUI.DrawTexture(new Rect(100, 43, 5, 20), tinyred);
		
		if (learnedability == "Spirit Bomb" || learnedability == "Teledoor" || learnedability == "Aegis Weapon" ||
			learnedability == "Mirror Image" || learnedability == "Vector Plate")
		GUI.DrawTexture(new Rect(100, 43, 6, 20), tinygreen);
		
		if (learnedability == "Hurricane" || learnedability == "Spirit Beam" || learnedability == "Third Mind" ||
			learnedability == "Serenity" || learnedability == "Za Warudo")
		GUI.DrawTexture(new Rect(100, 43, 6, 20), tinyblue);
		
		dojo2.label.fontSize = 16;
	}
	
	void sensuicomment(int windowID)
	{
		//GTFO
		if (Input.GetKeyDown(KeyCode.Escape))
			storeskills = false;	
		
		dojo2.label.fontSize = 18;
		GUI.Label(new Rect(60, 20, 120, 60), "Choose a skill to examine.");
		dojo2.label.fontSize = 16;
	}
	
	void runecrafting(int windowID)
	{
		//GTFO
		if (Input.GetKeyDown(KeyCode.Escape))
			storeskills = false;
		
		dojo2.label.fontSize = 20;
		if (runetier == 1)
		GUI.Label(new Rect(40, 20, 140, 60), "Craft a Tier 1 rune for this ability?");
		
		if (runetier == 2)
		GUI.Label(new Rect(40, 20, 140, 60), "Craft a Tier 2 rune for this ability?");

		dojo2.label.fontSize = 16;
	}
	
	
	void newskilllearn()
	{
		//DOBY
		if (learnedability == "Tackle")
		{
			if (PlayerPrefs.GetInt("Redscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Tackleabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 1);
				
				PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 300);
				message = "You have learned Tackle!";
				printmessage();
				learnedability = null;
				
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Force Push")
		{
			if (PlayerPrefs.GetInt("Redscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Forceabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 2);

				PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 500);
				message = "You have learned Force Push!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";	
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Hundred Fist")
		{
			if (PlayerPrefs.GetInt("Redscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Hfistabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 9);

				PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 750);
				message = "You have learned Hundred Fist!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Final Body")
		{
			if (PlayerPrefs.GetInt("Redscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Finalbodyabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 10);

				PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 1200);
				message = "You have learned Final Body!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";	
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		//TECH
		if (learnedability == "Spirit Bomb")
		{
			if (PlayerPrefs.GetInt("Greenscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Spiritbombabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 3);

				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 100);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Teledoor")
		{
			if (PlayerPrefs.GetInt("Greenscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Teledoorabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 6);

				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 300);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";	
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Aegis Weapon")
		{
			if (PlayerPrefs.GetInt("Greenscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Wallabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 7);

				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 500);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";	
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Mirror Image")
		{
			if (PlayerPrefs.GetInt("Greenscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Cloneabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 8);

				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 750);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Vector Plate")
		{
			if (PlayerPrefs.GetInt("Greenscroll") >= 5)
			{	
				PlayerPrefs.SetInt("Vplateabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 13);

				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 1200);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";	
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		//MNID
		if (learnedability == "Spirit Beam")
		{
			if (PlayerPrefs.GetInt("Bluescroll") >= 5)
			{	
				PlayerPrefs.SetInt("Lazerabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 4);

				PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 300);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Third Mind")
		{
			if (PlayerPrefs.GetInt("Bluescroll") >= 5)
			{	
				PlayerPrefs.SetInt("Thirdmindabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 11);

				PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 500);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Serenity")
		{
			if (PlayerPrefs.GetInt("Bluescroll") >= 5)
			{	
				PlayerPrefs.SetInt("Serenityabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 5);

				PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 750);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		if (learnedability == "Za Warudo")
		{
			if (PlayerPrefs.GetInt("Bluescroll") >= 5)
			{	
				PlayerPrefs.SetInt("Warudoabilitystate", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 12);

				PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") - 5);
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 1200);
				message = "You have learned " + learnedability + "!";
				printmessage();
				learnedability = null;
			}
			else
			{
				message = "You are lacking the knowledge, you need more scrolls.";	
				printmessage();
				learnedability = null;
				return;
			}
			
		}
		
		
	}//end of skilllearning
	
	
	//NOTE: No distinction needs to be made during black learning. This is because if they already have white the next ++ result is both.
	//In contrast, if learning white and one has black already, the next ++ is white only. Need to put double distinction in white.
	void newruneslearnblack()
	{
		if (PlayerPrefs.GetInt("Blackscroll") < 1)
		{
			message = "You are lacking the knowledge, you need more scrolls.";	
			printmessage();
			
		}
		
		if (PlayerPrefs.GetInt("Blackscroll") >= 1)
		{
			//BODY
			if (gsmashstats)
			{
				PlayerPrefs.SetInt("Groundsmashabilitystate", PlayerPrefs.GetInt("Groundsmashabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (tacklestats)
			{
				PlayerPrefs.SetInt("Tackleabilitystate", PlayerPrefs.GetInt("Tackleabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (forcestats)
			{
				PlayerPrefs.SetInt("Forceabilitystate", PlayerPrefs.GetInt("Forceabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (hfiststats)
			{
				PlayerPrefs.SetInt("Hfistabilitystate", PlayerPrefs.GetInt("Hfistabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (finalbodystats)
			{
				PlayerPrefs.SetInt("Finalbodyabilitystate", PlayerPrefs.GetInt("Finalbodyabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			//TECH
			if (spiritbstats)
			{
				PlayerPrefs.SetInt("Spiritbombabilitystate", PlayerPrefs.GetInt("Spiritbombabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (tdoorstats)
			{
				PlayerPrefs.SetInt("Teledoorabilitystate", PlayerPrefs.GetInt("Teledoorabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (wallstats)
			{
				PlayerPrefs.SetInt("Wallabilitystate", PlayerPrefs.GetInt("Wallabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (clonestats)
			{
				PlayerPrefs.SetInt("Cloneabilitystate", PlayerPrefs.GetInt("Cloneabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (vplatestats)
			{
				PlayerPrefs.SetInt("Vplateabilitystate", PlayerPrefs.GetInt("Vplateabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			//MIND
			if (hurricanestats)
			{
				PlayerPrefs.SetInt("Hurricaneabilitystate", PlayerPrefs.GetInt("Hurricaneabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (lazerstats)
			{
				PlayerPrefs.SetInt("Lazerabilitystate", PlayerPrefs.GetInt("Lazerabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (thirdmindstats)
			{
				PlayerPrefs.SetInt("Thirdmindabilitystate", PlayerPrefs.GetInt("Thirdmindabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (serenitystats)
			{
				PlayerPrefs.SetInt("Serenityabilitystate", PlayerPrefs.GetInt("Serenityabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			if (warudostats)
			{
				PlayerPrefs.SetInt("Warudoabilitystate", PlayerPrefs.GetInt("Warudoabilitystate") + 1);	
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
				if (runetier == 1)
				message = "You have crafted a Tier 1 Black rune!";
				printmessage();
				if (runetier == 2)
				message = "You have crafted a Tier 2 Black rune!";
				printmessage();
				return;
				
			}
			
			
		}
		
	}
	
	void newruneslearnwhite()
	{
		if (PlayerPrefs.GetInt("Whitescroll") < 1)
		{
			message = "You are lacking the knowledge, you need more scrolls.";	
			printmessage();
			
		}
		
		if (PlayerPrefs.GetInt("Whitescroll") >= 1)
		{
			//BODY
			if (gsmashstats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Groundsmashabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Groundsmashabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Groundsmashabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Groundsmashabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (tacklestats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Tackleabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Tackleabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Tackleabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Tackleabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Tackleabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Tackleabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Tackleabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Tackleabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (forcestats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Forceabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Forceabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Forceabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Forceabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Forceabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Forceabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Forceabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Forceabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (hfiststats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Hfistabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Hfistabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Hfistabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Hfistabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Hfistabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Hfistabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Hfistabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Hfistabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (finalbodystats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Finalbodyabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Finalbodyabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Finalbodyabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Finalbodyabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Finalbodyabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Finalbodyabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Finalbodyabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Finalbodyabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			//Tech
			if (spiritbstats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Spiritbombabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Spiritbombabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Spiritbombabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Spiritbombabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (tdoorstats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Teledoorabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Teledoorabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Teledoorabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Teledoorabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Teledoorabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Teledoorabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Teledoorabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Teledoorabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (wallstats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Wallabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Wallabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Wallabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Wallabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Wallabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Wallabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Wallabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Wallabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (clonestats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Cloneabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Cloneabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Cloneabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Cloneabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Cloneabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Cloneabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Cloneabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Cloneabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (vplatestats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Vplateabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Vplateabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Vplateabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Vplateabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Vplateabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Vplateabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Vplateabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Vplateabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			//MIND
			if (hurricanestats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Hurricaneabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Hurricaneabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Hurricaneabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Hurricaneabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (lazerstats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Lazerabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Lazerabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Lazerabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Lazerabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Lazerabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Lazerabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Lazerabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Lazerabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (thirdmindstats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Thirdmindabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Thirdmindabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Thirdmindabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Thirdmindabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Thirdmindabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Thirdmindabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Thirdmindabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Thirdmindabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (serenitystats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Serenityabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Serenityabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Serenityabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Serenityabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Serenityabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Serenityabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Serenityabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Serenityabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
			if (warudostats)
			{
				//situation: T1 black is learned already, so skip to 4
				if (PlayerPrefs.GetInt("Warudoabilitystate") == 2)
				{
					PlayerPrefs.SetInt("Warudoabilitystate", 4);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T1 black not learned, go to 3
				if (PlayerPrefs.GetInt("Warudoabilitystate") == 1)
				{
					PlayerPrefs.SetInt("Warudoabilitystate", 3);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 1 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black is learned already, so skip to 7
				if (PlayerPrefs.GetInt("Warudoabilitystate") == 5)
				{
					PlayerPrefs.SetInt("Warudoabilitystate", 7);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
				//situation: T2 black not learned, so go 6
				if (PlayerPrefs.GetInt("Warudoabilitystate") == 4)
				{
					PlayerPrefs.SetInt("Warudoabilitystate", 6);	
					PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
					PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
					message = "You have crafted a Tier 2 White rune!";
					printmessage();
					return;
				}
				
			}
			
		}	
	}
	
	void Queryavailables()
	{
		GUI.skin = null;
		
		//BODY
		//gsmash
		GUI.color = Color.white;
		if (GUI.Button(bodyiconstv, gsmashicon))
		{
			individualskill = true;
			cancelallstatscreens();
			gsmashstats = true;
		}
		
		//tackle
		if (PlayerPrefs.GetInt("Playerlevel") < 5)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (bodyiconstv.x + 72, bodyiconstv.y, 58, 58), tackleicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 5)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (bodyiconstv.x + 72, bodyiconstv.y, 58, 58), tackleicon))
			{
				individualskill = true;
				cancelallstatscreens();
				tacklestats = true;
			}
		}
		
		//force
		if (PlayerPrefs.GetInt("Playerlevel") < 10)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (bodyiconstv.x + 144, bodyiconstv.y, 58, 58), forceicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 10)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (bodyiconstv.x + 144, bodyiconstv.y, 58, 58), forceicon))
			{
				individualskill = true;
				cancelallstatscreens();
				forcestats = true;
			}
		}
		
		
		
		//hfist
		if (PlayerPrefs.GetInt("Playerlevel") < 15)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (bodyiconstv.x + 216, bodyiconstv.y, 58, 58), hfisticon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 15)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (bodyiconstv.x + 216, bodyiconstv.y, 58, 58), hfisticon))
			{
				individualskill = true;
				cancelallstatscreens();
				hfiststats = true;
			}
		}
		
		//finalbody
		if (PlayerPrefs.GetInt("Playerlevel") < 20)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (bodyiconstv.x + 288, bodyiconstv.y, 58, 58), finalbodyicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 20)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (bodyiconstv.x + 288, bodyiconstv.y, 58, 58), finalbodyicon))
			{
				individualskill = true;
				cancelallstatscreens();
				finalbodystats = true;
			}
		}
		
		
		//TECH
		//spiritb
		if (PlayerPrefs.GetInt("Playerlevel") < 5)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(techiconstv, spiritbicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 5)
		{
			GUI.color = Color.white;
			if (GUI.Button(techiconstv, spiritbicon))
			{
				individualskill = true;
				cancelallstatscreens();
				spiritbstats = true;
			}
		}
		
		
		
		//tdoor
		if (PlayerPrefs.GetInt("Playerlevel") < 10)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (techiconstv.x + 72, techiconstv.y, 58, 58), tdooricon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 10)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (techiconstv.x + 72, techiconstv.y, 58, 58), tdooricon))
			{
				individualskill = true;
				cancelallstatscreens();
				tdoorstats = true;
			}
		}
		
		//wall
		if (PlayerPrefs.GetInt("Playerlevel") < 15)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (techiconstv.x + 144, techiconstv.y, 58, 58), wallicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 15)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (techiconstv.x + 144, techiconstv.y, 58, 58), wallicon))
			{
				individualskill = true;
				cancelallstatscreens();
				wallstats = true;
			}
		}
		
		
		//clone
		if (PlayerPrefs.GetInt("Playerlevel") < 20)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (techiconstv.x + 216, techiconstv.y, 58, 58), cloneicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 20)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (techiconstv.x + 216, techiconstv.y, 58, 58), cloneicon))
			{
				individualskill = true;
				cancelallstatscreens();
				clonestats = true;
			}
		}
		
		
		//vplate
		if (PlayerPrefs.GetInt("Playerlevel") < 20)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (techiconstv.x + 288, techiconstv.y, 58, 58), vplateicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 20)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (techiconstv.x + 288, techiconstv.y, 58, 58), vplateicon))
			{
				individualskill = true;
				cancelallstatscreens();
				vplatestats = true;
			}
		}
		
		
		
		
		//MIND
		//hurri
		GUI.color = Color.white;
		if (GUI.Button(mindiconstv, hurricon))
		{
			individualskill = true;
			cancelallstatscreens();
			hurricanestats = true;
		}
		
		
		//lazer
		if (PlayerPrefs.GetInt("Playerlevel") < 5)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (mindiconstv.x + 72, mindiconstv.y, 58, 58), beamicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 5)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (mindiconstv.x + 72, mindiconstv.y, 58, 58), beamicon))
			{
				individualskill = true;
				cancelallstatscreens();
				lazerstats = true;
			}
		}
		
		
		//thirdmind
		if (PlayerPrefs.GetInt("Playerlevel") < 10)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (mindiconstv.x + 144, mindiconstv.y, 58, 58), thirdmindicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 10)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (mindiconstv.x + 144, mindiconstv.y, 58, 58), thirdmindicon))
			{
				individualskill = true;
				cancelallstatscreens();
				thirdmindstats = true;
			}
		}
		
		
		
		//serenity
		if (PlayerPrefs.GetInt("Playerlevel") < 15)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (mindiconstv.x + 216, mindiconstv.y, 58, 58), sereniticon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 15)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (mindiconstv.x + 216, mindiconstv.y, 58, 58), sereniticon))
			{
				individualskill = true;
				cancelallstatscreens();
				serenitystats = true;
			}
		}
		
		
		
		//warudo
		if (PlayerPrefs.GetInt("Playerlevel") < 20)
		{
			GUI.color = almostblack;
			GUI.DrawTexture(new Rect (mindiconstv.x + 288, mindiconstv.y, 58, 58), warudoicon);
		}
		if (PlayerPrefs.GetInt("Playerlevel") >= 20)
		{
			GUI.color = Color.white;
			if (GUI.Button(new Rect (mindiconstv.x + 288, mindiconstv.y, 58, 58), warudoicon))
			{
				individualskill = true;
				cancelallstatscreens();
				warudostats = true;
			}
		}
		
		GUI.color = Color.white;
		
	}
	
	//statscreen
	void Skillscreens()
	{
		//BODY
		if (gsmashstats)
		{
			//Title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Ground Smash");
			
			//icon & ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), gsmashicon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), redball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[0]);
			
			//runes
			statscreenrunedescriptions("Groundsmashabilitystate", gsmashtips2);

			//bottom buttons
			statscreenbuttons("Groundsmashabilitystate", "Ground Smash");
	
		}
				
		if (tacklestats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Something Buster");
			
			//icon & ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), tackleicon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), redball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[1]);	
				
			//runes
			statscreenrunedescriptions("Tackleabilitystate", tackletips2);
			
			//buttons
			statscreenbuttons("Tackleabilitystate", "Tackle");
			
		}
				
				
		if (forcestats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Force Push");
			
			//icon & ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), forceicon);	
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), redball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[2]);
	
			//runes
			statscreenrunedescriptions("Forceabilitystate", forcetips2);
			
			//buttons
			statscreenbuttons("Forceabilitystate", "Force Push");
			
		}
				
				
		if (hfiststats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Hundred Fists");
			
			//icon & ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), hfisticon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), redball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[3]);
	
			//runes
			dojoskin.label.fontSize = 14;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 200, 300, 60), "Coming Soon!!");
			
			
			//buttons
			statscreenbuttons("Hfistabilitystate", "Hundred Fist");
			
		}
				
		if (finalbodystats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Final body skill");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), finalbodyicon);	
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), redball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[4]);
			
			//runes
			dojoskin.label.fontSize = 14;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 200, 300, 60), "Coming Soon!!");
			
			
			//buttons
			statscreenbuttons("Finalbodyabilitystate", "Final Body");

			
		}

				
		//TECH
		if (spiritbstats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Spirit Bomb");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), spiritbicon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), greenball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[5]);
	
			//runes
			statscreenrunedescriptions("Spiritbombabilitystate", spiritbtips2);
			
			//buttons
			statscreenbuttons("Spiritbombabilitystate", "Spirit Bomb");
			
		}
				
			
		if (tdoorstats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Teledoor");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), tdooricon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), greenball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[6]);
	
			//runes
			statscreenrunedescriptions("Teledoorabilitystate", tdoortips2);
	
			//buttons
			statscreenbuttons("Teledoorabilitystate", "Teledoor");
			
		}	
				
		if (wallstats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Summon Aegis Weapon");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), wallicon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), greenball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[7]);
			
			//runes
			statscreenrunedescriptions("Wallabilitystate", walltips2);
			
			//buttons
			statscreenbuttons("Wallabilitystate", "Aegis Weapon");
			
		}
				
				
		if (clonestats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Mirror Image");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), cloneicon);	
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), greenball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[8]);
			
			//runes
			statscreenrunedescriptions("Cloneabilitystate", clonetips2);
			
			//buttons
			statscreenbuttons("Cloneabilitystate", "Mirror Image");
			
		}
				
				
		if (vplatestats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Vector Plate");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), vplateicon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), greenball);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[9]);
	
			//runes
			dojoskin.label.fontSize = 14;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 200, 300, 60), "Coming Soon!!");
			
			
			//buttons
			statscreenbuttons("Vplateabilitystate", "Vector Plate");
			
		}
				
				
		//MIND
		if (hurricanestats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Hurricane");
			
			//icons and balls
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), hurricon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), blueballs);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[10]);
	
			//runes
			statscreenrunedescriptions("Hurricaneabilitystate", hurritips2);
			
			//buttons
			statscreenbuttons("Hurricaneabilitystate", "Hurricane");
			
			
		}
		
		
		if (lazerstats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Spirit Beam");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), beamicon);	
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), blueballs);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[11]);
	
			//runes
			statscreenrunedescriptions("Lazerabilitystate", lazertips2);
			
			//buttons
			statscreenbuttons("Lazerabilitystate", "Spirit Beam");
			
		}
				
				
		if (thirdmindstats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Third Mind");
			
			//icons and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), thirdmindicon);	
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), blueballs);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[12]);
	
			//runes
			dojoskin.label.fontSize = 14;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 200, 300, 60), "Coming Soon!!");
			
			
			//buttons
			statscreenbuttons("Thirdmindabilitystate", "Third Mind");
			
		}
				
				
		if (serenitystats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Serenity");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), sereniticon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), blueballs);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[13]);
			
			//runes
			statscreenrunedescriptions("Serenityabilitystate", serentips2);
			
			
			//buttons
			statscreenbuttons("Serenityabilitystate", "Serenity");
			
			
		}
		
		
		if (warudostats)
		{
			//title
			dojoskin.label.alignment = TextAnchor.UpperCenter;
			dojoskin.label.fontSize = 22;
			GUI.Label(new Rect(skillstvbox.x + 90, skillstvbox.y + 30, 280, 40), "Za Warudo");
			
			//icon and ball
			GUI.DrawTexture(new Rect(bodyiconstv.x - 2, bodyiconstv.y - 14, bodyiconstv.width, bodyiconstv.height), warudoicon);
			GUI.DrawTexture(new Rect(bodyiconstv.x - 50, bodyiconstv.y - 5, 34, 34), blueballs);
			
			//ability description
			dojoskin.label.fontSize = 16;
			dojoskin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 80, 300, 60), abilitydescriptions[14]);
	
			//runes
			dojoskin.label.fontSize = 14;
			GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 200, 300, 60), "Coming Soon!!");
			
			
			//buttons
			statscreenbuttons("Warudoabilitystate", "Za Warudo");
			
		}	
		
		
	}
	
	void statscreenbuttons(string abilitystate, string tolearn)
	{
		//buttons
		GUI.skin = dojo2;
		dojo2.label.font = aesmat;
		dojo2.label.normal.textColor = Color.white;
		
		// if skill learned
		if (PlayerPrefs.GetInt(abilitystate) > 0)
		{
			GUI.DrawTexture(new Rect(skillstvbox.x + 30, skillstvbox.y + 295, 132, 30), fakebutton);

			GUI.Label(new Rect(skillstvbox.x + 45, skillstvbox.y + 298, 132, 30), "Skill Unlocked");

		}
		
		//if not learned
		if (PlayerPrefs.GetInt(abilitystate) == 0)
		if (GUI.Button(new Rect(skillstvbox.x + 30, skillstvbox.y + 295, 132, 30), "Unlock Skill"))
		{
			skillquestion = true;
			learnedability = tolearn;
		}
		
		//All black runes learned but not allt1 and t2w
		if (PlayerPrefs.GetInt(abilitystate) >= 5 && PlayerPrefs.GetInt(abilitystate) != 6)
		{
			GUI.DrawTexture(new Rect(skillstvbox.x + 200, skillstvbox.y + 295, 110, 30), fakebutton);
			GUI.Label(new Rect(skillstvbox.x + 210, skillstvbox.y + 298, 110, 30), "All Unlocked");
		}
		
		//Black T1
		if (PlayerPrefs.GetInt(abilitystate) == 1 || PlayerPrefs.GetInt(abilitystate) == 3)
			if (GUI.Button(new Rect(skillstvbox.x + 200, skillstvbox.y + 295, 110, 30), "T1 Black Rune"))
			{
				runesquestion = true;
				runetier = 1;
				runetype = 1;
				
			}
		//Black T2
		if (PlayerPrefs.GetInt(abilitystate) == 4 || PlayerPrefs.GetInt(abilitystate) == 6)
			if (GUI.Button(new Rect(skillstvbox.x + 200, skillstvbox.y + 295, 110, 30), "T2 Black Rune"))
			{
				runesquestion = true;
				runetier = 2;
				runetype = 1;
				
			}
		
		//All white runes learned 
		if (PlayerPrefs.GetInt(abilitystate) >= 6)
		{
			GUI.DrawTexture(new Rect(skillstvbox.x + 320, skillstvbox.y + 295, 110, 30), fakebutton);
			GUI.Label(new Rect(skillstvbox.x + 330, skillstvbox.y + 298, 110, 30), "All Unlocked");
		}
		
		//White T1
		if (PlayerPrefs.GetInt(abilitystate) == 1 || PlayerPrefs.GetInt(abilitystate) == 2)
		if (GUI.Button(new Rect(skillstvbox.x + 320, skillstvbox.y + 295, 110, 30), "T1 White Rune"))
		{
			runesquestion = true;
			runetier = 1;
			runetype = 2;
			
		}
		//White T2
		if (PlayerPrefs.GetInt(abilitystate) == 4 || PlayerPrefs.GetInt(abilitystate) == 5)
		if (GUI.Button(new Rect(skillstvbox.x + 320, skillstvbox.y + 295, 110, 30), "T2 White Rune"))
		{
			runesquestion = true;
			runetier = 2;
			runetype = 2;
			
		}	
		
		dojo2.label.font = comicfont;
		dojo2.label.normal.textColor = Color.black;
		GUI.skin = null;
	}
	
	void statscreenrunedescriptions(string abilitystate, string[] tips)
	{
		dojoskin.label.fontSize = 14;
		dojoskin.label.alignment = TextAnchor.UpperLeft;
		
		//Tier labels
		GUI.Label(new Rect(skillstvbox.x + 50, skillstvbox.y + 155, 300, 60), "Tier 1");
		GUI.Label(new Rect(skillstvbox.x + 50, skillstvbox.y + 215, 300, 60), "Tier 2");
		
		//T1 black
		if (PlayerPrefs.GetInt(abilitystate) >= 2 && PlayerPrefs.GetInt(abilitystate) != 3)
		dojoskin.label.normal.textColor = Color.cyan;
		GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 155, 300, 60), tips[0]);
		dojoskin.label.normal.textColor = Color.white;
		
		//T1 white
		if (PlayerPrefs.GetInt(abilitystate) >= 3)
		dojoskin.label.normal.textColor = Color.cyan;
		GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 185, 300, 60), tips[1]);
		dojoskin.label.normal.textColor = Color.white;

		//T2 black
		if (PlayerPrefs.GetInt(abilitystate) >= 5 && PlayerPrefs.GetInt(abilitystate) != 6)
		dojoskin.label.normal.textColor = Color.cyan;
		GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 215, 300, 60), tips[2]);
		dojoskin.label.normal.textColor = Color.white;

		//T2 white
		if (PlayerPrefs.GetInt(abilitystate) >= 6)
		dojoskin.label.normal.textColor = Color.cyan;
		GUI.Label(new Rect(skillstvbox.x + 140, skillstvbox.y + 245, 300, 60), tips[3]);
		dojoskin.label.normal.textColor = Color.white;
		
		
		
	}
	
	void cancelallstatscreens()
	{
		gsmashstats = false;
		tacklestats = false; 
		forcestats = false;
		hfiststats = false;
		finalbodystats = false; 
		
		spiritbstats = false;
		tdoorstats = false;
		wallstats = false;
		clonestats = false;
		vplatestats = false;
		
		hurricanestats = false; 
		lazerstats = false;
		thirdmindstats = false;
		serenitystats = false;
		warudostats = false;
		
		
	}
	
	void RandomizeDaily()
	{
		//first item
		int name1 = Random.Range(0, 3);
		if (name1 == 0)
			daily1 = "Red Scroll";
		if (name1 == 1)
			daily1 = "Green Scroll";
		if (name1 == 2)
			daily1 = "Blue Scroll";
		
		//first amount
		dailyamt1 = Random.Range(1, 4);
		
		//second item
		int name2 = Random.Range(0, 3);
		if (name2 == 0)
			daily2 = "Red Scroll";
		if (name2 == 1)
			daily2 = "Green Scroll";
		if (name2 == 2)
			daily2 = "Blue Scroll";
		
		//second amount
		dailyamt2 = Random.Range(1, 3);
		
		//third item
		int name3 = Random.Range(0, 101);
		if (name3 <= 100)
			daily3 = "Black Scroll";
		if (name3 <= 75)
			daily3 = "White Scroll";
		if (name3 <= 50)
			daily3 = "Blue Scroll";
		if (name3 <= 33)
			daily3 = "Green Scroll";
		if (name3 <= 16)
			daily3 = "Red Scroll";
		
		
		//thirdamount
		dailyamt3 = Random.Range(1, 3);
		
		
	}
				
//	void Randomsapecial()
//	{
//		PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 50);
//		PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") - 1);
//		PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") - 1);
//					
//		int roll = Random.Range(1, 5);
//		
//		if (roll == 1 && PlayerPrefs.GetInt("Sstrikeabilitystate") < 1)
//		{
//			PlayerPrefs.SetInt("Sstrikeabilitystate", 1);	
//			learnedability = "SpecialStrike green rune";
//		}
//		
//		if (roll == 2 && PlayerPrefs.GetInt("Sstrikeabilitystate") < 2)
//		{
//			PlayerPrefs.SetInt("Sstrikeabilitystate", 2);	
//			learnedability = "SpecialStrike yellow rune";
//			
//		}
//		
//		if (roll == 3 && PlayerPrefs.GetInt("Sstrikeabilitystate") < 3)
//		{
//			PlayerPrefs.SetInt("Sstrikeabilitystate", 3);	
//			learnedability = "SpecialStrike red rune";
//			
//		}
//		
//		if (roll == 4 && PlayerPrefs.GetInt("Sstrikeabilitystate") < 4)
//		{
//			PlayerPrefs.SetInt("Sstrikeabilitystate", 4);	
//			learnedability = "SpecialStrike blue rune";
//			
//		}
//					
//	}
	
	void Chatboxcontent(int windowID)
	{
		GUI.skin = dojoskin;
		dojoskin.label.normal.background = null;
		dojoskin.label.fontSize = 16;
		
		GUI.Label(message1, message);
		
		dojoskin.label.normal.textColor = graycolor;
		if (numberofmessages > 0 && numberofmessages <= 6)
		{
			if (numberofmessages >= 2)	
			GUI.Label(new Rect(message1.x, message1.y + 20, message1.width, message1.height), oldmessages[1]);
			
			if (numberofmessages >= 3)	
			GUI.Label(new Rect(message1.x, message1.y + 40, message1.width, message1.height), oldmessages[2]);
			
			if (numberofmessages >= 4)	
			GUI.Label(new Rect(message1.x, message1.y + 60, message1.width, message1.height), oldmessages[3]);
			
			if (numberofmessages >= 5)	
			GUI.Label(new Rect(message1.x, message1.y + 80, message1.width, message1.height), oldmessages[4]);
			
			if (numberofmessages >= 6)	
			GUI.Label(new Rect(message1.x, message1.y + 100, message1.width, message1.height), oldmessages[5]);
			
		}
		
		dojoskin.label.normal.textColor = Color.white;
		GUI.skin = null;
	}
	
	void printmessage()
	{
		if (numberofmessages <= 6)
		numberofmessages += 1;
		if (numberofmessages > 6)
		numberofmessages = 1;
		oldmessages[numberofmessages] = message;
	}
	
	void Endlesschoose(int windowID)
	{	
		dojoskin.button.fontSize = 12;
		if (GUI.Button(new Rect(10, 120, 100, 25), "Cancel", empty) || Input.GetKeyDown(KeyCode.Escape))
		{
			Enterbattlepopup = false;
			choosecheckpoint = false;
		}
		
		if (GUI.Button(new Rect(10, 30, 100, 25), "Start Fresh"))
			{
				PlayerPrefs.SetString("Startfromcheckpoint", "n");
				StartCoroutine ( Dofadeout("Endless") );
			}	
				
			if (GUI.Button(new Rect(10, 50, 100, 25), "Checkpoint 1"))
			{
				PlayerPrefs.SetString("Startfromcheckpoint", "y");
				PlayerPrefs.SetInt("Endlesscheckpoint", 1);	
				StartCoroutine ( Dofadeout("Endless") );
			}
				
			if (PlayerPrefs.GetInt("EndlessMax") >= 2)
			{
				if (GUI.Button(new Rect(10, 70, 100, 25), "Checkpoint 2"))
				{
					PlayerPrefs.SetString("Startfromcheckpoint", "y");
					PlayerPrefs.SetInt("Endlesscheckpoint", 2);	
					StartCoroutine ( Dofadeout("Endless") );
				}	
				
			}
				
			if (PlayerPrefs.GetInt("EndlessMax") >= 3)
			{
				if (GUI.Button(new Rect(10, 90, 100, 25), "Checkpoint 3"))
				{
					PlayerPrefs.SetString("Startfromcheckpoint", "y");
					PlayerPrefs.SetInt("Endlesscheckpoint", 3);	
					StartCoroutine ( Dofadeout("Endless") );
				}	
				
			}
		
		dojoskin.button.fontSize = 20;
	}
	
	void InventoryCircles(string abilitytier1, string abilitytier2, string abilitystate, int inventoryslot)
	{
		if (inventoryslot == 1)
		{
			if (PlayerPrefs.GetString(abilitytier1) == "black")
				GUI.DrawTexture(inv1bigleft, platebigleftblack);
			if (PlayerPrefs.GetString(abilitytier1) == "white")
				GUI.DrawTexture(inv1bigleft, platebigleftwhite);
		
			if (PlayerPrefs.GetString(abilitytier2) == "black")
				GUI.DrawTexture(inv1bigright, platebigrightblack);
			if (PlayerPrefs.GetString(abilitytier2) == "white")
				GUI.DrawTexture(inv1bigright, platebigrightwhite);
		
			
			if (PlayerPrefs.GetInt(abilitystate) == 2)
			{
				GUI.DrawTexture(inv1t1left, platet1white);
			}
			if (PlayerPrefs.GetInt(abilitystate) == 3)
			{
				GUI.DrawTexture(inv1t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 4)
			{
				GUI.DrawTexture(inv1t1left, platet1white);
				GUI.DrawTexture(inv1t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 5)
			{
				GUI.DrawTexture(inv1t1left, platet1white);
				GUI.DrawTexture(inv1t1right, platet1black);
				GUI.DrawTexture(inv1t2left, plt2w);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 6)
			{
				GUI.DrawTexture(inv1t1left, platet1white);
				GUI.DrawTexture(inv1t1right, platet1black);
				GUI.DrawTexture(inv1t2right, plt2b);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 7)
			{
				GUI.DrawTexture(inv1t1left, platet1white);
				GUI.DrawTexture(inv1t1right, platet1black);
				GUI.DrawTexture(inv1t2left, plt2w);
				GUI.DrawTexture(inv1t2right, plt2b);
			}	
		}
		
		if (inventoryslot == 2)
		{
			if (PlayerPrefs.GetString(abilitytier1) == "black")
				GUI.DrawTexture(inv2bigleft, platebigleftblack);
			if (PlayerPrefs.GetString(abilitytier1) == "white")
				GUI.DrawTexture(inv2bigleft, platebigleftwhite);
		
			if (PlayerPrefs.GetString(abilitytier2) == "black")
				GUI.DrawTexture(inv2bigright, platebigrightblack);
			if (PlayerPrefs.GetString(abilitytier2) == "white")
				GUI.DrawTexture(inv2bigright, platebigrightwhite);
		
			
			if (PlayerPrefs.GetInt(abilitystate) == 2)
			{
				GUI.DrawTexture(inv2t1left, platet1white);
			}
			if (PlayerPrefs.GetInt(abilitystate) == 3)
			{
				GUI.DrawTexture(inv2t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 4)
			{
				GUI.DrawTexture(inv2t1left, platet1white);
				GUI.DrawTexture(inv2t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 5)
			{
				GUI.DrawTexture(inv2t1left, platet1white);
				GUI.DrawTexture(inv2t1right, platet1black);
				GUI.DrawTexture(inv2t2left, plt2w);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 6)
			{
				GUI.DrawTexture(inv2t1left, platet1white);
				GUI.DrawTexture(inv2t1right, platet1black);
				GUI.DrawTexture(inv2t2right, plt2b);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 7)
			{
				GUI.DrawTexture(inv2t1left, platet1white);
				GUI.DrawTexture(inv2t1right, platet1black);
				GUI.DrawTexture(inv2t2left, plt2w);
				GUI.DrawTexture(inv2t2right, plt2b);
			}	
		}
		
		if (inventoryslot == 3)
		{
			if (PlayerPrefs.GetString(abilitytier1) == "black")
				GUI.DrawTexture(inv3bigleft, platebigleftblack);
			if (PlayerPrefs.GetString(abilitytier1) == "white")
				GUI.DrawTexture(inv3bigleft, platebigleftwhite);
		
			if (PlayerPrefs.GetString(abilitytier2) == "black")
				GUI.DrawTexture(inv3bigright, platebigrightblack);
			if (PlayerPrefs.GetString(abilitytier2) == "white")
				GUI.DrawTexture(inv3bigright, platebigrightwhite);
		
			
			if (PlayerPrefs.GetInt(abilitystate) == 2)
			{
				GUI.DrawTexture(inv3t1left, platet1white);
			}
			if (PlayerPrefs.GetInt(abilitystate) == 3)
			{
				GUI.DrawTexture(inv3t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 4)
			{
				GUI.DrawTexture(inv3t1left, platet1white);
				GUI.DrawTexture(inv3t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 5)
			{
				GUI.DrawTexture(inv3t1left, platet1white);
				GUI.DrawTexture(inv3t1right, platet1black);
				GUI.DrawTexture(inv3t2left, plt2w);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 6)
			{
				GUI.DrawTexture(inv3t1left, platet1white);
				GUI.DrawTexture(inv3t1right, platet1black);
				GUI.DrawTexture(inv3t2right, plt2b);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 7)
			{
				GUI.DrawTexture(inv3t1left, platet1white);
				GUI.DrawTexture(inv3t1right, platet1black);
				GUI.DrawTexture(inv3t2left, plt2w);
				GUI.DrawTexture(inv3t2right, plt2b);
			}	
		}
		
		if (inventoryslot == 4)
		{
			if (PlayerPrefs.GetString(abilitytier1) == "black")
				GUI.DrawTexture(inv4bigleft, platebigleftblack);
			if (PlayerPrefs.GetString(abilitytier1) == "white")
				GUI.DrawTexture(inv4bigleft, platebigleftwhite);
		
			if (PlayerPrefs.GetString(abilitytier2) == "black")
				GUI.DrawTexture(inv4bigright, platebigrightblack);
			if (PlayerPrefs.GetString(abilitytier2) == "white")
				GUI.DrawTexture(inv4bigright, platebigrightwhite);
		
			
			if (PlayerPrefs.GetInt(abilitystate) == 2)
			{
				GUI.DrawTexture(inv4t1left, platet1white);
			}
			if (PlayerPrefs.GetInt(abilitystate) == 3)
			{
				GUI.DrawTexture(inv4t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 4)
			{
				GUI.DrawTexture(inv4t1left, platet1white);
				GUI.DrawTexture(inv4t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 5)
			{
				GUI.DrawTexture(inv4t1left, platet1white);
				GUI.DrawTexture(inv4t1right, platet1black);
				GUI.DrawTexture(inv4t2left, plt2w);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 6)
			{
				GUI.DrawTexture(inv4t1left, platet1white);
				GUI.DrawTexture(inv4t1right, platet1black);
				GUI.DrawTexture(inv4t2right, plt2b);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 7)
			{
				GUI.DrawTexture(inv4t1left, platet1white);
				GUI.DrawTexture(inv4t1right, platet1black);
				GUI.DrawTexture(inv4t2left, plt2w);
				GUI.DrawTexture(inv4t2right, plt2b);
			}	
		}
		
		if (inventoryslot == 5)
		{
			if (PlayerPrefs.GetString(abilitytier1) == "black")
				GUI.DrawTexture(inv5bigleft, platebigleftblack);
			if (PlayerPrefs.GetString(abilitytier1) == "white")
				GUI.DrawTexture(inv5bigleft, platebigleftwhite);
		
			if (PlayerPrefs.GetString(abilitytier2) == "black")
				GUI.DrawTexture(inv5bigright, platebigrightblack);
			if (PlayerPrefs.GetString(abilitytier2) == "white")
				GUI.DrawTexture(inv5bigright, platebigrightwhite);
		
			
			if (PlayerPrefs.GetInt(abilitystate) == 2)
			{
				GUI.DrawTexture(inv5t1left, platet1white);
			}
			if (PlayerPrefs.GetInt(abilitystate) == 3)
			{
				GUI.DrawTexture(inv5t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 4)
			{
				GUI.DrawTexture(inv5t1left, platet1white);
				GUI.DrawTexture(inv5t1right, platet1black);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 5)
			{
				GUI.DrawTexture(inv5t1left, platet1white);
				GUI.DrawTexture(inv5t1right, platet1black);
				GUI.DrawTexture(inv5t2left, plt2w);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 6)
			{
				GUI.DrawTexture(inv5t1left, platet1white);
				GUI.DrawTexture(inv5t1right, platet1black);
				GUI.DrawTexture(inv5t2right, plt2b);
			}
		
			if (PlayerPrefs.GetInt(abilitystate) == 7)
			{
				GUI.DrawTexture(inv5t1left, platet1white);
				GUI.DrawTexture(inv5t1right, platet1black);
				GUI.DrawTexture(inv5t2left, plt2w);
				GUI.DrawTexture(inv5t2right, plt2b);
			}	
		}
		
		
	}
	
	void Toggleoffoptions()
	{
		gsmashoptions = false;
		tackleoptions = false;
		forceoptions = false;
		hfistoptions = false;
		finalbodyoptions = false;
		
		hurricaneoptions = false;
		lazeroptions = false;
		thirdmind = false;
		serenityoptions = false;
		warudooptions = false;
		
		sbomboptions = false;
		tdooroptions = false;
		cloneoptions = false;
		walloptions = false;
		vplateoptions = false;
		
		
		sapecialoptions = false;
		sapecialinventory = false;
		skillinventory = false;
		
		
	}
	
	IEnumerator Dofadeout(string level)
	{
		Instantiate(blackbg, new Vector3(0,0, -800), Quaternion.Euler(new Vector3(270, 0, 0)));
		
		yield return new WaitForSeconds(1);
		
		
		Application.LoadLevel(level);
	}
	
	void newreset()
	{
		PlayerPrefs.SetInt("Firsttime", 0);
		
		StartCoroutine ( Dofadeout("cinematic1") );
		
	}
	
	void shinynewtext(Rect rectlocation)
	{
		if (rainbownumber == 1)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb1);
		
		if (rainbownumber == 2)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb2);
		
		if (rainbownumber == 3)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb3);
		
		if (rainbownumber == 4)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb4);
		
		if (rainbownumber == 5)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb5);
		
		if (rainbownumber == 6)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb6);	
		
	}
	
	void WriteString()
	{
		
		if (stringcounter < fullstring.Length)
		{
			stringtimer += Time.deltaTime;
			
			if (stringtimer > 0.05f)
			{
				currentstring = fullstring.Substring(0, stringcounter);
				stringcounter++;
				stringtimer = 0;	
			}
		}
		
	}
	
	void checkshit()
	{
		Debug.Log("Slot1: " + PlayerPrefs.GetString("Slot1"));
		Debug.Log("Slot2: " + PlayerPrefs.GetString("Slot2"));	
		Debug.Log("Slot3: " + PlayerPrefs.GetString("Slot3"));	
		Debug.Log("Slot4: " + PlayerPrefs.GetString("Slot4"));
		Debug.Log("Slot5: " + PlayerPrefs.GetString("Slot5"));
		
		Debug.Log("GsmashT1: " + PlayerPrefs.GetString("GroundsmashT1"));
		Debug.Log("GsmashT2: " + PlayerPrefs.GetString("GroundsmashT2"));
		
		Debug.Log("HurrT1: " + PlayerPrefs.GetString("HurricaneT1"));
		Debug.Log("HurrT2: " + PlayerPrefs.GetString("HurricaneT2"));
		
		Debug.Log("LazerT1: " + PlayerPrefs.GetString("LazerT1"));
		Debug.Log("LazerT2: " + PlayerPrefs.GetString("LazerT2"));
		
		Debug.Log("SbombT1: " + PlayerPrefs.GetString("SpiritbombT1"));
		Debug.Log("SbombT2: " + PlayerPrefs.GetString("SpiritbombT2"));
		
		Debug.Log("SerenT1: " + PlayerPrefs.GetString("SerenityT1"));
		Debug.Log("SerenT2: " + PlayerPrefs.GetString("SerenityT2"));
		
		Debug.Log("TdoorT1: " + PlayerPrefs.GetString("TeledoorT1"));
		Debug.Log("TdoorT2: " + PlayerPrefs.GetString("TeledoorT2"));
		
		Debug.Log("CloneT1: " + PlayerPrefs.GetString("CloneT1"));
		Debug.Log("CloneT2: " + PlayerPrefs.GetString("CloneT2"));
		
		Debug.Log("WallT1: " + PlayerPrefs.GetString("WallT1"));
		Debug.Log("WallT2: " + PlayerPrefs.GetString("WallT2"));
		
		Debug.Log("TackleT1: " + PlayerPrefs.GetString("TackleT1"));
		Debug.Log("TackleT2: " + PlayerPrefs.GetString("TackleT2"));
		
		Debug.Log("ForceT1: " + PlayerPrefs.GetString("ForceT1"));
		Debug.Log("ForceT2: " + PlayerPrefs.GetString("ForceT2"));
		
		Debug.Log("Sstrikerune: " + PlayerPrefs.GetString("Sstrikerune"));
		
	}
	
}
