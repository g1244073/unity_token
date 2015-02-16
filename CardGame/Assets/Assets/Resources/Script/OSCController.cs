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

	/**
	* setに関してOSCの初期設定を行いサーバを立てる
	**/
	public void setOSC(string TargetAddr,int OutGoingPort,int InComingPort,string thisPC,string OutPCs) 
	{	
		this.TargetAddr = TargetAddr;
		this.OutGoingPort = OutGoingPort;
		this.thisPC = thisPC;
		this.OutPCs = OutPCs;

		OSCHandler.Instance.Init(TargetAddr, OutGoingPort, InComingPort,thisPC,OutPCs); //init OSC
		servers = new Dictionary<string, ServerLog>();
	}

	/**
	* 登録されたサーバに関して更新を行う
	**/
	public void UpdateOSC() 
	{
		OSCHandler.Instance.UpdateLogs();
		servers = OSCHandler.Instance.Servers;
	}

	/**
	* クライアントを処理によって作成する
	**/
	public void makeClient()
	{
		Debug.Log("makeClient");
		OSCHandler.Instance.InitClient(this.TargetAddr,this.OutGoingPort,this.OutPCs);
		Debug.Log("クライアントの作成に成功");
		CreateClient = true;
	}

	public void sendMessage(string sys,GameObject card,string PCNAME)
	{
		//メッセージを相手に送信します。
		Debug.Log("sending");

		Card info = card.GetComponent<Card>();
		string message = "/" + thisPC + "/" + sys + "/" + info.Mark + "/" + info.Number + "/" + PCNAME + "/";
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}

	/**
	*　デッキの作成のためのメゾット
	**/
	public void sendDeck(string sys,GameObject[] deck)
	{
		Debug.Log("sendDeck");
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

	/**
	*　カードの作成のためのメゾット
	**/
	public void sendCard(string sys,GameObject card,int number)
	{
		Debug.Log("sendCard");
		int i = 0;
		string message = thisPC + "/" + sys + "/";
		Card info = card.GetComponent<Card>();
		message = message + number + "." + info.Mark + "." + info.Number + "." ;
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}


	/**
	* デッキの配列の順番からカードの情報をリクエストするためのメゾット	
	**/
	public void RequestCard(string sys,int i,bool deckLock)
	{
		if(deckLock == false)
		{
			Debug.Log("RequestCard");
			string message = thisPC + "/" + sys + "/" + i + "/";
			OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
		}
	}

	/**
	* デッキのカードの更新を行うためのメゾット
	**/
	public void updateCard(string sys,GameObject card)
	{
		Debug.Log("updateCard");
		Card info = card.GetComponent<Card>();	
		string message = thisPC + "/" + sys + "/";
		message = message + info.Mark + "." + info.Number + "." + info.CardMode + ".";
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}

	public string catchMessage()
	{
		if((saveString != OSCHandler.Instance.getLastPacket() || saveString == null) )
		{			
			saveString = OSCHandler.Instance.getLastPacket();
			return saveString;
		}

		return null;
	}
	public void RequestDeck(string sys,bool deckLock)
	{
		if(deckLock == false)
		{
			Debug.Log("RequestDeck");
			string message = thisPC + "/" + sys + "/";
			OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
		}
	}
	
	public void sendMessage(string sys,string messages)
	{
		Debug.Log("send");
		string message = thisPC + "/"+sys+"/"+ messages; 
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}
	

	public void startGame(string sys)
	{
		Debug.Log("startGame");
		string message = thisPC + "/" + sys + "/";
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}


}
