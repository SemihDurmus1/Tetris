using System.Globalization;
using System.Resources;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]private Transform tilePrefab;

    [SerializeField] int height = 22;
    [SerializeField] int width = 10;

    private Transform[,] izgara;

    public int tamamlananSatir = 0;

    private void Awake()
    {
        izgara = new Transform[width, height];
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
        for (int x = 0; x < width; ++x)
        {
            if (izgara[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }//Sýra dolu mu
    void ExplodeLine(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (izgara[x,y] != null)
            {
                Destroy(izgara[x,y].gameObject);
            }

            izgara[x,y] = null;
        }
    }//Sýrayý patlat
    void DownOneLine(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (izgara[x, y] != null)
            {
                izgara[x,y-1] = izgara[x,y]; //üstteki ýzgarayý alta atar
                izgara[x, y] = null; //Üstteki ýzgara boþaltýlýr
                izgara[x, y - 1].position += Vector3.down;//Aþaðý indir 
            }
        }
    }//Bir sýra aþaðý düþ
    void DownAllLines(int baslangicY)
    {
        for (int i = baslangicY; i < height; ++i)
        {
            DownOneLine(i);
        }
    }//Tüm sýralarý düþ
    public void RemoveAllLines()
    {
        tamamlananSatir = 0;
        for (int y = 0; y < height; ++y)
        {
            if (IsLineFilled(y))
            {
                tamamlananSatir++;
                ExplodeLine(y);
                DownAllLines(y+1);
                y--;
            }
        }
    }//Tüm sýralarý sil
    //---------------------------



    bool IsTileFilled(int x, int y, ShapeManager shape)
    {
        return (izgara[x,y] != null && izgara[x,y].parent != shape.transform);
    }//Kareler dolu mu

    bool IsInBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0);
    }//Tahtanin icinde mi

    public bool IsLegalPosition(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = RoundVector(child.position);

            if(!IsInBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (pos.y < height)
            {
                if (IsTileFilled((int)pos.x, (int)pos.y, shape))
                {
                    return false;
                }
            }
            
        }

        return true;
    }//Map içinde mi


    public bool DisariTastiMi(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y >= height - 1)
            {
                return true;
            }
        }

        return false;
    }

    Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2((int)Mathf.Round(vector.x), (int)Mathf.Round(vector.y));
    }//Tamsayý garantisi için yuvarlama function

    void BosKareleriOlustur()
    {
        if(tilePrefab == null) { Debug.Log("Tile Prefab Does not Exist"); return; }


        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Transform tile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                tile.name = "x " + x.ToString() + ", " + "y " + y.ToString(); ;
                tile.parent = this.transform;
            }
        }
    }//Initialize Map
}
