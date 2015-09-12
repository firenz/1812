//This script is based on this script http://blog.adamboro.com/always-show-collider-in-unity/
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VisibleColliderInEditor : MonoBehaviour {
	
	// The Collider itself
	private PolygonCollider2D thisCollider;
	// array of collider points
	private Vector2[] pointsCollider;
	// the transform position of the collider
	private Vector3 transformCollider;
	
	private void Awake () {
		thisCollider = this.GetComponent<PolygonCollider2D>();
		pointsCollider = thisCollider.points;
		transformCollider = thisCollider.transform.position;
	}
	
	private void OnDrawGizmos() {
		this.transform.position = Vector2.zero;
		Gizmos.color = Color.blue;
		// for every point, draw line to the next point
		for(int i = 0; i < (pointsCollider.Length); i++){
			Gizmos.DrawLine(new Vector3(pointsCollider[i].x + transformCollider.x, pointsCollider[i].y + transformCollider.y), new Vector3(pointsCollider[(i + 1) % pointsCollider.Length].x + transformCollider.x, pointsCollider[(i + 1) % pointsCollider.Length].y + transformCollider.y));
			Gizmos.DrawLine(new Vector3(pointsCollider[(i + 1) % pointsCollider.Length].x + transformCollider.x, pointsCollider[(i + 1) % pointsCollider.Length].y + transformCollider.y), new Vector3(pointsCollider[(i + 2) % pointsCollider.Length].x + transformCollider.x, pointsCollider[(i + 2) % pointsCollider.Length].y + transformCollider.y));
		}
	}

	private void OnDrawGizmosSelected() {
		this.transform.position = Vector2.zero;
		Gizmos.color = Color.blue;
		// for every point, draw line to the next point
		for(int i = 0; i < (pointsCollider.Length); i++){
			Gizmos.DrawLine(new Vector3(pointsCollider[i].x + transformCollider.x, pointsCollider[i].y + transformCollider.y), new Vector3(pointsCollider[(i + 1) % pointsCollider.Length].x + transformCollider.x, pointsCollider[(i + 1) % pointsCollider.Length].y + transformCollider.y));
			Gizmos.DrawLine(new Vector3(pointsCollider[(i + 1) % pointsCollider.Length].x + transformCollider.x, pointsCollider[(i + 1) % pointsCollider.Length].y + transformCollider.y), new Vector3(pointsCollider[(i + 2) % pointsCollider.Length].x + transformCollider.x, pointsCollider[(i + 2) % pointsCollider.Length].y + transformCollider.y));
        }
    }
}
