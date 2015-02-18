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

    public Text helper;

    private float stageColor = 0;

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
		/*
		GameSystem(message);
		//マウスによる操作
		if(Input.GetMouseButtonDown(0))
		{

			GameObject cardInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit))
			{

				if(((hit.collider.gameObject.tag == "Card") ||(hit.collider.gameObject.tag == "ReveredCard")) && GameMode == 1)
				{
					//------------UI表示----------
					writeText(null,helper);
					//---------------------------

					Debug.Log("card認識");
					cardInfo = hit.collider.gameObject;
					SetDeck(player_number,deck);
					decklock = true;
					//oscController.sendSystem("startGame");
					waitTime(1000);
					StartGame();
					//oscController.sendSystem("ResetGame");
					GameMode = 2;
					Debug.Log("ゲームモードを[対戦]に変更");
				}
				else if(hit.collider.gameObject.tag == "ReveredCard" && GameMode == 2)
				{
					Debug.Log("Reveredcard認識");
					GameObject  parent =  hit.collider.transform.parent.gameObject;

					if(myturn == true)
					{
						Card info = parent.GetComponent<Card>();
						Debug.Log("カードの持ち主["+info.CardMode+"]");
						if(info.CardMode == OutPC)
						{
							Debug.Log("自分の処理を実行します");
							info.CardMode = MyPC;
							Reset();
							waitTime(1000);
							//oscController.exchange("exchange",parent);
							myturn = false;
						}
					}
				}
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
				//相手のoscサーバに対してクライアントを作成する	
				
				else if(hit.collider.gameObject.tag == "send" && GameMode == 0)
				{
					cardInfo = hit.collider.gameObject;
					//oscController.makeClient();
					cardInfo.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
					//-----------------UIの表示---------------
					writeText("GameMode["+GameMode+"]",main);
					writeText("○",osc);
					writeText("↑",create);
					//----------------------------------------
				}
				
				//デッキをランダムに作成する
				
				else if(hit.collider.gameObject.tag == "MakeDeck" && GameMode == 0)
				{
					cardInfo = hit.collider.gameObject;
					//deck = creatDeck.creatDeck();
					decklock = true;
					//oscController.sendDeck("deck",deck);
					GameMode = 1;
					cardInfo.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);;
					Debug.Log("ゲームモードを[開始]に変更");
					//-----------------UIの表示---------------
					writeText("Deck作成完了",main);
					writeText("○",osc);
					writeText("○",create);
					writeText("↓",helper);
					//----------------------------------------
				}
			}
		}

		if(GameMode == 2 && myturn == false)
		{
			Debug.Log("相手の処理を待っています");
			writeText("RIVALTURN",main);
		}
		else if(GameMode == 2 && myturn == true)
		{
			Debug.Log("自分の処理を待っています");
			writeText("YOURTURN",main);
		}
		*/
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

		if((countA == 0 || countB == 0) && gameComponent.GameMode == 2)
		{
			gameComponent.GameMode = 3;
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
		/*
		Card info;

		//--------------------UI表示--------------
		writeText(null,helper);
		writeText("カード配布&整理中",main);
		//----------------------------------------
		foreach(GameObject obj in deck)
		{
			info = obj.GetComponent<Card>();
			//Debug.Log("カードチェック"+info.CardMode);
			if(info.CardMode == MyPC)
			{
				CheckCard(obj);
			}
		}
		//--------------------UI表示-------------
		writeText("ゲームスタート",main);
		//----------------------------------------
		*/
	}

	private void CheckCard(GameObject obj_search)
	{
		/*
		Card info;
		Card info_to = obj_search.GetComponent<Card>();
		foreach(GameObject obj in deck)
		{
			info = obj.GetComponent<Card>();
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
						//oscController.updateCard("updateCard",obj_search);
						waitTime(100);
						//oscController.updateCard("updateCard",obj);
						waitTime(100);
						Reset();
						break;
					}
				}
			}
		}
		*/
	}
	private void ExchangeCard(GameObject obj,string player)
	{
		/*
		Card info = obj.GetComponent<Card>();
		info.CardMode = player;
		Debug.Log("交換を実行");
		*/
	}

	private void GameSystem(string message)
	{	
		/*
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
	  			else if(messageData[1].ToString() == "startGame")
	  			{
	  				Debug.Log("startGame該当");
	  				SetDeck(player_number,deck);
	  				GameMode = 2;
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
	  				GameObject obj = updateCard(messageData[2].ToString());
	  				CheckCard(obj);
	  				Reset();
	  			}
			}
  		}
  		*/
	}
	private void RequestDeck()
	{
		/*
		Debug.Log("デッキの確認[null]を行います");
		//int i = 0;
		while(decklock == false)
		{
			foreach(GameObject obj in deck)
			{
				if(obj == null)
				{
					//oscController.RequestCard("RequestCard",i,decklock);
				}
			}
		}
		*/
	}
	/*
	private bool CheckDeck_mode()
	{
		Debug.Log("デッキの確認を行います");
		bool mode = true;
		foreach(GameObject obj in deck)
		{
			Card info = obj.GetComponent<Card>();

			if(info.CardMode == "DECK")
			{
				mode = false;
				break;
			}
		}
		if(mode == false)
		{
			return false;
		}
		return true;
	}
	*/
}
