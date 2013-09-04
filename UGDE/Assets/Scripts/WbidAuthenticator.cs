using UnityEngine;
using System;
using System.Collections;

public class WbidAuthenticator : MonoBehaviour
{
  public string ClsUrl = "http://vm-sqa-temig01.test.turbine.com/CLS/WbAuthentication.asmx";

  private WbAuthentication wbAuthentication;

  // Use this for initialization
  void Start ()
  {
    // WCF service
    // MyServiceClient client = new MyService(new BasicHttpBinding(),
    // new EndpointAddress("http://localhost:8080/MyService.svc"));

    // ASMX service
    wbAuthentication = new WbAuthentication();
    wbAuthentication.Url = ClsUrl;
    wbAuthentication.AuthenticateWbidCompleted += AuthenticateWbidCallback;
  }

  public void AuthenticateWbid(string wbid, string password)
  {
    wbAuthentication.AuthenticateWbidAsync(String.Empty, "WBID", Guid.NewGuid().ToString(), wbid, password, "TAS", Guid.NewGuid().ToString(), this);
    Debug.Log("CALLED!");
  }

  void AuthenticateWbidCallback(object sender, AuthenticateWbidCompletedEventArgs args)
  {
    Debug.Log("RECEIVED!");
    Debug.Log("Errors: " + args.Error);
    Debug.Log("Ticket: " + args.Result);
  }
}
