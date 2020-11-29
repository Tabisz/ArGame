using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyCor());
    }
    

    IEnumerator DestroyCor()
    {
        yield return  new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
