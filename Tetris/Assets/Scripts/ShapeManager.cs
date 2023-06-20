using System.Collections;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{

    [SerializeField]private bool donebilirmi = true;


        //Move Functions--------------
        internal void MoveLeft()
        {
            transform.Translate(Vector3.left,Space.World);
        }
        internal void MoveRight()
        {
            transform.Translate(Vector3.right,Space.World);
        }
        internal void MoveDown()
        {
            transform.Translate(Vector3.down,Space.World);
        }
        internal void MoveUp()
        {

            transform.Translate(Vector3.up, Space.World);
        }
        //----------------------------


    //Rotate Functions----------------
    internal void RotateRight()
    {
        if(donebilirmi)
        {
            transform.Rotate(0,0,-90);
        }
    }
    internal void RotateLeft()
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
