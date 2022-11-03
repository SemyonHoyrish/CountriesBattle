using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public float Speed = 1.0f;
    public float ShiftMultiplier = 2.0f;

    private float realSpeed
    {
        get
        {
            if (Input.GetKey(KeyCode.LeftShift)) return Speed * ShiftMultiplier;
            return Speed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            gameObject.transform.position += new Vector3(0, 0, realSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            gameObject.transform.position -= new Vector3(0, 0, realSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            gameObject.transform.position -= new Vector3(realSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.D))
            gameObject.transform.position += new Vector3(realSpeed * Time.deltaTime, 0, 0);
    }
}
