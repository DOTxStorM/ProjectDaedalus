/**
*	UDP Server Program
*	Listens on a UDP port
*	Receives a line of input from a UDP client
*	Returns an upper case version of the line to the client
*
*	@author: Michael Fahy
@	version: 2.0
*/

import java.io.*;
import java.net.*;

class UDPServer {
	
	public static player[] makePlayerRoom(int numPlayers, DatagramSocket serverSocket){
		player[] waitingRoom = new player[numPlayers];
		System.out.println("Making a room for " + numPlayers + " players.");
		for(int i = 0; i < numPlayers; ++i){
				byte[] receiveData = new byte[1024];
				byte[] sendData = new byte[1024];
				
				DatagramPacket receivePacket = new DatagramPacket(receiveData, receiveData.length);
				try{
					serverSocket.receive(receivePacket);
				} catch(IOException e){
					System.out.println("caught IOException");
				}
				InetAddress IPAddress = receivePacket.getAddress();
				System.out.println(IPAddress);
				if(i == 0){
					waitingRoom[i] = new player(IPAddress, i+1);
				} else{
					for(player p : waitingRoom){
						if(p.m_IPAddress != IPAddress){
							waitingRoom[i] = new player(IPAddress, i+1);
						} else{
							System.out.println("There already exists a player with that IP address.");
							//then exit the program
						}
					}
				}
				System.out.println("Made player with ID " + waitingRoom[i].m_playerID + " and with IP address " + waitingRoom[i].m_IPAddress);
		}
		return waitingRoom;
	}
	
  public static void main(String args[]) throws Exception
    {
    	    player[] listOfPlayers = new player[4];

    DatagramSocket serverSocket = null;
	  
	try
		{
			serverSocket = new DatagramSocket(9876);
		}
	
	catch(Exception e)
		{
			System.out.println("Failed to open UDP socket");
			System.exit(0);
		}
		BufferedReader inFromUser = new BufferedReader(new InputStreamReader(System.in));
		System.out.printf(">: Enter the amount of players this room will hold (2 - 4): ");
		
		
		listOfPlayers = makePlayerRoom(Integer.valueOf(inFromUser.readLine()), serverSocket);
		System.out.println("GAME ROOM HAS BEEN MADE");
		for(player p : listOfPlayers){
			System.out.println("[ " + p.m_playerID + " ] " + p.m_IPAddress + '\n');
		}
      while(true)
        {
        	 byte[] receiveData = new byte[1024];
        	 byte[] sendData  = new byte[1024];
        	 String[] commands;

          DatagramPacket receivePacket =
             new DatagramPacket(receiveData, receiveData.length);
          serverSocket.receive(receivePacket);
          String sentence = new String(receivePacket.getData());

          InetAddress IPAddress = receivePacket.getAddress();
          

          int port = receivePacket.getPort();

          System.out.println(IPAddress.toString() + ": " + sentence);
          commands = stringParse.stringParse(sentence);
          for(String s : commands){
          	System.out.println(s);  
          }
          String returnToClient = "You sent: " + sentence;

          sendData = returnToClient.getBytes();

          DatagramPacket sendPacket =
             new DatagramPacket(sendData, sendData.length, IPAddress,
                               port);

          serverSocket.send(sendPacket);
        }
    }
}
