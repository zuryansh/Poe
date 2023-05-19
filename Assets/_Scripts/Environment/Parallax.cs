using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Camera cam;
    [Range(0, 1)]
    [SerializeField] float effectStrength;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        if(cam == null) cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float distancex = (cam.transform.position.x * effectStrength);
        float distancey = (cam.transform.position.y * effectStrength);
        transform.position = new Vector3(startPos.x + distancex, startPos.y + distancey , transform.position.z);
    }
}
