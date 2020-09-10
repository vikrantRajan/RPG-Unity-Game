using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickedPoint();
        }
        UpdatePlayerAnimation();
    }

    private void MoveToClickedPoint() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit) 
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }

    }

    private void UpdatePlayerAnimation() 
    {
        // USING LOCAL SPEED TO USE IN THE ANIMATION BLEND TREE, WE DONT NEED THE WORLD SPACE INFO HENCE INVERSETRANSFORMDIRECTION...
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
