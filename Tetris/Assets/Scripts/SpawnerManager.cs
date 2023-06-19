using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]ShapeManager[] allShapes;
    ShapeManager shape;

    int randomShape;







    private ShapeManager SpawnShape()
    {
        randomShape = Random.Range(0, allShapes.Length);
        shape = Instantiate(allShapes[randomShape], transform.position, Quaternion.identity) as ShapeManager;

        if (shape != null) { return shape; }
        else { print("Hata: Boþ Dizi"); return null; }
    }
}
