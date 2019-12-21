using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject item, crane, dropZone;
    private GameObject currentItem, currentDropZone;
    private Vector3 spawnArea;
    public float minSpawn, maxSpawn;
    private CraneController craneCon;
    private DetectItem detect;

    // Start is called before the first frame update
    void Start()
    {
        craneCon = crane.GetComponent<CraneController>();
        spawnArea = crane.transform.position;
        CreateItem();
    }

    // Update is called once per frame
    void Update()
    {
        if(craneCon.Grabbing && currentDropZone == null)
        {
            CreateDropZone();
        }

        if (currentDropZone != null)
        {
            if (detect.triggered)
            {
                CreateItem();
                CreateDropZone();
                detect.triggered = false;
            }
        }
    }

    void CreateItem ()
    {
        float spawnX = Random.Range(spawnArea.x - maxSpawn, spawnArea.x + maxSpawn);
        float spawnZ = Random.Range(spawnArea.z + minSpawn, spawnArea.z + maxSpawn);

        if (currentItem == null)
        {
            currentItem = Instantiate(item, new Vector3(spawnX, 1, spawnZ), Quaternion.identity);
        }
        else
        {
            currentItem.transform.position = new Vector3(spawnX, 1, spawnZ);
            currentItem.transform.rotation = Quaternion.identity;
            return;
        }
        
    }

    void CreateDropZone ()
    {
        float spawnX = Random.Range(spawnArea.x - maxSpawn, spawnArea.x + maxSpawn);
        float spawnZ = Random.Range(spawnArea.z + minSpawn, spawnArea.z + maxSpawn);

        if (currentDropZone == null)
        {
            currentDropZone = Instantiate(dropZone, new Vector3(spawnX, 0, spawnZ), Quaternion.identity);
            detect = currentDropZone.GetComponent<DetectItem>();
        }
        else
        {
            currentDropZone.transform.position = new Vector3(spawnX, 0, spawnZ);
            return;
        }
        
    }
}
