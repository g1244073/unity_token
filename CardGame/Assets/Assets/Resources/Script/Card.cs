using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour 
{
	public int Number;
	public int Mark;

	public string CardMode;
	
	public bool reverCard;

	public Texture2D rever;
	public Texture2D cardTex;



	public void MoveCard(GameObject CardObject,Vector3 v2,string player)
	{
		Vector3 v1 = CardObject.transform.localPosition;
		Hashtable table = new Hashtable();	
		table.Add("x",v2.x-v1.x);
		table.Add("y",v2.y-v1.y);
		table.Add("z",v2.z-v1.z);
		table.Add("time",1.0f);
		table.Add("delay",0.1f);

		iTween.MoveBy(CardObject,table);
		waitTime(1.1f);

		table = new Hashtable();	
		v1 = CardObject.transform.localEulerAngles;
		if(player == "A")
		{
			table.Add("x",90-v1.z);
			table.Add("y",0-v1.y);
			table.Add("z",0-v1.z);
			table.Add("time",4.0f);
			table.Add("delay",2.0f);
			iTween.RotateTo(CardObject,table);
			waitTime(6.0f);

		}
		else if(player == "B")
		{
			table.Add("x",-90-v1.z);
			table.Add("y",0-v1.y);
			table.Add("z",0-v1.z);
			table.Add("time",4.0f);
			table.Add("delay",2.0f);
			iTween.RotateTo(CardObject,table);
			waitTime(6.0f);
		}
		else if(player == "OUT")
		{
			table.Add("x",0-v1.z);
			table.Add("y",0-v1.y);
			table.Add("z",0-v1.z);
			table.Add("time",1.0f);
			table.Add("delay",0.1f);
			iTween.RotateTo(CardObject,table);
			waitTime(1.1f);
		}
	}
	public void SetCard(GameObject CardObject,Vector3 v2)
	{
		Hashtable table = new Hashtable();	
		Vector3 v1 = CardObject.transform.localEulerAngles;

		table.Add("x",0-v1.z);
		table.Add("y",0-v1.y);
		table.Add("z",0-v1.z);
		table.Add("time",1.0f);
		table.Add("delay",0.1f);
		iTween.RotateTo(CardObject,table);
		waitTime(5.0f);

		v1 = CardObject.transform.localPosition;
		table = new Hashtable();	
		table.Add("x",v2.x-v1.x);
		table.Add("y",v2.y-v1.y);
		table.Add("z",v2.z-v1.z);
		table.Add("time",1.0f);
		table.Add("delay",2.0f);

		iTween.MoveBy(CardObject,table);
		waitTime(1.1f);
	}
	public IEnumerator waitTime(float time)
	{
 	   yield return new WaitForSeconds(time);
	}
}
