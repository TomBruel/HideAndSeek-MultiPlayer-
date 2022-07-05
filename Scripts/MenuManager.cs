using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject LobbyMenu;


    public void PlayButton()
    {
        mainMenu.SetActive(false);
        LobbyMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReturnMenuButton()
    {
        LobbyMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void JoinGameButton()
    {
        SceneManager.LoadScene(1);
    }
}
