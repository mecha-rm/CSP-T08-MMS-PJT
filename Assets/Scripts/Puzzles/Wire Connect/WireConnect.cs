using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wire connection puzzle
// NOTE: do not use sphere colliders in orthographic mode for this.
public class WireConnect : PuzzleMechanic
{
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
    // [HideInInspector()] // removed for testing purposes.
    public bool mouseDown;

    // becomes 'true' when everything is connected.
    [Tooltip("Becomes 'true' when everything is connected. Call AllConnected() to actually check if everything is connected.")]
    public bool allConnected;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // the list is empty.
        if (nodes.Count == 0)
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
        // checks to see if everything is connected.
        bool result = true;

        // checks if all of the nodes are connected.
        foreach(WireConnectNode node in nodes)
        {
            // if an unconnected node is found, return false.
            if (!node.connected)
            {
                result = false;
                break;
            }
        }

        // this was causing problems for checking puzzle completion.
        // this line was moved to the wire node area.
        // the mouse down is now false.
        // mouseDown = false;

        // the puzzle is complete.
        if (result && puzzle != null)
            puzzle.OnPuzzleCompletion();

        // everything has been connected.
        //Debug.Log("All Connected!");

        // saves the result.
        allConnected = result;

        // are all of the nodes are connected?
        return result;
    }

    // initiates the main action for this puzzle.
    public override void InitiateMainAction()
    {
        // can't interact.
        if (!interactable)
            return;

        // checks if everything is connected.
        AllConnected();
    }

    // checks if the puzzle was completed successfully successful.
    public override bool IsPuzzleComplete()
    {
        // checks if complete.
        // this might get called every frame, so just return the variable.
        return allConnected;
    }

    // resets the puzzle.
    public override void ResetPuzzle()
    {
        // disconnects all nodes.
        foreach(WireConnectNode node in nodes)
        {
            node.Disconnect();
        }

        // TODO: check and see if the reset button works.

        // sets both to null.
        node1 = null;
        node2 = null;
        mouseDown = false;

        // called to reset the puzzle.
        if (puzzle != null)
            puzzle.OnPuzzleReset();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
