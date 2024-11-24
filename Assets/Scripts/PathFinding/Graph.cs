using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SS

{
    class Node
    {
        public Vector3 position;
        public int vertexIndex;
        public List<Node> neighbors;        
        public bool isAvailable = true;

        // for path finding
        public float fCost = 1.0f;
        public float gCost = 1.0f;
        public float hCost = 1.0f;
        public Node previousNode;

    }

    class Graph
    {
        public List<Node> nodes;
    }
}