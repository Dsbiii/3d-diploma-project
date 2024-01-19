using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBack : MonoBehaviour
{
	private float pos0x, pos0y; 

	public Transform back;
	// Start is called before the first frame update
    void Start()
    {
		pos0x = back.position.x;
		pos0y = back.position.y;		
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 mousePos = Input.mousePosition;

		float xx= (Screen.width/2-mousePos.x)/66+ pos0x;
		float yy= (Screen.height/2-mousePos.y)/66+ pos0y;

		back.position = new Vector3(xx, yy, transform.position.z);

		//back.position.x = (Screen.width/2-mousePos.x)/100+ back.position.x;
		//back.position.y = (Screen.height/2-mousePos.y)/100+ back.position.y;
		//Debug.Log( Screen.width/2-mousePos.x +' :: '+ Screen.height/2-mousePos.y );


	}
}
