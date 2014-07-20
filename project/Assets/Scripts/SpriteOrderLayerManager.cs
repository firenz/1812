/*	This file is part of 1812: La aventura.

    1812: La aventura is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    1812: La aventura is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with 1812: La aventura.  If not, see <http://www.gnu.org/licenses/>.
*/

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class SpriteOrderLayerManager : MonoBehaviour {
	public Player player;
	protected int originalSortingOrder;
	// Use this for initialization
	protected void Start () {
		originalSortingOrder = this.renderer.sortingOrder;
	}
	
	// Update is called once per frame
	protected void Update () {
		if(player.currentPosition.y > this.transform.position.y){
			LayerChangeWhenPlayerIsOverThisObject();
		}
		else{
			LayerChangeWhenPlayerisUnderThisObject();
		}
	}

	protected virtual void LayerChangeWhenPlayerIsOverThisObject(){
		this.renderer.sortingOrder = 1;
	}

	protected virtual void LayerChangeWhenPlayerisUnderThisObject(){
		this.renderer.sortingOrder = originalSortingOrder;
	}
}
