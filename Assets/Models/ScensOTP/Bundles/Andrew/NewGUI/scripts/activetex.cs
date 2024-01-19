using UnityEngine;
using System.Collections;

public class activetex : MonoBehaviour {

	public Texture tex1;
	public Texture tex2;

	public Color col1;
	public Color col2;

	private Renderer rend;
	
	void Start() {
		rend = GetComponent<Renderer>();
		rend.material.mainTexture = tex1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter()
	{
		rend.material.mainTexture = tex2;

		//rend.material.shader = Shader.Find("Specular");
		rend.material.SetColor("_Color", col2);
		rend.material.SetColor("_Tint", col2);

		Debug.Log("eM");
	}

	void OnMouseExit()
	{
		//renderer.material.mainTexture = tex1;
		rend.material.SetColor("_Color", col1);
		rend.material.SetColor("_Tint", col2);
		rend.material.mainTexture = tex1;
		Debug.Log("oM");
	}

	void OnMouseDown()
	{
		rend.material.mainTexture = tex2;
		rend.material.SetColor("_Color", col2);
		rend.material.SetColor("_TintColor", col2);
		//renderer.material.mainTexture = tex2;
	}
}
