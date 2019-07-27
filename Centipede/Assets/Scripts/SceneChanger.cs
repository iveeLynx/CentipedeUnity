using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    /*
     * Script for changing scenes (Menu screen, Game screen, Game over screen)
     */

    public GameObject text;
    public GameObject exitText;
    public GameObject score;

    /*
     * OnPress - changes color of the text to light-blue when button pressed
     * OnRelease - change color back to yellow
     */
    public void OnPress(string button)
    {
        switch (button)
        {
            case "0": text.GetComponent<Text>().color = new Color(0, 255, 3, 255); ; break;
            case "1": exitText.GetComponent<Text>().color = new Color(0, 255, 3, 255); ; break;
        }
    }

    public void OnRelease(string button)
    {
        switch (button)
        {
            case "0": text.GetComponent<Text>().color = new Color(255, 242, 0, 255); break;
            case "1": exitText.GetComponent<Text>().color = new Color(255, 242, 0, 255); break;
        }
    }

    public void MenuOption(string option)
    {
        switch (option)
        {
            case "StartGame": SceneManager.LoadScene(1); break;
            case "Menu": SceneManager.LoadScene(0); break;
            case "Exit": Application.Quit(); break;
        }
    }

    public void FinalScore()
    {
        SceneManager.LoadScene(2);
    }



}
