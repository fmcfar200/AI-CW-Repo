using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    bool paused = false;
    public GameObject pausePanel;
    GameObject player;
	// Use this for initialization
	void Start ()
    {
        pausePanel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found!!!!!!!");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.P))
        {
            if (!paused)
            {
                paused = true;
            }
            else
            {
                paused = false;
            }
        }

        if (paused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            player.GetComponent<PlayerShootScript>().enabled = false;

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadScene();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeScene("Menu");
            }

        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            player.GetComponent<PlayerShootScript>().enabled = true;

        }
    }

    public void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void Quit()
    {
        Application.Quit();
    }
    void ReloadScene()
    {
        if (Application.loadedLevelName == "UtilityScene")
        {
            SceneManager.LoadScene("UtilityScene");
        }
    }
}
