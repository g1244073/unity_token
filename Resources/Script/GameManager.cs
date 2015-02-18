using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	/**
	* ゲームの特有の機能を備えたオブジェクトを保持する変数	
	**/
	public GameObject Manager;
	/**
	/*デッキ情報を保持するオブジェクト	
	**/
	public GameObject[] deck;	
	/**
	* 通信情報を保持するオブジェクト
	**/
	public GameObject OSC;
	/**
	* デッキの生成に関するオブジェクト
	**/
	public GameObject DeckController;

	/**
	* デッキのコンポーネントを保存するオブジェクト	
	**/
	private BabaManager gameComponent;

	//---------------------UIの情報を保持するオブジェクト--------------
	/**
	* メインとなるTextフィールド
	**/
	public Text main_text;
	/**
	* OSCに関するTextフィールド
	**/
    public Text osc_text;
    /**
	* デッキに関するTextフィールド
	**/
    public Text deck_text;
    //-------------------------------------------------------------
	//---------------OSC設定のための変数-------------
	/**
	*　相手のIPアドレス
	**/
	public string TargetAddr;
	/**
	*　送信するポート番号
	**/
	public int OutGoingPort;
	/**
	* 受信するポート番号
	**/
	public int InComingPort;
	/**
	* 自身の名前
	**/
	public string MyPC;
	/**
	* 相手の名前
	**/
	public string OutPC;
	//---------------------------------------------
	/**
	* ゲームを起動してからの経過時間を保持
	**/
	private float gameTime = 0.0f;
	/**
	* ゲームのモードを保持する
	**/
	public int GameMode = 0;
	/**
	* デッキのロックを確認する
	**/
	private bool deckLock = false;
	/**
	* デッキが更新されたのか確認する
	**/
	private bool CheckDeck_mode = false;


	/**
	* ゲームを開始する
	**/
	public void Start () 
	{
		Debug.Log("ゲームを起動します");
		if(Manager == null)
		{
			Debug.Log("ゲームが実装されていません");
			Destroy(this);
		}
		//-----------------OSCの環境を構築----------------
		OSC = (GameObject)Instantiate(OSC);
		OSCController oscController = OSC.GetComponent<OSCController> ();
		oscController.setOSC(TargetAddr,OutGoingPort,InComingPort,MyPC,OutPC);
		//----------------------------------------------
		//-----------baba抜きのコンポーネントを取得する-------------
		Manager = (GameObject)Instantiate(Manager);
		gameComponent = Manager.GetComponent<BabaManager> ();
		gameComponent.setGame(OSC,DeckController,gameObject);
		//------------------------------------------------------
		//----------------色を変更する--------------
		this.changeObjectColor("Stage",Color.black);
		this.changeObjectColor("send",Color.black);
		this.changeObjectColor("MakeDeck",Color.black);
		//-----------------------------------------
		//-----------------UIの表示---------------
		this.writeText("GameSet",this.main_text);
		this.writeText("↑",this.osc_text);
		this.writeText("×",this.deck_text);
		//----------------------------------------
		deck = new GameObject[53];
	}
	/**
	*　Unityをupdateする
	**/
	public void Update () 
	{
		//ゲームの時間の更新
		gameTime += Time.deltaTime;

		//--------------------OSCの情報の更新---------------------
		OSCController oscController = OSC.GetComponent<OSCController> ();
		oscController.UpdateOSC();
		string message = oscController.catchMessage();
		this.gameSystem(message);
		//------------------------------------------------------
		//Gameコンポーネントの更新
		if(osc_text.text == "OK")
		{
			gameComponent.updateGame(message);
		}
		//----------------------コマンド操作------------------
		if(Input.GetKeyDown(KeyCode.C))
		{
			Debug.Log("キャンセル実行");
			GameObject[] objs = GameObject.FindGameObjectsWithTag("Card");
			foreach(GameObject obj in objs)
			{
				Destroy(obj);
			}
			this.checkClear();
		}
		//---------------------------------------------------
		//-------------------マウスによる操作------------------
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit))
			{
				if(hit.collider.gameObject.tag == "send" && GameMode == 0)
				{
					this.changeObjectColor("send",new Color(1.0f, 1.0f, 1.0f, 1f));
					oscController.makeClient();
					//-----------------UIの表示---------------
					writeText("○",osc_text);
					writeText("↑",deck_text);
					//----------------------------------------
					//ゲームのモードを変更する
					GameMode = 1;
				}
				//デッキをランダムに作成する
				else if(hit.collider.gameObject.tag == "MakeDeck" && GameMode == 1)
				{
					this.changeObjectColor("MakeDeck",new Color(1.0f, 1.0f, 1.0f, 1f));

					//デッキを作成する
					CreatDeck deckComponent = DeckController.GetComponent<CreatDeck> ();
					deck = deckComponent.creatDeck();
					deckLock = true;
					this.sendDeck("deck",deck);

					//ゲームのモードを変更する
					GameMode = 2;
					//-----------------UIの表示---------------
					Debug.Log("ゲームモードを[開始]に変更");
					writeText("Deck作成完了",main_text);
					writeText("○",deck_text);
					//----------------------------------------
				}
			}
		}
		//-----------------------------------------------------
	}
	//-----------------------------------------------------------------
	/**
	* UIにtextを表示するためのメゾット
	**/
	public void writeText(string text,Text target)
	{
        target.text = text;
	}
	/**
	* オブジェクトを対象の色に変更する
	**/
	public void changeObjectColor(string obj,Color color)
	{
		GameObject target = GameObject.Find(obj);
		target.renderer.material.color = color;
	}
	/**
	* 目的のカードがすべて消えたのか確認
	**/
	private bool checkClear()
	{
		bool ret = false;
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Card");
		if(objs.Length == 0)
		{
			ret = true;
		}
		return ret;
	}
	/**
	* 時間を入力した数値待機させる
	**/
	public void waitTime(int time)
	{
		Debug.Log("処理中");
		System.Threading.Thread.Sleep(time);
	}
	//-----------------------------------------------------------------
	public void setDeck(int player_number,GameObject[] deck)
	{
		int i = 0;
		foreach(GameObject obj in deck)
		{
			Card info = obj.GetComponent<Card>();
			obj.renderer.material.color = Color.white;
			if(player_number == 2)
			{
				if(i % player_number == 0)
				{

					info.CardMode = "A";
				}
				else 
				{
					info.CardMode = "B";
				}
			}
			else if(player_number == 4)
			{
				if(i % player_number == 0)
				{
					info.CardMode = "A";
				}
				else if(i % player_number == 1)
				{
					info.CardMode = "B";
				}
				else if(i % player_number == 2)
				{
					info.CardMode = "C";
				}
				else 
				{
					info.CardMode = "D";
				}
			}
			i++;
		}
		reset();
	}
	/**
	* カードの位置を調整する
	**/
	public void reset()
	{
		//ステーズのサイズを獲得
		GameObject Stage = GameObject.Find("Stage");
		Vector3 stage_v  = Stage.transform.localScale;
		int countA=0,countB=0;
		foreach(GameObject obj in deck)
		{
			Card info = obj.GetComponent<Card>();
			Vector3 v = obj.transform.localPosition;
			//Debug.Log(stage_v.x);

			if(info.CardMode == MyPC)
			{
				v.x = (float)(-(stage_v.x/2) + 2.5 + countA*4);
				v.y = (float)(5.0f);
				v.z = (float)(-(stage_v.z/2));
				countA++;
			}
			
			else if(info.CardMode == OutPC)
			{
				v.x = (float)((stage_v.x/2) - 2.5 - countB*4);
				v.y = (float)(5.0f);
				v.z = (float)((stage_v.z/2));
				countB++;
			}
			/*
			else if(info.CardMode == "C")
			{
				v.x = (float)(5*m+StageX - 50);
				v.y = (float)(5);
				v.z = (float)(StageZ+45);
				m++;
			}
			else if(info.CardMode == "D")
			{
				v.x = (float)(5*m+StageX - 50);
				v.y = (float)(5);
				v.z = (float)(StageZ+45);
				m++;
			}
			*/
			else if(info.CardMode == "OUT")
			{
				v.x = (float)(-55.0f);
				v.y = (float)(0.1f);
				v.z = (float)(0.0f);
			}
			info.D_MoveCard(obj,v,MyPC,OutPC);
		}
		gameComponent.checkGame();
	}
	//--------------------------OSCに通信を受信した時の処理------------------
	private void gameSystem(string message)
	{
		if(message != null)
		{
			string[] messageData = message.Split('/');

  			if(messageData[0].ToString().Contains("192"))
			{
				Debug.Log("OSC接続[確認]");
			}
			else
			{
				//デッキの更新を行う
  				if(messageData[1].ToString() == "deck")
  				{
  					this.MatchDeck(messageData[2].ToString());
  					if(CheckDeck_mode == false)
  					{
  						this.CheckDeck();
  					}
	  			}
				//デッキの更新を行う
  				else if(messageData[1].ToString() == "checkDeck")
  				{
  					this.CheckDeck();
	  			}
	  			//与えられた数字のカードの情報をリクエストする
	  			else if(messageData[1].ToString() == "RequestCard")
	  			{
	  				Debug.Log("RequestCard該当");
	  				string number = messageData[2].ToString();
	 				this.sendCard("sendCard",deck[int.Parse(number)],int.Parse(number));
	  			}
	  			//送られたカードの情報からカードの更新する
	  			else if(messageData[1].ToString() == "sendCard")
	  			{  				
	   				Debug.Log("sendCard該当");
	  				this.MatchDeck(messageData[2].ToString());
	  			}
	  			//送られたかカードの情報を上書きする
	  			else if(messageData[1].ToString() == "updateCard")
	  			{
	  				Debug.Log("updateCard該当");
	  				updateCard(messageData[2].ToString());
	  			}
	  			
	  			//------------以下はデバック用の処理---------------
				else if(messageData[1].ToString() == "SendMessage")
	  			{
	  				Debug.Log("デッキ構築完了[確認]" + messageData[2].ToString());
				}
			}
  		}
	}
	/**
	* 初回のデッキの更新
	**/
	private void MatchDeck(string message)
	{
		Debug.Log("デッキの同期を行います");
		string[] makeDeck = message.Split('.');
		
		if(deck[int.Parse(makeDeck[0])] == null)
		{
			CreatDeck deckComponent = DeckController.GetComponent<CreatDeck> ();
			deck[int.Parse(makeDeck[0])] = deckComponent.makeCard(int.Parse(makeDeck[1]),int.Parse(makeDeck[2]),int.Parse(makeDeck[0]));
		}
		this.CheckDeck();
	}
	/**
	*  デッキが未完な時に対して更新を行う
	**/
	private void CheckDeck()
	{
		CheckDeck_mode = true;
		float stageColor = 0.0f;
	   	int i = 1;
	   	int m = 0;
	   	bool mode = false;
  		//Debug.Log("デッキ構築のループ実行中["+i+"]");

		foreach(GameObject obj in deck)
		{
			if(obj == null)
			{
				this.RequestCard("RequestCard",m,deckLock);
				mode = true;
				break;
			}
			else
			{
				stageColor = stageColor + (1.0f / 53.0f);
			}
			m++;
		}
		if(mode == true)
		{
			deckLock = false;
			//-------------UI表示--------------------
			this.writeText("Deck作成中",main_text);
			//---------------------------------------
		}
		else
		{
			deckLock = true;
			Debug.Log("デッキ構成");
			this.writeText("Deck作成完了",main_text);
			GameMode = 2;
			Debug.Log("ゲームモードを[開始]に変更");

		}
		this.changeObjectColor("Stage",new Color(stageColor,stageColor,stageColor, 1f));
  		Debug.Log("デッキの確認を完了しました");
	}
	/**
	* 与えられた情報からカードの更新を行う；
	**/
	public GameObject updateCard(string message)
	{
		GameObject obj_save = null;
		string[] makeDeck = message.Split('.');
		Debug.Log("マーク["+makeDeck[0]+"]ナンバー["+makeDeck[1]+"]を"+makeDeck[2]+"に変更");

		foreach(GameObject obj in deck)
		{
			Card info = obj.GetComponent<Card>();
			if(info.Mark == int.Parse(makeDeck[0]))
			{
				if(info.Number == int.Parse(makeDeck[1]))
				{
					info.CardMode = makeDeck[2];
					obj_save = obj;
				}
			}
		}
		reset();
		return obj_save;
	}
	//--------------------------OSCに通信を送るための準備---------------
	/**
	* system的メッセージを送るメゾット(OSC)
	**/
	public void sendSystem(string sys)
	{
		Debug.Log("startGame");
		string message = MyPC + "/" + sys + "/";
		OSCController oscComponent = OSC.GetComponent<OSCController> ();
		oscComponent.oscSendMessege(message);
	}
	/**
	*　デッキの作成のためのメゾット(OSC)
	**/
	public void sendDeck(string sys,GameObject[] deck)
	{
		Debug.Log("sendDeck");
		int i = 0;string message;
		OSCController oscComponent = OSC.GetComponent<OSCController> ();
		foreach(GameObject obj in deck)
		{
			message = MyPC + "/" + sys + "/";
			Card info = obj.GetComponent<Card>();
			message = message + i + "." + info.Mark + "." + info.Number + "." ;
			oscComponent.oscSendMessege(message);
			i++;
		}
		this.sendSystem("checkDeck");
	}
	/**
	*　カードの作成のためのメゾット
	**/
	public void sendCard(string sys,GameObject card,int number)
	{
		Debug.Log("sendCard");
		OSCController oscComponent = OSC.GetComponent<OSCController> ();
		string message = MyPC + "/" + sys + "/";
		Card info = card.GetComponent<Card>();
		message = message + number + "." + info.Mark + "." + info.Number + "." ;
		oscComponent.oscSendMessege(message);
	}
	/**
	* デッキの配列の順番からカードの情報をリクエストするためのメゾット	
	**/
	public void RequestCard(string sys,int i,bool deckLock)
	{
		OSCController oscComponent = OSC.GetComponent<OSCController> ();
		if(deckLock == false)
		{
			Debug.Log("RequestCard");
			string message = MyPC + "/" + sys + "/" + i + "/";
			oscComponent.oscSendMessege(message);		
		}
	}
	/**
	* デッキのカードの更新を行うためのメゾット
	**/
	public void updateCard(string sys,GameObject card)
	{
		Debug.Log("updateCard");
		OSCController oscComponent = OSC.GetComponent<OSCController> ();
		Card info = card.GetComponent<Card>();	
		string message = MyPC + "/" + sys + "/";
		message = message + info.Mark + "." + info.Number + "." + info.CardMode + ".";
		oscComponent.oscSendMessege(message);		
	}
	/**
	* OSCデバック用のメゾット
	**/
	public void sendMessage(string sys,string messages)
	{
		Debug.Log("send");
		OSCController oscComponent = OSC.GetComponent<OSCController> ();
		string message = MyPC + "/"+sys+"/"+ messages; 
		oscComponent.oscSendMessege(message);		
	}
}
