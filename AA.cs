using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using States;

public class AA : ship {//this is your flagship
	public AA (){ }
	public AA(string spriteString,
		string shipNameINIT,
		int armorINIT,
		int shieldINIT,
		float rotationFactorINIT,
		float moveFactorINIT,
		weapon[] weaponsArrayINIT,
		defense[] defenseArrayINIT,
		float spawnX,
		float spawnY){
		
	}
	//public int length{get};
	
	public static Status shipState;//ship state
	
	
	public static float orientation=0.0f;
	public static int moveX, moveY=0;
	public string[] lockedOn = new string[5];
	/*
	 * This is a dictionary of commands that the player can use
	 * It is useful because now with the dictionary overhead, using GetMethod().Invoke() is more safe
	 * because they have to first hash throug the dictionary and if the command isn't in the dictionary
	 * then the script will not execute the script the player types in which will prevent exploits
	 */
	public Dictionary<string, string> commands = new Dictionary<string, string>{
		{"ROTATE", "rotate"},//rotate the ship (CANNOT MOVE)
		{"MOVE", "move"},//move the ship (CANNOT ROTATE)
		{"STOP", "stop"},//forces the ship to stop rotating or moving
		{"LOCKON", "lockon"},//ship is locking onto a target
		{"FIRE", "fire"}//fire weapons
	};
	
	//a dictionary of the IMPORTANT crew members aboard the Nordwind
	public Dictionary<string, crew> crewMembers = new Dictionary<string, crew>();//dictionary containing all the "important" crew members
	

		
	//THESE ARE ALL PLAYER COMMAND FUNCTIONS************************************
	/*
	 * NOTE that there are certain actions the player can't do at the same time
	 * AKA moving and rotating at the same time
	 * This is what the shipState is for
	 * */
	public static string rotate(string[] input, float[] currShipPos){
		string toReturn=null;
		float rotationValue=0.0f;
		
		if(shipState==States.Status.CANNOT_ROTATE){//this checks if the ship is currently unable to rotate. if it is, it'll ignore the rotate input
			toReturn="Ship cannot rotate right now.";
			return toReturn;
		} else{//if the ship is free to rotate, then it will
			try{
				rotationValue=Convert.ToSingle(input[1]);
			} catch(System.Exception e){
				if (e is FormatException || e is OverflowException || e is InvalidCastException)
				{
					Debug.Log ("Not a valid input");
					toReturn = "Failed to rotate.";
					return toReturn;
				}
			}
			
			//rotationValue%=360;
			//floats go to 3.4e38 in both directions, if someone is spinning enough in one level to cap that, then fuck them
			orientation+=rotationValue;
			/*
			if(orientation>=360){
				orientation-=360;
			} else if(orientation<=-360){
				orientation+=360;
			}
			*/
			toReturn="Rotated " + input[1] + " degrees.";
			Debug.Log (orientation);
			return toReturn;
		}
	}
	
	public static string move(string[] input, float[] currShipPos){
		float moveValue;
		string toReturn=null;
		
		
		if(shipState==States.Status.CANNOT_MOVE){//this checks if the ship is currently unable to move. if it is, it'll ignore the move input
			toReturn="Ship cannot move right now.";
			return toReturn;
		} else{//let them move
			if(input.Length==1){//that means they only typed in MOVE and so now we have to ask them for X and Y
				Debug.Log("not enough parameters");
				toReturn="Not enough parameters given. Move takes 1 parameter";
				return toReturn;
			} else if(input.Length==2){//input[1] can be a target location so check for that
				if (input[1]==null||input[1]==" "||input[1]==""){
					toReturn="Invalid move amount.";
					return toReturn;
				} else{
					toReturn="Moving foward " + input[1] + " units. The orientation is " + orientation;
					try{
						moveValue=Convert.ToSingle(input[1]);
						
						moveY=Convert.ToInt32(moveValue * (float)Math.Cos (orientation*Math.PI/180)+currShipPos[1]);
						moveX=Convert.ToInt32(moveValue * (float)Math.Sin (orientation*Math.PI/180)+currShipPos[0]);
						
					} catch(System.Exception e){
						if (e is FormatException || e is OverflowException || e is InvalidCastException)
						{
							Debug.Log ("Not a valid input");
							toReturn = "Failed to move.";
							return toReturn;
						}
					}
					return toReturn;
				}
			} else{//too many inputs
				toReturn="Too many parameters given. Move takes only 1 parameter";
				return toReturn;
			}
			//return "Move";	
		}
	}
	
	public static string stop(string[] input, float[] currShipPos){
		string toReturn=null;
		moveX=(int)currShipPos[0];
		moveY=(int)currShipPos[1];
		orientation=currShipPos[2];
		shipState=States.Status.NORMAL;
		toReturn="The ship has been halted.";
		return toReturn;
	}
	
	public static string lockon(string[] input, float[] currShipPos){
		string toReturn=null;
		return toReturn;
	}
	
	public static string fire(string[] input, float[] currShipPos){
		string toReturn=null;
		return toReturn;
	}
	
	// Use this for initialization
	public void Start () {
		shipState = States.Status.NORMAL;//set the ship state to be normal on startup
		objectID="00000";
		Futile.atlasManager.LoadAtlas("Atlases/basicShip");//finds the atlas for the ship sprite
		
		objectSprite = new FSprite("basicShip");//declares the ship sprites
		objectSprite.x = 0.0f;//puts it in the center
		objectSprite.y = 0.0f;//puts it in the center
		
		Futile.stage.AddChild(objectSprite);//adds it to the stage
		Debug.Log("ran Archangel start");
	}
	
	// Update is called once per frame
	public void Update () {
		
		
		
		if(objectSprite.rotation <= orientation+0.5 && objectSprite.rotation >= orientation-0.5){
			shipState = States.Status.NORMAL;
		} else if(objectSprite.rotation < orientation){
			objectSprite.rotation++;
			shipState = States.Status.CANNOT_MOVE;
		} else if(objectSprite.rotation > orientation){
			objectSprite.rotation--;
			shipState = States.Status.CANNOT_MOVE;
		}
		
		
		if(objectSprite.x<=moveX+1 && objectSprite.x>=moveX-1){
			if(shipState!=States.Status.CANNOT_MOVE)
				shipState = States.Status.NORMAL;
		}else if(objectSprite.x<moveX){
			objectSprite.x+=(float)Math.Abs(Math.Sin (orientation*Math.PI/180));
			shipState = States.Status.CANNOT_ROTATE;
		} else if(objectSprite.x>moveX){
			objectSprite.x-=(float)Math.Abs(Math.Sin (orientation*Math.PI/180));
			shipState = States.Status.CANNOT_ROTATE;
		}
		
		if(objectSprite.y>=moveY-1 && objectSprite.y<=moveY+1){
			if(shipState!=States.Status.CANNOT_MOVE)
				shipState = States.Status.NORMAL;
		}else if(objectSprite.y<moveY){
			objectSprite.y+=(float)Math.Abs(Math.Cos (orientation*Math.PI/180));
			shipState = States.Status.CANNOT_ROTATE;
		} else if(objectSprite.y>moveY){
			objectSprite.y-=(float)Math.Abs(Math.Cos (orientation*Math.PI/180));
			shipState = States.Status.CANNOT_ROTATE;
		}
	}
}
