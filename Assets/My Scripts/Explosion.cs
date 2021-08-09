using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public Transform PartExplosion;// this is the fire!

	// Fire Explosion
	void OnCollisionEnter (Collision other) {
		if(other.collider.CompareTag("Player")){// 'player' is the real name of the tank!
			other.collider.GetComponent<NetworkView>().RPC("HealthManager",RPCMode.All);// Sending message to HealthManager
		}
		if (other.collider) {
			Network.Instantiate (PartExplosion, this.transform.position, Quaternion.identity, 0);
			Destroy(this.gameObject);
		}
	}
	
}
