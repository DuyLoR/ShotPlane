using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private Vector2 boundary;
    public enum sphereType
    {
        Square,
        Rectangle,
        Diamond,
        Triangle

    }
    void Start()
    {
        boundary = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
        //DrawMatrix();
    }

    private void DrawMatrix()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, boundary);
    }
}
