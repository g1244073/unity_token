using UnityEngine;
using System.Collections;

public class CreatDeck:MonoBehaviour 
{
	public GameObject Card;

	private int[] idTable;
	private GameObject[] deck;

	private static Vector3 StageA = new Vector3(10.0f, 0.1f, 0.0f);
	private static Vector3 StageB = new Vector3(-10.0f, 0.1f, 0.0f);
	private static Vector3 StageOUT = new Vector3(-60.0f, 0.1f, 0.0f);

	private bool Card_Card;
	const string TEXNAME_CRAD = "Textures/";

    const string TEXNAME_JOKER = "Textures/x01";
    const string TEXNAME_REVERED = "Textures/Z01";

    private Texture2D[] Textures_c;
    private Texture2D[] Textures_d;
    private Texture2D[] Textures_h;
    private Texture2D[] Textures_s;

 	private Texture2D TEX_JOKER;

	public GameObject[] creatDeck()
	{		
		//カードのリソースを読み込む
		setTex();
		
		idTable = new int[53];
		deck = new GameObject[53];

		initDeck();

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
			cardobj = (GameObject)Instantiate(Card);
			info = cardobj.GetComponent<Card>();
			switch(idTable[count])	
			{	
				case 1:
					info.Number = 1;
					info.Mark = 1;
					info.cardTex = Textures_c[1];
					break;
				case 2:
					info.Number = 2;
					info.Mark = 1;
					info.cardTex = Textures_c[2];
					break;
				case 3:
					info.Number = 3;
					info.Mark = 1;			
					info.cardTex = Textures_c[3];
					break;
				case 4:
					info.Number = 4;
					info.Mark = 1;
					info.cardTex = Textures_c[4];
					break;
				case 5:
					info.Number = 5;
					info.Mark = 1;			
					info.cardTex = Textures_c[5];	
					break;
				case 6:
					info.Number = 6;
					info.Mark = 1;			
					info.cardTex = Textures_c[6];	
					break;
				case 7:
					info.Number = 7;
					info.Mark = 1;
					info.cardTex = Textures_c[7];
					break;
				case 8:
					info.Number = 8;
					info.Mark = 1;
					info.cardTex = Textures_c[8];
					break;
				case 9:
					info.Number = 9;
					info.Mark = 1;
					info.cardTex = Textures_c[9];
					break;
				case 10:
					info.Number = 10;
					info.Mark = 1;
					info.cardTex = Textures_c[10];
					break;
				case 11:
					info.Number = 11;
					info.Mark = 1;
					info.cardTex = Textures_c[11];
					break;
				case 12:
					info.Number = 12;
					info.Mark = 1;
					info.cardTex = Textures_c[12];
					break;
				case 13:
					info.Number = 13;
					info.Mark = 1;
					info.cardTex = Textures_c[13];
					break;
				case 14:
					info.Number = 1;
					info.Mark = 2;
					info.cardTex = Textures_d[1];
					break;
				case 15:
					info.Number = 2;
					info.Mark = 2;
					info.cardTex = Textures_d[2];
					break;
				case 16:
					info.Number = 3;
					info.Mark = 2;
					info.cardTex = Textures_d[3];
					break;
				case 17:
					info.Number = 4;
					info.Mark = 2;
					info.cardTex = Textures_d[4];
					break;
				case 18:
					info.Number = 5;
					info.Mark = 2;
					info.cardTex = Textures_d[5];
					break;
				case 19:
					info.Number = 6;
					info.Mark = 2;
					info.cardTex = Textures_d[6];
					break;
				case 20:
					info.Number = 7;
					info.Mark = 2;
					info.cardTex = Textures_d[7];
					break;
				case 21:
					info.Number = 8;
					info.Mark = 2;
					info.cardTex = Textures_d[8];
					break;
				case 22:
					info.Number = 9;
					info.Mark = 2;
					info.cardTex = Textures_d[9];
					break;
				case 23:
					info.Number = 10;
					info.Mark = 2;
					info.cardTex = Textures_d[10];
					break;
				case 24:
					info.Number = 11;
					info.Mark = 2;
					info.cardTex = Textures_d[11];
					break;
				case 25:
					info.Number = 12;
					info.Mark = 2;
					info.cardTex = Textures_d[12];
					break;
				case 26:
					info.Number = 13;
					info.Mark = 2;
					info.cardTex = Textures_d[13];
					break;
				case 27:
					info.Number = 1;
					info.Mark = 3;
					info.cardTex = Textures_h[1];
					break;
				case 28:
					info.Number = 2;
					info.Mark = 3;
					info.cardTex = Textures_h[2];
					break;
				case 29:
					info.Number = 3;
					info.Mark = 3;
					info.cardTex = Textures_h[3];
					break;
				case 30:
					info.Number = 4;
					info.Mark = 3;
					info.cardTex = Textures_h[4];
					break;
				case 31:
					info.Number = 5;
					info.Mark = 3;
					info.cardTex = Textures_h[5];
					break;
				case 32:
					info.Number = 6;
					info.Mark = 3;
					info.cardTex = Textures_h[6];
					break;
				case 33:
					info.Number = 7;
					info.Mark = 3;
					info.cardTex = Textures_h[7];
					break;
				case 34:
					info.Number = 8;
					info.Mark = 3;
					info.cardTex = Textures_h[8];
					break;
				case 35:
					info.Number = 9;
					info.Mark = 3;
					info.cardTex = Textures_h[9];
					break;
				case 36:
					info.Number = 10;
					info.Mark = 3;
					info.cardTex = Textures_h[10];
					break;
				case 37:
					info.Number = 11;
					info.Mark = 3;
					info.cardTex = Textures_h[11];
					break;
				case 38:
					info.Number = 12;
					info.Mark = 3;
					info.cardTex = Textures_h[12];
					break;
				case 39:
					info.Number = 13;
					info.Mark = 3;
					info.cardTex = Textures_h[13];
					break;
				case 40:
					info.Number = 1;
					info.Mark = 4;
					info.cardTex = Textures_s[1];
					break;
				case 41:
					info.Number = 2;
					info.Mark = 4;
					info.cardTex = Textures_s[2];
					break;
				case 42:
					info.Number = 3;
					info.Mark = 4;
					info.cardTex = Textures_s[3];
					break;
				case 43:
					info.Number = 4;
					info.Mark = 4;
					info.cardTex = Textures_s[4];
					break;
				case 44:
					info.Number = 5;
					info.Mark = 4;
					info.cardTex = Textures_s[5];
					break;
				case 45:
					info.Number = 6;
					info.Mark = 4;
					info.cardTex = Textures_s[6];
					break;
				case 46:
					info.Number = 7;
					info.Mark = 4;
					info.cardTex = Textures_s[7];
					break;
				case 47:
					info.Number = 8;
					info.Mark = 4;
					info.cardTex = Textures_s[8];
					break;
				case 48:
					info.Number = 9;
					info.Mark = 4;
					info.cardTex = Textures_s[9];
					break;
				case 49:
					info.Number = 10;
					info.Mark = 4;
					info.cardTex = Textures_s[10];
					break;
				case 50:
					info.Number = 11;
					info.Mark = 4;
					info.cardTex = Textures_s[11];
					break;
				case 51:
					info.Number = 12;
					info.Mark = 4;
					info.cardTex = Textures_s[12];
					break;
				case 52:
					info.Number = 13;
					info.Mark = 4;
					info.cardTex = Textures_s[13];
					break;
				case 53:
					info.Number = 0;
					info.Mark = 0;
					info.cardTex = TEX_JOKER;;
					break;
			}
			info.CardMode = "DECK";
			cardobj.transform.position = new Vector3(0.0f,(float)(count*0.1),0.0f);
			cardobj.renderer.material.mainTexture = info.cardTex;
			deck[count] = cardobj;
			count++;
		}
		return deck;
	}

	public void initDeck()
	{
		for(int i=0;i<53;i++)
		{
			idTable[i] = 0;
			deck[i] = null;
		}
	}

	public void SetDeck(int player_number,string player_name)
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
					if(player_name == info.CardMode)
					{
						info.reverCard = true;

					}
					else
					{
						info.reverCard = false;
					}
				}
				else 
				{
					info.CardMode = "B";
					if(player_name == info.CardMode)
					{
						info.reverCard = true;
					}
					else
					{
						info.reverCard = false;
					}
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
	}
	public void setCard(GameObject card)
	{
		Card info = card.GetComponent<Card>();
		if(info.CardMode == "A")
		{
			info.SetCard(card,StageA);
			info.CardMode = "OUT";
		}
		else if(info.CardMode == "B")
		{
			info.SetCard(card,StageB);
			info.CardMode = "OUT";
		}
		else if(info.CardMode == "OUT")
		{
			info.SetCard(card,StageOUT);
		}
	}
	public void setTex()
	{
		Textures_c = new Texture2D[14];
		Textures_d = new Texture2D[14];
		Textures_h = new Texture2D[14];
		Textures_s = new Texture2D[14];
		for(int i = 1;i<=13;i++)
		{
			Textures_c[i] = Resources.Load(TEXNAME_CRAD+"c"+i) as Texture2D;
			Textures_d[i] = Resources.Load(TEXNAME_CRAD+"d"+i) as Texture2D;
			Textures_h[i] = Resources.Load(TEXNAME_CRAD+"h"+i) as Texture2D;
			Textures_s[i] = Resources.Load(TEXNAME_CRAD+"s"+i) as Texture2D;
			if(Textures_c[i] == null)
			{
				Debug.Log("CARDTEX:ERROR[c]");
				break;
			}
			else if(Textures_d[i] == null)
			{
				Debug.Log("CARDTEX:ERROR[d]");
				break;
			}
			else if(Textures_h[i] == null)
			{
				Debug.Log("CARDTEX:ERROR[h]");
				break;
			}
			else if(Textures_s[i] == null)
			{
				Debug.Log("CARDTEX:ERROR[s]");
				break;
			}
		}
		TEX_JOKER = Resources.Load(TEXNAME_JOKER) as Texture2D;
	}

	public GameObject makeCard(int mark,int number)
	{
		GameObject cardobj = (GameObject)Instantiate(Card);
		Card info = cardobj.GetComponent<Card>();
		info.Mark = mark;
		info.Number = number;

		return cardobj;
	}
}
