using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_SetSize : MonoBehaviour
{
    public GameObject[] props;
    public Vector3 propsSize = new Vector3(1f, 1f, 1f);

    public GameObject[] concreteBarr;
    public Vector3 concreteBarrSize = new Vector3(1f, 1f, 1f);
    
    public GameObject[] rodBarr;
    public Vector3 rodBarrSize = new Vector3(1f, 1f, 1f);

    public GameObject[] Cones;
    public Vector3 conesSize = new Vector3(1f, 1f, 1f);

    public GameObject[] Lamberts;
    public Vector3 lambertsSize = new Vector3(1f, 1f, 1f);

    public GameObject[] tyres;
    public Vector3 tyresSize = new Vector3(1f, 1f, 1f);

    public GameObject[] Containers;
    public Vector3 containersSize = new Vector3(1f, 1f, 1f);

    public GameObject[] barrells;
    public Vector3 barrellsSize = new Vector3(1f, 1f, 1f);

    public GameObject[] crate_lambert;
    public Vector3 crateLambertSize = new Vector3(1f, 1f, 1f);


    void Start()
    {
        SetSizeForObjects(concreteBarr, concreteBarrSize);
        SetSizeForObjects(rodBarr, rodBarrSize);
        SetSizeForObjects(Cones, conesSize);
        SetSizeForObjects(Lamberts, lambertsSize);
        SetSizeForObjects(tyres, tyresSize);
        SetSizeForObjects(Containers, containersSize);
        SetSizeForObjects(barrells, barrellsSize);
        SetSizeForObjects(crate_lambert, crateLambertSize);
    }

    void SetSizeForObjects(GameObject[] objects, Vector3 size)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.transform.localScale = size;
            }
        }
    }
}


