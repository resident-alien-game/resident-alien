using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class AttackArea : MonoBehaviour
{
    public GameObject alien;
    public GameObject statusManager;
    private StatusManagement status;
    public GameObject change;
    private ChangeManager changeManager;
    private GameObject targetCivilian;
    private CivilianControl civilian;
    private FormChange formChange;
    public List<IAttackable> Attackables { get; } = new();

    void Start()
    {
        status = statusManager.GetComponent<StatusManagement>();
        changeManager = change.GetComponent<ChangeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Civilian"))
        {
            var attackable = other.gameObject.GetComponent<IAttackable>();
            targetCivilian = other.gameObject;
            civilian = targetCivilian.GetComponent<CivilianControl>();
            formChange = alien.GetComponent<FormChange>();
            if (!formChange.hasHumanForm)
            {
                if (attackable != null && status.CanUseSpell())
                {
                    Attackables.Add(attackable);
                }
            } else {
                Attackables.Clear();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Civilian"))
        {
            var attackable = other.gameObject.GetComponent<IAttackable>();
            if (attackable != null && Attackables.Contains(attackable))
            {
                Attackables.Remove(attackable);
            }
        }
    }
}
