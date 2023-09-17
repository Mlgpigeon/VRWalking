using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
   // public Transform player;
    NavMeshAgent nav;
    private Animator anim;
    bool isWalking = false;
    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);
        if(nav.remainingDistance < 1.6f)
        {
           nav.velocity = Vector3.zero;
           nav.isStopped=true;
           isWalking = false;
           isRunning = false;
        }
        else if(nav.remainingDistance > 1.6f && nav.remainingDistance < 4.6f)
        {  
            if(!isWalking){
                nav.velocity = nav.velocity * 0.3f;
                isWalking = true;
                isRunning = false;
            }
            nav.isStopped=false;
            
        }
        else
        {   
            if(!isRunning){
                isWalking = false;
                isRunning = true;
            }
            nav.isStopped=false;
            
        }
        
            anim.SetBool("isRunning", isRunning);
            anim.SetBool("isWalking", isWalking);
    }
}
