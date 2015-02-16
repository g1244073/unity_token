using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BabaManager : MonoBehaviour 
{
	//ステージ情報を保持するオブジェクト		
	public GameObject Stage;
	//カード情報を保持するオブジェクト
	public GameObject Card;
	//通信情報を保持するオブジェクト
	public GameObject OSC;
	//デッキの生成に関するオブジェクト
	public GameObject DeckController;
	//デッキ情報を保持するオブジェクト	
	private GameObject[] deck;
	//デッキのコンポーネントを保存するオブジェクト	
	public  CreatDeck creatDeck;

	//通信情報を保持するオブジェクト
	public OSCController oscController;

	public int player_number = 2;

	public string TargetAddr;
	public int OutGoingPort;
	public int InComingPort;

	public string MyPC;
	public string OutPC;

    public Canvas canvas;

    public float stageColor = 0;

    public int GameMode = 0;

	public float time;

	// Use this for initialization
	void Start () 
	
	{
		Debug.Log("PvPババ抜きの準備開始");
		Debug.Log("ゲームモードを[準備]に変更");
		Stage.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 1f);;


		//-----------------OSCの環境を構築----------------
		OSC = (GameObject)Instantiate(OSC);
		oscController = OSC.GetComponent<OSCController> ();
		oscController.setOSC(TargetAddr,OutGoingPort,InComingPort,MyPC,OutPC);
		//----------------------------------------------
		creatDeck = DeckController.GetComponent<CreatDeck> ();

		time = 0.0f;

		deck = new GameObject[53];
	}
	// Update is called once per frame
	void Update () 
	{
		time += Time.deltaTime;

		//OSCのアップデート	
		oscController.UpdateOSC();
		string message = oscController.catchMessage();

		GameSystem(message);

		//コマンド操作
		if(Input.GetKeyDown(KeyCode.C))
		{
			Debug.Log("Cの入力を確認");
			GameObject[] objs = GameObject.FindGameObjectsWithTag("Card");
			foreach(GameObject obj in objs)
			{
				Destroy(obj);
			}
			CheckClear();
		}

		if(Input.GetMouseButtonDown(0))
		{

			GameObject cardInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit))
			{

				if(hit.collider.gameObject.tag == "Card" && GameMode == 1)
				{
					Debug.Log("card認識");
					
					cardInfo = hit.collider.gameObject;

					SetDeck(player_number,deck);
					oscController.startGame("startGame");
					StartGame();
					GameMode = 2;
					Debug.Log("ゲームモードを[対戦]に変更");


				}
				else if(hit.collider.gameObject.tag == "Card" && GameMode == 2)
				{

				}
				else if(hit.collider.gameObject.tag == "Restart" && (GameMode == 1 || GameMode == 2))
				{					
					GameObject[] objs = GameObject.FindGameObjectsWithTag("Card");
					foreach(GameObject obj in objs)
					{
						Destroy(obj);
					}
					CheckClear();

					Start();
				}
				else if(hit.collider.gameObject.tag == "send" && GameMode == 0)
				{					
					oscController.makeClient();
				}
				else if(hit.collider.gameObject.tag == "MakeDeck" && GameMode == 0)
				{
					deck = creatDeck.creatDeck();
					oscController.sendDeck("deck",deck);
					GameMode = 1;
					Debug.Log("ゲームモードを[開始]に変更");
				}
			}
		}
	}
	private void Reset()
	{
		Card info;
		//ステーズのサイズを獲得
		Vector3 stage_v  = Stage.transform.localScale;

		int countA=0,countB=0;

		foreach(GameObject obj in deck)
		{
			info = obj.GetComponent<Card>();
			Vector3 v = obj.transform.localPosition;
			//Debug.Log(stage_v.x);

			if(info.CardMode == "A")
			{
				v.x = (float)(-(stage_v.x/2) + 2.5 + countA*4);
				v.y = (float)(5.0f);
				v.z = (float)(-(stage_v.z/2));
				countA++;
			}
			
			else if(info.CardMode == "B")
			{
				v.x = (float)(-(stage_v.x/2) + 2.5 + countB*4);
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
				v.x = (float)(-60.0f);
				v.y = (float)(0.1f);
				v.z = (float)(0.0f);
			}
			StartCoroutine(waitTime(100));

			info.D_MoveCard(obj,v);

		}
	}

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

	private void StartGame()
	{
		Card info;
		foreach(GameObject obj in deck)
		{
			info = obj.GetComponent<Card>();
			//Debug.Log("カードチェック"+info.CardMode);
			if(info.CardMode == MyPC)
			{
				CheckCard(obj);
			}
		}
	}
	private void CheckGame()
	{
		Card info;
		int countA=0,countB=0;
		int countOUT=0;

		foreach(GameObject obj in deck)
		{
			info = obj.GetComponent<Card>();
			//Debug.Log("カードチェック"+info.CardMode);
			if(info.CardMode == "A")
			{
				countA++;
			}
			else if(info.CardMode == "B")
			{
				countB++;
			}
			else if(info.CardMode == "OUT")
			{
				countOUT++;
			}
		}
		Debug.Log("現在のステージの状態,[A:"+countA+"][B:"+countB+"][OUT:"+countOUT+"]");
	}

	private void CheckCard(GameObject obj_search)
	{
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

						Debug.Log("検索元"+info.Mark+":"+info.Number);
						Debug.Log("検索対象"+info_to.Mark+":"+info_to.Number);
						info.CardMode = "OUT";
						oscController.updateCard("updateCard",obj_search);
						Reset();
						break;
					}
				}
			}
		}
	}
	private void ExchangeCard(GameObject obj,string player)
	{
		Card info = obj.GetComponent<Card>();
		info.CardMode = player;
		Debug.Log("交換を実行");
	}

	private void GameSystem(string message)
	{
		if(message != null)
		{
			string[] messageData = message.Split('/');
			foreach (string Data in messageData) 
			{
				//Debug.Log("messageData["+Data+"]");
  			}

  			//Debug.Log("ユーザ["+messageData[0]+"]による処理を行います");
  			//Debug.Log("処理モード["+messageData[1].ToString()+"]");

  			if(messageData[1].ToString() == "deck")
  			{
  				Debug.Log("deck該当");
  				MatchDeck(messageData[2].ToString());
  				if(this.CheckDeck() == true)
				{
					Debug.Log("Deck同期完了");
					oscController.sendMessage("SendMessage","デッキ構築完了");
					GameMode = 1;
					Debug.Log("ゲームモードを[開始]に変更");
				}
				else if(this.CheckDeck() == false)
				{
					oscController.RequestDeck("RequestDeck");
				}
				Stage.renderer.material.color = new Color(stageColor,stageColor,stageColor, 1f);;


  			}
  			else if(messageData[1].ToString() == "send")
  			{  				
  				Debug.Log("send該当");
  				ExchangeCard(messageData[2].ToString());
  			}
  			else if(messageData[1].ToString() == "RequestDeck")
  			{
  				oscController.sendDeck("deck",deck);
  			}
  			else if(messageData[1].ToString() == "startGame")
  			{
  				SetDeck(player_number,deck);
  			}


			else if(messageData[1].ToString() == "SendMessage")
  			{
  				Debug.Log("デッキ構築完了[確認]" + messageData[2].ToString());
			}
  		}
	}
	private void MatchDeck(string message)
	{
		Debug.Log("デッキの同期を行います");
		string[] makeDeck = message.Split('.');
		
		if(deck[int.Parse(makeDeck[0])] == null)
		{
			Debug.Log("新規のカードデータです");
			deck[int.Parse(makeDeck[0])] = creatDeck.makeCard(int.Parse(makeDeck[1]),int.Parse(makeDeck[2]),int.Parse(makeDeck[0]));
		}
		else
		{
			Debug.Log("すでに更新済みです");
		}
	}
	private void ExchangeCard(string message)
	{
		Debug.Log("手札の交換を行います");
	}
	private bool CheckDeck()
	{
		stageColor = 0.0f;
		Debug.Log("デッキの確認を行います");
		bool mode = true;
		foreach(GameObject obj in deck)
		{
			if(obj == null)
			{
				mode = false;
			}
			else
			{
				stageColor = stageColor + (1.0f / 53.0f);
			}
		}
		if(mode == false)
		{
			return false;
		}
		return true;
	}

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


	private void updateCard(string message)
	{
		string[] makeDeck = message.Split('.');
		foreach(GameObject obj in deck)
		{
			Card info = obj.GetComponent<Card>();
			if(info.Mark == int.Parse(makeDeck[0]))
			{
				if(info.Number == int.Parse(makeDeck[1]))
				{
					Debug.Log("マーク["+info.Mark+"]ナンバー["+info.Number+"]を"+makeDeck[2]+"に変更");
					info.CardMode = makeDeck[2];
				}
			}
		}
	}

	private void SetDeck(int player_number,GameObject[] deck)
	{
		Card info;
		int i = 0;

		foreach(GameObject obj in deck)
		{
			info = obj.GetComponent<Card>();
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
		Reset();
	}
	private IEnumerator waitTime(int time)
	{
		Debug.Log("処理中");
  	  yield return new WaitForSeconds ((float)time);
	}

}
