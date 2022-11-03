using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static GameObject MainCamera;

    public int Gold = 0;
    public int Stones = 0;
    public int Wood = 0;


    // Start is called before the first frame update
    void Start()
    {
        MainCamera = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
