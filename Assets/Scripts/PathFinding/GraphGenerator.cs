using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro.EditorUtilities;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;


namespace SS
{
    public class GraphGenerator : MonoBehaviour
    {
        [SerializeField] Transform world;        
        Mesh worldMesh;
        Transform worldMeshTransform;
        Graph worldGraph;
        private void Start()
        {
            Debug.Log("Generating graph");

            worldMeshTransform = world.Find("WorldMeshObject");
            worldMesh = worldMeshTransform.GetComponent<MeshFilter>().mesh;
            var worldVerts = worldMesh.vertices.ToList<Vector3>();
            worldGraph = GenerateGraph(worldVerts, worldMesh.GetIndices(0));
        }

        #region Generate Graph

        private Graph GenerateGraph(List<Vector3> vertices, int[] indices)
        {
            Debug.Log($"Vertex count {vertices.Count}");

            // generate all the nodes 
            Node[] nodes = new Node[vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
            {
                var node = new Node();
                node.neighbors = new List<Node>();

                node.vertexIndex = i;
                node.position = worldMeshTransform.TransformPoint(vertices[i]);
                nodes[i] = node;
            }

            // Create the neighbors
            // indices are stored in a group of 3 to form a triangle. this is the edges on the mesh and also 
            // be the edges in our graph

            for (int i = 0;i < indices.Length; i+=3)
            {
                // get the node index from indices
                var a = indices[i + 0];
                var b = indices[i + 1];
                var c = indices[i + 2];

                // add the edge to the node
                AddEdgeToNode(nodes[a], nodes[b]);
                AddEdgeToNode(nodes[b], nodes[a]);

                AddEdgeToNode(nodes[b], nodes[c]);
                AddEdgeToNode(nodes[c], nodes[b]);
            }

            Debug.Log("Done generating graph");
            var g = new Graph();
            g.nodes = nodes.ToList();
            return g;
        }

        private void AddEdgeToNode(Node from, Node too)
        {
            if (from.neighbors.Contains(too))
                return;
            from.neighbors.Add(too);

            if (too.neighbors.Contains(from))
                return;
            too.neighbors.Add(from);
        }

        #endregion Generate Graph


        #region PathFinding
        private List<Vector3> GetPath(Vector3 from, Vector3 target)
        {
            Node fromNode = GetClosestNode(from);
            Node targetNode= GetClosestNode(target);

            if (fromNode == null || targetNode == null)
                return null;

            List<Vector3> result = new List<Vector3>();
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost  < currentNode.fCost ||
                        (openSet[i].fCost == currentNode.fCost &&
                        openSet[i].hCost < currentNode.hCost))
                    {
                        if (!currentNode.Equals(openSet[i]))
                        {
                            currentNode = openSet[i];
                        }
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // we reached the target, trace back
                if (currentNode.Equals(targetNode))
                {
                    result = BuildPath(fromNode, currentNode);
                    break;
                }

                foreach(var b in currentNode.neighbors)
                {
                    if (!closedSet.Contains(b))
                    {
                        float moveCost = currentNode.gCost + GetDistance(currentNode, b);

                        if(moveCost < b.gCost || !openSet.Contains(b))
                        {
                            b.gCost = moveCost;
                            b.hCost = GetDistance(b, targetNode);
                            b.previousNode = currentNode;

                            if (!openSet.Contains(b))
                            {
                                openSet.Add(b);
                            }
                        }
                    }
                }
            }
            return null;
        }

        private float GetDistance(Node a, Node b) 
        {
            return (a.position - b.position).sqrMagnitude;
        }

        private List<Vector3> BuildPath(Node start, Node current)
        {
            List<Vector3> p = new List<Vector3>();
            Node currentBlock = current;

            while (currentBlock != start)
            {
                p.Add(currentBlock.position);
                currentBlock = current.previousNode;
            }

            return p;
        }

        private Node GetClosestNode(Vector3 position)
        {
            Node closest = null;
            float closestDist= float.MaxValue;

            for (int i = 1; i < worldGraph.nodes.Count; i++)
            {
                var d = Vector3.SqrMagnitude(position - worldGraph.nodes[i].position);

                if (d < closestDist)
                {
                    closest = worldGraph.nodes[i];
                    closestDist = d;
                }
            }

            return closest;
        }
        #endregion PathFinding
    }
}
