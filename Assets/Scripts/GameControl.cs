using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public ChessControl CC;
    public int playerState;
    public enum gameState
    {
        Check,
        p1,
        p2,
        Ai1,
        Ai2
    }
    gameState state=gameState.Check;
    bool black=true, white=true;
    int row, col;
    // Start is called before the first frame update
    void Start()
    {
        CC.reStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (black || white)
        {
            switch (state)
            {
                case gameState.p1:
                    {
                        if(CC.checkVaild())
                        {
                            black = true;
                            CC.curPlayer = 1;
                            playerState = 1;
                            row = setChess(out col);
                            if (row != -1 && col != -1)
                            {
                                CC.Swap(row, col);
                                CC.printScore();
                                state = gameState.p2;
                                break;
                            }
                        }
                        else
                        {
                            black = false;
                            Debug.Log("White continue");
                            state = gameState.p2;
                            //CC.curPlayer = 2;
                            break;
                        }
                        break;
                    }

                case gameState.p2:
                    {
                        if (CC.checkVaild())
                        {
                            white = true;
                            CC.curPlayer = 2;
                            playerState = 2;
                            row = setChess(out col);
                            if (row != -1 && col != -1)
                            {
                                CC.Swap(row, col);
                                CC.printScore();
                                state = gameState.p1;
                                break;
                            }
                        }
                        else
                        {
                            white = false;
                            Debug.Log("Black continue");
                            state = gameState.p1;
                            //CC.curPlayer = 1;
                            break;
                        }
                        break;
                    }

                case gameState.Ai1:
                    {
                        if (CC.checkVaild())
                        {
                            black = true;
                            CC.curPlayer = 1;
                            playerState = 1;
                            row = setChess(out col);
                            if (row != -1 && col != -1)
                            {
                                CC.Swap(row, col);
                                CC.printScore();
                                state = gameState.Ai2;
                                break;
                            }
                        }
                        else
                        {
                            black = false;
                            Debug.Log("White continue");
                            state = gameState.Ai2;
                            //CC.curPlayer = 2;
                            break;
                        }
                        break;
                    }

                case gameState.Ai2:
                    {
                        if (CC.checkVaild())
                        {
                            white = true;
                            CC.curPlayer = 2;
                            playerState = 2;
                            int row, col;
                            AiSetChess(out row, out col);
                            if (row != -1 && col != -1)
                            {
                                CC.Swap(row, col);
                                CC.printScore();
                                state = gameState.Ai1;
                                break;
                            }
                        }
                        else
                        {
                            white = false;
                            Debug.Log("Black continue");
                            state = gameState.Ai1;
                            //CC.curPlayer = 1;
                            break;
                        }
                        break;
                    }

            }
        }
        else
        {
            CC.CheckChampion();
        }
    }

    int setChess(out int col)
    {
        int tempRow = -1;
        col = -1;
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 ScreenPostion = Input.mousePosition;

            Ray Mray = Camera.main.ScreenPointToRay(ScreenPostion);
            RaycastHit Mhit;
            Debug.DrawRay(Mray.origin, Mray.direction * 1000, Color.white, 100, true);
            if (Physics.Raycast(Mray, out Mhit))
            {
                if (Mhit.collider.tag == "Chess")
                {
                    col = Mhit.collider.GetComponent<Chess>().col;
                    tempRow = Mhit.collider.GetComponent<Chess>().row;
                    bool canSet;
                    CC.check(tempRow, col, out canSet);
                    if (canSet)
                    {
                        Mhit.collider.GetComponent<Chess>().setTrue(playerState);
                        return tempRow;
                    }
                }
            }
        }
        
        return -1;
    }
    void AiSetChess(out int row, out int col)
    {
        row = -1;
        col = -1;
        bool canSet;
        for (int i = 0; i < CC.rows; i++)
        {
            for (int j = 0; j < CC.cols; j++)
            {
                CC.check(i, j, out canSet);
                if (canSet)
                {
                    row = i;
                    col = j;
                    CC.Table[i][j].setTrue(playerState);
                    return;
                }
            }
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void reLoad()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void AIGame()
    {
        state = gameState.Ai1;
    }

    public void NormalGame()
    {
        state = gameState.p1;
    }
}
