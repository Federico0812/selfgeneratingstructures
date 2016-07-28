using UnityEngine;
using System.Collections;

public class MagicCreator : MonoBehaviour {

	public System.Random ran = new System.Random();
	public GameObject character;
	public GameObject innerTotem;
	public GameObject selfTotem;
	private int timer;
	private int stepCounter;
	private int floorCounter;
	private int stoneCounter;
	private bool shapeFinished;
	private float previousAngle;
	private float fixedAngle;
	private float firstDesviationAngle;
	private float secondDesviationAngle;
	private float fixedBaseExtension;
	private float fixedMountainHeight;

	void Start () {
		stepCounter = 0;
		timer = 0;
		previousAngle = 0;
		shapeFinished = false;
		fixedAngle = 0;

		fixedBaseExtension = (float)ran.Next (100, 280) / 150.0f;
		fixedMountainHeight = (float)ran.Next (100, 180) / 300.0f;
	}

	void Update () {
		Vector3 vectorCharacter = character.transform.position;
		Vector3 vectorTotem = transform.position;
		Vector2 direction = new Vector2(vectorTotem.x - vectorCharacter.x, vectorTotem.z - vectorCharacter.z);
		float distance = direction.magnitude;


		if (distance < 20) {

			if (timer == 20) {
				timer = 0;
			}

			if (timer % 5 == 0) {
				floorBuild ();

			}

			if (timer == 15) {
				nextGeneratorBuild ();

			}
			
			if (timer == 0 || timer == 10) {
				towerBuild ();
			}

			timer++;
		}

	}

	void towerBuild () {
		stepCounter++;

		if (stepCounter > 20) {
			return;
		}

		Vector3 initialPosition = transform.position;

		int randomPlus = ran.Next(20, 40);
		float newAngle = previousAngle + randomPlus;
		previousAngle = newAngle;
		float desviation = 1.0f / stepCounter; 
		Vector2 vector = new Vector2 (Mathf.Sin (newAngle) * desviation, Mathf.Cos (newAngle) * desviation);

		float height = (float) stepCounter;
		Vector3 relativePosition = new Vector3 (vector.x, height *fixedMountainHeight, vector.y);
		Vector3 finalPosition = initialPosition + relativePosition;


		GameObject newBox = Instantiate(innerTotem, finalPosition , Quaternion.identity) as GameObject;

		float variation = (float) Mathf.Sqrt(stepCounter);
		float width =  fixedBaseExtension * 10 / variation;

		if (stepCounter > 10) {
		
			width = width / (stepCounter - 10);
		
		}

		newBox.transform.localScale = new Vector3 (width,1*fixedMountainHeight, width);
		newBox.transform.parent = transform;
	}


	void floorBuild () {

		floorCounter++;

		if (floorCounter > 20) {
			return;
		}

		Vector3 initialPosition = transform.position;

		int randomPlus = ran.Next(20, 40);
		float newAngle = previousAngle + randomPlus;
		previousAngle = newAngle;
		Vector2 vector = new Vector2 (Mathf.Sin (newAngle) * 2 *fixedBaseExtension, Mathf.Cos (newAngle) * 2 * fixedBaseExtension);

		float height = (float) 1.5f - (floorCounter / (20 / 5.0f));
		Vector3 relativePosition = new Vector3 (vector.x, height, vector.y);
		Vector3 finalPosition = initialPosition + relativePosition;

		GameObject newBox = Instantiate(innerTotem, finalPosition , Quaternion.identity) as GameObject;

		float variation = (float) Mathf.Pow(floorCounter, 2)/200.0f + 1;
		float width =  5 * variation * fixedBaseExtension;

		newBox.transform.localScale = new Vector3 (width,3, width);
		newBox.transform.parent = transform;
	}


	void nextGeneratorBuild () {

		if (shapeFinished) {
			return;
		}

		float degreeToRadians = 2 * 3.1415926f / 360.0f;

		if (fixedAngle == 0) {
			fixedAngle = (float) ran.Next(0, 360) * degreeToRadians;
			firstDesviationAngle = fixedAngle + (float)ran.Next (60, 180) * degreeToRadians;
			secondDesviationAngle = fixedAngle - (float)ran.Next (60, 180) * degreeToRadians;
		}

		stoneCounter++;
		int maxStones = ran.Next (0, 10) + 13;
		if (stoneCounter > maxStones) {

			createTotemWithAngle (fixedAngle);
			createTotemWithAngle (firstDesviationAngle);
			createTotemWithAngle (secondDesviationAngle);
			shapeFinished = true;

			return;
		}

		createBoxWithAngle (fixedAngle);
		createBoxWithAngle (firstDesviationAngle);
		createBoxWithAngle (secondDesviationAngle);

	}

	void createBoxWithAngle (float angle) {

		Vector3 initialPosition = transform.position;
		float distanceToCenter = stoneCounter * 2.0f;

		Vector2 vector = new Vector2 (Mathf.Sin (angle) * distanceToCenter, Mathf.Cos (angle) * distanceToCenter);

		Vector3 relativePosition = new Vector3 (vector.x, -3.25f, vector.y);
		Vector3 finalPosition = initialPosition + relativePosition;

		GameObject newBox = Instantiate(innerTotem, finalPosition , Quaternion.identity) as GameObject;

		float width =  ran.Next(50, 150) / 50.0f;

		newBox.transform.localScale = new Vector3 (width,2, width);
		newBox.transform.parent = transform;
	
	}

	void createTotemWithAngle (float angle) {

		Vector3 initialPosition = transform.position;
		float distanceToCenter = stoneCounter * 2.0f + 3;

		Vector2 vector = new Vector2 (Mathf.Sin (angle) * distanceToCenter, Mathf.Cos (angle) * distanceToCenter);

		Vector3 relativePosition = new Vector3 (vector.x, 0, vector.y);
		Vector3 finalPosition = initialPosition + relativePosition;

		GameObject genericTotem = GameObject.FindWithTag("Box");

		GameObject newBox = Instantiate(genericTotem, finalPosition , Quaternion.identity) as GameObject;

		newBox.transform.localScale = new Vector3 (5,5,5);
		newBox.transform.parent = transform;

	}



	void clusterBuild () {

		if (shapeFinished) {
			return;
		}

		float stopProbability = timer / 3.0f;
		int randomValue = ran.Next(0, 100);

		if (randomValue < stopProbability) {
			shapeFinished = true;


			return;
		}



		Vector3 initialPosition = transform.position;

		fixedAngle = (float) ran.Next(0, 360);
		float distanceToCenter = timer * 10.0f;

		Vector2 vector = new Vector2 (Mathf.Sin (fixedAngle) * distanceToCenter, Mathf.Cos (fixedAngle) * distanceToCenter);

		Vector3 relativePosition = new Vector3 (vector.x, -2.5f, vector.y);
		Vector3 finalPosition = initialPosition + relativePosition;

		GameObject newBox = Instantiate(innerTotem, finalPosition , Quaternion.identity) as GameObject;

		float width =  ran.Next(50, 150) / 50.0f;

		newBox.transform.localScale = new Vector3 (width,2, width);
		newBox.transform.parent = transform;

	}

}
