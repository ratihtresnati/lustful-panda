using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class TriggerKardus : MonoBehaviour
{
    // public GameObject ground;
    public NavMeshSurface navmesh;
    private Rigidbody rigidbody;

    private void Start()
    {
        GameObject navmeshObject = GameObject.Find("NavmeshBlock");

        navmesh = navmeshObject.GetComponent<NavMeshSurface>();
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            navmesh.BuildNavMesh();  
            if (rigidbody.velocity.magnitude < 0.1f) rigidbody.isKinematic = true;              
        }
    }

}
