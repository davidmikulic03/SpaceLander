using UnityEngine;

public class RefuelController : MonoBehaviour
{
    [SerializeField] float fuel = 20;
    private bool isEnabled = true;

    private void OnTriggerEnter(Collider trigger)
    {
        if(trigger.name == "LanderModel" && isEnabled == true)
        {
            LanderController landerController = trigger.GetComponent<LanderController>();
            PlayerValues playerValues = landerController.GetComponent<PlayerValues>();
            if (playerValues.currentFuel == playerValues.maxFuel) return;

            landerController.Refuel(fuel);

            TargetAnim anim = GetComponent<TargetAnim>();
            StartCoroutine(anim.DeathAnim());
            isEnabled = false;
        }
    }
}
