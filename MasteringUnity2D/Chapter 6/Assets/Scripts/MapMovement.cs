﻿using UnityEngine;

public class MapMovement : MonoBehaviour {

	public AnimationCurve MovementCurve;
	Vector3 StartLocation;
	Vector3 TargetLocation;
	float timer = 0;
	bool startedTravelling = false;
	bool inputActive = true;
	bool inputReady = true;

	void Awake()
	{
		this.collider2D.enabled = false;
	}

	void Start()
	{
		MessagingManager.Instance.SubscribeUIEvent(UpdateInputAction);
	}

	private void UpdateInputAction(bool uiVisible)
	{
		inputReady = !uiVisible;
	}

	void Update()
	{
		if (inputActive && Input.GetMouseButtonUp(0))
		{
			StartLocation = transform.position.ToVector3_2D();
			timer = 0;
			TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.mousePosition);
			startedTravelling = true;
		}
		else if (inputActive && Input.touchCount > 0)
		{
			StartLocation = transform.position.ToVector3_2D();
			timer = 0;
			TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.GetTouch(0).position);
			startedTravelling = true;
		} 
		
		if (TargetLocation != Vector3.zero && TargetLocation != transform.position && TargetLocation != StartLocation)
		{
			transform.position = Vector3.Lerp(StartLocation, TargetLocation, timer);
			timer += Time.deltaTime;
		}
		if (startedTravelling && Vector3.Distance(StartLocation, transform.position.ToVector3_2D()) > 0.5)
		{
			this.collider2D.enabled = true;
			startedTravelling = false;
		}
		if (!inputReady && inputActive)
		{
			TargetLocation = this.transform.position;
		}
		inputActive = inputReady;
	}
	
	void OnDestroy()
    {
        if (MessagingManager.Instance != null)
        {
            MessagingManager.Instance.UnSubscribeUIEvent(UpdateInputAction);
        }
    }
}

