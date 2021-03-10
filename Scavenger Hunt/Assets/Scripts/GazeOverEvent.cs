using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// MonoBehavior for an interactable objects. Triggers different <c>UnityEvent</c>s
/// (sets of functions) when the user is gazing at the object. The margin of error
/// for gazing (in degrees) can be adjusted to allow for more generous, comfortable
/// control.
/// </summary>
public class GazeOverEvent : MonoBehaviour
{
    /// <summary>
    /// Margin of error for a valid gaze. The number of degrees off from a direct
    /// gaze that will still be counted as looking at the object.
    /// </summary>
    /// <remarks>This should be larger for larger objects.</remarks>
    [Range(0, 360)]
    public float maximumAngleForEvent = 15f;

    public UnityEvent OnHoverBegin;
    public UnityEvent OnHover;
    public UnityEvent OnHoverEnd;
    public UnityEvent OnButtonPressedDuringHover;

    public GameObject cam;
    Text t;

    /// <summary>
    /// A boolean that tracks if the object is currently hovered over. Used to
    /// ensure OnHoverBegin and OnHoverEnd are only fired once per gaze start/end.
    /// </summary>
    private bool isHovering = false;

    void Start()
    {
        cam = GameObject.Find("Camera");
        t = cam.GetComponent<MouseCameraControl>().scText;
    }

    void Update()
    {
        var cameraForward = Camera.main.transform.forward;
        var vectorToObject = transform.position - Camera.main.transform.position;

        // Check if the angle between the camera and object is within the hover range
        var angleFromCameraToObject = Vector3.Angle(cameraForward, vectorToObject);
        if (angleFromCameraToObject <= maximumAngleForEvent)
        {
            Hovering();

            if (XRInputManager.IsButtonDown())
            {
                ButtonPressed();
            }
        }
        else
        {
            NotHovering();
        }
    }

    private void Hovering()
    {
        float distance = Vector3.Distance(GameObject.Find("Player").transform.position, this.transform.position);
        if (isHovering && distance<3)
        {
            //OnHover.Invoke();
            if (this.transform.gameObject.name == "Mom")
            {
                t.text = "Hey! How are you doing today? You're looking for your Xbox controller? Always so reckless. " +
                    "I think I remember seeing your dad playing on the Xbox. If I remember correctly the dog was there as well";
            }
            if (this.transform.gameObject.name == "Dad")
            {
                t.text = "Your mom said I was playing on your Xbox? She must be mistaken, in fact I saw your grandpa playing" +
                    " as strange as that sounds.";
            }
            if (this.transform.gameObject.name == "Grandpa")
            {
                t.text = "Me? I am way to old to play Xbox silly youngin'. But I did see your mother playing Xbox with the dog";
            }
            if (this.transform.gameObject.name == "Dog")
            {
                t.text = "Woof Woof Woof Woof";
            }
            if (this.transform.gameObject.name == "Brother")
            {
                t.text = "I wasn't using your phone I swear but I did overhear Mom and Dad say they were going to confiscate it from you. You better find it soon. M";
            }
            if (this.transform.gameObject.name == "Mom1")
            {
                t.text = "Good morning, you lost your phone you say? No I am not hiding it from you, I promise. You should go ask your dad. R";
            }
            if (this.transform.gameObject.name == "Dad1")
            {
                t.text = "What your mom said I have your phone? Outrageous. Go ask your brother, he's always playing games on your phone. OO";
            }
            if(this.transform.gameObject.name == "Phone")
            {
                t.text = "You found your phone! Good job!";
            }
        }
        else
        {
            //OnHoverBegin.Invoke();
            isHovering = true;
        }
    }

    private void NotHovering()
    {
        if (isHovering)
        {
            t.text = "";
            OnHoverEnd.Invoke();
            isHovering = false;
        }
    }

    private void ButtonPressed()
    {
        OnButtonPressedDuringHover.Invoke();
    }
}