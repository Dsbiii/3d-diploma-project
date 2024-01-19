using UnityEngine;
using System.Collections;

public class Testika : MonoBehaviour {
	
	public Camera camNew;
	// Use this for initialization
	
	void Start () {
	
	//yield return new WaitForSeconds(2);
		camNew.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
