using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartServer : MonoBehaviour {

	// Starting server
	public Transform FormServer;
	public HostData[] data;
	public Transform[] ButtonRoom;
	public Text[] TextNameRoom;
	public string NamePlayer;
	public bool OkShoWGUI = true;
	public Transform TankObj;
	public Transform PlaceCreate;
	public Transform Cam;
	public Transform InTank;//used for text

	public void NewStartServer(){
			var useNat = !Network.HavePublicAddress ();//if Nat of connection on useNat =1, else return its connecction address(nat)
			Network.InitializeServer (50, 25000, useNat);
			MasterServer.RegisterHost ("AliPender", "Tank", "Unity");//sending server to clients to connect
	}

	//Find a way to connect!
	public void refresh(){
		//Network.Disconnect (200); at most 200 clients will be disconnected
		MasterServer.RequestHostList ("AliPender");//sending request to connect to server
		data = MasterServer.PollHostList ();// data = IP and Port which had been registered before 
		for (int i=0; i<data.Length ; i++) {
			ButtonRoom[i].gameObject.SetActive(true);//create buttons based on client's data
			TextNameRoom[i].text = data[i].gameName;// client's text = tank!
		}
	}

	//connection
	public void ConnectToRoom(int NumberRooM){
		for (var i=0; i<data.Length; i++) {
			Network.Connect(data[NumberRooM].ip,data[NumberRooM].port);//Process of connecting for each client
		}
	}


	void OnGUI(){ //Name selecting and removing buttons
		if (Network.peerType != NetworkPeerType.Disconnected) {
			FormServer.gameObject.SetActive(false);//to remove button when connected to the server
			if(OkShoWGUI){
			if(GUILayout.Button("CreateName")&& NamePlayer!= ""){//we select a name for the tank
					InTank=Network.Instantiate(TankObj,PlaceCreate.transform.position,Quaternion.identity,0) as Transform;
					InTank.GetComponent<CtrlTank>().Cam = Cam;// Cam OK!

					OkShoWGUI=false;// The name disappears!
			}
				NamePlayer = GUILayout.TextField(NamePlayer);
			}
			if(InTank){
			InTank.GetComponent<NetworkView>().RPC("NamePlayer",RPCMode.All,NamePlayer);//The name apears above the tank(it's like send message but it's used for netwok issue
			}
			else{//The ability to revive
				OkShoWGUI = true;
			}
		}

	}
}
