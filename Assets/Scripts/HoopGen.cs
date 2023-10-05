using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopGen : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject hoopPrefab;
    [SerializeField] int numHoops;
    [SerializeField] float height;
    private GameObject[] hoops;
    private float planetRadius;

    void Awake()
    {
        hoops = new GameObject[numHoops];
        planetRadius = transform.localScale.x / 2;

        Generate();
    }
    private void Generate()
    {
        float goldenRatio = (1 + Mathf.Sqrt(5f)) / 2;

        float radius = planetRadius + height;

        float theta;
        float phi;

        GameObject empty = Instantiate(new GameObject("HoopParent"));

        for (int i = 0; i < numHoops; i++)
        {
            theta = 2 * Mathf.PI / goldenRatio * i;
            phi = Mathf.Acos(1 - 2 * (i + 0.5f) / numHoops);
            Vector3 vertexLocation = transform.rotation *
                (radius * new Vector3(
                Mathf.Cos(theta) * Mathf.Sin(phi),
                Mathf.Sin(theta) * Mathf.Sin(phi),
                Mathf.Cos(phi)));

            Quaternion newRotation = 
                Quaternion.LookRotation(vertexLocation, Vector3.ProjectOnPlane(spawnPoint.position, vertexLocation));
            hoops[i] = Instantiate(hoopPrefab, vertexLocation + transform.position, newRotation);

            hoops[i].transform.parent = empty.transform;
        }
    }
}
