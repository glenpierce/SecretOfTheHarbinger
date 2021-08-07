using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public bool myWalkable;
    public Vector3 myWorldPosition;

    public Node(bool walkable, Vector3 worldPosition) {
        myWalkable = walkable;
        myWorldPosition = worldPosition;
    }
}
