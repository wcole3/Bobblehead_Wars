using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    //control the UI elements
    [SerializeField]
    private Text enemyText;
    [SerializeField]
    private Image[] healthButtons;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text elevatorPrompt;

	// Use this for initialization
	void Start () {
		//make sure that the health boxs are setup
        foreach(Image healthButton in healthButtons)
        {
            healthButton.gameObject.SetActive(true);
        }
        //make sure gameover panel isn't shown
        gameOverPanel.SetActive(false);
        elevatorPrompt.gameObject.SetActive(false);
	}
	
	public void SetEnemyText(int enemiesLeft)
    {
        enemyText.text = "Enemies Left: " + enemiesLeft;
    }

    public void DisableHealthBox(int box)
    {
        healthButtons[box].gameObject.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowElevatorPrompt()
    {
        elevatorPrompt.gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
