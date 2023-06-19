using UnityEngine;

public class GameManager : MonoBehaviour
{
    SpawnerManager spawner;

    BoardManager board;

    private void Start()
    {
        board = GameObject.FindObjectOfType<BoardManager>();
        spawner = GameObject.FindObjectOfType<SpawnerManager>();
    }
}
