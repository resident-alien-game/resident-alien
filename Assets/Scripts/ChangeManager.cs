using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeManager : MonoBehaviour
{
    public SoundManagement soundManagement;
    public GameObject alien;
    public GameObject human;
    public GameObject currentForm;
    public bool changeToAlien = false;
    public bool changeToHuman = false;
    public bool hasHumanForm = false;
    // Start is called before the first frame update
    void Start()
    {
        SwitchToAlien();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeToAlien)
        {
            SwitchToAlien();
            changeToAlien = false;
        }
        if (changeToHuman)
        {
            SwitchToHuman();
            changeToHuman = false;
        }

    }

    public void SwitchToAlien()
    {  
        currentForm = alien;
        Vector3 humanPosition = human.transform.position;
        Vector3 temp = new Vector3(humanPosition.x, 2f, humanPosition.z);
        currentForm.transform.position = temp;
        human.SetActive(false);
        alien.SetActive(true);
        hasHumanForm = alien.GetComponent<FormChange>().hasHumanForm;
        alien.transform.position = currentForm.transform.position;    
        soundManagement.isAlien = true;
        soundManagement.alienGrowl();
        //soundManagement.police(true);
    }

    public void SwitchToHuman()
    {  
        currentForm = human;
        Vector3 alienPosition = alien.transform.position;
        Vector3 temp = new Vector3(alienPosition.x, 2f, alienPosition.z);
        currentForm.transform.position = temp;
        alien.SetActive(false);
        human.SetActive(true);
        hasHumanForm = human.GetComponent<FormChange>().hasHumanForm;
        human.transform.position = currentForm.transform.position;
        soundManagement.isAlien = false;
        //soundManagement.police(false);
    }
}
