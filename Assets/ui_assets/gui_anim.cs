using UnityEngine;

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class gui_anim : MonoBehaviour {


	public float ro = 0.1f;
	private bool rr = false;

	void Start() {
		

	}

	void Update() {
		if (rr) { transform.Rotate( new Vector3( 0, 0, ro ) );}

	}
		



	public void into()
	{
		transform.localScale = new Vector3(1f,1f,1f)+ new Vector3(0.15f,0.15f,0.15f);
		Debug.Log("aaa!");
	}

	public void outo()
	{
		transform.localScale = new Vector3(1f,1f,1f);
		Debug.Log("ooo!");
	}

	public void into_r()
	{
		rr= true;
		
	}
	
	public void outo_r()
	{
		rr= false;
		
	}

	public void exitApp()
	{
		Application.Quit();
	}


	
		
}
