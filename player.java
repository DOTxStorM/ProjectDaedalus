import java.io.*;
import java.net.*;

class player{
	public InetAddress m_IPAddress;
	public int m_playerID;
	public player(InetAddress IPAddress, int playerID){
		m_IPAddress = IPAddress;
		m_playerID = playerID;
	}
}
