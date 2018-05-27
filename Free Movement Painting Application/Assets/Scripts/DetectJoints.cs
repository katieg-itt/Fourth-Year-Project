using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DetectJoints : MonoBehaviour
{

    public GameObject BodySrcManager,
                      greenSphere,
                      quadHitPoint,
                      lineObject;
    public Renderer   Cat,
                      Mushroom,
                      Unicorn;
    public List<Vector3> currentLinePoints;
    public List<LineRenderer> lines = new List<LineRenderer>();
    public LineRenderer currentLine;
    public JointType TrackedJoint;
    private BodySourceManager bodyManager;
    private Body[] bodies,
                   _bodies;
    public float multiplier = 25f;
    private object frame;
    public int BodyFrameSource,
               sortOrder = 10,
               lineCount = 0;
    private Windows.Kinect.Joint handRight;
    public RaycastHit hit;
    private TrailRenderer myMeshRenderer;

    private Boolean handOpen = false;

    private Color currentColor = Color.black;

    public float lineWidth = .5f;
    
    public string layerName;
    private bool pauseEnabled;

    // Create a new line
    void NewLine()
    {
        // Each gameobject can only have a single LineRenderer, so we need to create a new game object each time
        this.lines.Add(new GameObject().AddComponent<LineRenderer>());
        this.currentLine = this.lines[this.lines.Count - 1];    // Use the last created line
        this.currentLine.name = "Line " + (this.lines.Count - 1);
        this.currentLine.sortingLayerName = layerName;
        this.currentLine.sortingOrder = sortOrder;
        this.currentLine.material.color = this.currentColor;

        // Reset some things that are used to track the current line
        this.lineCount = 0;
        this.sortOrder++;
        this.currentLinePoints = new List<Vector3>();
    }

    void UpdateLine(LineRenderer currentLine)
    {
        currentLine.startWidth = this.lineWidth;
        currentLine.endWidth = this.lineWidth;

        currentLine.positionCount = currentLinePoints.Count;

        for(int i = lineCount; i < currentLinePoints.Count; i++) {
            currentLine.SetPosition(i, currentLinePoints[i]);
        }
        lineCount = currentLinePoints.Count;
    }

    // Use this for initialization
    void Start()
    {
        greenSphere = GameObject.FindGameObjectWithTag("Green Sphere");
        // Load Artworks
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "ColouringBook")
        {
            this.Cat = GameObject.Find("Cat").GetComponent<Renderer>();
            this.Unicorn = GameObject.Find("Unicorn").GetComponent<Renderer>();
            this.Mushroom = GameObject.Find("Mushroom").GetComponent<Renderer>();
            this.Cat.enabled = false;
            this.Mushroom.enabled = false;
            this.Unicorn.enabled = false;
        }
        myMeshRenderer = quadHitPoint.GetComponent<TrailRenderer>();
        myMeshRenderer.enabled = false;
        NewLine();

        if (BodySrcManager == null) {
            Debug.Log("Assign Game Object with Body Source Manager");
        } else {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
    }

    private void LoadArtwork(String type)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ColouringBook"))
        {
            switch (type)
            {
                case "Cat":
                    this.Cat.enabled = true;
                    this.Mushroom.enabled = false;
                    this.Unicorn.enabled = false;
                    break;
                case "Mushroom":
                    this.Cat.enabled = false;
                    this.Mushroom.enabled = true;
                    this.Unicorn.enabled = false;
                    break;
                case "Unicorn":
                    this.Cat.enabled = false;
                    this.Mushroom.enabled = false;
                    this.Unicorn.enabled = true;
                    break;
            }
        }
    }

    private void UpdateBrush(RaycastHit hit)
    {
        string hitDebug = "";

        switch(hit.collider.tag) {
            case "Green Sphere":
                this.currentColor = new Color(0,204,0);
                hitDebug = "green";
                break;
            case "Red Sphere":
                this.currentColor = new Color(255,0,0);
                hitDebug = "Red";
                LoadArtwork("Cat");
                break;
            case "Yellow Sphere":
                this.currentColor = new Color(255,255,0);
                hitDebug = "Yellow";
                LoadArtwork("Unicorn");
                break;
            case "Blue Sphere":
                this.currentColor = Color.blue;
                hitDebug = "Blue";
                LoadArtwork("Mushroom");
                break;
            case "Black Sphere":
                this.currentColor = Color.black;
                hitDebug = "Black";
                break;
            case "Eraser":
                this.currentColor = new Color(255,255,255);
                hitDebug = "Eraser";
                break;
            case "Stroke Small":
                this.lineWidth = .1f;
                hitDebug = "Thin line";
                break;
            case "Stroke Medium":
                this.lineWidth = .5f;
                hitDebug = "Medium line";
                break;
            case "Stroke Large":
                this.lineWidth = .9f;
                hitDebug = "Large line";
                break;
            case "Menu":
                SceneManager.LoadScene(0);
                hitDebug = "Menu";
                break;
            case "Colour Book Button":
                SceneManager.LoadScene(2);
                // disable menu scene
                // enable colouring book scene
                break;
            case "paintButton":
                SceneManager.LoadScene(1);
                
                break;
        }
            if (hitDebug != "") {
            Debug.Log("Hit " + hitDebug);
        }

        if(this.myMeshRenderer)
        {
            this.myMeshRenderer.material.color = this.currentColor;
        }
        
    }
    void OnLevelWasLoaded()
    {
        pauseEnabled = false;
    }

    private void TrackRightHand(Body[] bodies)
    {
        foreach (var body in bodies)
        {
            if (body != null && body.IsTracked)
            {
                var pos = body.Joints[TrackedJoint].Position;
                gameObject.transform.position = new Vector3(pos.X * multiplier, pos.Y * multiplier);
                if (!this.handOpen) {
                    currentLinePoints.Add(new Vector3(pos.X * multiplier, pos.Y * multiplier));
                    UpdateLine(this.currentLine);
                }

                string whichHand = "right"; //change to "left" in order to track left hand
                handRight = body.Joints[JointType.HandRight];

                if (whichHand.Equals("right"))
                {
                    switch (body.HandRightState)
                    {
                        case HandState.Open:
                            this.handOpen = true;
                            //Debug.Log("Right hand is now open");
                            break;
                        case HandState.Closed:
                            if(this.handOpen) {
                                // Hand was open previous to this
                                NewLine();
                            }
                            this.handOpen = false;
                            //Debug.Log("Right hand is now closed");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, Vector3.forward, out hit)) {
            if (!this.handOpen)
            {
                quadHitPoint.transform.position = hit.point;
            }

            UpdateBrush(hit);
        }

        //find where cube hits drawing plane
        if (!this.handOpen) {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.green);
        }
        if (bodyManager == null) {
            return;
        }
        bodies = bodyManager.GetData();

        if (bodies == null) {
            return;
        }

        TrackRightHand(bodies);
    }
}






