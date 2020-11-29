﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform target;
    public Tower tower;
    [SerializeField] Rigidbody rb;
    float speed = .1f;

    [Range(0f, 1f)] public float positionStrength = 1f;
    [Range(0f, 1f)] public float rotationStrength = 1f;


    public void Init(Tower tower)
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.maxAngularVelocity = 30f;
        this.tower = tower;
        target = tower.transform.GetChild(0);
    }


    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 deltaPos = target.transform.position - transform.position;
        rb.velocity = 1f / Time.fixedDeltaTime * deltaPos * Mathf.Pow(positionStrength, 90f * Time.fixedDeltaTime);

        Quaternion deltaRot = target.transform.rotation * Quaternion.Inverse(transform.rotation);

        float angle;
        Vector3 axis;

        deltaRot.ToAngleAxis(out angle, out axis);

        if (angle > 180.0f) angle -= 360.0f;

        if (angle != 0) rb.angularVelocity = (1f / Time.fixedDeltaTime * angle * axis * 0.01745329251994f * Mathf.Pow(rotationStrength, 90f * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            tower.currentEnemiesCount--;
            Destroy(this.gameObject);
        }
    }
}
