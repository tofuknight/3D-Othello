using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessControl : MonoBehaviour
{
    public List<List<Chess>> Table=new List<List<Chess>>();
    public Chess Chess;
    public int rows = 8;
    public int cols = 8;
    public Transform point;
    public int curPlayer = 1;
    //public AudioSource win;
    //public AudioSource fail;

    public GameObject UI;
    public Text Wins;
    public Text blackScore;
    public Text whiteScore;
    public void GenerateChasses()
    {
        for(int i=0; i<cols; i++ )
        {
            Table.Add(new List<Chess>());
            for (int j=0; j<rows; j++ )
            {
                Table[i].Add(Instantiate(Chess, point.position, point.rotation));
                Table[i][j].gameObject.transform.SetParent(gameObject.transform);
                Table[i][j].row = i;
                Table[i][j].col = j;
                Table[i][j].setFalse();
                point.position = new Vector3(point.position.x, point.position.y, point.position.z-2);
            }
            point.position = new Vector3(point.position.x - 2, point.position.y, 7);
        }
    }

    public void reStart()
    {
        Table.Clear();
        GenerateChasses();
        //初始化初始棋子
        Table[rows / 2 - 1][cols / 2 - 1].setTrue(2);
        Table[rows / 2][cols / 2 - 1].setTrue(1);
        Table[rows / 2 - 1][cols / 2].setTrue(1);
        Table[rows / 2][cols / 2].setTrue(2);
    }

    public void Swap(int row, int col)
    {
        bool canSet;
        List<Chess> setChess = check(row, col,out canSet);
        //if(canSet)
        //{
        for(int i = 0; i < setChess.Count; i++)
        {
            setChess[i].changeColor();
        }
        //}
    }

    public List<Chess> check(int row, int col, out bool canSet)
    {
        List<Chess> setChesses = new List<Chess>();
        List<Chess> pendingChesses = new List<Chess>();

        //遍历左侧
        for (int i = col - 1; i >= 0; i--)
        {
            if (Table[row][i].GetComponent<Chess>().chessState == 0)
            {
                break;
            }
            if (Table[row][i].GetComponent<Chess>().chessState != curPlayer)
            {
                pendingChesses.Add(Table[row][i]);
            }
            if (Table[row][i].GetComponent<Chess>().chessState == curPlayer)
            {
                setChesses.AddRange(pendingChesses);
                break;
            }
        }
        pendingChesses.Clear();

        //遍历右侧
        for (int i = col + 1; i < cols; i++)
        {
            if (Table[row][i].GetComponent<Chess>().chessState == 0)
            {
                break;
            }
            if (Table[row][i].GetComponent<Chess>().chessState != curPlayer)
            {
                pendingChesses.Add(Table[row][i]);
            }
            if (Table[row][i].GetComponent<Chess>().chessState == curPlayer)
            {
                setChesses.AddRange(pendingChesses);
                break;
            }
        }
        pendingChesses.Clear();

        //遍历上侧
        for (int i = row - 1; i >= 0; i--)
        {
            if (Table[i][col].GetComponent<Chess>().chessState == 0)
            {
                break;
            }
            if (Table[i][col].GetComponent<Chess>().chessState != curPlayer)
            {
                pendingChesses.Add(Table[i][col]);
            }
            if (Table[i][col].GetComponent<Chess>().chessState == curPlayer)
            {
                setChesses.AddRange(pendingChesses);
                break;
            }
        }
        pendingChesses.Clear();

        //遍历下侧
        for (int i = row + 1; i < rows; i++)
        {
            if (Table[i][col].GetComponent<Chess>().chessState == 0)
            {
                break;
            }
            if (Table[i][col].GetComponent<Chess>().chessState != curPlayer)
            {
                pendingChesses.Add(Table[i][col]);
            }
            if (Table[i][col].GetComponent<Chess>().chessState == curPlayer)
            {
                setChesses.AddRange(pendingChesses);
                break;
            }
        }
        pendingChesses.Clear();

        //遍历左上
        for (int i = col - 1, j = row - 1; i >= 0 && j >= 0; i--, j--)
        {
            if (Table[j][i].GetComponent<Chess>().chessState == 0)
            {
                break;
            }
            if (Table[j][i].GetComponent<Chess>().chessState != curPlayer)
            {
                pendingChesses.Add(Table[j][i]);
            }
            if (Table[j][i].GetComponent<Chess>().chessState == curPlayer)
            {
                setChesses.AddRange(pendingChesses);
                break;
            }
        }
        pendingChesses.Clear();

        //遍历右上
        for (int i = col + 1, j = row - 1; i < cols && j >= 0; i++, j--)
        {
            if (Table[j][i].GetComponent<Chess>().chessState == 0)
            {
                break;
            }
            if (Table[j][i].GetComponent<Chess>().chessState != curPlayer)
            {
                pendingChesses.Add(Table[j][i]);
            }
            if (Table[j][i].GetComponent<Chess>().chessState == curPlayer)
            {
                setChesses.AddRange(pendingChesses);
                break;
            }
        }
        pendingChesses.Clear();

        //遍历左下
        for (int i = col - 1, j = row + 1; i >= 0 && j < rows; i--, j++)
        {
            if (Table[j][i].GetComponent<Chess>().chessState == 0)
            {
                break;
            }
            if (Table[j][i].GetComponent<Chess>().chessState != curPlayer)
            {
                pendingChesses.Add(Table[j][i]);
            }
            if (Table[j][i].GetComponent<Chess>().chessState == curPlayer)
            {
                setChesses.AddRange(pendingChesses);
                break;
            }
        }
        pendingChesses.Clear();

        //遍历左上
        for (int i = col + 1, j = row + 1; i < cols && j < rows; i++, j++)
        {
            if (Table[j][i].GetComponent<Chess>().chessState == 0)
            {
                break;
            }
            if (Table[j][i].GetComponent<Chess>().chessState != curPlayer)
            {
                pendingChesses.Add(Table[j][i]);
            }
            if (Table[j][i].GetComponent<Chess>().chessState == curPlayer)
            {
                setChesses.AddRange(pendingChesses);
                break;
            }
        }
        pendingChesses.Clear();
        if (setChesses.Count == 0 || Table[row][col].ifSet()) canSet = false;
        else canSet = true;
        return setChesses;
    }

    public bool checkVaild()
    {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                bool canSet;
                check(i, j, out canSet);
                if (canSet)
                {
                    return true;
                }
            }
        }
        Debug.Log("False");
        return false;
    }

    public void CheckChampion()
    {
        int black = 0;
        int white = 0;
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                if (Table[i][j].chessState == 1) black++;
                if (Table[i][j].chessState == 2) white++;
            }
        }
        UI.SetActive(true);
        if (black>white)
        {
//win.Play();
            Wins.text = "黑方获胜";
            Debug.Log("Black wins!");
        }
        else
        {
            //fail.Play();
            Wins.text = "白方获胜";
            Debug.Log("White wins!");
        }
    }
    public void printScore()
    {
        int black = 0;
        int white = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (Table[i][j].chessState == 1) black++;
                if (Table[i][j].chessState == 2) white++;
            }
        }
        blackScore.text = black.ToString();
        whiteScore.text = white.ToString();

    }
}
