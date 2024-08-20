using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winningLineParent;
    public GameObject winningLine;
    public int playerTurn;
    public int playerTurnCount;
    public Text playerTurnIndicator;
    public Text[] playerTextScores;
    public int[] playerScores;
    public Sprite[] playerTurnIcons;
    public Button[] boardButtons;
    public Color[] colors;
    public int[] markedSpaces;
    void Start()
    {
        playerScores = new int[] { int.Parse(playerTextScores[0].text), int.Parse(playerTextScores[1].text) };
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
            if (playerTurnCount == 9 && GameObject.FindGameObjectWithTag("Winner Line") == null)
            {
                RestartButton();
                Debug.Log("Game Over");
            }
        }

        if (playerTurn == 0)
        {
            playerTurnIndicator.text = "O";
            playerTurn = 1;
        }
        else
        {
            playerTurnIndicator.text = "X";
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
                playerTextScores[playerTurn].text = (playerScores[playerTurn] + 1).ToString();
                switch (i)
                {
                    // Horizontal Winnings
                    case 0:
                        GameObject winningLinePos = Instantiate(winningLine, new Vector3(0, winningLineParent.transform.position.y + 2.15f, 0), Quaternion.identity, winningLineParent.transform);
                        break;
                    case 1:
                        GameObject winningLinePos1 = Instantiate(winningLine, new Vector3(0, winningLineParent.transform.position.y, 0), Quaternion.identity, winningLineParent.transform);
                        break;
                    case 2:
                        GameObject winningLinePos2 = Instantiate(winningLine, new Vector3(0, winningLineParent.transform.position.y + -2.15f, 0), Quaternion.identity, winningLineParent.transform);
                        break;


                    // Vertical Winnings
                    case 3:
                        GameObject winningLinePos3 = Instantiate(winningLine, new Vector3(-2.15f, winningLineParent.transform.position.y, 0), Quaternion.Euler(0, 0, 90), winningLineParent.transform);
                        break;
                    case 4:
                        GameObject winningLinePos4 = Instantiate(winningLine, new Vector3(0, winningLineParent.transform.position.y, 0), Quaternion.Euler(0, 0, 90), winningLineParent.transform);
                        break;
                    case 5:
                        GameObject winningLinePos5 = Instantiate(winningLine, new Vector3(2.15f, winningLineParent.transform.position.y, 0), Quaternion.Euler(0, 0, 90), winningLineParent.transform);
                        break;

                    // Diagonal Winnings
                    case 6:
                        GameObject winningLinePos6 = Instantiate(winningLine, new Vector3(winningLineParent.transform.position.x, winningLineParent.transform.position.y, 0), Quaternion.Euler(0, 0, 135), winningLineParent.transform);
                        break;
                    case 7:
                        GameObject winningLinePos7 = Instantiate(winningLine, new Vector3(winningLineParent.transform.position.x, winningLineParent.transform.position.y, 0), Quaternion.Euler(0, 0, 45), winningLineParent.transform);
                        break;
                    default:
                        break;
                }

                for (int j = 0; j < boardButtons.Length; j++)
                {
                    if (markedSpaces[j] == -100)
                    {
                        boardButtons[j].interactable = false;
                        boardButtons[j].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    }
                }
                // Debug.Log(playerScores[playerTurn]);
                // playerScores[playerTurn]++;
                // playerTextScores[playerTurn].text = playerScores[playerTurn].ToString();
            }
        }

    }

    public void RestartButton()
    {
        for (int i = 0; i < boardButtons.Length; i++)
        {
            boardButtons[i].interactable = true;
            boardButtons[i].GetComponent<Image>().sprite = null;
            boardButtons[i].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
        Destroy(GameObject.FindGameObjectWithTag("Winner Line"));
        playerScores = new int[] { int.Parse(playerTextScores[0].text), int.Parse(playerTextScores[1].text) };
        playerTurn = 0;
        playerTurnCount = 0;
        playerTurnIndicator.text = "X";
    }
}
