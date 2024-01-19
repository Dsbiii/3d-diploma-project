using UnityEngine;
using System.Collections;

public class button : MonoBehaviour {

	public string param="click";
	// Use this for initialization

	public string func="";
	public string funcParam="";

	public GameObject obj;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		if (param == "click") {
			
			obj.SendMessage(func,funcParam);
		}
	}

	void OnMouseEnter()
	{
	 if (param == "enter") {
		
			obj.SendMessage(func,funcParam);
		}
	}
}
