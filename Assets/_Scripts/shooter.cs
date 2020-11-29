using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    public float shootForce = 50;

    private void Start()
    {
        StartCoroutine(DestroyCor());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject g = Instantiate(bulletPrefab, transform.position, transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        }
    }

    IEnumerator DestroyCor()
    {
        yield return  new WaitForSeconds(6);
        Destroy(transform);
    }
}

