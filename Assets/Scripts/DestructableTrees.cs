using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableTrees : MonoBehaviour
{
    
    public int size = 50;
    //Space is the amount of space to which the next element to be placed in worldSpace
    public int space = 10;

    public Trees[] Tree;
    
    public List<GameObject> DestroyTree;
    

    // Start is called before the first frame update
    void Start()
    {
        

        foreach(GameObject i in DestroyTree)
        {
            for (int x = 0; x < size; x += space)
            {
                for (int z = 0; z < size; z += space)
                {
                    Trees element = Tree[0];

                    if (element.canPlace())
                    {
                        //giving positions in the terrain
                        //if we dont add i gameobject's transform  all trees spawn at its prefab transform position 
                        Vector3 position = new Vector3(x, 0f, z) + i.transform.position;
                        position.y = Terrain.activeTerrain.SampleHeight(position);//terrain height
                        Vector3 offset = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
                        Vector3 rotation = new Vector3(Random.Range(0, 5f), Random.Range(0, 360f), Random.Range(0, 5f));
                        Vector3 scale = Vector3.one * Random.Range(1.0f, 2.25f);

                        //spawing the Trees 
                        GameObject newTree = Instantiate(element.prefab);
                        newTree.transform.SetParent(transform);
                        newTree.transform.position = position + offset;
                        newTree.transform.eulerAngles = rotation;
                        newTree.transform.localScale = scale;


                    }

                }
            }
        }

    }
    


}
[System.Serializable]
public class Trees
{
    public string name;

    [Range(1, 10)]
    public int density;

    public GameObject prefab;

    public bool canPlace()
    {
        if (Random.Range(0, 10)<density)
            return true;
        else
            return false;
    }

}
