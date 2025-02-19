using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class EnemyMovementLvl2 : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }
    void Update()
    {
        if (player != null)
        {
            // Verificar si el agente está en un NavMesh antes de establecer un destino
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                Debug.LogWarning("Agente no está en el NavMesh, esperando...");
            }

        }
    }
}