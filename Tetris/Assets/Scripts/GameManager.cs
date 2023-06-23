using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Global Variables
    private SpawnerManager spawner;
    private BoardManager board;
    private ShapeManager activeShape;

    [Header("Sayaçlar")]

    [Range(0.02f, 1f)]
    [SerializeField]
    private float downTime = .1f;
    private float downSayac;

    [Range(0.02f, 1f)]
    [SerializeField]
    private float horizonPressTime = 0.25f;
    private float horizonPressSayac;

    [Range(0.02f, 1f)]
    [SerializeField]
    private float rotateTime = 0.25f;
    private float rotateSayac;

    [Range(0.02f, 1f)]
    [SerializeField] 
    private float downPressTime = 0.25f;
    private float downPressSayac;

    bool gameOver = false;
    #endregion

    private void Start()
    {
        board = GameObject.FindObjectOfType<BoardManager>();
        spawner = GameObject.FindObjectOfType<SpawnerManager>();


        if (spawner)
        {
            if (activeShape == null)
            {
                activeShape = spawner.SpawnShape();
                activeShape.transform.position = RoundVector(activeShape.transform.position);
            }
        }
    }

    private void Update()
    {
        if (!board || !spawner || !activeShape || gameOver) { return; }
        InputControl();
    }

    void InputControl()
    {
        if ((Input.GetKey("right") && Time.time > horizonPressSayac) || Input.GetKeyDown("right"))
        {
            InputRight();
        }
        else if ((Input.GetKey("left") && Time.time > horizonPressSayac) || Input.GetKeyDown("left"))
        {
            InputLeft();
        }
        else if (Input.GetKeyDown("up") && Time.time > rotateSayac)
        {
            InputRotate();
        }
        else if ((Input.GetKey("down") && Time.time > downPressSayac) || Time.time > downSayac)
        {
            InputDown();
        }
    }//Gelen inputlara göre alttaki metotlara götürür


    private void Yerlestir()
    {
        horizonPressSayac = Time.time;
        downPressSayac = Time.time;
        rotateSayac = Time.time;


        activeShape.MoveUp();

        board.SekliIzgaraIcineAl(activeShape);

        if (spawner)
        {
            activeShape = spawner.SpawnShape();
        }

        board.RemoveAllLines();
    }//Yerleþen nesneyi durdurur

    //Inputs---------------------
    private void InputRotate()
    {
        activeShape.RotateRight();
        rotateSayac = Time.time + rotateTime;

        if (!board.IsLegalPosition(activeShape))
        {
            activeShape.RotateLeft ();
        }
    }//Nesneyi çevirir
    private void InputLeft()
    {
        activeShape.MoveLeft();
        horizonPressSayac = Time.time + horizonPressTime;

        if (!board.IsLegalPosition(activeShape))
        {
            activeShape.MoveRight();
        }
    }
    private void InputRight()
    {
        activeShape.MoveRight();
        horizonPressSayac = Time.time + horizonPressTime;

        if (!board.IsLegalPosition(activeShape))
        {
            activeShape.MoveLeft();
        }
    }
    private void InputDown()
    {
        downSayac = Time.time + downTime;
        downPressSayac = Time.time + downPressTime;

        if (activeShape)
        {
            activeShape.MoveDown();


            if (!board.IsLegalPosition(activeShape))
            {
                if (board.DisariTastiMi(activeShape))
                {
                    activeShape.MoveUp();
                    gameOver = true;
                }
                else
                {
                    Yerlestir();
                }
            }

        }
    }
    //---------------------------



    Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }//Tamsayý garantisi için yuvarlama function
}
