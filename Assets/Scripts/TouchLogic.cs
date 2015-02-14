using UnityEngine;
using System.Collections;

public class TouchLogic : MonoBehaviour
{

	public static int currTouch = 0;
	public int cachedTouchIndex = 100;
	public int CurrentTouch;
	//so other scripts can know what touch is currently on screen
	[HideInInspector]
	public UnityEngine.Touch touch2Watch;
	public RaycastHit Hit;
	RaycastHit hit;
	public Ray Ray;
	Ray ray;
	public int zoom = 5;
	public int rotate = 5;
	public Camera mycamera;
	GameObject Object_To_Move = null;
	Vector2 v2_current_position;
	Vector2 v2_previous_position;
	public float TouchDelta;
	float touchDelta;
	public float Angle;
	float angle;
	int i_comfort = 10;
	
	public virtual void Update ()//If your child class uses Update, you must call base.Update(); to get this functionality
	{
			//is there a touch on screen?
			if (Input.touches.Length <= 0) 
			{
					OnNoTouches ();
			} else { //if there is a touch
					//loop through all the the touches on screen
					for (int i = 0; i < Input.touchCount; i++) 
					{
							currTouch = i;
							CurrentTouch = currTouch;
							touch2Watch = Input.GetTouch(currTouch);
							ray = Camera.main.ScreenPointToRay (Input.GetTouch (currTouch).position);
							Ray = ray;
							

							//executes this code for current touch (i) on screen
								if(this.guiTexture != null && this.guiTexture.HitTest(Input.GetTouch(currTouch).position))
								{
									if (Input.GetTouch (currTouch).phase == TouchPhase.Began) 
									{
										OnTouchBegan ();
										touch2Watch = Input.GetTouch(currTouch);
									}
									if (Input.GetTouch (currTouch).phase == TouchPhase.Ended) 
									{
										OnTouchEnded ();
										touch2Watch = Input.GetTouch(currTouch);
									}
									if (Input.GetTouch (currTouch).phase == TouchPhase.Moved) 
									{
										OnTouchMoved ();
										touch2Watch = Input.GetTouch(currTouch);
									}
									if (Input.GetTouch (currTouch).phase == TouchPhase.Stationary) 
									{
										OnTouchStayed ();
										touch2Watch = Input.GetTouch(currTouch);
									}	
								}else if (Physics.Raycast (ray, out hit)) 
								{
					//if current touch hits our guitexture, run this code
									if (Input.GetTouch (currTouch).phase == TouchPhase.Began) 
									{
										if(hit.transform == this.transform)
										{
											OnTouchBegan ();
											touch2Watch = Input.touches[currTouch];
										}
									}
									if (Input.GetTouch (currTouch).phase == TouchPhase.Ended) 
									{
										if(hit.transform == this.transform)
										{
											OnTouchEnded ();
											touch2Watch = Input.GetTouch(currTouch);
										}
									}
									if (Input.GetTouch (currTouch).phase == TouchPhase.Moved) 
									{
										if(hit.transform == this.transform)
										{
											OnTouchMoved ();
											touch2Watch = Input.GetTouch(currTouch);
										}
									}
									if (Input.GetTouch (currTouch).phase == TouchPhase.Stationary) 
									{
										if(hit.transform == this.transform)
										{
											OnTouchStayed ();
											touch2Watch = Input.GetTouch(currTouch);
										}
									}
							}
							// assign a public raycasthit
							Hit = hit;
				
							//outside so it doesn't require the touch to be over the guitexture
							switch (Input.GetTouch (currTouch).phase) 
							{
							case TouchPhase.Began:
									touch2Watch = Input.GetTouch(currTouch);
									OnTouchBeganAnywhere ();
									break;
							case TouchPhase.Ended:
									touch2Watch = Input.GetTouch(currTouch);
									OnTouchEndedAnywhere ();
									break;
							case TouchPhase.Moved:
									touch2Watch = Input.GetTouch(currTouch);
									OnTouchMovedAnywhere ();
									break;
							case TouchPhase.Stationary:
									touch2Watch = Input.GetTouch(currTouch);
									OnTouchStayedAnywhere ();
									break;
							}
							if (Input.touchCount == 1) 
							{
									if (Input.touchCount == 1 && Input.GetTouch (currTouch).phase == TouchPhase.Began) 
									{
											v2_previous_position = Input.GetTouch (currTouch).position;
									}
									if (Input.touchCount == 1 && Input.GetTouch (currTouch).phase == TouchPhase.Ended) 
									{
											v2_current_position = Input.GetTouch (currTouch).position;
					
											touchDelta = v2_current_position.magnitude - v2_previous_position.magnitude; 
											TouchDelta = touchDelta;
					
											if (Mathf.Abs (touchDelta) > i_comfort) 
											{
													Debug.Log ("Swipe");
													Debug.Log ("Touch Delta : " + touchDelta);
													if (touchDelta > 0) 
													{
															if ((v2_current_position.x - v2_previous_position.x) > (v2_current_position.y - v2_previous_position.y)) 
															{
																	Debug.Log ("Swipe Right");
																	OnSwipeRight ();
															}
															if ((v2_current_position.x - v2_previous_position.x) < (v2_current_position.y - v2_previous_position.y)) 
															{
																	Debug.Log ("Swipe Up");
																	OnSwipeRight ();
															}
													}
													if (touchDelta < 0) 
													{
															if ((v2_current_position.x - v2_previous_position.x) > (v2_current_position.y - v2_previous_position.y)) 
															{
																	Debug.Log ("Swipe Down");
																	OnSwipeDown ();
															}
															if ((v2_current_position.x - v2_previous_position.x) < (v2_current_position.y - v2_previous_position.y)) 
															{
																	Debug.Log ("Swipe Left");
																	OnSwipeLeft ();
															}
													}
											}	
									}
							} else {
									if (Input.GetTouch (currTouch).tapCount == 2) 
									{
											Debug.Log ("1 finger touch double tap");
											if (Object_To_Move != null) 
											{
													Vector3 pos;
													pos.x = Input.GetTouch (currTouch).position.x;
													pos.y = Input.GetTouch (currTouch).position.y;
													pos.z = Mathf.Abs (Camera.current.transform.position.z - Object_To_Move.transform.position.z);
													Object_To_Move.transform.position = Camera.main.ScreenToWorldPoint (pos);
													OnDoubleTap();
											}
									}
							}
		
	
							if (Input.touchCount == 2)
							{

							if (Input.GetTouch (0).phase == TouchPhase.Moved && Input.GetTouch (1).phase == TouchPhase.Moved) 
							{
									v2_current_position = Input.GetTouch (0).position - Input.GetTouch (1).position;
									v2_previous_position = (Input.GetTouch (0).position - Input.GetTouch (0).deltaPosition)
											- (Input.GetTouch (1).position - Input.GetTouch (1).deltaPosition);
									touchDelta = v2_current_position.magnitude - v2_previous_position.magnitude;
									TouchDelta = touchDelta; 
									angle = Vector2.Angle (v2_previous_position, v2_current_position);
									Angle = angle;
		
									if (Mathf.Abs (touchDelta) > i_comfort) 
									{
											if (touchDelta > 0) {
													Debug.Log ("Zoom In");
													if (!mycamera.isOrthoGraphic) 
													{
															Debug.Log ("perspective");
															mycamera.fieldOfView = Mathf.Clamp (Mathf.Lerp (mycamera.fieldOfView, mycamera.fieldOfView
																	- Mathf.Abs (touchDelta) * zoom, Time.deltaTime * zoom), 10, 70);
															OnSpread();
													}
													if (mycamera.isOrthoGraphic) 
													{
															Debug.Log ("orthographic");
															mycamera.orthographicSize = Mathf.Clamp (Mathf.Lerp (mycamera.orthographicSize, mycamera.orthographicSize 
																	- Mathf.Abs (touchDelta), Time.deltaTime), 0.5f, 10);
															OnSpread();
													}
				
											}
											if (touchDelta < 0) 
											{
													Debug.Log ("Zoom out");
													if (!mycamera.isOrthoGraphic) 
													{
															Debug.Log ("perspective");
															mycamera.fieldOfView = Mathf.Clamp (Mathf.Lerp (mycamera.fieldOfView, mycamera.fieldOfView 
																	+ Mathf.Abs (touchDelta) * zoom, Time.deltaTime * zoom), 10, 70);
															OnPinch();
													}
													if (mycamera.isOrthoGraphic) 
													{
															Debug.Log ("orthographic");
															mycamera.orthographicSize = Mathf.Clamp (Mathf.Lerp (mycamera.orthographicSize, mycamera.orthographicSize 
																	+ Mathf.Abs (touchDelta), Time.deltaTime), 0.5f, 10);
															OnPinch();
													}
											}
									}
									if (angle > 0.1) 
									{
											if (Vector3.Cross (v2_current_position, v2_previous_position).z > 0) 
											{
													Debug.Log ("Clockwise Rotation");
													Debug.Log ("Angle : " + angle);
													//Object_To_Move.transform.Rotate (Vector3.back, angle * rotate);
													OnRotateClockwise();
											}
											if (Vector3.Cross (v2_current_position, v2_previous_position).z < 0) 
											{
													Debug.Log ("Anti Clockwise Rotation");
													Debug.Log ("Angle : " + angle);
													//Object_To_Move.transform.Rotate (Vector3.back, angle * -1 * rotate);
													OnRotateAntiClockwise();
											}
									}
							}
						}
					}
			}
	}

	
	//the default functions, define what will happen if the child doesn't override these functions
	public virtual void OnNoTouches ()
	{
	}

	public virtual void OnTouchBegan ()
	{
	}

	public virtual void OnTouchEnded ()
	{
	}

	public virtual void OnTouchMoved ()
	{
	}

	public virtual void OnTouchStayed ()
	{
	}

	public virtual void OnTouchBeganAnywhere ()
	{
	}

	public virtual void OnTouchEndedAnywhere ()
	{
	}

	public virtual void OnTouchMovedAnywhere ()
	{
	}

	public virtual void OnTouchStayedAnywhere ()
	{
	}

	public virtual void OnSwipeUp ()
	{
	}

	public virtual void OnSwipeRight ()
	{
	}

	public virtual void OnSwipeDown ()
	{
	}

	public virtual void OnSwipeLeft ()
	{
	}
	public virtual void OnPinch ()
	{
	}
	public virtual void OnSpread ()
	{
	}
	public virtual void OnDoubleTap ()
	{
	}
	public virtual void OnRotateClockwise ()
	{
	}
	public virtual void OnRotateAntiClockwise ()
	{
	}

}

