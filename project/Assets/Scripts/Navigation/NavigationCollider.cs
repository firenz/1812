using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
public class NavigationCollider : MonoBehaviour {
	public int colliderID{
		get{
			return ID;
		}
		private set {}
	}

	public Transform centroid {get; private set;}
	public Vector2 centroidPosition {get; private set;}

	[SerializeField]
	private int ID = -1;

	private PolygonCollider2D thisCollider;
	private List<int> connectedLinkersID;

	private void Awake(){
		connectedLinkersID = new List<int>();
		this.name = "Collider_" + ID;
		this.tag = "NavigationCollider";
		this.transform.position = Vector2.zero;
		thisCollider = this.GetComponent<PolygonCollider2D>();

		CalculateCentroid();
	}

	//Calculate the centroid of a non-self intersecting closed polygon
	//more info https://en.wikipedia.org/wiki/Centroid#Centroid_of_polygon
	public void CalculateCentroid(){

		Vector2[] _vertices = thisCollider.points;
		int _totalVertices = _vertices.Length;
		Vector2 _centroid = Vector2.zero;
		Vector2 _currentVertex = Vector2.zero;
		Vector2 _nextVertex = Vector2.zero;
		float _polygonSignedArea = 0f;
		float _partialSignedArea = 0f;
		
		//For all vertices except last
		for(int i = 0; i < (_totalVertices - 1); i++){
			_currentVertex = _vertices[i];
			_nextVertex = _vertices[i+1];
			_partialSignedArea = _currentVertex.x * _nextVertex.y - _nextVertex.x * _currentVertex.y;
			_polygonSignedArea += _partialSignedArea;
			_centroid.x += (_currentVertex.x + _nextVertex.x) * _partialSignedArea;
			_centroid.y += (_currentVertex.y + _nextVertex.y) * _partialSignedArea;
		}
		
		//Last vertex operation
		_currentVertex = _vertices[_totalVertices - 1];
		_nextVertex = _vertices[0];
		_partialSignedArea = _currentVertex.x * _nextVertex.y - _nextVertex.x * _currentVertex.y;
		_polygonSignedArea += _partialSignedArea;
		_centroid.x += (_currentVertex.x + _nextVertex.x) * _partialSignedArea;
		_centroid.y += (_currentVertex.y + _nextVertex.y) * _partialSignedArea;
		
		_polygonSignedArea *= 0.5f;
		_centroid.x /= (6f * _polygonSignedArea);
		_centroid.y /= (6f * _polygonSignedArea);

		//Initialize centroid
		GameObject _centroidObject = Instantiate(Resources.Load<GameObject>("Prefabs/Navigation/Centroid"));
        Transform _pointsParent = this.transform.parent.FindChild("Points");
		_centroidObject.GetComponent<NavigationPointCentroid>().Initialize(ID);
		_centroidObject.name = "Collider_" + colliderID + "_centroid";
		_centroidObject.tag = "NavigationCentroid";
		_centroidObject.transform.position = _centroid;
		_centroidObject.transform.SetParent(_pointsParent, false);

		centroid = _centroidObject.transform;
		centroidPosition = centroid.position;
	}

	public void AddConnectedLinkerID(int linkerID){
		connectedLinkersID.Add(linkerID);
	}

	public List<int> GetConnectedLinkersID(){
		return connectedLinkersID;
	}
}
