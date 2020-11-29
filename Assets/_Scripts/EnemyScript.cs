using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Vector3 target;
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
        target = tower.enemyTarget.position;
    }


    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 deltaPos = target - transform.position;
        rb.velocity = 1f / Time.fixedDeltaTime * deltaPos * Mathf.Pow(positionStrength, 90f * Time.fixedDeltaTime);


        RotateTowards(target);
    }

    private void RotateTowards(Vector3 to) {
     
        Quaternion _lookRotation = 
            Quaternion.LookRotation((to - transform.position).normalized);
        
        transform.rotation = 
            Quaternion.Slerp(transform.rotation, _lookRotation, Time.fixedDeltaTime * rotationStrength);
        
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
