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
	* OSCによって取得したメッセージを応答する
	**/
	public string catchMessage()
	{
		if((saveString != OSCHandler.Instance.getLastPacket() || saveString == null) )
		{			
			saveString = OSCHandler.Instance.getLastPacket();
			return saveString;
		}
		return null;
	}
	/**
	* 一定時間作業を停止する
	**/
	private void waitTime(int time)
	{
		System.Threading.Thread.Sleep(time);
	}
	/**
	* OSCにメッセージを取得する
	**/
	public void oscSendMessege(string message)
	{
		this.waitTime(100);
		OSCHandler.Instance.SendMessageToClient(this.OutPCs,this.TargetAddr,message);
	}
}
