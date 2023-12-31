using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Global Variables
    private SpawnerManager spawner;
    private BoardManager board;
    private ShapeManager activeShape;

    [Header("Saya�lar")]

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

    public bool gameOver = false;

    public GameObject gameOverPanel;
    private ScoreManager scoreManager;
    #endregion

    private void Start()
    {
        board = GameObject.FindObjectOfType<BoardManager>();
        spawner = GameObject.FindObjectOfType<SpawnerManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();


        if (spawner)
        {
            if (activeShape == null)
            {
                activeShape = spawner.SpawnShape();
                activeShape.transform.position = RoundVector(activeShape.transform.position);
            }
        }

        if (gameOverPanel)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!board || !spawner || !activeShape || gameOver || !scoreManager) { return; }
        InputControl();
    }

    void InputControl()
    {
        if ( (Input.GetKey("right") && Time.time > horizonPressSayac) || Input.GetKeyDown("right") || (Input.GetKey("d") && Time.time > horizonPressSayac) || Input.GetKeyDown("d") )
        {
            InputRight();
        }
        else if ( (Input.GetKey("left") && Time.time > horizonPressSayac) || Input.GetKeyDown("left") || (Input.GetKey("a") && Time.time > horizonPressSayac) || Input.GetKeyDown("a") )
        {
            InputLeft();
        }
        else if ( (Input.GetKeyDown("up") && Time.time > rotateSayac) || (Input.GetKeyDown("w") && Time.time > rotateSayac) )
        {
            InputRotate();
        }
        else if ( (Input.GetKey("down") && Time.time > downPressSayac) || Time.time > downSayac || (Input.GetKey("s") && Time.time > downPressSayac) || Time.time > downSayac)
        {
            InputDown();
        }
    }//Gelen inputlara g�re alttaki metotlara g�t�r�r


    private void Yerlestir()
    {
        horizonPressSayac = Time.time;
        downPressSayac = Time.time;
        rotateSayac = Time.time;


        activeShape.MoveUp();

        board.SekliIzgaraIcineAl(activeShape);
        SoundManager.instance.DoSoundFX(4);

        if (spawner)
        {
            activeShape = spawner.SpawnShape();
        }

        board.RemoveAllLines();
        if (board.tamamlananSatir > 0)
        {

            scoreManager.SatirSkoru(board.tamamlananSatir);


            SoundManager.instance.DoVocalSound(2);
            if (board.tamamlananSatir > 2)
            {
                SoundManager.instance.DoVocalSound(0);
            }
            else if (board.tamamlananSatir > 1)
            {
                SoundManager.instance.DoVocalSound(1);
            }

            SoundManager.instance.DoSoundFX(4);
        }
    }//Yerle�en nesneyi durdurur

    //Inputs---------------------
    private void InputRotate()
    {
        activeShape.RotateRight();
        rotateSayac = Time.time + rotateTime;

        if (!board.IsLegalPosition(activeShape))
        {
            SoundManager.instance.DoSoundFX(2);
            activeShape.RotateLeft ();
        }
        else
        {
            SoundManager.instance.DoSoundFX(2);
        }
    }//Nesneyi �evirir
    private void InputLeft()
    {
        activeShape.MoveLeft();
        horizonPressSayac = Time.time + horizonPressTime;

        if (!board.IsLegalPosition(activeShape))
        {
            SoundManager.instance.DoSoundFX(1);
            activeShape.MoveRight();
        }
        else
        {
            SoundManager.instance.DoSoundFX(2);
        }
    }
    private void InputRight()
    {
        activeShape.MoveRight();
        horizonPressSayac = Time.time + horizonPressTime;

        if (!board.IsLegalPosition(activeShape))
        {
            SoundManager.instance.DoSoundFX(1);
            activeShape.MoveLeft();
        }
        else
        {
            SoundManager.instance.DoSoundFX(2);
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

                    if (gameOverPanel)
                    {
                        gameOverPanel.SetActive(true);
                        SoundManager.instance.DoSoundFX(6);
                    }

                    SoundManager.instance.DoSoundFX(5);
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
    }//Tamsay� garantisi i�in yuvarlama function
}
