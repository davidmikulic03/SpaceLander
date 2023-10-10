using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public GravitationalConstant gravitationalConstant;
    private float gravConst;
    GameObject[] allBodies;
    [HideInInspector] public GameObject closestBody;

    private void Awake()
    {
        gravConst = gravitationalConstant.value;
        allBodies = GameObject.FindGameObjectsWithTag("Gravitational Body");
        closestBody = allBodies[0];
    }

    void FixedUpdate()
    {
        Physics.gravity = gravityVector;
        closestBody = ClosestPlanet();
    }

    public Vector3 gravityVector
    {
        get
        {
            Vector3 outputVector = Vector3.zero;

            foreach (var body in allBodies)
            {
                float mass = body.GetComponent<GravitationalBody>().mass;
                Vector3 distance = body.transform.position - transform.position;

                outputVector += distance.normalized * (mass / distance.sqrMagnitude);
            }

            outputVector *= gravConst;

            return outputVector;
        }
    }

    public GameObject ClosestPlanet()
    {
        GameObject output = allBodies[0];

        foreach (var body in allBodies)
        {
            float currentRadius = body.transform.localScale.x;
            float outputRadius = output.transform.localScale.x;

            Vector3 currentDistance = body.transform.position -   transform.position;
            currentDistance /= currentRadius;
            Vector3 outputDistance = output.transform.position - transform.position;
            outputDistance /= outputRadius;
            if (Vector3.SqrMagnitude(currentDistance) < Vector3.SqrMagnitude(outputDistance))
                output = body;
        }

        return output;
    }
}
