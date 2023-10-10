using UnityEngine;

public class PointGen : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 initialRotation;
    [SerializeField] Transform facingTarget;

    [SerializeField] int number;
    [SerializeField] float height;
    private GameObject[] hoops;
    private float planetRadius;

    void Awake()
    {
        hoops = new GameObject[number];
        planetRadius = transform.localScale.x / 2;

        Generate();
    }
    private void Generate()
    {
        float goldenRatio = (1 + Mathf.Sqrt(5f)) / 2;

        float radius = planetRadius + height;

        float theta;
        float phi;

        GameObject empty = new GameObject(prefab.name + "Parent");
        empty.transform.position = transform.position;
        empty.transform.parent = transform;

        for (int i = 0; i < number; i++)
        {
            theta = 2 * Mathf.PI / goldenRatio * i;
            phi = Mathf.Acos(1 - 2 * (i + 0.5f) / number);
            Vector3 vertexLocation = transform.rotation *
                (radius * new Vector3(
                Mathf.Cos(theta) * Mathf.Sin(phi),
                Mathf.Sin(theta) * Mathf.Sin(phi),
                Mathf.Cos(phi)));

            Quaternion newRotation;
            if (facingTarget != null)
            {
                newRotation =
                Quaternion.LookRotation(Vector3.ProjectOnPlane(facingTarget.position, vertexLocation), vertexLocation) * Quaternion.Euler(initialRotation);
            }
            else newRotation = 
                    Quaternion.LookRotation(Vector3.Cross(transform.forward, vertexLocation), vertexLocation) * Quaternion.Euler(initialRotation);
            hoops[i] = Instantiate(prefab, vertexLocation + transform.position, newRotation);

            hoops[i].transform.parent = empty.transform;
        }
    }
}
