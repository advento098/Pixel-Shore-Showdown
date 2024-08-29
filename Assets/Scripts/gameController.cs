using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    // GameObjects for handling the winning line visuals
    public GameObject winningLineParent;
    public GameObject winningLine;

    // Variables to manage player turns and the game state
    public int playerTurn;
    public int playerTurnCount;

    // UI elements for displaying player turn and scores
    public TextMeshProUGUI playerTurnIndicator;
    public TextMeshProUGUI[] playerTextScores;

    // Array to store player scores
    public int[] playerScores;

    // Array to store player turn icons (e.g., X and O sprites)
    public Sprite[] playerTurnIcons;

    // Array of buttons representing the game board
    public Button[] boardButtons;

    // Colors for customizing UI elements
    public Color[] colors;

    // Array to keep track of marked spaces on the board
    public int[] markedSpaces;

    // Default sprite for the board buttons
    public Sprite defaultButtonSprite;

    void Start()
    {
        // Initialize player scores based on the current score displayed in the UI
        playerScores = new int[] { int.Parse(playerTextScores[0].text), int.Parse(playerTextScores[1].text) };

        // Set the starting player to player 1 (X)
        playerTurn = 0;
        playerTurnCount = 0;

        // Set up the board by making all buttons interactable and resetting their sprites
        for (int i = 0; i < boardButtons.Length; i++)
        {
            boardButtons[i].interactable = true;
            boardButtons[i].GetComponent<Image>().sprite = null;
        }

        // Initialize all marked spaces to an invalid value (-100)
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame (currently empty but available for future use)
    void Update()
    {

    }

    // Function called when a board button is clicked
    public void OnClickBoardButton(int buttonIndex)
    {
        // Play sound effect for player move
        FindObjectOfType<AudioManager>().Play("Player Move");

        // Set the sprite for the clicked button based on the current player's icon
        boardButtons[buttonIndex].image.sprite = playerTurnIcons[playerTurn];
        boardButtons[buttonIndex].interactable = false;
        playerTurnCount++;

        // Mark the space with the current player's identifier (1 or 2)
        markedSpaces[buttonIndex] = playerTurn + 1;

        // Check for a winner if there have been at least 4 moves
        if (playerTurnCount >= 4 && playerTurnCount <= 9)
        {
            WinnerChecker();

            // If it's the last move and there's no winner, restart the game
            if (playerTurnCount == 9 && GameObject.FindGameObjectWithTag("Winner Line") == null)
            {
                RestartButton();
                Debug.Log("Game Over");
            }
        }

        // Switch the turn to the other player and update the UI
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

    // Function to check if there is a winning combination on the board
    void WinnerChecker()
    {
        // Calculate the sum of values in each possible winning combination
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        // Store all possible winning combinations in an array
        int[] solutions = { s1, s2, s3, s4, s5, s6, s7, s8 };

        // Iterate through the possible solutions to find a winner
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (playerTurn + 1))
            {
                // Play sound effect for winning
                FindObjectOfType<AudioManager>().Play("Winning Sound");
                Debug.Log("Player " + (playerTurn + 1) + " wins");

                // Update the winner's score in the UI
                playerTextScores[playerTurn].text = (playerScores[playerTurn] + 1).ToString();

                // Display the winning line based on the winning combination
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
                        GameObject winningLinePos2 = Instantiate(winningLine, new Vector3(0, winningLineParent.transform.position.y - 2.15f, 0), Quaternion.identity, winningLineParent.transform);
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

                // Disable interaction with the remaining board buttons and make them transparent
                for (int j = 0; j < boardButtons.Length; j++)
                {
                    if (markedSpaces[j] == -100)
                    {
                        boardButtons[j].interactable = false;
                        boardButtons[j].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    }
                }
            }
        }
    }

    // Function to restart the game
    public void RestartButton()
    {
        // Play sound effect for button click
        FindObjectOfType<AudioManager>().Play("Button Click");

        // Destroy any existing winning line visuals
        GameObject[] winningLineObjects = GameObject.FindGameObjectsWithTag("Winner Line");

        // Reset all board buttons to be interactable, with no sprite and full opacity
        for (int i = 0; i < boardButtons.Length; i++)
        {
            boardButtons[i].interactable = true;
            boardButtons[i].GetComponent<Image>().sprite = defaultButtonSprite;
            boardButtons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        // Reinitialize the marked spaces to an invalid value (-100)
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }

        // Destroy the winning line objects
        foreach (GameObject obj in winningLineObjects)
        {
            Destroy(obj);
        }

        // Reset player scores from the UI, and reset the game state to start
        playerScores = new int[] { int.Parse(playerTextScores[0].text), int.Parse(playerTextScores[1].text) };
        playerTurn = 0;
        playerTurnCount = 0;

        // Set the turn indicator back to player 1 (X)
        playerTurnIndicator.text = "X";
    }
}
