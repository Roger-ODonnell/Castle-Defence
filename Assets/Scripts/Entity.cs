using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform end;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        end = GameObject.FindGameObjectWithTag("End").transform;
        NavMeshHit hit;
      if (NavMesh.SamplePosition(transform.position, out hit, 2.0f, NavMesh.AllAreas)) {
         agent.transform.position = hit.position;
       } else {
        Debug.LogWarning("No valid NavMesh position found near spawn point.");
       }
    }


    void Update()
    {
        agent.SetDestination(end.position);
    }
}
