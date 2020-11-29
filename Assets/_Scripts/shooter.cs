using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    public float shootForce = 50;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject g = Instantiate(bulletPrefab, transform.position, transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        }
    }

}

