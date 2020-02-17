using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int whoseTurn; // 0 = x and 1 = o
    public int turnCount; // Counts the number of turns played
    public GameObject[] turnIcons; // Displays whose turns it is
    public Sprite[] playerIcons; // 0 = x icon and 1 = o icon
    public Button[] tictactoesSpaces; // playable spaces for our game
    public int[] markedSpaces; // ID's which space was marked by each player
    public Text winnerText; // Hold the text component of the winner text;
    public GameObject[] winningLine; // Hold all the different line for show that there is a winner
    public GameObject winnerPanel;
    public int xPlayersScore;
    public int oPlayersScore;
    public Text xPlayersScoreText;
    public Text oPlayersScoreText;
    public Button xPlayersButton;
    public Button oPlayersButton;
    public GameObject catImage;


    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for(int i = 0; i < tictactoesSpaces.Length; i++)
        {
            tictactoesSpaces[i].interactable = true;
            tictactoesSpaces[i].GetComponent<Image>().sprite = null;
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

    public void TicTacToeButton(int WhichNumber)
    {
        xPlayersButton.interactable = false;
        oPlayersButton.interactable = false;
        tictactoesSpaces[WhichNumber].image.sprite = playerIcons[whoseTurn];
        tictactoesSpaces[WhichNumber].interactable = false;

        markedSpaces[WhichNumber] = whoseTurn + 1;
        turnCount++;
        if(turnCount > 4)
        {
            bool isWinner = WinnerCheck();
            if (turnCount == 9 && isWinner == false)
            {
                Cat();
            }
        }

        if(whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    bool WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8 ];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for(int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3*(whoseTurn+1))
            {
                WinnerDisplay(i);
                return true;
            }
        }
        return false;
    }

    void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(false);
        if(whoseTurn == 0)
        {
            xPlayersScore++;
            xPlayersScoreText.text = xPlayersScore.ToString();
            winnerText.text = "Player X Wins!";
        }
        else if (whoseTurn == 1)
        {
            oPlayersScore++;
            oPlayersScoreText.text = oPlayersScore.ToString();
            winnerText.text = "Player O Wins!";
        }
        winningLine[indexIn].SetActive(true);
    }
    
    public void Rematch()
    {
        GameSetup();
        for(int i = 0; i < winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        xPlayersButton.interactable = true;
        oPlayersButton.interactable = true;
        catImage.SetActive(false);
    }

    public void Restart()
    {
        Rematch();
        xPlayersScore = 0;
        oPlayersScore = 0;
        xPlayersScoreText.text = "0";
        oPlayersScoreText.text = "0";
    }

    public void SwitchPlayer(int whichPlayer)
    {
        if(whichPlayer == 0)
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if(whichPlayer == 1)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }
    void Cat()
    {
        winnerPanel.SetActive(true);
        catImage.SetActive(true);
        winnerText.text = "CAT";
    }
}
