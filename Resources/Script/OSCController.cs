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
	/**
	* デッキの配列の順番からカードの情報をリクエストするためのメゾット	
	**/
	public void RequestCard(string sys,int i,bool deckLock)
	{
		if(deckLock == false)
		{
			Debug.Log("RequestCard");
			string message = thisPC + "/" + sys + "/" + i + "/";
			StartCoroutine("waitTime");

			OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
		}
	}
	/**
	* デッキのカードの更新を行うためのメゾット
	**/
	public void updateCard(string sys,GameObject card)
	{
		StartCoroutine("waitTime");

		Debug.Log("updateCard");
		Card info = card.GetComponent<Card>();	
		string message = thisPC + "/" + sys + "/";
		message = message + info.Mark + "." + info.Number + "." + info.CardMode + ".";
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}
	/**
	* 手札のカードの交換を行うためのメゾット
	**/
	public void exchange(string sys,GameObject card)
	{
		Debug.Log("exchange");
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

	private void waitTime()
	{
		Debug.Log("--------------処理中----------------");
		System.Threading.Thread.Sleep(1);
		Debug.Log("--------------処理終了----------------");
	}

	public void oscSendMessege(string message)
	{
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}
}
