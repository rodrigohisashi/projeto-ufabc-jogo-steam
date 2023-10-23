using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainImageScript2 : MonoBehaviour
{
    
    [SerializeField] private GameObject image_unknown;
    [SerializeField] private GameControllerScript2 gameController;

    public void OnMouseDown()
    {
        if (image_unknown.activeSelf && gameController.canOpen)
        {
            image_unknown.SetActive(false);
            gameController.imageOpened(this);
        }
    }

    private int _spriteId;
    public int spriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprites(int id, Sprite image)
{
    _spriteId = id;

    GetComponent<SpriteRenderer>().sprite = image;


}


    public void Close()
    {
        image_unknown.SetActive(true); // Hide image
    }
    
}
