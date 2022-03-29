using UnityEngine;

// a connection node.
public class WireConnectNode : MonoBehaviour
{
    // the connection puzzle.
    public WireConnect wireConnect;

    // the paired connection node.
    public WireConnectNode paired = null;

    // the line renderer for this node.
    public LineRenderer line;

    // the mouse world position when on mouse down.
    // used to draw the line.
    private Vector3 mouseWorldPosOnDown;

    // becomes 'true' when the node is connected.
    public bool connected = false;

    // Start is called before the first frame update
    void Start()
    {
        // finds the wire connect object in the parent.
        if (wireConnect == null)
            wireConnect = GetComponentInParent<WireConnect>();

        // line not set.
        if (line == null)
        {
            // gets the component.
            line = GetComponent<LineRenderer>();

            // adds a line renderer.
            // the line by default has two points.
            if (line == null)
                line = gameObject.AddComponent<LineRenderer>();
        }
            
        // adjusts line parameters.
        if (line != null)
        {
            // gets the material saved to wire connect.
            // if (line.material == null)
            //     line.material = wireConnect.lineMaterial;

            // change the line material.
            // TODO: fix the line material so that it actually shows up.
            // line.material = wireConnect.lineMaterial;

            // changes the line width.
            line.startWidth = wireConnect.lineWidth;
            line.endWidth = wireConnect.lineWidth;

            // hide line.
            line.enabled = false;
        }
            
    }

    // WIRE CONNECTION //
    // This works by holding down the mouse over a node and dragging the wire to another node.

    // called when the user clicks down on a collider.
    // starts connection attempt by saving node 1.
    private void OnMouseDown()
    {
        // the mouse is down.
        wireConnect.mouseDown = true;

        // this node is not connected to anything yet.
        if (connected)
            return;

        // the mouse is down.
        Debug.Log("MouseDown on " + gameObject.name);

        // saves this as node 1.
        // since this is for mouse down, this variable should be null at this stage.
        wireConnect.node1 = this;

        // shows the line.
        line.enabled = true;

        // sets the line positions.
        // OnMouseDrag will change position 2.
        line.SetPosition(0, transform.position); // sets line to this node's position.
        line.SetPosition(1, transform.position);

        // saves the mouse world position in world space at this stage.
        // TODO: the z-value change may not be sufficient in the future.
        mouseWorldPosOnDown = Mouse.GetMousePositionInWorldSpace();
        mouseWorldPosOnDown.z = transform.position.z; // z-value should not change.
    }

    // called when the mouse has been clicked on a collider and has been dragged whiles still being held down.
    private void OnMouseDrag()
    {
        // if connected, stop moving the line.
        if (connected)
            return;

        // the mouse position and line position are not in the same scale.
        // I don't know why, but the line position is different than the mouse position at the same place.
        // if this can be fixed, change the code here.
        // if not, use this to figure out the line position.

        // gets point 0.
        Vector3 p0 = line.GetPosition(0);

        // gets the mouse's world position.
        Vector3 mouseWPos = Mouse.GetMousePositionInWorldSpace();

        // difference in position.
        Vector3 mouseWPosDiff = mouseWPos - mouseWorldPosOnDown;

        // z-value should stay the same.
        mouseWPosDiff.z = 0;

        // gets the final position.
        Vector3 finalPos = p0 + 
            mouseWPosDiff.normalized * mouseWPosDiff.magnitude;

        // saves the final position z-value.
        finalPos.z = p0.z;

        // changes the line ending position.
        // the position must be adjusted for it to be accurate.
        line.SetPosition(1, finalPos);
    }

    // called when the mouse enters a collider.
    // saves node 2 if it's the paired node.
    private void OnMouseEnter()
    {
        // this node is not connected to anything yet.
        if (connected)
            return;

        // mouse has been dragged.
        // Debug.Log("MouseEntered on " + gameObject.name);

        // if the mouse is down.
        if(wireConnect.mouseDown)
        {
            // if node 2 is not equal to this object, save it as this object.
            // this makes sure it's only saved in one node.
            if (wireConnect.node1 != this && wireConnect.node2 != this)
            {
                // enters in the node.
                wireConnect.node2 = this;

                // checks to see if the object is connected.
                // this should only be called when the second node is entered.
                Connected();
            }
        }   
    }

    // called when the mouse is no longer over a collider.
    // this removes this nodeif it is saved as node 2.
    private void OnMouseExit()
    {
        // this node is not connected to anything yet.
        if (connected)
            return;

        // if this is node 2, remove it from this object.
        if (wireConnect.node2 == this)
            wireConnect.node2 = null;
    }

    // called when the mouse button has been released, even if not over the same collider.
    // if the mouse is released above another node, it will check for the paired node.
    private void OnMouseUp()
    {
        // the mouse is up.
        wireConnect.mouseDown = false;

        // this node is not connected to anything yet.
        if (connected)
        {
            // if the line is connected and enabled...
            // make sure it's end positions are set properly.
            if(line.enabled)
            {
                // sets the positions.
                line.SetPosition(0, transform.position);
                line.SetPosition(1, paired.transform.position);
            }

            return;
        }
            

        // removes self from saved node.
        if (wireConnect.node1 == this) // node 1
            wireConnect.node1 = null;

        if(wireConnect.node2 == this) // node 2
            wireConnect.node2 = null;

        // hides the line.
        // if this line is reached, the node isn't connected.
        line.enabled = false;
    }

    // the puzzle is finished.
    public bool Connected()
    {
        // the result.
        connected = false;

        // if this is equal to node 1.
        if(wireConnect.node1 == this)
        {
            // if the second node is the one paired with this node.
            if (wireConnect.node2 == paired)
                connected = true;
        }
        // if this is equal to node 2.
        else if(wireConnect.node2 == this)
        {
            // if the second node is the one paired with this node.
            if (wireConnect.node1 == paired)
                connected = true;
        }

        // saves connected to the other object.
        paired.connected = connected;

        // if the nodes are connected.
        if (connected)
        {
            // message.
            Debug.Log(gameObject.name + " connected to " + paired.gameObject.name);

            // sets the line points.
            if(line != null)
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, paired.transform.position);
            }

            // checks if all of the nodes are connected.
            wireConnect.AllConnected();
        }

        // not connected.
        return connected;
    }

    // disconnects the node.
    public void Disconnect()
    {
        // disconnects on this end.
        connected = false;

        // disconnects on the other end.
        if (paired != null)
            paired.connected = false;

        // hides both lines.
        line.enabled = false;
        paired.line.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: take this out since the nodes will not moved after the lines are attached.

        // // if the line is enabled.
        // if(line.enabled && connected)
        // {
        //     // sets the positions.
        //     line.SetPosition(0, transform.position);
        //     line.SetPosition(1, paired.transform.position);
        // }
    }
}
