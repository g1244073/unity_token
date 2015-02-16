using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class OSCController : MonoBehaviour 
{
	private string TargetAddr;
	private int OutGoingPort;
	private string thisPC;
	private string OutPCs;
	
	private Dictionary<string, ServerLog> servers;

	public bool CreateClient = false;

	private string saveString = null;


	public void setOSC(string TargetAddr,int OutGoingPort,int InComingPort,string thisPC,string OutPCs) 
	{	
		this.TargetAddr = TargetAddr;
		this.OutGoingPort = OutGoingPort;
		this.thisPC = thisPC;
		this.OutPCs = OutPCs;

		OSCHandler.Instance.Init(TargetAddr, OutGoingPort, InComingPort,thisPC,OutPCs); //init OSC
		servers = new Dictionary<string, ServerLog>();
	}

	// NOTE: The received messages at each server are updated here
    // Hence, this update depends on your application architecture
    // How many frames per second or Update() calls per frame?
	public void UpdateOSC() 
	{
		OSCHandler.Instance.UpdateLogs();
		servers = OSCHandler.Instance.Servers;

	    foreach( KeyValuePair<string, ServerLog> item in servers )
		{
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			
			if(item.Value.log.Count > 0) 
			{
				//int lastPacketIndex = item.Value.packets.Count - 1;
				
				/*
				UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}", 
				item.Key, // Server name
				item.Value.packets[lastPacketIndex].Address, // OSC address
				item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value
				*/
			}
	    }
	    //OSCHandler.Instance.SendMessageToClient(OutPCs,this.TargetAddr,1+testnumber);
	}
	public void makeClient()
	{
		Debug.Log("make");

		OSCHandler.Instance.InitClient(this.TargetAddr,this.OutGoingPort,this.OutPCs);
		if(OSCHandler.Instance.SendClient_text(this.TargetAddr,this.OutGoingPort,this.OutPCs,"test") == true)
		{
			Debug.Log("クライアントの作成に成功");
			CreateClient = true;
		}
		else
		{
			Debug.Log("クライアントの作成に失敗");
		}

	}

	public void sendMessage(string sys,GameObject card,string PCNAME)
	{
		//メッセージを相手に送信します。
		Debug.Log("sending");

		Card info = card.GetComponent<Card>();
		string message = "/" + thisPC + "/" + sys + "/" + info.Mark + "/" + info.Number + "/" + PCNAME + "/";
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}
	public void sendDeck(string sys,GameObject[] deck)
	{
		Debug.Log("match-deck");
		int i = 0;
		string message;
		foreach(GameObject obj in deck)
		{
			message = thisPC + "/" + sys + "/";
			Card info = obj.GetComponent<Card>();
			message = message + i + "." + info.Mark + "." + info.Number + "." ;
			OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
			i++;
		}
	}

	public string catchMessage()
	{
		if((saveString != OSCHandler.Instance.getLastPacket() || saveString == null) || (OSCHandler.Instance.getLastPacket().Contains("RequestDeck")) )
		{			
			saveString = OSCHandler.Instance.getLastPacket();
			return saveString;
		}

		return null;
	}
	public void RequestDeck(string sys)
	{
		Debug.Log("RequestDeck");
		string message = thisPC + "/" + sys + "/";
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}

	public void sendMessage(string sys,string messages)
	{
		Debug.Log("send");
		string message = thisPC + "/"+sys+"/"+ messages; 
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}
	public void updateCard(string sys,GameObject card)
	{
		Debug.Log("updateCard");
		Card info = card.GetComponent<Card>();	
		string message = thisPC + "/" + sys + "/";
		message = message + info.Mark + "." + info.Number + "." + info.CardMode + ".";
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}

	public void startGame(string sys)
	{
		Debug.Log("startGame");
		string message = thisPC + "/" + sys + "/";
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}


}
