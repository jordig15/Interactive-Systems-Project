using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jhonRotate : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float angle = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0f, -1f, 0f) * rotationSpeed);
    }
}
