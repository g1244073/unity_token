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
	// Use this for initialization
	void Start () 
	
	{
		Debug.Log("PvPババ抜きの準備開始");

		//-----------------OSCの環境を構築----------------
		OSC = (GameObject)Instantiate(OSC);
		oscController = OSC.GetComponent<OSCController> ();
		oscController.setOSC(TargetAddr,OutGoingPort,InComingPort,MyPC,OutPC);
		//----------------------------------------------
		creatDeck = DeckController.GetComponent<CreatDeck> ();

		deck = new GameObject[53];
	}
	// Update is called once per frame
	void Update () 
	{

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

				if(hit.collider.gameObject.tag == "Card")
				{
					Debug.Log("card認識");
					
					cardInfo = hit.collider.gameObject;
					//Card info = cardInfo.GetComponent<Card>();

					oscController.sendMessage("send",cardInfo,OutPC);
				}
				else if(hit.collider.gameObject.tag == "make")
				{

				}
				else if(hit.collider.gameObject.tag == "Restart")
				{					
					GameObject[] objs = GameObject.FindGameObjectsWithTag("Card");
					foreach(GameObject obj in objs)
					{
						Destroy(obj);
					}
					CheckClear();

					Start();
				}
				else if(hit.collider.gameObject.tag == "send")
				{					
					oscController.makeClient();
				}
				else if(hit.collider.gameObject.tag == "MakeDeck")
				{
					deck = creatDeck.creatDeck();
					oscController.sendDeck("deck",deck);
				}
			}
		}
	}
	private void Reset()
	{
		int i=0,j=0,m=0;
		Card info;

		//ステーズのサイズを獲得
		float StageX = Stage.transform.position.x;
		float StageZ = Stage.transform.position.z;

		foreach(GameObject obj in deck)
		{
			info = obj.GetComponent<Card>();
			Vector3 v = obj.transform.localPosition;
			if(info.CardMode == "A")
			{
				v.x = (float)(5*j+StageX - 50);
				v.y = (float)(5);
				v.z = (float)(StageZ-45);
				j++;
			}
			else if(info.CardMode == "B")
			{
				v.x = (float)(5*m+StageX - 50);
				v.y = (float)(5);
				v.z = (float)(StageZ+45);
				m++;
			}
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
			else if(info.CardMode == "OUT")
			{
				v.x = (float)(-60.0f);
				v.y = (float)(0.1f);
				v.z = (float)(0.0f);
			}
		info.MoveCard(obj,v,info.CardMode);
		i++;
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
			if(info.CardMode == "A")
			{
				CheckCard(obj);
			}
			else if(info.CardMode == "B")
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
						creatDeck = DeckController.GetComponent<CreatDeck> ();
						creatDeck.setCard(obj);
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
				Debug.Log("messageData["+Data+"]");
  			}

  			Debug.Log("ユーザ["+messageData[0]+"]による処理を行います");
  			Debug.Log("処理モード["+messageData[1].ToString()+"]");

  			if(messageData[1].ToString() == "deck")
  			{
  				Debug.Log("deck該当");
  				MatchDeck(messageData[2].ToString());
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
  			else if(messageData[1].ToString() == "CheckDeck")
  			{
				if(this.CheckDeck() == true)
				{
					Debug.Log("Deck同期完了");
				}
				else if(this.CheckDeck() == false)
				{
					oscController.RequestDeck("RequestDeck");
				}
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
			deck[int.Parse(makeDeck[0])] = creatDeck.makeCard(int.Parse(makeDeck[1]),int.Parse(makeDeck[2]));
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
		foreach(GameObject obj in deck)
		{
			if(obj == null)
			{
				return false;
			}
		}
		return true;
	}	
}
