using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormChange : MonoBehaviour
{
    // The color to change to when colliding with something
    private Material[] allienMaterials;
    public float colorChangeDuration = 10f;

    // Original color of the character
    public Material[] humanMaterials;
    private Renderer characterRenderer;
    private bool isAlien = false;
    private bool hasHumanForm = false;
    private float formChangeTimer = 0f;

    private StatusManagement status;
    private GameObject targetCivilian;
    private CivilianControl civilian;


    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = GetComponent<Renderer>();
        allienMaterials = characterRenderer.materials;
        status = GameObject.Find("Status").GetComponent<StatusManagement>();
    }

    void Update()
    {
        // If the character is an alien and has human form, change back to human form after a certain duration
        if (isAlien && hasHumanForm)
        {
            formChangeTimer += Time.deltaTime;
            if (formChangeTimer >= colorChangeDuration)
            {
                characterRenderer.materials = humanMaterials;
                isAlien = false;
                formChangeTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Civilian")
        {
            // kill the civilian and change into human form
            if (!hasHumanForm)
            {
                targetCivilian = other.gameObject;
                characterRenderer.materials = humanMaterials;
                isAlien = false;
                hasHumanForm = true;

                civilian = targetCivilian.GetComponent<CivilianControl>();
                if (civilian != null && status.CanUseSpell())
                {
                    civilian.GotKilled();
                }
                status.AddScore(10);
                status.ReduceEnergy(1);
                targetCivilian = null;
            }

        }
        // Check if the collision involves the character
        if (other.gameObject.tag == "Kid")
        {
            // Change the character's color to the collision color
            characterRenderer.materials = allienMaterials;
            isAlien = true;
            formChangeTimer = 0f;
        }
    }

}
