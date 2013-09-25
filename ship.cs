using UnityEngine;
//using System.Collections.Generic;
using System.Collections;

public class ship : gameObject {
	
	public string shipName;//every ship will have a name
	public int armor, shield;//health and shield
	public float rotationFactor, moveFactor;//laymens terms: inertia FACTOR of the ship. the larger its	
	//"mass" the longer it'll take to turn or get moving.
	
	public weapon[] weaponsArray;//array of weapons
	public defense[] defenseArray;//array of defense options
	
	public ship(){ }
	
	public ship(string spriteString,
		string shipNameINIT,
		int armorINIT,
		int shieldINIT,
		float rotationFactorINIT,
		float moveFactorINIT,
		weapon[] weaponsArrayINIT,
		defense[] defenseArrayINIT,
		float spawnX,
		float spawnY){
		
		shipName=shipNameINIT;
		armor=armorINIT;
		shield= shieldINIT;
		rotationFactor=rotationFactorINIT;
		moveFactor=moveFactorINIT;
		weaponsArray=weaponsArrayINIT;
		defenseArray=defenseArrayINIT;
		//INIT sprite
		Futile.atlasManager.LoadAtlas("Atlases/basicShip");//finds the atlas for the ship sprite
		
		objectSprite = new FSprite(spriteString);//declares the ship sprites
		objectSprite.x = spawnX;//puts it in given X
		objectSprite.y = spawnY;//puts it in given Y
		
		Futile.stage.AddChild(objectSprite);//adds it to the stage
	}
	

	
	
				
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
