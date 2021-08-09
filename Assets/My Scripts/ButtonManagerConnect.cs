using UnityEngine;
using System.Collections;

public class ButtonManagerConnect : MonoBehaviour {

	public int NumberConnect ;
	public Transform ManagerServer;

	public void SendConnect(){
		ManagerServer.SendMessage ("ConnectToRoom", NumberConnect);//Send numberconnect to Connection Room
	}
}
