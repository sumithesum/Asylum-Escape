using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAnimationController : MonoBehaviour
{
    [SerializeField] private CreatureModelSwitcher _modelSwitcher;
    [SerializeField] private GameObject _demon;

    private Animator animator;
    private CreatureAI _creatureAI;


    void Start()
    {
        animator = GetComponent<Animator>();
        _creatureAI = _demon.GetComponent<CreatureAI>();
    }

    void Update()
    {
        animator.SetBool("is_walking", _creatureAI.isWalking);
        animator.SetBool("is_attacking", _creatureAI.isAttacking);

        /*if (_creatureAI.isWalking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            animator.CrossFade("Walk", 0.1f); // Transition to Walk animation smoothly
        }

        if (_creatureAI.isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            animator.CrossFade("Punch", 0.1f); // Transition to Attack animation smoothly
        }*/

    }
}

