using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private NavMeshAgent navAgent;
    public EnemyData enemyData;
    private float wanderDistance = 4;
    // Start is called before the first frame update
    void Start()
    {
        if (navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
        }
        if (enemyData != null)
        {
            LoadEnemy(enemyData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Si enemyData n'est pas assigné, on ne fait rien dans Update
        if (enemyData == null)
        {
            return;
        }
        // Si l'ennemi a atteint sa destination, on lui attribue une nouvelle destination
        // Cette partie ne concerne pas le tutoriel sur les Scriptable Objects)
        if (navAgent.remainingDistance < 1f)
        {
            GetNewDestination();
        }
    }

    private void LoadEnemy(EnemyData _data)
    {
        foreach (Transform child in transform)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(child.gameObject);
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
        if (navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
        }
        GameObject visuals = Instantiate(enemyData.model);
        visuals.transform.SetParent(transform);
        visuals.transform.localPosition = Vector3.zero;

        navAgent.speed = enemyData.speed;
    }
    
    private void GetNewDestination()
    {
        Vector3 nextDestination = transform.position;
        nextDestination += wanderDistance * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextDestination, out hit, 3f, NavMesh.AllAreas))
        {
            navAgent.SetDestination(hit.position);
        }
    }
}
