using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RadialSlider: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	bool isPointerDown=false;

	// Called when the pointer enters our GUI component.
	// Start tracking the mouse
	public void OnPointerEnter( PointerEventData eventData )
	{
		StartCoroutine( "TrackPointer" );            
	}
	
	// Called when the pointer exits our GUI component.
	// Stop tracking the mouse
	public void OnPointerExit( PointerEventData eventData )
	{
		StopCoroutine( "TrackPointer" );
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Vector3 mousePos = Input.mousePosition;

		// Convert mouse position to screen space
		mousePos.z = Camera.main.nearClipPlane;
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);

		// Rotate towards mouse position
		Vector3 difference = mouseWorldPosition - transform.position;
		float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0, 0, rotation);
		isPointerDown = true;
		//Debug.Log("mousedown");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPointerDown= false;
		//Debug.Log("mousedown");
	}

	// mainloop
	IEnumerator TrackPointer()
	{
		var ray = GetComponentInParent<GraphicRaycaster>();
		var input = FindObjectOfType<StandaloneInputModule>();

		var text = GetComponentInChildren<Text>();
		
		if( ray != null && input != null )
		{
			while( Application.isPlaying )
			{                    

				// TODO: if mousebutton down
				if (isPointerDown)
				{

					Vector2 localPos; // Mouse position  
					RectTransformUtility.ScreenPointToLocalPointInRectangle( transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos );
						
					// local pos is the mouse position.
					float angle = (Mathf.Atan2(-localPos.y, localPos.x)*180f/Mathf.PI+180f)/360f;

					GetComponent<Image>().fillAmount = angle;

					//GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, angle);

					text.text = angle.ToString();

					//Debug.Log(localPos+" : "+angle);	
				}

				yield return 0;
			}        
		}
		else
			UnityEngine.Debug.LogWarning( "Could not find GraphicRaycaster and/or StandaloneInputModule" );        
	}





}
