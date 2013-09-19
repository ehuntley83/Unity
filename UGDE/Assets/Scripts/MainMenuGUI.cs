using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MainMenuGUI : MonoBehaviour
{
  public AudioClip beep;
  public GUISkin menuSkin;
  public float busyIndicatorRotateSpeed;
  public Rect menuArea;
  public Rect playButton;
  public Rect instructionsButton;
  public Rect wbidButton;
  public Rect quitButton;
  public Rect busyIndicator;
  public Rect instructions;

  private enum menuPage
  {
    main,
    instructions,
    wbidLogin,
    wbidWaiting,
    wbidLoginComplete
  };

  private Rect menuAreaNormalized;
  private menuPage currentPage = menuPage.main;
  private float rotationAngle = 0.0f;
  private Vector2 pivotPoint;

  private string wbid = "WBID";
  private string password = "PASSWORD";
  private string authenticateWbidResult = String.Empty;
  private string amsTicket = String.Empty;

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

    switch (currentPage)
    {
    case menuPage.main:
      // Handle web streaming by disabling the Island until it's loaded
      if (Application.CanStreamedLevelBeLoaded("Island"))
      {
        if (GUI.Button(playButton, "Play"))
        {
          StartCoroutine("ButtonAction", "Island");
        }
      }
      else
      {
        float percentLoaded = Application.GetStreamProgressForLevel("Island") * 100;
        GUI.Box(new Rect(playButton), "Loading... " + percentLoaded.ToString("f0") + "% Loaded");
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
      // Don't show Quit button if we're in a Web Player
      if (Application.platform != RuntimePlatform.OSXWebPlayer &&
          Application.platform != RuntimePlatform.WindowsWebPlayer)
      {
        if (GUI.Button(quitButton, "Quit"))
        {
          StartCoroutine("ButtonAction", "quit");
        }
      }

      break;

    case menuPage.instructions:

      GUI.Label(instructions, "You awake on a mysterious island... Find a way to signal for help or face certain doom!");
      if (GUI.Button(quitButton, "Back"))
      {
        audio.PlayOneShot(beep);
        currentPage = menuPage.main;
      }

      break;

    case menuPage.wbidLogin:

      GUI.Label(instructions, "Please enter your WBID email and password");

      wbid = GUI.TextField(new Rect(instructions.x, instructions.y + 50.0f, wbidButton.width, wbidButton.height), wbid);
      password = GUI.PasswordField(new Rect(instructions.x, instructions.y + 100.0f, wbidButton.width, wbidButton.height), password, '*');

      if (GUI.Button(new Rect(instructions.x, instructions.y + 150.0f, wbidButton.width, wbidButton.height), "Submit"))
      {
        audio.PlayOneShot(beep);
        var wbidAuthenticator = gameObject.GetComponent("WbidAuthenticator") as WbidAuthenticator;
        wbidAuthenticator.AuthenticateWbid(wbid, password, AuthenticateWbidCallback, this);
        currentPage = menuPage.wbidWaiting;
      }

      if (GUI.Button(new Rect(instructions.x, instructions.y + 200.0f, wbidButton.width, wbidButton.height), "Cancel"))
      {
        audio.PlayOneShot(beep);
        currentPage = menuPage.main;
      }

      break;

    case menuPage.wbidWaiting:

      GUI.Label(instructions, "Logging in...");

      // Create a rotating button to show the progress
      pivotPoint = new Vector2(busyIndicator.x + (busyIndicator.width * 0.5f), busyIndicator.y + (busyIndicator.height * 0.5f));
      GUIUtility.RotateAroundPivot (rotationAngle, pivotPoint);
      GUI.Button(busyIndicator, "");
      // Reset the rotation so we only rotate the progress button
      GUIUtility.RotateAroundPivot (-rotationAngle, pivotPoint);

      if (GUI.Button(quitButton, "Cancel"))
      {
        audio.PlayOneShot(beep);
        currentPage = menuPage.main;
      }
      rotationAngle += busyIndicatorRotateSpeed;

      break;

    case menuPage.wbidLoginComplete:

      GUI.Label(instructions, authenticateWbidResult + "\n" + amsTicket);
      if (GUI.Button(quitButton, "Continue"))
      {
        audio.PlayOneShot(beep);
        currentPage = menuPage.main;
      }
      rotationAngle = 0.0f;

      break;
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

  void AuthenticateWbidCallback(object sender, AuthenticateWbidCompletedEventArgs result)
  {
    if (this == result.UserState && currentPage == menuPage.wbidWaiting)
    {
      if (null == result.Error)
      {
        amsTicket = result.Result;
        authenticateWbidResult = "Success:";
      }
      else
      {
        amsTicket = result.Error.Message;
        authenticateWbidResult = "Error:";
      }

      currentPage = menuPage.wbidLoginComplete;
      Debug.Log(amsTicket);
    }
  }
}
