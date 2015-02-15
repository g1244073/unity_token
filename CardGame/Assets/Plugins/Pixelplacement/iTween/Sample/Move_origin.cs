using UnityEngine;
using System.Collections;

public class Move_origin : MonoBehaviour
 {
 	private Hashtable table = new Hashtable();		// ハッシュテーブルを用意
	// Use this for initialization
	void Start () 
	{
		table.Add("x",10);
		table.Add("y",20);
		table.Add("time",3.0f);
		table.Add("delay",1.0f);
		iTween.MoveBy(gameObject,table);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
