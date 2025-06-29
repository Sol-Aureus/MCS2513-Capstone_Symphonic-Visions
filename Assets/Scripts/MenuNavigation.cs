using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip[] soundFX;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    // PlayGame opens the level select menu
    public void PlayGame()
    {
        //SoundFXManager.instance.PlaySound(soundFX[0], transform, 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Options opens the options menu
    public void Options()
    {
        //SoundFXManager.instance.PlaySound(soundFX[0], transform, 1);
        optionsMenu.SetActive(true);
    }

    // Options opens the options menu
    public void Credits()
    {
        //SoundFXManager.instance.PlaySound(soundFX[0], transform, 1);
        creditsMenu.SetActive(true);
    }

    // Back closes the options and level select menus
    public void Back()
    {
        //SoundFXManager.instance.PlaySound(soundFX[0], transform, 1);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }


    // QuitGame quits the application
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
