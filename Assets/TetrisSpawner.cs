using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    public GameObject[] tetrominoPrefabs;
    //public List<GameObject> tetrominoes;
    private TetrisGrid grid;
    private GameObject nextPiece;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<TetrisGrid>();
        if (grid == null)
        {
            return;
        }
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        //calculate top center of grid and spawn there
        Vector3 spawnPosition = new Vector3(Random.Range(1, grid.width - 3), grid.height - 3, 0);

        if (nextPiece != null)
        {
            nextPiece.SetActive(true);
            nextPiece.transform.position = spawnPosition;
        }
        else
        {
            nextPiece = InstantiateRandomPiece();
            nextPiece.transform.position = spawnPosition;
        }

        // check the spawned piece has the correct tag
        if (nextPiece.CompareTag("SpecialPiece"))
        {
            foreach (Transform block in nextPiece.transform)
            {
                block.tag = "SpecialPiece"; // check all child blocks to see if they have the correct tag
            }
        }

        nextPiece = InstantiateRandomPiece();
        nextPiece.SetActive(false);
    }

    private GameObject InstantiateRandomPiece()
    {
        int index = Random.Range(0, tetrominoPrefabs.Length);
        return Instantiate(tetrominoPrefabs[index]);
    }
}
