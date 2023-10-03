using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelController : MonoBehaviour
{
    [SerializeField] float fuel = 20;

    private void OnTriggerEnter(Collider trigger)
    {
        if(trigger.name == "LanderModel")
        {
            LanderController landerController = trigger.GetComponent<LanderController>();
            landerController.Refuel(fuel);

            GameObject.Destroy(this.gameObject);
        }
    }
}
