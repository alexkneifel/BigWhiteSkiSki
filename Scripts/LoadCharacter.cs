using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadCharacter : MonoBehaviour
{
    //not using this script anymore
    public GameObject[] characterPrefabs;
    public GameObject joint;
    public Transform spawnPoint;
    private Transform jointSpawn;

    void Start()
    {

        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        jointSpawn.position = new Vector3(spawnPoint.position.x + 4, spawnPoint.position.y + 8, spawnPoint.position.z);
        GameObject cloneJoint = Instantiate(joint, jointSpawn.position, Quaternion.identity);
       
    }

}
