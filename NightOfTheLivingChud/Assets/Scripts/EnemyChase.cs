using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 40f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!agent.isOnNavMesh) return;

        int pages = GameManager.instance.pagesCollected;

        if (pages <= 1)
        {
            agent.speed = 2.2f;
            detectionRange = 25f;
        }
        else if (pages <= 3)
        {
            agent.speed = 3.2f;
            detectionRange = 45f;
        }
        else if (pages <= 5)
        {
            agent.speed = 4.4f;
            detectionRange = 70f;
        }
        else
        {
            agent.speed = 5.5f;
            detectionRange = 999f;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}