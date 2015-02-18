using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BabaManager : MonoBehaviour 
{
	private GameObject Manager;

	//通信情報を保持するオブジェクト
	private GameObject OSC;
	//デッキの生成に関するオブジェクト
	private GameObject DeckController;

	/**
	* ゲームのコンポーネントを所得します	
	**/
	private GameManager gameComponent;

	public int player_number = 2;

	private bool CheckDeck_mode = false;

	private bool myturn = false;


	public void setGame (GameObject oscObject,GameObject deckObject,GameObject managerObject) 
	
	{
		Debug.Log("PvPババ抜きの準備開始");
		Debug.Log("ゲームモードを[準備]に変更");

		//-----------------必要な情報を記録-----------------
		this.OSC = oscObject;
		this.DeckController = deckObject;
		this.Manager = managerObject;
		//-----------------------------------------------
		//ステージの色を黒にする
		gameComponent = Manager.GetComponent<GameManager> ();
        gameComponent.changeObjectColor("Stage",Color.black);
		//"今回はAを先攻とする"
		if(gameComponent.MyPC == "A")
		{
			myturn = true;
		}
	}
	public void updateGame (string message) 
	{
		GameSystem(message);
		//マウスによる操作
		if(Input.GetMouseButtonDown(0))
		{
			GameObject cardInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit))
			{
				if(((hit.collider.gameObject.tag == "Card") ||(hit.collider.gameObject.tag == "ReveredCard")) && gameComponent.GameMode == 2)
				{
					Debug.Log("card認識");
					cardInfo = hit.collider.gameObject;
					gameComponent.setDeck(player_number,gameComponent.deck);
					gameComponent.waitTime(100);
					gameComponent.sendSystem("startGame");
					StartGame();
					gameComponent.waitTime(100);
					gameComponent.sendSystem("ResetGame");
					gameComponent.GameMode = 3;
					Debug.Log("ゲームモードを[対戦]に変更");
				}
				else if(hit.collider.gameObject.tag == "ReveredCard" && gameComponent.GameMode == 3)
				{
					Debug.Log("Reveredcard認識");
					GameObject  parent =  hit.collider.transform.parent.gameObject;
					if(myturn == true)
					{
						Card info = parent.GetComponent<Card>();
						Debug.Log("カードの持ち主["+info.CardMode+"]");
						if(info.CardMode == gameComponent.OutPC)
						{
							Debug.Log("自分の処理を実行します");
							info.CardMode = gameComponent.MyPC;
							gameComponent.reset();
							this.exchange("exchange",parent);
							myturn = false;
						}
					}
				}
				/*
				else if(hit.collider.gameObject.tag == "Restart" && (GameMode == 1 || GameMode == 2))
				{					
					GameObject[] objs = GameObject.FindGameObjectsWithTag("Card");
					foreach(GameObject obj in objs)
					{
						Destroy(obj);
					}
					CheckClear();
					//Start();
				}
				*/
			}
		}

		if(gameComponent.GameMode == 3 && myturn == false)
		{
			Debug.Log("相手の処理を待っています");
			gameComponent.writeText("RIVALTURN",gameComponent.main_text);
		}
		else if(gameComponent.GameMode == 3 && myturn == true)
		{
			Debug.Log("自分の処理を待っています");
			gameComponent.writeText("YOURTURN",gameComponent.main_text);
		}
	}
	public void checkGame()
	{
		//------------------------デッキの情報を取得してきます-----------------------
		int countA=0,countB=0;
		int countOUT=0;
		int countDECK = 0;
		foreach(GameObject obj in gameComponent.deck)
		{
			Card info = obj.GetComponent<Card>();
			//Debug.Log("カードチェック"+info.CardMode);
			if(info.CardMode == "A")
			{
				countA++;
			}
			else if(info.CardMode == "B")
			{
				countB++;
			}
			else if(info.CardMode == "DECK")
			{
				countDECK++;
			}
			else if(info.CardMode == "OUT")
			{
				countOUT++;
			}
		}
		Debug.Log("現在のステージの状態,[A:"+countA+"][B:"+countB+"][DECK:"+countDECK+"][OUT:"+countOUT+"]");

		if((countA == 0 || countB == 0) && gameComponent.GameMode == 3)
		{
			gameComponent.GameMode = 4;
			if(countA == 0)
			{
				gameComponent.writeText("FINISH[A]",gameComponent.main_text);
			}
			else
			{
				gameComponent.writeText("FINISH[B]",gameComponent.main_text);
			}
			Destroy(this);
		}
	}
	private void StartGame()
	{
		//--------------------UI表示--------------
		gameComponent.writeText("カード配布&整理中",gameComponent.main_text);
		//----------------------------------------
		foreach(GameObject obj in gameComponent.deck)
		{
			Card info = obj.GetComponent<Card>();
			if(info.CardMode == gameComponent.MyPC)
			{
				CheckCard(obj);
			}
		}
		//--------------------UI表示-------------
		gameComponent.writeText("ゲームスタート",gameComponent.main_text);
		//----------------------------------------
	}

	private void CheckCard(GameObject obj_search)
	{
		Card info_to = obj_search.GetComponent<Card>();
		foreach(GameObject obj in gameComponent.deck)
		{
			Card info = obj.GetComponent<Card>();
			if(info.CardMode == info_to.CardMode)
			{
				if(info.Number == info_to.Number)
				{
					if(info.Mark == info_to.Mark && info.Number == info_to.Number)
					{
						Debug.Log("同じ");
					}
					else
					{
						//Debug.Log("検索元"+info.Mark+":"+info.Number);
						//Debug.Log("検索対象"+info_to.Mark+":"+info_to.Number);
						info.CardMode = "OUT";
						info_to.CardMode = "OUT";
						gameComponent.waitTime(100);
						gameComponent.updateCard("updateCard",obj_search);
						gameComponent.waitTime(100);
						gameComponent.updateCard("updateCard",obj);
						gameComponent.reset();
						break;
					}
				}
			}
		}
	}
	private void GameSystem(string message)
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
	  			//デッキを定めたルールで配布する
	  			if(messageData[1].ToString() == "startGame")
	  			{
	  				Debug.Log("startGame該当");
	  				gameComponent.setDeck(player_number,gameComponent.deck);
	  				gameComponent.GameMode = 3;
	  			}
	  			//相手側のカードをリクエストする
	  			else if(messageData[1].ToString() == "ResetGame")
	  			{
	  				Debug.Log("ResetGame該当");
	  				StartGame();
	  				Debug.Log("ゲームモードを[対戦]に変更");
	  			}
	  			//相手側のカードをリクエストする
	  			else if(messageData[1].ToString() == "exchange")
	  			{
	  				Debug.Log("exchange該当");
	  				Debug.Log("相手の入力を確認");
	  				myturn = true;
	  				GameObject obj = gameComponent.updateCard(messageData[2].ToString());
	  				CheckCard(obj);
	  				gameComponent.reset();
	  			}
			}
  		}
	}
	//--------------------------------OSC関連------------------------
	/**
	* 手札のカードの交換を行うためのメゾット
	**/
	private void exchange(string sys,GameObject card)
	{
		Debug.Log("exchange");
		OSCController oscComponent = OSC.GetComponent<OSCController> ();
		Card info = card.GetComponent<Card>();	
		string message = gameComponent.MyPC + "/" + sys + "/";
		message = message + info.Mark + "." + info.Number + "." + info.CardMode + ".";
		oscComponent.oscSendMessege(message);		
	}
}
