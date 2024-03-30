using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormChange : MonoBehaviour
{
    // The color to change to when colliding with something
    public Material[] changedMaterials;
    public float colorChangeDuration = 10f;

    // Original color of the character
    private Material[] originalMaterials;
    private Renderer characterRenderer;
    private bool isColorChanged = false;
    private float colorChangeTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = GetComponent<Renderer>();
        originalMaterials = characterRenderer.materials;
    }

    void Update()
    {
        // If color is changed and the timer exceeds the duration, restore the original color
        if (isColorChanged)
        {
            colorChangeTimer += Time.deltaTime;
            if (colorChangeTimer >= colorChangeDuration)
            {
                characterRenderer.materials = originalMaterials;
                isColorChanged = false;
                colorChangeTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision involves the character
        if (other.gameObject.tag == "Player")
        {
            // Change the character's color to the collision color
            characterRenderer.materials = changedMaterials;
            isColorChanged = true;
            colorChangeTimer = 0f;
        }
    }

}
