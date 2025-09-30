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
    }


    void Update()
    {
        agent.SetDestination(end.position);
    }
}
