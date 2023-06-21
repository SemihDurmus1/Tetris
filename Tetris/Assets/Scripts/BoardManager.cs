using System.Globalization;
using System.Resources;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]private Transform tilePrefab;

    [SerializeField] int yukseklik = 22;
    [SerializeField] int genislik = 10;

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



    //Line Methods---------------
    bool IsLineFilled(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (izgara[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }
    void ExplodeLine(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (izgara[x,y] != null)
            {
                Destroy(izgara[x,y].gameObject);
            }

            izgara[x,y] = null;
        }
    }
    void DownOneLine(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (izgara[x, y] != null)
            {
                izgara[x,y-1] = izgara[x,y]; //üstteki ýzgarayý alta atar
                izgara[x, y] = null; //Üstteki ýzgara boþaltýlýr
                izgara[x, y - 1].position += Vector3.down;//Aþaðý indir 
            }
        }
    }
    void DownAllLines(int baslangicY)
    {
        for (int i = baslangicY; i < yukseklik; ++i)
        {
            DownOneLine(i);
        }
    }

    public void RemoveAllLines()
    {
        for (int y = 0; y < yukseklik; ++y)
        {
            if (IsLineFilled(y))
            {
                ExplodeLine(y);
                DownAllLines(y+1);
                y--;
            }
        }
    }
    //---------------------------



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
        return new Vector2((int)Mathf.Round(vector.x), (int)Mathf.Round(vector.y));
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
