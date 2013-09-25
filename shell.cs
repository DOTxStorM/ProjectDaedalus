using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using System;
//no longer : Monobehavior
public class shell : MonoBehaviour {
	

	
	public Material material;
	public Color color = Color.cyan;
	
	public string inputCommand;
	public string stringShown = "I am a text area\nI have 2 lines";
	public string logger = ">[Commander]: ";
	public char[] delimiterChar={' '};
	public string[] commands;
	public string testString;
	public string listOfIDs=null;
	public AA ArchAngel = new AA();
	//public States.Status state;
	//public bool bHitEnter=false;
	
	public weapon[] AAWeaponsArray;
	public defense[] AADefenseArray;
	
	//private Dictionary<string, string> IDsVisible = new Dictionary<string, string>();
	
	Event enter;
	
	public shell(Dictionary<string, string> IDsOnLevel){
		foreach(KeyValuePair<string, string> kvp in IDsOnLevel){
			listOfIDs+=kvp.Key + "\n";
		}
	}
	DemoLevel thisLevel;
	//Use this for initialization BUT NOT ANYMORE BECAUSE IT ISN'T MONOBEHAVIOR
	void Start () {
		//gameObject.AddComponent("AA");
		//ArchAngel = gameObject.AddComponent("AA") as AA;
		/*
		Debug.Log("running start");
		ArchAngel.shipName="ArchAngel";
		ArchAngel.armor=100;
		ArchAngel.shield=100;
		ArchAngel.rotationFactor=20.0f;
		ArchAngel.moveFactor=20.0f;
		*/
		Debug.Log("running shell start");
		ship basicShip = new ship("basicShip", "Archangel", 100, 100, 20.0f, 20.0f, AAWeaponsArray, AADefenseArray, 0.0f, 0.0f);
		Debug.Log("archangel has arived");
	}
	
	// Update is called once per frame BUT NOT USED BECAUSE IT IS NOT MONOBEHAVIOR
	void Update () {
		
	}
	void submit(){
		inputCommand=inputCommand.ToUpper();
		stringShown = logger + inputCommand;//adds the logger statement to make it look pretty
		commands = inputCommand.Split(delimiterChar);//parses and splits the input command and puts it in a string[]
		inputCommand="";//clears the inputCommand
		stringShown = stringShown + "\n" + logger + runCommands(commands);
		
		/* PROBABLY A BAD IDEA TO BE CHECKING STATES EVERYTIME
		switch(ArchAngel.shipState){//when the user submits, we have to check what the status of the ship is before we roll out
			
		case States.Status.NORMAL://in case that the ship is in a normal state and ready to go
			inputCommand=inputCommand.ToUpper();
			stringShown = logger + inputCommand;//adds the logger statement to make it look pretty
			commands = inputCommand.Split(delimiterChar);//parses and splits the input command and puts it in a string[]
			inputCommand="";//clears the inputCommand
			stringShown = stringShown + "\n" + logger + runCommands(commands);
			break;
			
		case States.Status.WAITINGFORMOVE_X://this means that the user input "MOVE" and now we have to ask for the X value for the move vector
			Debug.Log ("okay, i got the X");
			ArchAngel.shipState=States.Status.WAITINGFORMOVE_Y;//next input should be the Y value
			break;
			
		case States.Status.WAITINGFORMOVE_Y://now we are waiting for the Y value for the move vector
			Debug.Log("okay, i got the Y, switching back to normal");
			ArchAngel.shipState=States.Status.NORMAL;//we have both values, set the ship to NORMAL state
			break;
		} 
		*/
	}
	
	void OnGUI(){		
		enter = Event.current;		
		if(enter.type == EventType.KeyDown && enter.keyCode == KeyCode.Return){
			submit();//if the event is Return, then submit whatever the user inputed
		} else if(enter.type == EventType.KeyDown && enter.keyCode == KeyCode.UpArrow){//just like a console, you will return the last thing they submitted
			
		}
		inputCommand = GUI.TextField (new Rect(0,Screen.height-50,500,50),inputCommand, 100);//the input section
		GUI.TextField (new Rect(0,Screen.height-150,500,100),stringShown, 350);
		GUI.TextField (new Rect(Screen.width-250,0,100,250),listOfIDs, 100);
		
		//GUI.TextField (new Rect(400,0,350,500), testString, 100);//using to see how the commands are parsed
	}
	
	
	//MOST IMPORTANT SCRIPT*****************************************
	string runCommands(string[] input){
		string toReturn=null;
		float[] currShipPos = new float[3];
		currShipPos[0] = ArchAngel.objectSprite.x;
		currShipPos[1] = ArchAngel.objectSprite.y;
		currShipPos[2] = ArchAngel.objectSprite.rotation;
		
		try{
			toReturn=(string)typeof(AA).GetMethod(ArchAngel.commands[input[0]]).Invoke(null, new object[] {input, currShipPos});
		} catch(System.Exception e){
			if(e is System.NullReferenceException || e is KeyNotFoundException) {
				toReturn="No such command exists";
				Debug.Log ("No such command exists");
			}
		}
		
		return toReturn;
		
	}

}
