using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] pooledobject;

    public int maxpool;
    

    List<GameObject> Treespooled;

    // Start is called before the first frame update
    void Start()
    {
        Treespooled = new List<GameObject>();
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < maxpool; i++)
        {
            int j = Random.Range(0, pooledobject.Length);
            GameObject obj = (GameObject)Instantiate(pooledobject[j]);
            obj.SetActive(false);
            Treespooled.Add(obj);
        }
    }
    public GameObject GetpooledTrees()
    {
        //checking the availibility of objects that are not active
        for (int i = 0; i < Treespooled.Count; i++)
        {
            if (!Treespooled[i].activeInHierarchy)
            {
                return Treespooled[i];

            }
        }
        int j = Random.Range(0, pooledobject.Length);
        GameObject obj = (GameObject)Instantiate(pooledobject[j]);
        obj.SetActive(false);
        Treespooled.Add(obj);
        return obj;
    }
}
