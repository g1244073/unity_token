using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour 
{
	public int Number;
	public int Mark;

	public string CardMode;
	
	public Texture2D cardTex;

	private bool CardMoving = false;

	public void ItweenCard(GameObject CardObject,Vector3 v2)
	{
		ResetRotateCard(CardObject);
		ResetMoveCard(CardObject);

		//RotateCard(CardObject);
		MoveCard(CardObject,v2);
	}
	private void ResetRotateCard(GameObject CardObject)
	{
		Debug.Log("カードの回転を初期化");
		waitMove();
		CardMoving = true;

		Hashtable table = new Hashtable();	
		Vector3 v1 = CardObject.transform.localEulerAngles;

		if(v1.x>=0)
		{
			table.Add("x",0-v1.x);
		}
		else
		{
			table.Add("x",0+v1.x);
		}
		if(v1.y>=0)
		{
			table.Add("y",0-v1.y);
		}
		else
		{
			table.Add("y",0+v1.y);
		}
		if(v1.z>=0)
		{
			table.Add("z",0-v1.z);
		}
		else
		{
			table.Add("z",0+v1.z);
		}

		table.Add("time",1.0f);
		table.Add("delay",1.0f);
		iTween.RotateTo(CardObject,table);
		waitTime(1.0f);

		CardMoving = false;

	}

	private void ResetMoveCard(GameObject CardObject)
	{
		Debug.Log("カードの位置を初期化");

		waitMove();
		CardMoving = true;

		Hashtable table = new Hashtable();	
		Vector3 v1 = CardObject.transform.localPosition;

		if(v1.x>=0)
		{
			table.Add("x",0+v1.x);
		}
		else
		{
			table.Add("x",0-v1.x);
		}
		if(v1.y>=0)
		{
			table.Add("y",0+v1.y);
		}
		else
		{
			table.Add("y",0-v1.y);
		}
		if(v1.z>=0)
		{
			table.Add("z",0+v1.z);
		}
		else
		{
			table.Add("z",0-v1.z);
		}

		table.Add("time",1.0f);
		table.Add("delay",1.0f);
		iTween.MoveBy(CardObject,table);
		waitTime(1.0f);

		CardMoving = false;

	}

	private void RotateCard(GameObject CardObject)
	{
		Debug.Log("カードの回転を実行");

		waitMove();
		CardMoving = true;

		Hashtable table = new Hashtable();	
		Vector3 v1 = CardObject.transform.localEulerAngles;

		Card info = CardObject.GetComponent<Card>();

		if(info.CardMode == "A")
		{
			table.Add("x",v1.x+90);
		}
		else if(info.CardMode == "B")
		{
			table.Add("x",v1.x-90);
		}

		table.Add("y",v1.y);
		table.Add("z",v1.z);

		table.Add("time",5.0f);
		table.Add("delay",5.0f);
		iTween.RotateTo(CardObject,table);
		waitTime(5.0f);

		CardMoving = false;

	}
	private void MoveCard(GameObject CardObject,Vector3 v2)
	{
		Debug.Log("カードの移動を実行");

		waitMove();
		CardMoving = true;

		Hashtable table = new Hashtable();	
		Vector3 v1 = CardObject.transform.localPosition;

		Card info = CardObject.GetComponent<Card>();


		if(v1.x>=v2.x)
		{
			table.Add("x",v1.x-v2.x);
		}
		else
		{
			table.Add("x",v2.x+v1.x);
		}
		if(v1.y>=v2.y)
		{
			table.Add("y",v1.y-v2.y);
		}
		else
		{
			table.Add("y",v2.y+v1.y);
		}
		if(v1.z>=v2.z)
		{
			table.Add("z",v1.z-v2.z);
		}
		else
		{
			table.Add("z",v2.z+v1.z);
		}

		table.Add("time",5.0f);
		table.Add("delay",5.0f);
		iTween.MoveBy(CardObject,table);
		waitTime(5.0f);

		CardMoving = false;
	}

	public IEnumerator waitTime(float time)
	{
 	   yield return new WaitForSeconds(time);
	}
	private void waitMove()
	{
		while(CardMoving == true)
		{
			Debug.Log("カード待機中");
		}
	}

	public void D_MoveCard(GameObject CardObject,Vector3 v)
	{
		ResetD_RotateCard(CardObject);
		ResetD_MoveCard(CardObject);
		MoveD_Rotate(CardObject);
		D_Move(CardObject,v);

	}
	public void ResetD_MoveCard(GameObject CardObject)
	{
		Vector3 v;
		v.x = (float)0.0f;
		v.y = (float)0.0f;
		v.z = (float)0.0f;
		CardObject.transform.position = v; 
	}
	public void ResetD_RotateCard(GameObject CardObject)
	{
		Vector3 v;
		v.x = (float)0.0f;
		v.y = (float)0.0f;
		v.z = (float)0.0f;
		CardObject.transform.localRotation = Quaternion.Euler(v);

	}
	public void MoveD_Rotate(GameObject CardObject)
	{
		Card info = CardObject.GetComponent<Card>();
		Vector3 v;

		if(info.CardMode == "A")
		{
			v.x = (float)-90.0f;
		}
		else if(info.CardMode == "B")
		{
			v.x = (float)90.0f;
		}
		else if(info.CardMode == "OUT")
		{
			v.x = (float)180.0f;
		}
		else
		{
			v.x = (float)0.0f;
		}
		v.y = (float)0.0f;
		v.z = (float)0.0f;
		CardObject.transform.localRotation = Quaternion.Euler(v);
	}
	public void D_Move(GameObject CardObject,Vector3 v)
	{
		CardObject.transform.position = v; 
	}
}
