using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum VanguardState { IDLE, RUN, JUMP }

public class VanguardBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
    public bool HasLOS;
    public GameObject player;

    private NavMeshAgent nav;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLOS) {
            nav.SetDestination(player.transform.position);
        }

        if(HasLOS && (Vector3.Distance(transform.position, player.transform.position) < 2.5)) { 
            //maybe an attack?
            animator.SetInteger("AnimState", (int)VanguardState.IDLE);
            transform.LookAt(transform.position - player.transform.forward);

            if (nav.isOnOffMeshLink) {
                animator.SetInteger("AnimState", (int)VanguardState.JUMP);
            }
        } else {
            animator.SetInteger("AnimState", (int)VanguardState.RUN);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }
}
