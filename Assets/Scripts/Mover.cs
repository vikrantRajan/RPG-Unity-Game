﻿using System.Collections;
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
}
