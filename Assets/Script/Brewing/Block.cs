using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    Element element;
    BoardTile boardTile;
    bool placable = false;
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Place() {
        if (placable && boardTile != null) {
            boardTile.PlaceOn(element);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        // Vector3 globalPos = transform.TransformPoint(transform.position);
        // Vector3 globalOtherPos = transform.TransformPoint(other.gameObject.transform.position);
        Vector3 globalPos = transform.position;
        Vector3 globalOtherPos = other.gameObject.transform.position;
        if (other.GetComponent<BoardTile>() != null) {
            //if boardTile = null -> boardTile = this obj | if this obj near obj than boardTile (or it is boardTile) -> boardTile = this obj
            if (boardTile == null) {
                boardTile = other.GetComponent<BoardTile>();
            } else if ((boardTile.gameObject.transform.position - transform.position).magnitude 
                        >= (other.gameObject.transform.position - transform.position).magnitude) {
                //The current boardTile is not capable anymore
                placable = false;

                boardTile.ChangeColor(GlobalGameVar.Instance().elementDic[boardTile.element].color);
                //Change the boardTile
                boardTile = other.GetComponent<BoardTile>();
                
                if (Math.Abs(globalPos.x - globalOtherPos.x) < GlobalGameVar.Instance().blockWidth / 2
                && Math.Abs(globalPos.y - globalOtherPos.y) < GlobalGameVar.Instance().blockWidth / 2
                && boardTile.CanBePlacedOn(element)) {
                    placable = true;
                    boardTile.ChangeColor(Color.cyan);
                } else {
                    placable = false;
                    if (boardTile.element == Element.None) {
                        boardTile.ChangeColor(Color.white);
                    }
                }
            }
        }
    }
    public bool IsPlacable() {
        return placable;
    }
    public void SetElement(Element el) {
        element = el;
        GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[element].color;
    }
}
