using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;

    public GameObject groundPrefab;
    public int numberOfSegments = 5;
    public float segmentLength = 5f;
    

    private List<GameObject> groundSegments = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        SpawnInitialGround();
    }

    void SpawnInitialGround()
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
       
        foreach (var segment in groundSegments)
        {
            segment.transform.Translate(Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime);
        }

        
        if (groundSegments.Count > 0 && groundSegments[0].transform.position.x < -segmentLength*2)
        {
            SpawnGround();
            Destroy(groundSegments[0]);
            groundSegments.RemoveAt(0);
        }
    }

    void SpawnGround()
    {
        Vector3 spawnPosition = new Vector3(-4.66f,-4.06f,0f);

        if (groundSegments.Count > 0)
        {
            spawnPosition = groundSegments[groundSegments.Count - 1].transform.position + new Vector3(segmentLength, 0f, 0f);
        }

        GameObject newGround = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);
        groundSegments.Add(newGround);
    }
}
