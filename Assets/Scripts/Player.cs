using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movespeed;
    public float jumpforce = 5f;
    public bool move = true;
    public bool sidewalk;
    [HideInInspector]public float x, z;
    
    
    public float maxhunger, maxstamina, maxhealth;
    [HideInInspector]public  float hunger, stamina, health;

    public float hungerincrease, staminadecrease;
    public bool dies=false;
    private float count;

    public UIManager instance;
    public CharacterController controller;
    public Animator anim;
    Vector3 movedirection;
    public float verticalvelocity;

    public GameObject Axe_1;
    public bool equip = false;
    public BoxCollider Axecollider;

    public Boat myBoat;
    private FPScam fps;
    public GameObject blur;

    public GameObject[] HouseLoc;

    public AudioSource footstep;
    
    public AudioSource Treesound;

    private float Staminatimer=0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        myBoat = FindObjectOfType<Boat>();
        fps = FindObjectOfType<FPScam>();

        health = maxhealth;
        stamina = maxstamina;

       
        //initially setting all locations to deactivate state
        foreach (GameObject obj in HouseLoc)
        {          
                obj.SetActive(false);           
        }
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        if (!dies)
        {
            Health();
            Movement();
            axe();

            //Boat HAndler
            if (myBoat.onboat == true)
            {
                transform.position = myBoat.spawnpos.position;
                myBoat.BoatMovement(x, z);
            }

            //stamina reduce
            Staminatimer += Time.deltaTime;
            if (Staminatimer > 5)
            {
                stamina -= Time.deltaTime * 4;
                Staminatimer = 0;
            }

        }
       
        environment();
    }

    void axe()
    {       
        if (!Axe_1.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            equip = true;
            Axe_1.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            equip = false;
            Axe_1.SetActive(false);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        RaycastHit Axe_hit;
       
        if (Physics.Raycast(transform.position, forward, out hit, 5f))
        {
            if (hit.collider.tag == "Tree" && equip == true && Input.GetMouseButton(0))
            {
                    anim.Play("Swing");
                    if (Physics.SphereCast(Axecollider.bounds.center, 0.1f, forward, out Axe_hit, 2f))
                    {
                        if (Axe_hit.collider.tag == "Tree")
                        {
                            Treesound.Play();
                            anim.Play("R_Swing");

                            Tree treescript = hit.collider.gameObject.GetComponent<Tree>();
                            treescript.TreeHealth--;
                            stamina -= staminadecrease * Time.deltaTime;
                        }
                    }
            }
            
            if (hit.collider.tag == "Boat")
            {
                instance.Continue.enabled = true;
                instance.Continue.text = "" + "PRESS F TO ENTER";
                //enter boat
                if (Input.GetKeyDown(KeyCode.F))
                {
                    move = false;
                    myBoat.onboat = true;
                    instance.Continue.enabled =false;
                    instance.Continue.text = "";
                }
            }          
        }        
    }

    void Movement()
    {
        if(move)
        {            
            movedirection = new Vector3(x, 0f, z);
            movedirection = transform.TransformDirection(movedirection);
            movedirection *= movespeed * Time.deltaTime;

            verticalvelocity += Physics.gravity.y * Time.deltaTime;
            verticalvelocity = Mathf.Clamp(verticalvelocity, -30f, 10f);
            if (controller.isGrounded && Input.GetButtonDown("Jump") && z > 0)
            {
                verticalvelocity = jumpforce;
                anim.SetBool("jump", true);
            }
            else { anim.SetBool("jump", false); }

            movedirection.y = verticalvelocity * Time.deltaTime;

            controller.Move(movedirection);

            if (z < 0)
            { anim.SetBool("walkback", true); }
            else
            {
                anim.SetBool("walkback", false);
            }

            if(x==0)
            {
                anim.SetBool("sidewalk", false);
            }
            else
            {
                anim.SetBool("sidewalk", true);
            }

            anim.SetFloat("speed", z);//animation idle to move
            anim.SetFloat("sidespeed", x);
            if(z==0 && x==0)
            { footstep.enabled = false; }
            else
            { footstep.enabled = true;  }
        }       
    }

    void Health()
    {
        count += Time.deltaTime;

        if (!dies&&count>10)
        {
                hunger += hungerincrease * Time.deltaTime;

                if (hunger > maxhunger)
                    stamina -= Time.deltaTime;
                if (stamina < 0)
                {
                    health -= 2;
                    stamina = maxstamina / 2;
                }
            count = 0;
        }
       
        if(Input.GetKeyDown(KeyCode.Q))
        {          
            if(instance.foodcount>0)
            {
                stamina +=2;
                health += 2;
                hunger -= 7;
                instance.foodCounter(-1);
            }
        }

        if (transform.position.y < 1.5)
        {
            health -= 1;
            blur.SetActive(true);
        }
        else
            blur.SetActive(false);

        if(health<=0)
        {
            dies = true;
            blur.SetActive(false);
            footstep.enabled = false;
        }
        
    }
    
    //activating and Deactivating the group of Houses
    void environment()
    {
        foreach(GameObject obj in HouseLoc)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if(distance<150)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }  
    

}
