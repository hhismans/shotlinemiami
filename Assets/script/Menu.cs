using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public GameObject	Menu_pause;
	public GameObject	Menu_win;
	public GameObject	Menu_loose;
	public bool			win = false;
	public bool			loose = false;
	private static int	niveau = 0;
	private AudioSource Song_v;

	public void Start_Game_Button(){
		Debug.Log("Je lance la game");
		niveau++;
		Application.LoadLevel (1);
	}
	public void load_lvl(int lvl){
		Debug.Log("Je lance la game");

		if (lvl == -1) {
			niveau++;
			Application.LoadLevel (niveau);
			Time.timeScale = 1;
		} else if (lvl == -2){
			Application.LoadLevel(niveau);
			Time.timeScale = 1;
		}else {
			niveau = lvl;
			Application.LoadLevel (lvl);
			Time.timeScale = 1;
		}
	}
	public void Resume_Game_Button(){
		Debug.Log("Je leave la pause");
		Menu_pause.SetActive(false);
		Time.timeScale = 1;
	}
	public void Return_Menu_Button(){
		Menu_pause.SetActive(true);
	}
	public void Leave_Game_Button(){
		Debug.Log("Je leave le jeux");
		Application.Quit();
	}

	public void pause(bool paused) {
		if (paused == true) {
			Time.timeScale = 0;
		} else
			Time.timeScale = 1;
	}

	void Start(){
		Time.timeScale = 1;
		Song_v = GetComponent<AudioSource> ();
	}
	void Update() {
		if (Input.GetKeyUp ("escape") && Time.timeScale == 1) {
			if (Application.loadedLevelName != "00"){
				Menu_pause.SetActive(true);
				Time.timeScale = 0;
			} else
			{
				Leave_Game_Button();
			}
		}
		if (win == true) {
			Menu_win.SetActive(true);
			Time.timeScale = 0;
		}
		if (loose == true) {
			Menu_loose.SetActive(true);
			Time.timeScale = 0;
		}
	}

}