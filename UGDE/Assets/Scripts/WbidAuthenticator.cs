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
  }

  public void AuthenticateWbid(string wbid, string password, AuthenticateWbidCompletedEventHandler callback, object context)
  {
    wbAuthentication.AuthenticateWbidCompleted += (null != callback) ? callback : AuthenticateWbidCallback;
    wbAuthentication.AuthenticateWbidAsync(String.Empty, "WBID", Guid.NewGuid().ToString(), wbid, password, "TAS", Guid.NewGuid().ToString(), context);
    Debug.Log("AuthenticateWbid Called");
  }

  void AuthenticateWbidCallback(object sender, AuthenticateWbidCompletedEventArgs args)
  {
    Debug.Log("AuthenticateWbid Callback Received");
    Debug.Log("Errors: " + args.Error);
    Debug.Log("Ticket: " + args.Result);
  }
}
