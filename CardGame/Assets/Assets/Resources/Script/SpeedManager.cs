using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SpeedManager : MonoBehaviour 
{

	public GameObject Stage;

	public GameObject Card;

	private int GameMode = 0;

	private int[] idTable;

	private int[] TableA;
	private int[] TableB;

	private GameObject[] deck;

	private int stageCard;

	private int[] saveCard = new int[2];

	private bool playerA = true;
	private bool playerB = true;

    public Canvas canvas;

    private int pointA=0;
    private int pointB=0;

	// Use this for initialization
	void Start () 
	{

		idTable = new int[53];
		deck = new GameObject[53];

		Debug.Log("Speedもどきの準備開始");

		initScore();

		
			

		//初期化
		for(int i=0;i<53;i++)
		{
			idTable[i] = 0;
		}


		//カードを一面に配置するためのランダムな配置を決定する
		for(int i=0;i<53;i++)
		{
			int index = Random.Range(0,53);
			//Debug.Log("検索"+index);
			while(idTable[index] != 0)
			{
				index = (index+1)%53;
			}
			idTable[index] = i+1;
		}

		int count = 0;
		//上記で決めた配列からカードを配置する
		while(count < 53)
		{
			GameObject cardobj = null;
			Card info;
			//Debug.Log("count:"+count);
			switch(idTable[count])	
			{	
				case 1:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 1;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 2:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 2;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 3:
					cardobj = (GameObject)Instantiate(Card);
     				info = cardobj.GetComponent<Card>();
					info.Number = 3;
					info.Mark = 1;			
					info.CardMode = "DECK";		
					cardobj.renderer.material.color = Color.red;
					break;
				case 4:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 4;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 5:
  					cardobj = (GameObject)Instantiate(Card);
  					info = cardobj.GetComponent<Card>();
					info.Number = 5;
					info.Mark = 1;			
					info.CardMode = "DECK";		
					cardobj.renderer.material.color = Color.red;
					break;
				case 6:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 6;
					info.Mark = 1;			
					info.CardMode = "DECK";		
					cardobj.renderer.material.color = Color.red;
					break;
				case 7:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 7;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 8:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 8;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 9:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 9;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 10:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 10;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 11:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 11;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 12:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 12;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 13:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 13;
					info.Mark = 1;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.red;
					break;
				case 14:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 1;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 15:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 2;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 16:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 3;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 17:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 4;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 18:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 5;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 19:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 6;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 20:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 7;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 21:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 8;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 22:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 9;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 23:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 10;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 24:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 11;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 25:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 12;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 26:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 13;
					info.Mark = 2;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.blue;
					break;
				case 27:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 1;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 28:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 2;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 29:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 3;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 30:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 4;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 31:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 5;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 32:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 6;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 33:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 7;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 34:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 8;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 35:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 9;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 36:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 10;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 37:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 11;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 38:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 12;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 39:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 13;
					info.Mark = 3;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.green;
					break;
				case 40:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 1;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 41:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 2;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 42:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 3;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 43:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 4;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 44:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 5;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 45:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 6;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 46:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 7;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 47:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 8;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 48:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 9;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 49:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 10;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 50:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 11;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 51:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 12;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 52:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 13;
					info.Mark = 4;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.yellow;
					break;
				case 53:
					cardobj = (GameObject)Instantiate(Card);
					info = cardobj.GetComponent<Card>();
					info.Number = 0;
					info.Mark = 0;
					info.CardMode = "DECK";
					cardobj.renderer.material.color = Color.black;
					break;
			}
			cardobj.transform.position = new Vector3(0.0f,(float)(count*0.1),0.0f);
			deck[count] = cardobj;
			count++;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
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
			Card info;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if(Physics.Raycast(ray,out hit))
			{
				if(hit.collider.gameObject.tag == "Card")
				{
					Debug.Log("card認識");
					if(GameMode == 0)
					{
						Set();
						GameMode = 1;
						Debug.Log("ゲームスタート");
					}

					else if(GameMode == 1)
					{
						if(stageCard == 0 || stageCard == 1)
						{
							cardInfo = hit.collider.gameObject;
							info = cardInfo.GetComponent<Card>();
							//Debug.Log("カード所有者:"+info.CardMode);
							//Debug.Log("カード所有者:"+info.CardMode);

							if(info.CardMode == "A" && playerA == true)
							{
								Vector3 v = cardInfo.transform.localPosition;
								v.x = (float)(10.0f);
								v.y = (float)(0.1f);
								v.z = (float)(0.0f);
								cardInfo.transform.localPosition = v;
								stageCard++;
								playerA = false;
								info.CardMode = "OUT";
								saveCard[0] = info.Mark;
								//Debug.Log("saveCardA"+saveCard[0]);
								/*
								if(saveCard[0] != null)
								{
									Debug.Log("保存を確認");
								}
								*/
								Debug.Log("A:更新");
							}
							else if(info.CardMode == "B" && playerB == true)
							{
								Vector3 v = cardInfo.transform.localPosition;
								v.x = -(float)(10.0f);
								v.y = (float)(0.1f);
								v.z = (float)(0.0f);
								cardInfo.transform.localPosition = v;
								stageCard++;
								playerB = false;
								info.CardMode = "OUT";
								saveCard[1] = info.Mark;
								//Debug.Log("saveCardB"+saveCard[1]);
								Debug.Log("B:更新");

								//Debug.Log("Bの移動を確認");
							}
						}
						if(stageCard == 2)
						{
							Debug.Log("saveCardA"+saveCard[0]);
							Debug.Log("saveCardB"+saveCard[1]);

							CheckGame(saveCard[0],saveCard[1]);

							saveCard[0] = 0;
							saveCard[1] = 0;

							stageCard = 0;
							playerA = true;
							playerB = true;
							Reset();
							//Debug.Log("ステージ開放");
							CheckGame();
						}
					}
					UpdateScore();
				}
				else if(hit.collider.gameObject.tag == "Restart")
				{
					GameMode = 0;
					stageCard = 0;
					playerA = true;
					playerB = true;
					GameObject[] objs = GameObject.FindGameObjectsWithTag("Card");
					foreach(GameObject obj in objs)
					{
						Destroy(obj);
					}
					CheckClear();

					Start();
				}
			}
		}
	}

	private void Set()
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
			if(i % 2 == 0)
			{
				info.CardMode = "A";
				v.x = (float)(5*j+StageX - 50);
				v.y = (float)(0.1*j);
				v.z = (float)(StageZ-45);
				j++;
			}
			else
			{
				info.CardMode = "B";
				v.x = (float)(5*m+StageX - 50);
				v.y = (float)(0.1*m);
				v.z = (float)(StageZ+45);
				m++;
			}
			obj.transform.localPosition = v;
			i++;
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
				v.y = (float)(0.1*j);
				v.z = (float)(StageZ-45);
				j++;
			}
			else if(info.CardMode == "B")
			{
				v.x = (float)(5*m+StageX - 50);
				v.y = (float)(0.1*m);
				v.z = (float)(StageZ+45);
				m++;
			}
			else if(info.CardMode == "OUT")
			{
				v.x = (float)(-60.0f);
				v.y = (float)(0.1f);
				v.z = (float)(0.0f);
			}
			obj.transform.localPosition = v;
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

	private void CheckGame(int playerA,int playerB)
	{
		if(playerA == playerB)
		{
			Debug.Log("引き分け");
		}
		else if(playerA == 0)
		{
			Debug.Log("Aの勝ち");
			pointA++;
		}
		else if(playerB == 0)
		{
			Debug.Log("Bの勝ち");
			pointB++;
		}
		else if(playerA == 1 && playerB == 2)
		{
			Debug.Log("Aの勝ち");
			pointA++;
		}
		else if(playerA == 2 && playerB == 3)
		{
			Debug.Log("Aの勝ち");
			pointA++;
		}
		else if(playerA == 3 && playerB == 4)
		{
			Debug.Log("Aの勝ち");
			pointA++;
		}
		else if(playerA == 4 && playerB == 1)
		{
			Debug.Log("Aの勝ち");
			pointA++;
		}
		else if(playerB == 1 && playerA == 2)
		{
			Debug.Log("Bの勝ち");
			pointB++;
		}
		else if(playerB == 2 && playerA == 3)
		{
			Debug.Log("Bの勝ち");
			pointB++;
		}
		else if(playerB == 3 && playerA == 4)
		{
			Debug.Log("Bの勝ち");
			pointB++;
		}
		else if(playerB == 4 && playerA == 1)
		{
			Debug.Log("Bの勝ち");
			pointB++;
		}
		else
		{
			Debug.Log("無効勝負");
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

	private void initScore()
	{
		Text target = null;
		pointA = 0;
		pointB = 0;
        foreach (Transform child in canvas.transform){
            if(child.name == "ScoreA"){
                target = child.gameObject.GetComponent<Text>();
                target.text = "0";
            } else if (child.name == "ScoreB") {
                target = child.gameObject.GetComponent<Text>();
                target.text = "0";
            }
        }
	}

	private void UpdateScore()
	{
		Text target = null;
        foreach (Transform child in canvas.transform)
        {
            if(child.name == "ScoreA"){
                target = child.gameObject.GetComponent<Text>();
                target.text = pointA.ToString();
            } 
            else if (child.name == "ScoreB") {
                target = child.gameObject.GetComponent<Text>();
                target.text = pointB.ToString();
            }
        }
	}
}
