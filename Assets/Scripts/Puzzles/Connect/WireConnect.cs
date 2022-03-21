﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wire connection puzzle
public class WireConnect : MonoBehaviour
{
    // the puzzle that this keypad belongs to.
    public Puzzle puzzle;

    // the first node.
    public WireConnectNode node1;

    // the second node.
    public WireConnectNode node2;

    // list of nodes
    public List<WireConnectNode> nodes = new List<WireConnectNode>();

    // the default line material.
    // if the line already has a material it is not overwritten.
    public Material lineMaterial = null;

    // the line width.
    public float lineWidth = 0.05F;

    // becomes 'true' if the mouse is down.
    // used for the wire connected nodes.
    [HideInInspector()]
    public bool mouseDown;

    // Start is called before the first frame update
    void Start()
    {       
        // the list is empty.
        if(nodes.Count == 0)
        {
            // puts the nodes into the list.
            GetComponentsInChildren<WireConnectNode>(true, nodes);
        }

        // goes through each node and sets the wire connect to this.
        foreach(WireConnectNode node in nodes)
        {
            node.wireConnect = this;
        }
    }

    // checks to see if all of the wires are connected properly.
    public bool AllConnected()
    {
        // checks if all of the nodes are connected.
        foreach(WireConnectNode node in nodes)
        {
            // if an unconnected node is found, return false.
            if (!node.connected)
                return false;
        }

        // the mouse down is now false.
        mouseDown = false;

        // everything has been connected.
        Debug.Log("All Connected!");

        // all of the nodes are connected.
        return true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}