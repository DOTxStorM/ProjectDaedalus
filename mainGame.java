import java.io.*;
import java.net.*;

class mainGame{
	public static void main(String argv[]){
		ship ArcAngel = new ship(100f, 100f, 0f, 0.1f, 1f, 0, 0);
		
		int[] location = new int[2];
		location = ArcAngel.getLocation();
		
		System.out.println(location[0]);
	}
}
