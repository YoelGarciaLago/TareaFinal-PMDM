using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //StartCoroutine(CheckAndMoveToTarget());

    }
    IEnumerator CheckAndMoveToTarget()
    {
        // Espera hasta que el agente esté en el NavMesh
        while (!navMeshAgent.isOnNavMesh)
        {
            Debug.Log("Esperando a que el agente esté en el NavMesh...");
            yield return new WaitForSeconds(0.5f); // Verifica cada 0.5 segundos
        }

        Debug.Log("Agente en NavMesh. Moviendo al objetivo...");
        navMeshAgent.SetDestination(player.position); // Ahora puede moverse al destino
    }
    void Update()
    {
        if (player != null)
        {
            if(!navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.enabled = false;
            }
            else
            {
                navMeshAgent.SetDestination(player.position);
            }
            
        }

    }
}
