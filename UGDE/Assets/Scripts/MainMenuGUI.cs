using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MainMenuGUI : MonoBehaviour
{
  public AudioClip beep;
  public GUISkin menuSkin;
  public Rect menuArea;
  public Rect playButton;
  public Rect instructionsButton;
  public Rect wbidButton;
  public Rect quitButton;
  public Rect instructions;

  private enum menuPage
  {
    main,
    instructions,
    wbidLogin
  };

  private Rect menuAreaNormalized;
  private menuPage currentPage = menuPage.main;

  private string wbid = "WBID";
  private string password = "PASSWORD";

  void Start()
  {
    menuAreaNormalized = new Rect(
      menuArea.x * Screen.width - (menuArea.width * 0.5f),
      menuArea.y * Screen.height - (menuArea.height * 0.5f),
      menuArea.width,
      menuArea.height);
  }

  void OnGUI()
  {
    GUI.skin = menuSkin;

    GUI.BeginGroup(menuAreaNormalized);

    if (currentPage == menuPage.main)
    {
      if (GUI.Button(playButton, "Play"))
      {
        StartCoroutine("ButtonAction", "Island");
      }
      if (GUI.Button(instructionsButton, "Instructions"))
      {
        audio.PlayOneShot(beep);
        currentPage = menuPage.instructions;
      }
      if (GUI.Button(wbidButton, "WBID Login"))
      {
        audio.PlayOneShot(beep);
        currentPage = menuPage.wbidLogin;
      }
      if (GUI.Button(quitButton, "Quit"))
      {
        StartCoroutine("ButtonAction", "quit");
      }
    }
    else if (currentPage == menuPage.instructions)
    {
      GUI.Label(instructions, "You awake on a mysterious island... Find a way to signal for help or face certain doom!");
      if (GUI.Button(quitButton, "Back"))
      {
        audio.PlayOneShot(beep);
        currentPage = menuPage.main;
      }
    }
    else if (currentPage == menuPage.wbidLogin)
    {
      GUI.Label(instructions, "Please enter your WBID email and password");

      wbid = GUI.TextField(new Rect(instructions.x, instructions.y + 50.0f, wbidButton.width, wbidButton.height), wbid);
      password = GUI.PasswordField(new Rect(instructions.x, instructions.y + 100.0f, wbidButton.width, wbidButton.height), password, '*');

      if (GUI.Button(new Rect(instructions.x, instructions.y + 150.0f, wbidButton.width, wbidButton.height), "Submit"))
      {
        var wbidAuthenticator = gameObject.GetComponent("WbidAuthenticator") as WbidAuthenticator;
        wbidAuthenticator.AuthenticateWbid(wbid, password);
      }

      if (GUI.Button(new Rect(instructions.x, instructions.y + 200.0f, wbidButton.width, wbidButton.height), "Cancel"))
      {
        audio.PlayOneShot(beep);
        currentPage = menuPage.main;
      }
    }

    GUI.EndGroup();
  }

  IEnumerator ButtonAction(string levelName)
  {
    audio.PlayOneShot(beep);
    yield return new WaitForSeconds(0.35f);

    if (levelName != "quit")
    {
      Application.LoadLevel(levelName);
    }
    else
    {
      Application.Quit();
    }
  }
}
