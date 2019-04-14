using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    //control the UI elements
    [SerializeField]
    private Text enemyText;
    [SerializeField]
    private Image[] healthButtons;

	// Use this for initialization
	void Start () {
		//make sure that the health boxs are setup
        foreach(Image healthButton in healthButtons)
        {
            healthButton.gameObject.SetActive(true);
        }
	}
	
	public void SetEnemyText(int enemiesLeft)
    {
        enemyText.text = "Enemies Left: " + enemiesLeft;
    }

    public void DisableHealthBox(int box)
    {
        healthButtons[box].gameObject.SetActive(false);
    }
}
