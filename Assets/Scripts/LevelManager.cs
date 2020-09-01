using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform mainMenu, optionMenu;
    public Animator BtnAnim;

    public AudioSource menu_bgm;
   
    public float timer=1f;
 
    public void LoadScene()
    {
        BtnAnim.SetTrigger("play");
        menu_bgm.Stop();
        SceneManager.LoadScene("World");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionMenu(bool clicked)
    {
        if (clicked == true)
        {
            optionMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            optionMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }

}
