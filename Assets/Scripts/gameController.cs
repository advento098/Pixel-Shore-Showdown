using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerTurn;
    public int playerTurnCount;
    // public GameObject[] playerTurnIcons;
    public Sprite[] playerTurnIcons;
    public Button[] boardButtons;
    public int[] markedSpaces;
    void Start()
    {
        playerTurn = 0;
        playerTurnCount = 0;
        for (int i = 0; i < boardButtons.Length; i++)
        {
            boardButtons[i].interactable = true;
            boardButtons[i].GetComponent<Image>().sprite = null;
        }

        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnClickBoardButton(int buttonIndex)
    {
        boardButtons[buttonIndex].image.sprite = playerTurnIcons[playerTurn];
        boardButtons[buttonIndex].interactable = false;
        playerTurnCount++;

        markedSpaces[buttonIndex] = playerTurn + 1;

        if (playerTurnCount >= 4 && playerTurnCount <= 9)
        {
            WinnerChecker();
        }
        else if (playerTurnCount == 9)
        {
            Debug.Log("Game Over");
        }

        if (playerTurn == 0)
        {
            playerTurn = 1;
        }
        else
        {
            playerTurn = 0;
        }
    }

    void WinnerChecker()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        int[] solutions = { s1, s2, s3, s4, s5, s6, s7, s8 };

        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (playerTurn + 1))
            {
                Debug.Log("Player " + (playerTurn + 1) + " wins");
            }
        }
    }
}
