using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> OnMouseClick, OnMouseHold, OnMouseHover;
    public Action OnMouseUp, OnPressingEsc, OnMouseClickUI;
	private Vector2 cameraMovementVector;
	private float zoom;

	[SerializeField]
	Camera mainCamera;

	public LayerMask groundMask;

	public Vector2 CameraMovementVector
	{
		get { return cameraMovementVector; }
	}
	public float Zoom
	{
		get { return zoom; }
	}

	private void Update()
	{
		CheckClickDownEvent();
		CheckClickUpEvent();
		CheckClickHoldEvent();
		CheckArrowInput();
		CheckHoveringObjects();
		CheckPressingEsc();
		CheckMouseWheel();
		CheckClickDownEventUI();
		CheckPressingE();
		CheckPressingQ();
	}

	private Vector3Int? RaycastGround()
	{
		RaycastHit hit;
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
		{
			Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
			return positionInt;
		}
		return null;
	}

	private void CheckArrowInput()
	{
		cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	private void CheckMouseWheel()
	{
		zoom = Input.GetAxis("Mouse ScrollWheel");
	}
	

	private void CheckClickHoldEvent()
	{
		if(Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
		{
			var position = RaycastGround();
			if (position != null)
			{
				OnMouseHold?.Invoke(position.Value);
			}

		}
	}

	private void CheckClickUpEvent()
	{
		if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
		{
			OnMouseUp?.Invoke();

		}
	}

	private void CheckClickDownEvent()
	{
		if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
		{
			var position = RaycastGround();
			if (position != null)
			{
				OnMouseClick?.Invoke(position.Value);
			}

		}
	}
	private void CheckClickDownEventUI()
	{
		if (Input.GetMouseButtonDown(0))
		{
				OnMouseClickUI?.Invoke();
		}
	}

	private void CheckHoveringObjects()
	{
		if (EventSystem.current.IsPointerOverGameObject() == false)
		{
			var position = RaycastGround();
			if (position != null)
				OnMouseHover?.Invoke(position.Value);

		}
		
	}

	private void CheckPressingEsc()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnPressingEsc?.Invoke();
		}
	}
	private void CheckPressingQ()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			OnPressingEsc?.Invoke();
		}
	}
	private void CheckPressingE()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			OnPressingEsc?.Invoke();
		}
	}
	
	
}

