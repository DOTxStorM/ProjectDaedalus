/**
*	UDP Client Program
*	Connects to a UDP Server
*	Receives a line of input from the keyboard and sends it to the server
*	Receives a response from the server and displays it.
*
*	@author: Michael Fahy
@	version: 2.1
*/

import java.io.*;
import java.net.*;

class UDPClient {
    public static void main(String args[]) throws Exception
    {
    	    BufferedReader inFromUser = new BufferedReader(new InputStreamReader(System.in));
    	    

      

      DatagramSocket clientSocket = new DatagramSocket();

      InetAddress IPAddress = InetAddress.getByName("localhost");

      byte[] sendData = new byte[1024];
      byte[] receiveData = new byte[1024];
      System.out.printf(">: ");
      String sentence = inFromUser.readLine();
      sentence = sentence.toUpperCase();
      sendData = sentence.getBytes();
	  DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.length, IPAddress, 9876);

      clientSocket.send(sendPacket);

      DatagramPacket receivePacket = new DatagramPacket(receiveData, receiveData.length);

      clientSocket.receive(receivePacket);

      String modifiedSentence = new String(receivePacket.getData());

      System.out.println(modifiedSentence);
      clientSocket.close();
      }
}
