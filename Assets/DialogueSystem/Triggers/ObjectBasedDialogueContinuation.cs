using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class ObjectBasedDialogueContinuation : MonoBehaviour
{
    [SerializeField] private string triggerName = "";
    [SerializeField] private bool DestroyOnTrigger;

    private void Start()
    {
        if (triggerName == "")
        {
            Debug.LogWarning("Trigger name is empty, the Dialogue Trigger will not work.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DialogueContinuationTrigger"))
        {
            if (other.name == triggerName)
            {
                //Simulate the next button click
                DialogueManager.instance.OnNextButtonClicked();

                //Destroy if required
                if (DestroyOnTrigger)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
