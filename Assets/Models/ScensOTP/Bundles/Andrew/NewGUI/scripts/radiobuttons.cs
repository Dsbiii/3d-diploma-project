using UnityEngine;
using System.Collections;

public class radiobuttons : MonoBehaviour {

	public GameObject[] buttons;
	public Texture tex1;
	public Texture tex2;
	public Texture tex3;

	private Renderer rend;

	public bool actived;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.material.mainTexture = tex1;
		
		if (actived==true) 
		{	rend.material.mainTexture = tex3;	}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		reclick ();
	}

	void OnMouseEnter()
	{
		if (actived == false) rend.material.mainTexture = tex3;

	}

	void OnMouseExit()
	{
		if (actived == false) rend.material.mainTexture = tex1;
	}

	void reclick( )
	{

		for (int i=0; i<buttons.Length; i++) {
			buttons [i].SendMessage ("unactiv");
		}	
		rend.material.mainTexture = tex2;
		actived = true;
	}


		void unactiv()
		{
			rend.material.mainTexture = tex1;
			actived = false;
		}

	


}
