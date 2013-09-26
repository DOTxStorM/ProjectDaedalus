import java.util.Scanner;
import java.io.File;
class stringParse{
	public static String[] stringParse(String args){
	

	//Scanner reader = new Scanner(System.in);
	
	//System.out.print("Enter input: ");
	//String input = reader.nextLine();
	String delims = "[ ]+";
	String[] tokens = args.split(delims);
	return tokens;
	
	/* Uncomment if you want to have the array printed
	for (int i = 0; i < tokens.length; i++)
		System.out.println(tokens[i]);

	}
	*/
	}
}