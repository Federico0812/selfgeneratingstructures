using UnityEngine;
using System.Collections;

public class RedMagicCreator : MonoBehaviour {

	public System.Random ran = new System.Random();
	public GameObject character;
	public GameObject redtotem;
	private int timer;
	private int stepCounter;

	// Use this for initialization
	void Start () {
		timer = 0;
		stepCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vectorCharacter = character.transform.position;
		Vector3 vectorTotem = transform.position;
		Vector2 direction = new Vector2(vectorTotem.x - vectorCharacter.x, vectorTotem.z - vectorCharacter.z);
		float distance = direction.magnitude;


		if (distance < 35) {

			if (timer == 20) {
				createWalls ();
				timer = 0;
			}
			timer++;
		}

	}
		
	void createWalls () {


		int openDoor = ran.Next (0, 5);
		createWallWithDoorOpen (0, 1, openDoor == 1);
		createWallWithDoorOpen (0, -1, openDoor == 2);
		createWallWithDoorOpen (1, 0, openDoor == 3);
		createWallWithDoorOpen (-1, 0, openDoor == 4);

		Debug.Log ("Door open? " + openDoor);

		stepCounter++;

	}

	void createWallWithDoorOpen (float x, float z, bool openDoor) {
	
		if (openDoor) {
			createWallWithDoor (x, z);
		} else {
			createWall (x, z);
		}

	}

	void createWall (float x, float z) {
		float relativeDistance = 15.0f;

		Vector3 position1 = new Vector3 (x * relativeDistance,5*stepCounter,z * relativeDistance);

		Vector3 defaultScale1 = new Vector3 (1,1,1);

		if (x != 0) {
			defaultScale1 = new Vector3 (1, 5, relativeDistance*2*x);
		}
		if (z != 0) {
			defaultScale1 = new Vector3 (relativeDistance*2*z, 5, 1);
		}

		createBox (position1, Quaternion.identity, defaultScale1);
	
	}

	void createWallWithDoor (float x, float z) {
		float relativeDistance = 15.0f;
		float ratioDoor = (2.0f / 3.0f);

		float xposleft = x * relativeDistance + z * ratioDoor * relativeDistance;
		float zposleft = z * relativeDistance + x * ratioDoor * relativeDistance;
		float xposright = x * relativeDistance - z * ratioDoor * relativeDistance;
		float zposright = z * relativeDistance - x * ratioDoor * relativeDistance;

		Vector3 positionleft = new Vector3 (xposleft,stepCounter*5,zposleft);
		Vector3 positionright = new Vector3 (xposright,stepCounter*5,zposright);
		Vector3 defaultScale1 = new Vector3 (1,1,1);

		if (x != 0) {
			defaultScale1 = new Vector3 (1, 5, relativeDistance* ratioDoor *x);
		}
		if (z != 0) {
			defaultScale1 = new Vector3 (relativeDistance* ratioDoor *z, 5, 1);
		}

		createBox (positionleft, Quaternion.identity, defaultScale1);
		createBox (positionright, Quaternion.identity, defaultScale1);

	}

	void createBox (Vector3 position, Quaternion rotation, Vector3 scale) {
	
		Vector3 initialPosition = transform.position;
		Vector3 finalPosition = initialPosition + position;


		GameObject newBox = Instantiate(redtotem, finalPosition , rotation) as GameObject;

		newBox.transform.localScale = scale;
		newBox.transform.parent = transform;
	}

}
