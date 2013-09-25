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
  public static void main(String args[]) throws Exception
    {

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
		/*
      byte[] receiveData = new byte[1024];
      byte[] sendData  = new byte[1024];
      */
      while(true)
        {
        	 byte[] receiveData = new byte[1024];
        	 byte[] sendData  = new byte[1024];

          DatagramPacket receivePacket =
             new DatagramPacket(receiveData, receiveData.length);
          serverSocket.receive(receivePacket);
          String sentence = new String(receivePacket.getData());

          InetAddress IPAddress = receivePacket.getAddress();

          int port = receivePacket.getPort();

          System.out.println(IPAddress.toString() + ": " + sentence);
          String returnToClient = "You sent: " + sentence;

          sendData = returnToClient.getBytes();

          DatagramPacket sendPacket =
             new DatagramPacket(sendData, sendData.length, IPAddress,
                               port);

          serverSocket.send(sendPacket);
        }
    }
}
