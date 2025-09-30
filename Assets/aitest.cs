using UnityEngine;
using UnityEngine.AI;

public class aitest : MonoBehaviour
{
   [SerializeField] NavMeshAgent ai;

    void Start()
    {
        ai.destination = GameObject.FindGameObjectWithTag("End").transform.position;
    }
}
