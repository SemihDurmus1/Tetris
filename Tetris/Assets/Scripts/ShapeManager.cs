using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{

    [SerializeField]private bool donebilirmi = true;

    void Start()
    {
        //InvokeRepeating("MoveDown",0f,0.25f);
        InvokeRepeating("RotateLeft", 0f, 2f);

        StartCoroutine(MoveRoutine());
    }

    void Update()
    {
        
    }
        //Move Functions--------------
        private void MoveLeft()
        {
            transform.Translate(Vector3.left,Space.World);
        }
        private void MoveRight()
        {
            transform.Translate(Vector3.right,Space.World);
        }
        public void MoveDown()
        {
            transform.Translate(Vector3.down,Space.World);
        }
        private void MoveUp()
        {

            transform.Translate(Vector3.up, Space.World);
        }
        //----------------------------


    //Rotate Functions----------------
    private void RotateRight()
    {
        if(donebilirmi)
        {
            transform.Rotate(0,0,-90);
        }
    }
    private void RotateLeft()
    {
        if (donebilirmi)
        {
            transform.Rotate(0, 0, +90);
        }
    }
    //--------------------------------

    
    IEnumerator MoveRoutine()
    {
        while (true)
        {
            MoveDown();
            yield return new WaitForSeconds(.25f);
        }
    }

}
