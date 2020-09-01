using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DanceScene : MonoBehaviour
{
    Animator anim;
    AudioSource source;
    bool pause;

    public GameObject view;
    public GameObject resume;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        view.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer<100)
        {
            anim.SetBool("Continue", true);       
        }
        else
        {
            anim.SetBool("Continue", false);
            view.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            onPause();
        }
    }
    public void menu()
    {
        source.Stop();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void onPause()
    {
        pause = !pause;
        if (!pause)
        {
            Time.timeScale = 1;          
        }
        else if (pause)
        {
            Time.timeScale = 0;       
        }
        resume.SetActive(pause);
    }
}
