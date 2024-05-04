using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private float damageAfterTime;
    [SerializeField] private AttackArea _attackArea;
    [SerializeField] private Animator _animator;
    public GameObject statusManager;
    private StatusManagement status;
    public GameObject change;
    private ChangeManager changeManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        status = statusManager.GetComponent<StatusManagement>();
        changeManager = change.GetComponent<ChangeManager>();
    }

    public void OnAttack()
    {
        _animator.SetBool("isAttack", true);
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(damageAfterTime);
        foreach (var attackAreaAttackable in _attackArea.Attackables)
        {
            var formChange = GetComponent<FormChange>();
            if (!formChange.hasHumanForm)
            {
                attackAreaAttackable.Attack();
                changeManager.changeToHuman = true;
                formChange.isAlien = false;
                formChange.hasHumanForm = true;
                status.AddScore(10);
                status.ReduceEnergy(1);
            } else {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

}
