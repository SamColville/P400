﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameBoard : MonoBehaviour
{
    //TODO: 

    //Read board size
    //generate grid of size (x,y)
    [SerializeField]
    GameObject boardTile;
    [SerializeField]
    List<GameObject> AllTiles;
    public bool isDrawn;
    [SerializeField]
    int width;
    [SerializeField]
    int length;

    [SerializeField]
    public Material ClickedMat;
    [SerializeField]
    public Material UnclickedMat;

    GameObject CurrentTile;
    GameObject PreviousTile;

    [SerializeField]
    Tile tile;
    [SerializeField]
    PlayerCharacter player;

    public Tile selectedTile;
    public PlayerCharacter selectedPlayer;

    //public ARRaycastManager arRaycastManager;


    void Start()
    {
        isDrawn = false;
        AllTiles = new List<GameObject>();
        //arRaycastHits= new List<ARRaycastHit>();
        //StartCoroutine(DrawBoardDelayed(width, length));
        //DrawBoard();
        
        //CreatePlayer("Player1", new Vector3(0, 0, 0));
        //CreatePlayer("Player1", new Vector3(0, 0, 4));
        //CreatePlayer("Player1", new Vector3(4, 0, 4));
        //CreatePlayer("Player1", new Vector3(4, 0, 0));
    }

    public void DrawBoard()
    {
        isDrawn = true;
        //Vector3 widthVe = Vector3.right * w;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                Tile spawnedTile = Instantiate(tile, new Vector3(x, 0, z), Quaternion.identity);
                spawnedTile.transform.SetParent(transform);
                spawnedTile.name = string.Concat(x.ToString(), " , ", z.ToString());
                spawnedTile.GridPos.x = x;
                spawnedTile.GridPos.y = z;

            }
        }
    }

    public void DrawBoardFromTouch(Vector3 startPos)
    {
        isDrawn = true;
        Vector3 currentPos = startPos;
        //new Vector3(x, 0, z)
        for (int x = 0; x < width; x++)
        {
            
            for (int z = 0; z < length; z++)
            {
                //currentPos
                Tile spawnedTile = Instantiate(tile, currentPos, Quaternion.identity);
                spawnedTile.transform.SetParent(transform);
                spawnedTile.name = string.Concat(x.ToString(), " , ", z.ToString());
                spawnedTile.GridPos.x = x;
                spawnedTile.GridPos.y = z;
            }
        }
    }

    public void DrawTiles(Vector3 start)
    {
        isDrawn = true;
        Vector3 lengthVe = start * length;

        for (int i = 0; i < width; i++)
        {
            //start = Vector3.forward * i;
            Vector3 startWidth;

            for (int j = 0; j < length; j++)
            {
                startWidth = Vector3.right * j;
                startWidth += start;
                Instantiate(tile, startWidth + lengthVe, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    var touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Ended)
        //    {
        //        if (Input.touchCount == 1)
        //        {
        //            if (arRaycastManager.Raycast(touch.position, arRaycastHits))
        //            {
        //                var pose = arRaycastHits[0].pose;
        //                CreateCube(pose.position);
        //                return;
        //            }
        //            Ray rayCast = Camera.main.ScreenPointToRay(touch.position);
        //            if (Physics.Raycast(rayCast, out RaycastHit hit))
        //            {
        //                if (hit.collider.tag == "CubeObject")
        //                {
        //                    DeleteCube(hit.collider.gameObject);
        //                }
        //            }

        //        }
        //    }
        //}
    }

    

    public Tile GetSelectedTile()
    {
        if (!selectedTile)
        {
            Debug.Log("tile is null GameBoard.GetSelectedTile()");
            throw new System.Exception("tile is null GameBoard.GetSelectedTile()");
        }
        else
            return selectedTile;
    }

    public PlayerCharacter GetSelectedPlayer()
    {
        if (!selectedPlayer)
        {
            Debug.Log("player is null GameBoard.GetSelectedPlayer()");
            throw new System.Exception("player is null GameBoard.GetSelectedPlayer()");
        }
        else
            return selectedPlayer;
    }

    public void DeselectPlayer()
    {
        selectedPlayer.UpdateMaterial(UnclickedMat);
        selectedPlayer.isSelected = false;
    }

    public void DeselectTile()
    {
        if (selectedTile != null)
        {
            selectedTile.UpdateMat(UnclickedMat);
            selectedTile.isSelected = false;
        }
    }

    private void CreatePlayer(string name, Vector3 pos)
    {
        PlayerCharacter newPlayer = Instantiate(player, transform.position, Quaternion.identity);
        newPlayer.transform.SetParent(transform);
        newPlayer.name = name;
        newPlayer.SetPosition(pos);
    }
}


    #region Commented Code
    //private void OnMouseUp()
    //{
    //    Debug.Log("Entered OnMouseUp()");
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        if (PreviousTile != null)
    //        {
    //            PreviousTile.GetComponent<Renderer>().material = UnclickedMat;
    //        }

    //        CurrentTile = hit.collider.gameObject;
    //        PreviousTile = CurrentTile;
    //        if (CurrentTile.TryGetComponent(out Tile tile))
    //        {
    //            CurrentTile.GetComponent<Renderer>().material = ClickedMat;
    //            Debug.Log(tile.GridName);
    //        }
    //    }
    //}

    //private IEnumerator DrawBoardDelayed(int w, int l)
    //{
    //    WaitForSeconds wait = new WaitForSeconds(0.05f);

    //    //Vector3 widthVe = Vector3.right * w;
    //    Vector3 heightVe = Vector3.forward * l;

    //    for (int i = 0; i < w; i++)
    //    {
    //        Vector3 start = Vector3.forward * i;
    //        Vector3 startWidth;
    //        //Debug.DrawLine(start, start + widthVe);

    //        //Instantiate(boardTile, start + widthVe, Quaternion.identity);

    //        for (int j = 0; j < l; j++)
    //        {
    //            startWidth = Vector3.right * j;
    //            startWidth += start;
    //            GameObject newTile = Instantiate(boardTile, startWidth + heightVe, Quaternion.identity);
    //            AllTiles.Add(newTile);
    //            if(newTile.TryGetComponent(out Tile tile))
    //            {
    //                tile.GridPos.Add(i);
    //                tile.GridPos.Add(j);
    //                tile.GridName = i.ToString() + j.ToString();
    //            }
    //            yield return wait;
    //        }
    //    }

    //}

    //public void SelectPlayer(PlayerCharacter player)
    //{
    //    DeselectTile();

    //    if (selectedPlayer)
    //        DeselectPlayer();

    //    selectedPlayer = player;
    //    selectedPlayer.UpdateMaterial(ClickedMat);
    //    selectedPlayer.isSelected = true;
    //}

    //public void SelectTile(Tile tile)
    //{
    //    if (selectedTile)
    //        DeselectTile();

    //    selectedTile = tile;
    //    selectedTile.UpdateMat(ClickedMat);
    //    selectedTile.isSelected = true;
    //}
    #endregion