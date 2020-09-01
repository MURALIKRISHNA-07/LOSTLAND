using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Boat : MonoBehaviour
{
    public float waterlevel;
    public float threshold;
    public float waterdensity;
    public float downforce;

    public float turnspeed, acceleration;
    public bool onboat;
    public Transform spawnpos;
    float forcefactor;
    Vector3 floatforce;

    float boatcounter;
    public UIManager instance;
    public GameObject Fpscam;
    public GameObject boatcam;
    Rigidbody boatRB;

    // Start is called before the first frame update
    void Start()
    {
        boatRB = GetComponent<Rigidbody>();
        onboat = false;
        boatcounter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        forcefactor = 1.0f - ((transform.position.y - waterlevel) / threshold);
        if(forcefactor>0.0f)
        {
            //multiplying with mass because it will not effect the floating object when its mass is changed
            floatforce = -Physics.gravity * boatRB.mass * (forcefactor - GetComponent<Rigidbody>().velocity.y * waterdensity);
            floatforce += new Vector3(0.0f, -downforce * boatRB.mass, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(floatforce, transform.position);//adding force at object position
        }
        
        
    }
    public void BoatMovement(float horizontal,float vertical)
    {        
        boatcam.SetActive(true);
        Fpscam.SetActive(false);
        boatRB.isKinematic = false;
        boatRB.AddTorque(0f, horizontal * turnspeed * Time.deltaTime, 0f);
        boatRB.AddForce(-transform.right* acceleration * vertical * Time.deltaTime);

        if(horizontal==0 && vertical==0)
        {            
            boatcounter+=Time.deltaTime;
            if(boatcounter>10)
            {
                boatRB.isKinematic =true;
                boatcounter= 0;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            instance.Continue.enabled = true;
            instance.Continue.text = "" + "PRESS F TO EXIT";
            if (Input.GetKeyDown(KeyCode.F))
            {
                boatRB.isKinematic = true;
                Fpscam.SetActive(true);
                boatcam.SetActive(false);
                FindObjectOfType<Player>().move = true;                

                instance.Continue.enabled =false;
                instance.Continue.text = "";

                onboat = false;
            }
        }        
    }
}
