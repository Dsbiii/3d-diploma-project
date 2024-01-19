using UnityEngine;
using System.Collections;

public class helpscroller : MonoBehaviour {

	private Transform trn;
	// Use this for initialization
	void Start () {
		trn = GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/**
	void Scrlup()
	{
		if (transform.localPosition.y < 4500) {
			transform.localPosition.y = transform.localPosition.y+1500;
		}

	}

	void Scrldn()
	{
		if (transform.localPosition.y > 0) {
			transform.localPosition.y = transform.localPosition.y-1500;
		}
		
	}
	*/
}
