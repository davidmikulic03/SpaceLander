using UnityEngine;

public class RepairController : MonoBehaviour
{
    [SerializeField] float repairAmount = 50;
    private bool isEnabled = true;

    private void OnTriggerEnter(Collider trigger)
    {
        if(trigger.name == "LanderModel" && isEnabled == true)
        {
            LanderController landerController = trigger.GetComponent<LanderController>();
            PlayerValues playerValues = landerController.GetComponent<PlayerValues>();
            if (playerValues.currentHealth == playerValues.maxHealth) return;

            landerController.Repair(repairAmount);

            TargetAnim anim = GetComponent<TargetAnim>();
            StartCoroutine(anim.DeathAnim());

            isEnabled = false;
        }
    }
}
