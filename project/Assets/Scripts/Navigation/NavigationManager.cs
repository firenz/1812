using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NavigationManager : Singleton<NavigationManager> {
	private const float INFINITY_DISTANCE = 9999f;
	private const float DISTANCE_TO_ITSELF = 0f;
	private const int NO_CONNECTING_ID = -1;

	private NavigationCollider[] colliders;
	private NavigationPoint[] navPoints;
	private int[][] colliderConnections;
	private Dictionary<int, Vector2> graphNodes;
	private float[][] distancesGraph;
	private int[][] minPaths;
	private int totalColliders;
	private int totalGraphNodes;
	private int indexNavPoints;

	private int currentSelectedColliderIDByMouse;

	private Transform thisTransform;
	private Transform collidersParentTransform;
	private Transform linkersParentTransform;

	protected override void InitializeOnAwake (){
		thisTransform = this.transform;
		collidersParentTransform = thisTransform.FindChild("Colliders");
		linkersParentTransform = thisTransform.FindChild("Points");

		colliders = collidersParentTransform.GetComponentsInChildren<NavigationCollider>();
		navPoints = linkersParentTransform.GetComponentsInChildren<NavigationPoint>();
		totalColliders = colliders.Length; //For knowing the starting index of the linker point IDs between colliders
		totalGraphNodes = totalColliders + navPoints.Length; //For knowing the size for the graph matrix
		graphNodes = new Dictionary<int, Vector2>();
	}

	private void Start(){
		//Always initialize graph in this order
		InitializeNavGraph();
		AddColliderCentroidsToNavGraph();
		AddColliderLinkersToNavGraph();
		FloydWarshallAlgorithm();
	}

	//This Initialize method will only work if collider IDs are from 0 to (n-1) order
	//if not, the matrix in some moment will give an Index Out Of Range Error.
	private void InitializeNavGraph(){
		distancesGraph = new float[totalGraphNodes][];
		minPaths = new int[totalGraphNodes][];

		for(int i = 0; i < totalGraphNodes; i++){
			distancesGraph[i] = new float[totalGraphNodes];
			minPaths[i] = new int[totalGraphNodes];

			for(int j = 0; j < totalGraphNodes; j++){
				if(i == j){
					distancesGraph[i][i] = 0f;
					minPaths[i][i] = i;
				}
				else{
					distancesGraph[i][j] = INFINITY_DISTANCE;
					minPaths[i][j] = NO_CONNECTING_ID;
				}
			}
		}
	}

	private void AddColliderCentroidsToNavGraph(){
		foreach(NavigationCollider collider in colliders){
			//collider.CalculateCentroid();
			int _colliderID = collider.colliderID;
			Vector2 _colliderCentroidPosition = collider.centroidPosition;

			graphNodes.Add(_colliderID, _colliderCentroidPosition);
			distancesGraph[_colliderID][_colliderID] = DISTANCE_TO_ITSELF;
			minPaths[_colliderID][_colliderID] = _colliderID;
		}
	}


	private void AddColliderLinkersToNavGraph(){
		int _graphNodeIndex = totalColliders;
		foreach(NavigationPoint colliderLinker in navPoints){
			if(_graphNodeIndex > totalGraphNodes){
				Debug.LogError("Error indexing collider linker ID: current graphNodeIndex " + _graphNodeIndex + " value is out of range of total graph nodes " + totalColliders + ")");
				return;
			}
			else{
				colliderLinker.InitializeID(_graphNodeIndex);
				int _linkColliderID_1 = colliderLinker.connectPointID_1;
				int _linkColliderID_2 = colliderLinker.connectPointID_2;
				Vector2 _linkerPosition = colliderLinker.transform.position;
				Vector2 _linkColliderCentroidPosition_1 = graphNodes[_linkColliderID_1];
				Vector2 _linkColliderCentroidPosition_2 = graphNodes[_linkColliderID_2];
				float _linkColliderDistante_1 = Math.Abs(Vector2.Distance(_linkColliderCentroidPosition_1, _linkerPosition));
				float _linkColliderDistante_2 = Math.Abs(Vector2.Distance(_linkColliderCentroidPosition_2, _linkerPosition));

				graphNodes.Add(_graphNodeIndex, _linkerPosition);
				collidersParentTransform.FindChild("Collider_" + _linkColliderID_1).GetComponent<NavigationCollider>().AddConnectedLinkerID(_graphNodeIndex);
				collidersParentTransform.FindChild("Collider_" + _linkColliderID_2).GetComponent<NavigationCollider>().AddConnectedLinkerID(_graphNodeIndex);

				//self path and distance
				minPaths[_graphNodeIndex][_graphNodeIndex] = _graphNodeIndex;
				distancesGraph[_graphNodeIndex][_graphNodeIndex] = DISTANCE_TO_ITSELF;

				//Were using a non-directed graph
				minPaths[_graphNodeIndex][_linkColliderID_1] = _linkColliderID_1;
				minPaths[_linkColliderID_1][_graphNodeIndex] = _graphNodeIndex;
				minPaths[_graphNodeIndex][_linkColliderID_2] = _linkColliderID_2;
				minPaths[_linkColliderID_2][_graphNodeIndex] = _graphNodeIndex;
				distancesGraph[_linkColliderID_1][_graphNodeIndex] = _linkColliderDistante_1;
				distancesGraph[_graphNodeIndex][_linkColliderID_1] = _linkColliderDistante_1;
				distancesGraph[_linkColliderID_2][_graphNodeIndex] = _linkColliderDistante_2;
				distancesGraph[_graphNodeIndex][_linkColliderID_2] = _linkColliderDistante_2;

				_graphNodeIndex++;
			}
		}
	}

	//Initializing the shortest paths for every node
	//more info about the algorithm used https://en.wikipedia.org/wiki/Floyd%E2%80%93Warshall_algorithm#Comparison_with_other_shortest_path_algorithms
	private void FloydWarshallAlgorithm(){
		for(int k = 0; k < totalGraphNodes; k++){
			for(int i = 0; i < totalGraphNodes; i++){
				for(int j = 0; j < totalGraphNodes; j++){
					if(distancesGraph[i][k] + distancesGraph[k][j] < distancesGraph[i][j]){
						distancesGraph[i][j] = distancesGraph[i][k] + distancesGraph[k][j];
						minPaths[i][j] = minPaths[i][k];
					}
				}
			}
		}
	}

	private List<int> FindMinPath(int originID, int destinyID){
		if( minPaths[originID][destinyID] == NO_CONNECTING_ID){
			return null;
		}

		List<int> _path = new List<int>();
		int _intermediateID = originID;

		while(_intermediateID != destinyID){
			_intermediateID = minPaths[_intermediateID][destinyID];
			_path.Add(_intermediateID);
		}

		return _path;
	}

	private List<Vector2> FindMinPathPositions(int originID, int destinyID){
		if( minPaths[originID][destinyID] == NO_CONNECTING_ID){
			return null;
		}
		
		List<Vector2> _path = new List<Vector2>();
		int _intermediateID = originID;
		
		while(_intermediateID != destinyID){
			_intermediateID = minPaths[_intermediateID][destinyID];

			_path.Add(graphNodes[_intermediateID]);
		}
		
		return _path;
	}

	private int GetNearestColliderID(Vector2 position){
		int _resultingColliderID = NO_CONNECTING_ID;
		float _distanceToNearestCollider = INFINITY_DISTANCE;
		
		foreach(NavigationCollider collider in colliders){
			int _colliderID = collider.colliderID;
			Vector2 _colliderCentroidPosition = collider.centroidPosition;
			float _distanteToCollider = Math.Abs(Vector2.Distance(position, _colliderCentroidPosition));
			
			if(_distanteToCollider < _distanceToNearestCollider){
				_distanceToNearestCollider = _distanteToCollider;
				_resultingColliderID = _colliderID;
			}
		}

		return _resultingColliderID;
	}

	private int GetNearestNavPointIDInsideCollider(Vector2 position, int colliderID){
		NavigationCollider collider = collidersParentTransform.FindChild("Collider_" + colliderID).GetComponent<NavigationCollider>();
		List<int> _linkersInCollider = collider.GetConnectedLinkersID();
		int _resultingNavPointID = colliderID;
		float _distanceToNearestNavPoint = Math.Abs(Vector2.Distance(position, collider.centroidPosition));

		foreach(int linkerID in _linkersInCollider){
			float _distanceToLinker = Math.Abs(Vector2.Distance(position, collider.centroidPosition));

			if(_distanceToLinker < _distanceToNearestNavPoint){
				_resultingNavPointID = linkerID;
				_distanceToNearestNavPoint = _distanceToLinker;
			}
		}

		return _resultingNavPointID;
	}

	private int GetNearestNavPointID(Vector2 position){
		int _resultingColliderID = NO_CONNECTING_ID;
		float _distanceToNearestCollider = INFINITY_DISTANCE;
		
		foreach(KeyValuePair<int, Vector2> node in graphNodes){
			Vector2 _nodePosition = node.Value;
			float _distanteToCollider = Math.Abs(Vector2.Distance(position, _nodePosition));
			
			if(_distanteToCollider < _distanceToNearestCollider){
				_distanceToNearestCollider = _distanteToCollider;
				_resultingColliderID = node.Key;
			}
		}
		
		return _resultingColliderID;
	}

	private int FindIntersectingColliderID(Vector2 position){
		int _resultingColliderID = NO_CONNECTING_ID;

		RaycastHit2D _hit = new RaycastHit2D();
		int _navigationLayerID = LayerMask.NameToLayer("Navigation"); //Obtaining the layer ID of Navigation Layer
		int _navigationMask = 1 << _navigationLayerID; // This is used to generate the Mask derived from Navigation Layer
		_hit = Physics2D.Raycast(position, Vector2.up, Mathf.Infinity, _navigationMask);

		if(_hit.collider != null){
			_resultingColliderID = _hit.collider.GetComponent<NavigationCollider>().colliderID;
		}
		else{
			_resultingColliderID = GetNearestColliderID(position);
			_resultingColliderID = GetNearestNavPointIDInsideCollider(position, _resultingColliderID);
		}

		return _resultingColliderID;
	}

	public List<Vector2> FindPath(Vector2 originPos, Vector2 destinyPos){
		int _originID = GetNearestNavPointID(originPos);
		int _destinyID = GetNearestNavPointID(destinyPos);

		List<Vector2> _path = new List<Vector2>();

		if(_originID != _destinyID){
			_path = FindMinPathPositions(_originID, _destinyID);
		}

		if(_path.Count == 1){
			float _distanceToPath = Mathf.Abs(Vector2.Distance(originPos, _path[0]));
			float _distanceToDestinyPos = Mathf.Abs(Vector2.Distance(originPos, destinyPos));
			if(_distanceToPath > _distanceToDestinyPos){
				_path.Clear();
			}
		}

		_path.Add(destinyPos);

		return _path;
	}

	//Debug Methods
	private void ShowLinkGraphMatrix(){
		string _matrix = "RESULTED MIN PATH MATRIX:\n";
		for(int i = 0; i < graphNodes.Count; i++){
			_matrix += "|";
			for(int j = 0; j < graphNodes.Count; j++){
				_matrix += " " + minPaths[i][j] + "\t|";
			}
			_matrix += "\n";
		}
		Debug.Log("Resulting linker collider graph matrix:\n" + _matrix);
	}

	private void ShowAdjacencyGraphMatrix(){
		string _matrix = "RESULTED ADJACENCY MATRIX:\n";
		for(int i = 0; i < totalGraphNodes; i++){
			_matrix += "|";
			for(int j = 0; j < totalGraphNodes; j++){
				_matrix += " " + distancesGraph[i][j] + "\t|";
			}
			_matrix += "\n";
		}
		Debug.Log("Resulting adjacency collider graph matrix:\n" + _matrix);
	}

}
