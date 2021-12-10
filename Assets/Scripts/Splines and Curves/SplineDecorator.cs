using UnityEngine;

public class SplineDecorator : MonoBehaviour {

	public BezierSpline spline;
	public BezierSpline playerSpline = null;

	public int frequency;

	public float newSpeed;

	public bool lookForward;

	public float newValue;

	public GameObject[] items;

	private void Awake () {
		if (frequency <= 0 || items == null || items.Length == 0) {
			return;
		}
		float stepSize = frequency * items.Length;
		if (spline.Loop || stepSize == 1) {
			stepSize = 1f / stepSize;
		}
		else {
			stepSize = 1f / (stepSize - 1);
		}
		for (int p = 0, f = 0; f < frequency; f++) {
			for (int i = 0; i < items.Length; i++, p++) {
				GameObject cube = Instantiate(items[i]) as GameObject;
				Transform item = cube.transform;
				Vector3 position = spline.GetPoint(p * stepSize);
				newValue = p * stepSize;
				cube.GetComponent<RailCubeData>().newPosition = newValue;
				cube.GetComponent<RailCubeData>().playerActiveSpline = playerSpline;
				cube.GetComponent<RailCubeData>().speed = newSpeed;
				item.transform.localPosition = position;
				if (lookForward) {
					item.transform.LookAt(position + spline.GetDirection(p * stepSize));
				}
				item.transform.parent = transform;
			}
		}
	}
}