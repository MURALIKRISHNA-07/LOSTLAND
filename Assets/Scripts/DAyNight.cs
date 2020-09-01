using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAyNight : MonoBehaviour
{
    private Light sun;
    public GameObject[] Lights;

    public WindZone wind;

    private float Xangle;
    float count;

    // Start is called before the first frame update
    void Start()
    {
        sun = GetComponent<Light>();
        Xangle = this.transform.eulerAngles.x;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Xangle = this.transform.eulerAngles.x;
        transform.RotateAround(transform.position, Vector3.right, 0.5f* Time.deltaTime);
        transform.LookAt(transform.position);

        if (Xangle >= 5 && Xangle <= 185)
        {
            foreach(GameObject obj in Lights)
            {
                obj.SetActive(false);
               
            }
            //Enabling soft Shadows
            sun.shadows = LightShadows.Soft;
            setintensity();  

        }
        else
        {
            foreach (GameObject obj in Lights)
            {
                obj.SetActive(true);
            }
            sun.intensity = 0;
            RenderSettings.fog = false;
            //disabling Shadows
            sun.shadows = LightShadows.None;
        }
        count += Time.deltaTime;
        if (count > 100)
        {
            wind.windMain = Random.Range(0, 4);
            wind.windTurbulence = Random.Range(0, 4);
            count=0;
        }
    }

    int intens()
    {
        int c;
        c = (int)Xangle/15;
        return c;
    }

    void setintensity()
    {
        int angle = intens();
        
        switch (angle)
        {
            case 1:
                sun.intensity = 0.5f;
  
                break;

            case 2:
                sun.intensity = 0.6f;
  
                break;

            case 3:
                sun.intensity = 0.8f;
 
                break;

            case 4:
                sun.intensity = 1f;
             
                break;

            case 5:
                sun.intensity = 1.2f;
              
                break;

            case 6:
                sun.intensity = 1.5f;
             
                break;

            case 7:
                sun.intensity = 1.5f;
             
                break;

            case 8:
                sun.intensity = 1.5f;
            
                break;

            case 9:
                sun.intensity = 1.2f;
        
                break;

            case 10:
                sun.intensity = 1f;
      
                break;

            case 11:
                sun.intensity = 0.88f;
       
                break;

            case 12:
                sun.intensity = 0.5f;
    
                break;

            default:
    
                break;
        }
    }
}
