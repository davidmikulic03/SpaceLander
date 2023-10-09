using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    [SerializeField] int points;
    public bool isActive = true;

    private void OnTriggerEnter(Collider trigger)
    {
        GameObject hitObject = trigger.gameObject;
        if(trigger.gameObject.name == "LanderModel" && isActive)
        {
            PlayerValues playerValues = hitObject.GetComponent<PlayerValues>();
            playerValues.score += points;
            isActive = false;
            StartCoroutine(KillHoop());
        }
    }

    IEnumerator KillHoop()
    {
        TargetAnim anim = GetComponent<TargetAnim>();
        StartCoroutine(anim.DeathAnim());
        yield return new WaitForSeconds(anim.deathTime);
        Destroy(this.gameObject);
    }
}
