using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseScript : MonoBehaviour {

	private GameObject PausePanel;
	private GameObject GameEndedPanel;

	// Use this for initialization
	void Start () {
		PausePanel = GameObject.Find("PausePanel");
		PausePanel.SetActive(false);
		GameEndedPanel = GameObject.Find("GameEndedPanel");
		GameEndedPanel.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale == 1) {
				PauseGame();
			}else{
				ResumeGame();
			}
		}
	}
	private void PauseGame(){
		Time.timeScale = 0;
		PausePanel.SetActive(true);
	}
	public void ResumeGame(){
		Time.timeScale = 1;
		PausePanel.SetActive(false);
	}

	public void MainMenu(){
		Time.timeScale = 1;
		PausePanel.SetActive(false);
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

	public void RestartGame(){
		Time.timeScale = 1;
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

  public void QuitGame(){
		Application.Quit();
	}

	public void ShowGameEndedPanel(string highscore){
		Time.timeScale = 0;
		GameEndedPanel.SetActive(true);
		GameObject.Find("EndGameHighscore").GetComponent<TextMeshProUGUI>().SetText(highscore.ToString());
	}
}