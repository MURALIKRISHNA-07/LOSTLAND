using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    
    public int TreeHealth=10;
    public GameObject thisTree;
    Animator treefall;
    
    public float treespeed;

    // Start is called before the first frame update
    void Start()
    {
        thisTree = this.gameObject;
        treefall = GetComponent<Animator>();
        treefall.enabled = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        if(TreeHealth<=0)
        {           
            treefall.enabled = true;
            StartCoroutine(destroyTree());           
        }
    }

    IEnumerator destroyTree()
    {
        treefall.enabled = true;
        yield return new WaitForSeconds(10);
        FindObjectOfType<UIManager>().Woodcounter(3);
        Destroy(thisTree);

    }
}
