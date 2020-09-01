using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public Level[] levels;

    int currentlevel = 0;
    int Loc;//which Location to go, in targetlocations
    public Transform[] targetLocations;

    private float TargetDistance;

    public UIManager instance;
    public Player player;
    public GameObject boat;
    public GameObject axe;
    public GameObject gameover;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        enddialogue();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            
            Displaynext();
        }

        if(currentlevel>3)
        {
            axe.SetActive(true);
        }

        if(currentlevel>10)
        {
            boat.SetActive(true);
        }

        Objectives();

        if(player.dies)
        {
            gameover.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Objectives()
    {
        //objectives to be done
        switch (currentlevel)
        {
            case 0:
                Loc = 0;
                interact(Loc, 5, 0, 4);
                break;
            case 1:
                Loc = 1;
                interact(Loc, 7, 0, 4);
                break;
            case 2:
                Loc = 0;
                interact(Loc, 2, 0, 4);
                break;
            case 3:
                Loc = 4;
                interact(Loc, 1, 0, 4);

                break;
            case 4:
                Loc = 0;
                interact(Loc, 5, 0, 4);
                break;
            case 5:
                if (instance.woodcount > 5)
                {
                    currentlevel++;
                    instance.CoinCounter(5);
                    instance.foodCounter(2);
                }
                break;
            case 6:
                Loc = 0;
                interact(Loc, 5, -5, 4);
                break;
            case 7:
                if (instance.woodcount > 35)
                {
                    currentlevel++;
                    instance.CoinCounter(5);
                    instance.foodCounter(2);
                }
                break;
            case 8:
                Loc = 0;
                interact(Loc, 10, -30, 4);
                break;
            case 9:
                Loc = 2;
                interact(Loc, 10, 0, 4);
                break;

            case 10:
                Loc = 3;
                interact(Loc, 10, 0, 50);
                break;
            case 11:
                Loc = 0;
                interact(Loc, 10, 0, 4);
                break;
            case 12:
                Loc = 5;
                interact(Loc, -20, 0, 3);
                onsound();
                SceneManager.LoadScene("Dance");
                break;

            default:
                break;
        }
    }

    void interact(int i,int coin,int wood,float Targetdistance)
    {
        if (Vector3.Distance(player.transform.position, targetLocations[i].position) < Targetdistance)
        {
            instance.Continue.enabled = true;
            instance.Continue.text = "" + "PRESS F TO INTERACT";
            instance.goalname.enabled = false;

            if (Input.GetKey(KeyCode.F))
            {
                startdialogue(levels[currentlevel]);
                instance.CoinCounter(coin);
                instance.foodCounter(2);
                instance.Woodcounter(wood);
                currentlevel++;
            }
        }
    }

    public void startdialogue(Level thislevel)
    {
        //clearing so that it will be empty
        sentences.Clear();

        foreach (string sentence in thislevel.dialogue)
        {
            sentences.Enqueue(sentence);
        }
        instance.DialogueBox.SetActive(true);

        Displaynext();
    }

    //for the next sentence in dialogue
    public void Displaynext()
    {
        if (sentences.Count == 0)
        {
            enddialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();//stoping previous one's so that sentence would be neat and clean
        StartCoroutine(Type(sentence));//starting auto type the sentence
    }

    public void enddialogue()
    {
        //RESETTING DIALOGUE AND CONTINUE texts
        instance.DiaText.text = "";
        instance.DialogueBox.SetActive(false);
        instance.Continue.text = "";
        instance.Continue.enabled = true;

        //LEVEL NAME
        if (currentlevel< levels.Length)
        {
            instance.goalname.enabled = true;
            instance.goalname.text = "" + levels[currentlevel].levelname;
        }
    }

    //Passing sentence as parameter
    IEnumerator Type(string sen)
    {
        instance.DiaText.text = "";
        foreach (char letter in sen.ToCharArray())
        {
            instance.DiaText.text += letter;
            yield return null;
        }
    }

    void onsound()
    {
        //Stopping all the audiosources
        AudioSource[] audio;
        audio = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audio.Length; i++)
        {
            audio[i].Stop();
        }
    }
    public void reload()
    {
        SceneManager.LoadScene("World");
    }
}

[System.Serializable]
public class Level
{
    public string levelname;
    [TextArea(2,7)]
    public string[] dialogue;
}

