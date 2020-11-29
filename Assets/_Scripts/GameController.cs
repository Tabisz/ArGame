using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameController : MonoBehaviour
{

    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    [SerializeField]
    Text  text;

    //void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    //void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event

                text.text = "Wieza dodana " + newImage.referenceImage.name;
            
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (updatedImage.referenceImage.name == "ar_marker")

                text.text = "Wieza update";

        }

        foreach (var removedImage in eventArgs.removed)
        {
            if (removedImage.referenceImage.name == "ar_marker")

                // Handle removed event
                text.text = "Wieza usunieta";

        }
    }

    void Start()
    {
        m_TrackedImageManager = GameObject.FindObjectOfType<ARTrackedImageManager>();
        text = GameObject.FindObjectOfType<Text>();
        //m_TrackedImageManager.trackedImagesChanged += OnChanged;
        //text.text = "Start";
    }


    void Update()
    {
        
    }
    private void OnDestroy()
    {
        //m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    }
}
