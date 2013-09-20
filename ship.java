class ship{
	float m_shields;
	float m_armor;
	float m_currRotation;
	float m_movementSpeed;
	float m_rotationSpeed;
	
	int posX;
	int posY;
	
	boolean bIsRotation = false;
	boolean bIsMoving = false;
	public ship(float shields,
		float armor,
		float spawnRotation,
		float movementSpeed,
		float rotationSpeed,
		int spawnX,
		int spawnY){
		
		m_shields = shields;
		m_armor = armor;
		m_currRotation = spawnRotation;
		m_movementSpeed = movementSpeed;
		m_rotationSpeed = rotationSpeed;
		
		posX = spawnX;
		posY = spawnY;
	}
	
	public int[] getLocation(){
		int[] toReturn = new int[2];
		
		toReturn[0] = posX;
		toReturn[1] = posY;
		
		return toReturn;
	}
	
	public static void main(String argv[]){
		
	}
}
