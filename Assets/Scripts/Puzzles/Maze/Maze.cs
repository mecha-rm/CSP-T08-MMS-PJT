using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a maze puzzle.
public class Maze : PuzzleMechanic
{
    // the maze direction.
    // straight and forward mean the same thing.
    public enum mazeDirec { left, right, forward, back }

    // the marker that shows the current position.
    public GameObject marker;

    // the reset position of the marker.
    private Vector3 markerResetPos;

    // TODO: leave room for opening doors animation.

    // the size of the space the marker is in.
    // this controls where the marker moves.
    public float spaceSize = 0.5F;

    // the correct combination for the maze.
    public List<mazeDirec> path = new List<mazeDirec>();

    // the index
    public int index = 0;

    // the list of keycodes
    [Header("Key Codes/Movement")]

    // left
    public KeyCode leftKey1 = KeyCode.LeftArrow;
    public KeyCode leftKey2 = KeyCode.A;

    // right
    public KeyCode rightKey1 = KeyCode.RightArrow;
    public KeyCode rightKey2 = KeyCode.D;

    // straight
    public KeyCode forwardKey1 = KeyCode.UpArrow;
    public KeyCode forwardKey2 = KeyCode.W;

    // back
    public KeyCode backKey1 = KeyCode.DownArrow;
    public KeyCode backKey2 = KeyCode.S;

    // rotations
    // TODO: setup the orientation properly so that these functions actually work.
    [Header("Key Codes/Rotations")]
    // if the rotation keys should be available.
    public bool useRotationKeys = true;

    // positive rotation
    public KeyCode posRotKey1 = KeyCode.PageUp;
    public KeyCode posRotKey2 = KeyCode.Q;

    // negative rotation.
    public KeyCode negRotKey1 = KeyCode.PageDown;
    public KeyCode negRotKey2 = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        // sets marker.
        if (marker == null)
        {
            Transform t = transform.Find("Marker");

            // transform found.
            if (t != null)
                marker = t.gameObject;
        }
            

        // saves the marker position.
        if (marker != null)
            markerResetPos = marker.transform.position;

        // there is only one maze puzzle, so it's filled by default.
        if (path.Count == 0)
        {
            // section 1
            path.Add(mazeDirec.forward);
            path.Add(mazeDirec.left);
            path.Add(mazeDirec.left);

            // section 2
            path.Add(mazeDirec.right);
            path.Add(mazeDirec.right);
            path.Add(mazeDirec.forward);

            // section 3
            path.Add(mazeDirec.right);
            path.Add(mazeDirec.left);
            path.Add(mazeDirec.forward);

            // exit move (through the door)
            // TODO: add straight move.
        }

    }

    // rotates the maze view.
    // TODO:maybe have the maze be the wrong perspective by default?
    public void Rotate(bool positive)
    {
        // TODO: maybe rotate the camera instead?
        // TODO: make sure this aligns with the orientation of the maze.

        // rotates the view of the maze.
        transform.Rotate(transform.up, (positive) ? 90.0F : -90.0F);
    }

    // gets an input direction.
    public void OnInput(mazeDirec direc)
    {
        // index out of bounds.
        if (index >= path.Count)
        {
            Complete();
            return;
        }

        // correct direction chosen.
        if(path[index] == direc)
        {
            index++;

            // the marker's position translation
            Vector3 translate = Vector3.zero;

            // translation amount.
            switch(direc)
            {
                case mazeDirec.left: // -x
                    translate.x = -spaceSize * transform.localScale.x;
                    break;
                case mazeDirec.right: // +x
                    translate.x = spaceSize * transform.localScale.x;
                    break;

                case mazeDirec.forward: // y+
                    translate.y = spaceSize * transform.localScale.y;
                    break;
                case mazeDirec.back: // -y
                    translate.y = -spaceSize * transform.localScale.y;
                    break;
            }

            // marker translation
            marker.transform.Translate(translate);

            // TODO: prevent marker from leaving the maze.
            // TODO: the wrong input resets the marker to the start. Is this what the client wants?

            // check if the player has completed the maze.
            if (index >= path.Count)
                Complete();
        }
        else // wrong direction chosen, so reset.
        {
            ResetPuzzle();
        }
            
    }

    // checks the success function.
    public bool Complete()
    {
        // if the index is at the end of the path...
        // the right combination has been put in, the index reaches the end. 
        bool complete = index >= path.Count;

        // tells the puzzle the maze is complete.
        if (complete && puzzle != null)
            puzzle.OnPuzzleCompletion();

        // moves the marker up so that it can go through the maze?
        // marker.transform.Translate(0, spaceSize * transform.localScale.y, 0);

        // the maze is complete.
        if (complete)
            Debug.Log("Maze Complete!");

        return complete;

    }

    // checks if the puzzle was completed successfully successful.
    public override bool CompleteSuccess()
    {
        // checks if complete.
        return Complete();
    }

    // resets the puzzle.
    public override void ResetPuzzle()
    {
        // reset to the default text.
        index = 0;

        // reset position.
        marker.transform.position = markerResetPos;
    }

    // Update is called once per frame
    void Update()
    {
        // moves left.
        if (Input.GetKeyDown(leftKey1) || Input.GetKeyDown(leftKey2))
            OnInput(mazeDirec.left);

        // moves right.
        if (Input.GetKeyDown(rightKey1) || Input.GetKeyDown(rightKey2))
            OnInput(mazeDirec.right);

        // moves straight.
        if (Input.GetKeyDown(forwardKey1) || Input.GetKeyDown(forwardKey2))
            OnInput(mazeDirec.forward);

        // moves backward.
        if (Input.GetKeyDown(backKey1) || Input.GetKeyDown(backKey2))
            OnInput(mazeDirec.back);

        // rotates the maze.
        if(useRotationKeys)
        {
            // rotate left (+rot)
            if (Input.GetKeyDown(posRotKey1) || Input.GetKeyDown(posRotKey2))
                Rotate(true);

            // rotate right (-rot)
            if (Input.GetKeyDown(negRotKey1) || Input.GetKeyDown(negRotKey2))
                Rotate(false);
        }
        
    }
}
