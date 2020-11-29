using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImgTracker : MonoBehaviour
{
    [SerializeField]
    Camera m_WorldSpaceCanvasCamera;

    public Camera worldSpaceCanvasCamera
    {
        get { return m_WorldSpaceCanvasCamera; }
        set { m_WorldSpaceCanvasCamera = value; }

 
    }

    Texture2D m_DefaultTexture;
    public Texture2D defaultTexture
    {
        get { return m_DefaultTexture; }
        set { m_DefaultTexture = value; }
    }

    ARTrackedImageManager m_TrackedImageManager;


    [SerializeField]
    Text text;
    private void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        text = GameObject.FindObjectOfType<Text>();
        text.text = "Start";
    }

    private void UpdateInfo(ARTrackedImage trackedImage)
    {
        var canvas = trackedImage.GetComponentInChildren<Canvas>();
        canvas.worldCamera = worldSpaceCanvasCamera;

        var text = canvas.GetComponentInChildren<Text>();
        text.text = string.Format
            (
            "{0}\ntrackingState: {1}\nGUID: {2}\nReference size: {3} cm\nDetected size: {4} cm",
            trackedImage.referenceImage.name,
            trackedImage.trackingState,
            trackedImage.referenceImage.guid,
            trackedImage.referenceImage.size * 100f,
            trackedImage.size * 100f);

        var planeParentGo = trackedImage.transform.GetChild(0).gameObject;
        var planeGo = planeParentGo.transform.GetChild(0).gameObject;

        if(trackedImage.trackingState != TrackingState.None)
        {
            planeGo.SetActive(true);

            trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);

            var material = planeGo.GetComponentInChildren<MeshRenderer>().material;
            material.mainTexture = trackedImage.referenceImage.texture == null ? defaultTexture : trackedImage.referenceImage.texture;
        }
        else
        {
            planeGo.SetActive(false);
        }
    }

    private void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    private void OnDesable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

            //trackedImage.transform.localScale = new Vector3(.07f, .07f, .07f);

            //UpdateInfo(trackedImage);
            foreach (var newImage in eventArgs.added)
            {
            // Handle added event
            UpdateInfo(newImage);

            text.text = "Wieza dodana " + newImage.referenceImage.name;

            }

            foreach (var updatedImage in eventArgs.updated)
            {
            UpdateInfo(updatedImage);


                    text.text = "Wieza update " + updatedImage.referenceImage.name; 

            }

            foreach (var removedImage in eventArgs.removed)
            {


                    // Handle removed event
                    text.text = "Wieza usunieta " + removedImage.referenceImage.name; 

            }
        

        //foreach (var trackedImage in eventArgs.updated)
        //    UpdateInfo(trackedImage);

        
    }
}
