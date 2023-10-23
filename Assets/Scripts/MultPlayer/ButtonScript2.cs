using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript2 : MonoBehaviour
{
   [SerializeField] private GameControllerScript2 gameController2;
    [SerializeField] private string functionOnClick;


    public void OnMouseOver()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // Get the GameObject component of the current GameObject
        //GameObject currentGameObject = GetComponent<GameObject>();
        //string gameObjectName = currentGameObject.name;

        if(sprite != null)
        {
            if( gameObject.name.Equals("Parabens-Button"))
            {
                Debug.Log("GameObject name: " + gameObject.name);
                sprite.color = Color.cyan;
            }
            else{
                //Debug.LogError("GameObject component not found on the current GameObject.");
            }
            
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
    }

    public void OnMouseUp()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);
        if(gameController2 != null)
        {
            gameController2.SendMessage(functionOnClick);
        }
    }

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        // Get the GameObject component of the current GameObject
        //GameObject currentGameObject = GetComponent<GameObject>();
        //string gameObjectName = currentGameObject.name;

        if(sprite != null)
        {
            if(gameObject.name.Equals("Parabens-Button"))
            {
                Debug.Log("GameObject name: " + gameObject.name);
                sprite.color = Color.white;
            }
            else{
                //Debug.LogError("GameObject component not found on the current GameObject.");
            }
            
        }
    }
}
