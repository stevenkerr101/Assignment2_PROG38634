using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Touch : Sense
{
    private void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            if(aspect.aspectType != aspectName)
            {
                //code for touch on aspect mismatch
                this.gameObject.GetComponent<NavMeshAgent>().speed -= 4;

                Vector3 dirToPlayer = transform.position - other.gameObject.transform.position;
                Vector3 newPos = transform.position + dirToPlayer;

                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(newPos);

            }
        }
    }
}
