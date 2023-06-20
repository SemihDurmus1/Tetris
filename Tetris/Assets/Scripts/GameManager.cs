using UnityEngine;
public class GameManager : MonoBehaviour
{
    private SpawnerManager spawner;
    private BoardManager board;
    private ShapeManager activeShape;

    [Header("Sayaçlar")]
    [Range(0.02f, 1f)]
    [SerializeField]private float downTime = .1f;
    private float downSayac;

    [Range(0.02f, 1f)]
    [SerializeField] private float horizonPressTime = 0.25f;
    private float horizonPressSayac;

    [Range(0.02f, 1f)]
    [SerializeField] private float verticalRotateTime = 0.25f;
    private float verticalRotateSayac;

    [Range(0.02f, 1f)]
    [SerializeField] private float downPressTime = 0.25f;
    private float downPressSayac;

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
        if (!board || !spawner || !activeShape) { return; }

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
        else if (Input.GetKey("up") && Time.time > verticalRotateSayac)
        {
            InputRotate();
        }
        else if ((Input.GetKey("down") && Time.time > downPressSayac) || Time.time > downPressSayac)
        {
            downSayac = Time.time + downTime;
            downPressSayac = Time.time + downPressTime;

            if (activeShape)
            {
                activeShape.MoveDown();


                if (!board.IsLegalPosition(activeShape))
                {
                    Yerlesti();
                }

            }
        }
    }

    private void Yerlesti()
    {
        horizonPressSayac = Time.time;
        downPressSayac = Time.time;
        verticalRotateSayac = Time.time;


        activeShape.MoveUp();

        board.SekliIzgaraIcineAl(activeShape);

        if (spawner)
        {
            activeShape = spawner.SpawnShape();
        }
    }

    //Ýnputs---------------------
    private void InputRotate()
    {
        activeShape.RotateRight();
        verticalRotateSayac = Time.time + verticalRotateTime;

        if (!board.IsLegalPosition(activeShape))
        {
            activeShape.MoveLeft();
        }
    }
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
    //---------------------------

    Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
}
