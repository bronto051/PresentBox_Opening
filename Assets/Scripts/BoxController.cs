using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject presentBox; // The closed present box object
    public GameObject contentOverlay; // The content overlay object
    public GameObject particleEffect;

    private enum SequenceStage { Enter, Opening, Exit }
    private SequenceStage currentStage;

    private void Start()
    {
        // Initialize the sequence
        currentStage = SequenceStage.Enter;
        SetupIntro();
    }

    private void Update()
    {
        // Detect player input to transition between stages
        if (Input.GetMouseButtonDown(0))
        {
            if (currentStage == SequenceStage.Enter)
            {
                TransitionToOpening();
            }
            else if (currentStage == SequenceStage.Opening)
            {
                TransitionToExit();
            }
        }

        if (currentStage == SequenceStage.Exit)
        {
            RestartSequence();
        }
    }

    private void SetupIntro()
    {
        // Ensure the box and content overlay are visible
        if (presentBox != null)
        {
            presentBox.SetActive(true);
        }

        if (contentOverlay != null)
        {
            contentOverlay.SetActive(true);
        }

        Debug.Log("Entry stage: Closed present box is displayed.");
    }

    private void TransitionToOpening()
    {
        currentStage = SequenceStage.Opening;

        Debug.Log("Opening stage: Opened present box and overlay of the content are displayed.");

        // Activate the Opening box animation
        if (presentBox != null)
        {
            Animator boxAnimator = presentBox.GetComponent<Animator>();
            if (boxAnimator != null)
            {
                boxAnimator.SetTrigger("Open");
            }
        }

        // Activate the Opening content overlay animation
        if (contentOverlay != null)
        {
            contentOverlay.SetActive(true);

            Animator contentAnimator = contentOverlay.GetComponent<Animator>();
            if (contentAnimator != null)
            {
                contentAnimator.SetTrigger("Open");
            }
        }

        // Activate the VFX animation
        if (particleEffect != null)
        {
            particleEffect.SetActive(true);
        }
    }

    private void TransitionToExit()
    {
        currentStage = SequenceStage.Exit;

        Debug.Log("Exit stage: Opened present box and content overlay are dismissed.");

        // Activate the Exit box animation
        Animator boxAnimator = presentBox.GetComponent<Animator>();
        if (presentBox != null)
        {
            boxAnimator.SetTrigger("Exit");
        }

        // Activate the Exit content overlay animation
        Animator contentAnimator = contentOverlay.GetComponent<Animator>();
        if (contentAnimator != null)
        {
            contentAnimator.SetTrigger("Exit");
        }

        // Deactivate the VFX animation
        if (particleEffect != null)
        {
            particleEffect.SetActive(false);
        }
    }

    private void RestartSequence()
    {
        Debug.Log("Restarting sequence.");
        currentStage = SequenceStage.Enter;
        SetupIntro();
    }
}