using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool gameStopped = false;

    public GameObject pausePanel;
    
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        if (pausePanel)
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanelOpenClose();
        }
    }

    public void PausePanelOpenClose()
    {
        if (gameManager.gameOver) return;

        gameStopped = !gameStopped;

        if (pausePanel)
        {
            pausePanel.SetActive(gameStopped);
            if (SoundManager.instance)
            {
                SoundManager.instance.DoSoundFX(0);
                Time.timeScale = (gameStopped) ? 0 : 1;
            }
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

}
