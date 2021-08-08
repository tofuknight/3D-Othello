using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
    public GameObject ChessPre;
    public int row;
    public int col;
    public Material[] Black;
    public Material[] White;
   
    public int chessState=0;
    public AudioSource ch;

    public void setTrue(int playerState)
    {
        ch.Play();
        ChessPre.SetActive(true);
        if (playerState == 1)
        {
            ChessPre.GetComponent<MeshRenderer>().materials = Black;
            chessState=1;
        }
        else if (playerState == 2)
        {
            ChessPre.GetComponent<MeshRenderer>().materials = White;
            chessState=2;
        }



    }

    public void setFalse()
    {
        ChessPre.SetActive(false);
    }

    public int changeColor()
    {
        if (chessState==1)
        {
            ChessPre.GetComponent<MeshRenderer>().materials = White;
            chessState = 2;
;       }
        else if (chessState==2)
        {
            ChessPre.GetComponent<MeshRenderer>().materials = Black;
            chessState = 1;
        }
        return chessState;
    }

    public bool ifSet()
    {
        if (chessState != 0)
            return true;
        else
            return false;
    }
}
