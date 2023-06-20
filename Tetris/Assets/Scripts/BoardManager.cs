using System.Globalization;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]private Transform tilePrefab;

    [SerializeField]byte yukseklik = 22;
    [SerializeField]int genislik = 10;

    private Transform[,] izgara;

    private void Awake()
    {
        izgara = new Transform[genislik, yukseklik];
    }

    private void Start()
    {
        BosKareleriOlustur();
    }



    

    public void SekliIzgaraIcineAl(ShapeManager shape)
    {
        if (shape == null) { return; }

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = RoundVector(child.position);
            izgara[(int)pos.x, (int)pos.y] = child;
        }
    }



    bool IsTileFilled(int x, int y, ShapeManager shape)
    {
        return (izgara[x,y] != null && izgara[x,y].parent != shape.transform);
    }

    bool IsInBoard(int x, int y)
    {
        return (x >= 0 && x < genislik && y >= 0);
    }

    public bool IsLegalPosition(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = RoundVector(child.position);

            if(!IsInBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (pos.y < yukseklik)
            {
                if (IsTileFilled((int)pos.x, (int)pos.y, shape))
                {
                    return false;
                }
            }
            

        }

        return true;
    }

    Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
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
