using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]private Transform tilePrefab;

    [SerializeField]byte yukseklik = 22;
    [SerializeField]int genislik = 10;

    private void Start()
    {
        BosKareleriOlustur();
    }
    void BosKareleriOlustur()
    {
        if(tilePrefab == null) { Debug.Log("Tile Prefab Does not Exist"); return; }


        for (int y = 0; y < yukseklik; y++)
        {
            for (int x = 0; x < genislik; x++)
            {
                Transform tile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                tile.name = "x " + x.ToString() + ", " + "y " + y.ToString(); ;
                tile.parent = this.transform;
            }
        }
    }
}
