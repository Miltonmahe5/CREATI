/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;
using UnityEngine.Video;
using UnityEngine.Events;


/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    public bool stateCard;
    public string nameCard;
    public string numercard;
 

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS


    
    protected virtual void Start()
    {
        
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;
        
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + 
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
          
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);
            var videoLooper = mTrackableBehaviour.GetComponentsInChildren<VideoPlayer>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in videoLooper)
                component.Play();

            //////////////////////////////////////////// codigo Juan
            var cardInScreen = GetComponent<ImageTargetBehaviour>();
            string test = cardInScreen.ImageTarget.Name;
            nameCard = DataNumerCard(test);
            numercard = test;
            stateCard = true;           
            //////////////////////////////////////////// codigo Juan
        }
    }

    string DataNumerCard(string nameIn)
    {
        string nameReturn = "default";

        switch (nameIn)
        {
            case "1": nameReturn = "Carta Uno"; break;
            case "2": nameReturn = "Carta Dos"; break;
            case "3": nameReturn = "Carta Tres"; break;
            case "4": nameReturn = "Carta Cuatro"; break;
            case "5": nameReturn = "Carta Cinco"; break;
            case "6": nameReturn = "Carta Seis"; break;
            case "7": nameReturn = "Carta Siete"; break;
            case "8": nameReturn = "Carta Ocho"; break;
            case "9": nameReturn = "Carta Nueve"; break;
            case "10": nameReturn = "Carta Diez"; break;
            case "11": nameReturn = "Carta Once"; break;
            case "12": nameReturn = "Carta Doce"; break;
            case "13": nameReturn = "Carta Trece"; break;
            case "14": nameReturn = "Carta Catorce"; break;
            case "15": nameReturn = "Carta Quince"; break;
            case "16": nameReturn = "Carta Diez y Seis"; break;
            case "17": nameReturn = "Carta Diez y Siete"; break;
            case "18": nameReturn = "Carta Diez y Ocho"; break;
            case "Cara1": nameReturn = "Cubo cara Uno"; break;
            case "Cara2": nameReturn = "Cubo cara Dos"; break;
            case "Cara3": nameReturn = "Cubo cara Tres"; break;
            case "Cara4": nameReturn = "Cubo cara Cuatro"; break;
            case "Cara5": nameReturn = "Cubo cara Cinco"; break;
            case "Cara6": nameReturn = "Cubo cara Seis"; break;
            case "CartaPoder": nameReturn = "Carta de Poder"; break;
        }
        return nameReturn;
    }

    protected virtual void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);
            var videoLooper = mTrackableBehaviour.GetComponentsInChildren<VideoPlayer>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;

            // Enable canvas':
            foreach (var component in videoLooper)
                component.Stop();


            //////////////////////////////////////////// codigo Juan
            stateCard = false;
           
        }
    }

    #endregion // PROTECTED_METHODS
}
