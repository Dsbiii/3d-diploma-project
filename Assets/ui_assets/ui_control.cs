using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_control : MonoBehaviour
{

 	private Image 	log_image_1, log_image_2;

	public bool no_objects = true;
	
	//public String 	log_type;
    public Text		log_name;	
    public GameObject log_image;

	public GameObject panel_top;
	public GameObject panel_niz;
	public GameObject exit;
	public GameObject panel_zast;
	public GameObject panel_login;
	public GameObject panel_login_1;
	public GameObject panel_login_2;
	public GameObject panel_login_judge;
	public GameObject panel_judge_new;
	public GameObject panel_menu1;
	public GameObject panel_menu2;


	

	public void exitApp()
	{
		Application.Quit();
	}

	public void logPanels()
	{
		
	}	
	
	public void ekran_0 ()
	{
		panel_zast.gameObject.SetActive(true);
		panel_login_1.gameObject.SetActive(false);
		panel_login_2.gameObject.SetActive(false);
		panel_login_judge.gameObject.SetActive(false);
		panel_judge_new.gameObject.SetActive(false);
		panel_niz.gameObject.SetActive(false);
		panel_login.gameObject.SetActive(false);
		panel_menu1.gameObject.SetActive(false);
		panel_menu2.gameObject.SetActive(false);
	}
	
	public void ekran_1 ()
	{
		panel_zast.gameObject.SetActive(false);
		panel_login_1.gameObject.SetActive(false);
		panel_login_2.gameObject.SetActive(false);
		panel_login_judge.gameObject.SetActive(false);
		panel_judge_new.gameObject.SetActive(false);
		panel_niz.gameObject.SetActive(false);
		panel_login.gameObject.SetActive(true);
		panel_menu1.gameObject.SetActive(false);
		panel_menu2.gameObject.SetActive(false);
	}
	
	public void ekran_login_1 ()
	{
		panel_zast.gameObject.SetActive(false);
		panel_login_1.gameObject.SetActive(true);
		panel_login_2.gameObject.SetActive(false);
		panel_login_judge.gameObject.SetActive(false);
		panel_judge_new.gameObject.SetActive(false);
		panel_login.gameObject.SetActive(false);
		panel_menu1.gameObject.SetActive(false);
		panel_menu2.gameObject.SetActive(false);

		panel_niz.gameObject.SetActive(false);
	}
	

	public void ekran_login_judge ()
	{
		panel_zast.gameObject.SetActive(false);
		panel_login_1.gameObject.SetActive(false);
		panel_login_2.gameObject.SetActive(false);
		panel_login_judge.gameObject.SetActive(true);
		panel_judge_new.gameObject.SetActive(false);
		panel_login.gameObject.SetActive(false);
		panel_menu1.gameObject.SetActive(false);
		panel_menu2.gameObject.SetActive(false);

		panel_niz.gameObject.SetActive(false);
	}

		public void ekran_judge_new ()
	{
		panel_zast.gameObject.SetActive(false);
		panel_login_1.gameObject.SetActive(false);
		panel_login_2.gameObject.SetActive(false);
		panel_login_judge.gameObject.SetActive(false);
		panel_judge_new.gameObject.SetActive(true);
		panel_login.gameObject.SetActive(false);
		panel_menu1.gameObject.SetActive(false);
		panel_menu2.gameObject.SetActive(false);

		panel_niz.gameObject.SetActive(false);
	}


	public void ekran_login_2 ()
	{
		panel_zast.gameObject.SetActive(false);
		panel_login_1.gameObject.SetActive(false);
		panel_login_2.gameObject.SetActive(true);
		panel_login_judge.gameObject.SetActive(false);
		panel_judge_new.gameObject.SetActive(false);
		panel_login.gameObject.SetActive(false);
		panel_menu1.gameObject.SetActive(false);
		panel_menu2.gameObject.SetActive(false);

		panel_top.gameObject.SetActive(false);
		panel_niz.gameObject.SetActive(false);
	}

	
	
	public void ekran_menu_v1 ()
	{
		panel_zast.gameObject.SetActive(false);
		panel_login_1.gameObject.SetActive(false);
		panel_login_2.gameObject.SetActive(false);
		panel_login_judge.gameObject.SetActive(false);
		panel_judge_new.gameObject.SetActive(false);
		panel_login.gameObject.SetActive(false);
	
		panel_menu1.gameObject.SetActive(true);
		panel_menu2.gameObject.SetActive(false);

		panel_top.gameObject.SetActive(true);
		panel_niz.gameObject.SetActive(true);
		exit.gameObject.SetActive(true);


	}
	
	public void ekran_menu_v2 ()
	{
		panel_zast.gameObject.SetActive(false);
		panel_login_1.gameObject.SetActive(false);
		panel_login_2.gameObject.SetActive(false);
		panel_judge_new.gameObject.SetActive(false);
		panel_login.gameObject.SetActive(false);
	
		panel_menu1.gameObject.SetActive(false);
		panel_menu2.gameObject.SetActive(true);
		
		panel_top.gameObject.SetActive(true);
		panel_niz.gameObject.SetActive(false);
		exit.gameObject.SetActive(true);

		
	}
    // Start is called before the first frame update
    void Start()
    {
		if (no_objects)
		{ekran_0();   }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void newFunc()
	{

	}

	
}
