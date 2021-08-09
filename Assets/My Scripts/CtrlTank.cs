using UnityEngine;
using System.Collections;

public class CtrlTank : MonoBehaviour {

	public Transform CamPlace;
	public Transform Cam;
	public float x;
	public string Name;
	public TextMesh NameText;
	public Rigidbody Bomb;
	public Transform PlaceCreateBomb;
	public float Health = 100;
	public TextMesh HealthText;

	// Update is called once per frame
	void Update () {
		HealthText.text = Health.ToString();
		if (Health < 1) {
			Destroy(this.gameObject);// The tank destroys!
		}
		if (Cam) {
			Cam.transform.position = Vector3.Lerp (Cam.transform.position, CamPlace.transform.position, Time.deltaTime * 2);//setting the position of the camera
			Cam.transform.LookAt (this.transform.position);
			x += Input.GetAxis("Mouse X");
			transform.rotation=Quaternion.Euler(0f,x,0f);// The tank looks around!
			transform.Translate(0f,0f,Input.GetAxis("Vertical")*0.2f);// the tank moves forward and backward!
		}
		NetworkView nView = GetComponent<NetworkView>();// The new scripting mode!
		if (nView.isMine) {//Only one of the tanks will throw the bombs
			if (Input.GetMouseButtonDown (0)) {//process of throwing bomb
				Rigidbody InBomb = Network.Instantiate (Bomb, PlaceCreateBomb.transform.position, PlaceCreateBomb.transform.rotation, 0) as Rigidbody;
				InBomb.AddForceAtPosition (PlaceCreateBomb.transform.forward * 2000f, PlaceCreateBomb.transform.position);
			}
		}
		if (Name != "") {
			NameText.text = Name;//the text above the tank= name
		}
	}
	[RPC]//it declares that the function below in based on Network!
	void NamePlayer(string InNamePlayer){
		Name = InNamePlayer;
	}

	[RPC]
	void HealthManager(){
		Health -= 5;
	}
}