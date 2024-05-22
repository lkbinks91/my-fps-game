using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnnemiPatrouille : MonoBehaviour
{
   
    
    NavMeshAgent agent;
    [SerializeField]
    private GameObject waypointsParent;
    List<Vector3> waypoints;
    private int currentWPIndex;

    // Start is called before the first frame update
    void Start()
    {
        // construire 
        agent = GetComponent<NavMeshAgent>();
        waypoints = new List<Vector3>();

        foreach (Transform t in waypointsParent.GetComponentsInChildren<Transform>())
        {
            waypoints.Add(t.position);
        }
        waypoints.Remove(waypointsParent.transform.position);
        Debug.Log(waypoints.Count);

        // premiere destination
        agent = GetComponent<NavMeshAgent>();
        currentWPIndex = 0;
        agent.SetDestination(waypoints[currentWPIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWPIndex = ++currentWPIndex % waypoints.Count;
            agent.SetDestination(waypoints[currentWPIndex]);
        }
    }
}
