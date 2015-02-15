using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Big_Money : MonoBehaviour 
{
	//通信情報を保持するオブジェクト
	public GameObject OSCController;
	//デッキの生成に関するオブジェクト
	public GameObject DeckController;
	

	//通信情報を保持するオブジェクトのコンポーネント	
	private OSCController oscComponent;
	//デッキのコンポーネントを保存するオブジェクト	
	private  CreatDeck creatDeck;

	//デッキを保持するオブジェクト	
	private GameObject[] deck;

	//------通信に必要な変数------
	public string TargetAddr;
	public int OutGoingPort;
	public int InComingPort;
	public string MyPC;
	public string OutPC;
	//--------------------------

	void Start()
	{
		Debug.Log("PvP大富豪の準備開始");
		//-----------------OSCの環境を構築----------------
		OSCController = (GameObject)Instantiate(OSCController);
		oscComponent = OSCController.GetComponent<OSCController> ();
		oscComponent.setOSC(TargetAddr,OutGoingPort,InComingPort,MyPC,OutPC);
		//----------------------------------------------
		//デッキのオブジェクトデータを取得
		creatDeck = DeckController.GetComponent<CreatDeck> ();
	}
	void Update()
	{
		//------------OSCの更新を行う-----------------
		if(oscComponent.CreateClient == true)
		{
			oscComponent.UpdateOSC();
			GameSystem(oscComponent.catchMessage());
		}
		//------------------------------------------

		//--------------コマンド操作[C]--------------
		if(Input.GetKeyDown(KeyCode.C))
		{
			Debug.Log("Cの入力を確認");
			foreach(GameObject obj in deck)
			{
				Destroy(obj);
			}
			while(CheckClear() != true)
			{
				foreach(GameObject obj in deck)
				{
					Destroy(obj);
				}
			}
		}
		//---------------------------------------
		//-------------マウス操作-----------------
		if(Input.GetMouseButtonDown(0))
		{

			GameObject cardInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit))
			{

				if(hit.collider.gameObject.tag == "Card")
				{
					Debug.Log("card認識");
					
					cardInfo = hit.collider.gameObject;
					//Card info = cardInfo.GetComponent<Card>();
					oscComponent.sendMessage("send",cardInfo,OutPC);
				}
				else if(hit.collider.gameObject.tag == "CreateDeck")
				{
					deck = creatDeck.creatDeck();
					//oscComponent.sendDeck("deck",deck);
				}
			}
		}
		//---------------------------------------
	}
	/**
	* ボタン操作によるクライアント作成のメゾット	
	**/
	public void CreateClient()
	{
		oscComponent.makeClient();
	}
	/**
	* OSCによって得られたメッセージデータの解析
	**/
	private void GameSystem(string message)
	{
		if(message != null)
		{
			string[] messageData = message.Split('/');
			foreach (string Data in messageData) 
			{
				Debug.Log("messageData["+Data+"]");
  			}

  			Debug.Log("ユーザ["+messageData[0]+"]による処理を行います");
  			Debug.Log("処理モード["+messageData[1].ToString()+"]");

  			if(messageData[1].ToString() == "deck")
  			{
  				Debug.Log("deck該当");
  				if(MatchDeck(messageData[2].ToString()))
  				{
  					Debug.Log("デッキ構築完了");
  				}
  			}
  			else if(messageData[1].ToString() == "send")
  			{  				
  				Debug.Log("send該当");
  				ExchangeCard(messageData[2].ToString());
  			}
  			else if(messageData[1].ToString() == "RequestDeck")
  			{
  				oscComponent.sendDeck("deck",deck);
  			}
  			else
  			{
  				Debug.Log("対応してない処理です");
  			}
  		}
	}
	private bool MatchDeck(string message)
	{
		Debug.Log("デッキの同期を行います");
		string[] makeDeck = message.Split('.');
		
		if(deck[int.Parse(makeDeck[0])] == null)
		{
			Debug.Log("新規のカードデータです");
		}
		else
		{
			Debug.Log("すでに更新済みです");
		}
		return true;
	}
	private void ExchangeCard(string message)
	{
		Debug.Log("手札の交換を行います");
	}


	/**
	* デッキの内容の情報が消えているのかを確認する
	**/
	private bool CheckClear()
	{
		bool ret = false;
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Card");
		if(objs.Length == 0)
		{
			ret = true;
		}
		return ret;
	}
}
