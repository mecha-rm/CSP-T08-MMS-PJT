﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a maze puzzle.
// NOTE: the maze movement assumes the maze has a rotation of (0, 0, 0)
// this means that (x,z) are used for movement.
// if the maze is oriented in another way, use a parent object and child the marker to it.
public class Maze : PuzzleMechanic
{
    // the maze direction.
    // straight and forward mean the same thing.
    public enum mazeDirec { left, right, forward, back }

    // marker variables.
    [Header("Marker")]

    // the marker that shows the current position.
    public GameObject marker;

    // if 'true', the marker rotates when moved.
    // this changes what direction the marker is going in.
    public bool reorientMarker = true;

    // these values are automatically set.
    // the reset position of the marker. This is set the marker's position at the start.
    public Vector3 markerResetPos;

    // the marker's reset rotation in eulers.
    public Vector3 markerResetRotEulers;

    // if 'true', the reset tranformation is automatically set.
    public bool autoSetResetTransform = true;

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
        if (marker != null && autoSetResetTransform)
        {
            // reset position
            markerResetPos = marker.transform.position;

            // reset euler rotation
            markerResetRotEulers = marker.transform.eulerAngles;
        }
            

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

    // rotates the maze view by 90 degrees.
    // this was made because of a misinterpretation of what the client wanted.
    // this function is still here, but it goes unused.
    public void Rotate90(bool positive)
    {
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
            
            // move the marker if it exists.
            if(marker != null)
            {
                // the marker's position translation
                Vector3 translate = Vector3.zero;

                // translation amount.
                switch (direc)
                {
                    case mazeDirec.left: // -x
                        translate.x = -spaceSize * transform.localScale.x;
                        break;
                    case mazeDirec.right: // +x
                        translate.x = spaceSize * transform.localScale.x;
                        break;

                    case mazeDirec.forward: // z+
                        translate.z = spaceSize * transform.localScale.z;
                        break;
                    case mazeDirec.back: // -z
                        translate.z = -spaceSize * transform.localScale.z;
                        break;
                }

                // marker translation
                // the translation function goes by the 'forward' of the marker.
                marker.transform.Translate(translate);


                // if the marker should face the direction it moved in. 
                if (reorientMarker)
                {
                    float angle = 0;

                    // checks what direction the marker went.
                    switch (direc)
                    {
                        case mazeDirec.left: // rotated left
                            angle = -90.0F;
                            break;
                        case mazeDirec.right: // rotated right
                            angle = 90.0F;
                            break;

                        case mazeDirec.forward: // no rotation
                            angle = 0.0F;
                            break;
                        case mazeDirec.back: // rotated left twice/right twice
                            angle = 180.0F;
                            break;
                    }

                    // rotates the marker.
                    marker.transform.Rotate(Vector3.up, angle);
                }
            }
            

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

        // reset position of the marker.
        if(marker != null)
        {
            // reset position.
            marker.transform.position = markerResetPos;

            // reset rotation.
            marker.transform.eulerAngles = markerResetRotEulers;
        }
            
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
        
    }
}