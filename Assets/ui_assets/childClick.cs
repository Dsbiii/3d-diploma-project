using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childClick : MonoBehaviour
{
	public Transform toPanel;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void clickToHide()
	{


		foreach (Transform eachChild in toPanel) {
			if (eachChild.name == "Panel (1)") {
				Debug.Log ("Child found. Mame: " + eachChild.name);
			}
		}
		
		
	}
}
