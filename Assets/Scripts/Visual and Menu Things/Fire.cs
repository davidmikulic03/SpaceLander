using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject landerObject;
    private LanderController landerController;

    Light lightComponent;

    [SerializeField] private float lightIntensity;
    [SerializeField] private float toggleTime;
    private float inverseTime;

    private void Awake()
    {
        landerController = landerObject.GetComponent<LanderController>();
        lightComponent = GetComponent<Light>();

        inverseTime = 1 / toggleTime;
    }

    // Update is called once per frame
    void Update()
    {
        float increment = inverseTime * Time.deltaTime * lightIntensity;

        if (landerController.isThrusting && lightComponent.intensity < lightIntensity)
        {
            lightComponent.intensity += increment;
        }
        else lightComponent.intensity -= increment;

        lightComponent.intensity = Mathf.Clamp(lightComponent.intensity, 0, lightIntensity);
    }
}
