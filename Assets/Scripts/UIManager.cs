using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public FPScam cam;
    public Player theplayer;
    public Slider Health_S, Stamina_S, Hunger_s;
    
    public GameObject inventory;
    bool Inventrydisplay;

    public int woodcount= 0;
    public int coincount= 0;
    public int foodcount= 1;

    public bool pause;

    public Text DiaText;
    public GameObject DialogueBox;
    public GameObject resume;
    
    public Text Continue;
    public Text goalname;

    public Text Coins;
    public Text Food;
    public Text Wood;


    private void Start()
    {
        //setting slider to max
        Health_S.maxValue = theplayer.maxhealth;
        Stamina_S.maxValue = theplayer.maxstamina;
        Hunger_s.maxValue = theplayer.maxhunger;

        Inventrydisplay = false;
        inventory.SetActive(Inventrydisplay);

        DialogueBox.SetActive(false);
    }

    private void Update()
    {
        //slider values
        Health_S.value = theplayer.health;
        Stamina_S.value = theplayer.stamina;
        Hunger_s.value = theplayer.hunger;

        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventrydisplay = !Inventrydisplay;
            inventory.SetActive(Inventrydisplay);
        }

        inven();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPause();
        }
    }

     //UPDATING INVENTORY
    void inven()
    {
       Coins.text = "COINS: " + coincount;
       Wood.text = "WOOD: " + woodcount;
       Food.text = "FOOD: " + foodcount;
    }

    public void Woodcounter(int add)
    {
        woodcount += add;
    }
    public void CoinCounter(int add)
    {
        coincount += add;
    }
    public void foodCounter(int add)
    {
        foodcount += add;
    }

    public void onPause()
    {
        pause = !pause;
        if (!pause)
        {
            Time.timeScale = 1;
            cam.Lock();
        }
        else if (pause)
        {
            Time.timeScale = 0;
            cam.Unlock();
        }
        resume.SetActive(pause);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

