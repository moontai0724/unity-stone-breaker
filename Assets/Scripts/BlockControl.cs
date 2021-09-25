using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(AudioSource))]
public class BlockControl : MonoBehaviour
{
    private float timer = 0.0f;
    private float timeLimit = 5.0f;
    private int blockAmount = 10;
    private int tileXStart = -9, tileXEnd = 8, tileYStart = -2, tileYEnd = 4;
    public Text grade;
    public bool initialized = false;
    public Text start;
    public AudioSource hitAudio;

    Tile block_once;
    Tile block_twice;
    Tile block_unbreakable;

    Tilemap tilemap;

    void Start()
    {
        this.initialized = false;
        this.start.enabled = true;
        this.block_once = Resources.Load<Tile>("Tiles/block_once");
        this.block_twice = Resources.Load<Tile>("Tiles/block_twice");
        this.block_unbreakable = Resources.Load<Tile>("Tiles/block_unbreakable");

        this.tilemap = this.GetComponent<Tilemap>();
    }

    void Update()
    {
        if (BallControl.started)
        {
            if (this.initialized == false)
                this.initialize();

            this.timer += Time.deltaTime;
            if (this.timer > this.timeLimit)
            {
                this.timeLimit += 1.0f;
                this.blockAmount += 1;
                this.drawTiles(false);
                this.timer = 0.0f;
                this.addGrade(this.blockAmount);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        hitAudio.Stop();
        hitAudio.Play(0);
        foreach (ContactPoint2D hit in collision.contacts)
        {
            float x1 = hit.collider.transform.position.x + hit.normal.x, y1 = hit.collider.transform.position.y + hit.normal.y;
            float x2 = hit.point.x + hit.normal.x, y2 = hit.point.y + hit.normal.y;
            float x = (x1 + x2) / 2, y = (y1 + y2) / 2;
            Vector3Int position = this.tilemap.WorldToCell(new Vector2(x, y));
            // Debug.Log("point2: s" + hit.point + " c" + new Vector2(x, y) + " r" + position);

            if (object.ReferenceEquals(this.tilemap.GetTile(position), this.block_once))
            {
                this.addGrade(10);
                this.tilemap.SetTile(position, null);
            }
            else if (object.ReferenceEquals(this.tilemap.GetTile(position), this.block_twice))
            {
                this.addGrade(10);
                this.tilemap.SetTile(position, this.block_once);
            }
        }

        if (this.targetsRemain() == 0)
        {
            this.win();
        }
    }

    void initialize()
    {
        SceneController.status = 0;
        SceneController.grade = 0;
        this.drawTiles(true);
        this.blockAmount = 5;
        this.start.enabled = false;
        this.initialized = true;
    }

    void drawTiles(bool clearTiles)
    {
        if (clearTiles)
            this.tilemap.ClearAllTiles();

        for (int i = 0; i < this.blockAmount; i++)
            this.drawRandomTile();

        if (this.isTileMapFull())
            this.lose();
    }

    void drawRandomTile()
    {
        Tile[] tiles = new Tile[] { block_once, block_twice, block_unbreakable };

        int randomX = (int)this.generateRandomNumber(this.tileXStart, this.tileXEnd);
        int randomY = (int)this.generateRandomNumber(this.tileYStart, this.tileYEnd);
        Vector3Int targetPoint = new Vector3Int(randomX, randomY, 0);

        if (this.tilemap.HasTile(targetPoint) == false)
        {
            // Debug.Log("Target: " + targetPoint);
            this.tilemap.SetTile(targetPoint, tiles[(int)this.generateRandomNumber(0, tiles.Length - 1)]);
        }
        else if (this.isTileMapFull() == false)
            drawRandomTile();
    }

    double generateRandomNumber(float minimum, float maximum)
    {
        return Math.Round(UnityEngine.Random.Range(minimum, maximum));
    }

    int targetsRemain()
    {
        int amount = 0;
        TileBase[] tileblock = this.tilemap.GetTilesBlock(this.tilemap.cellBounds);
        for (int i = 0; i < tileblock.Length; i++)
            if (tileblock[i] != null && object.ReferenceEquals(tileblock[i], this.block_unbreakable) == false)
                amount++;
        return amount;
    }

    int tilesMaxAmount()
    {
        Vector3Int tileSize = this.tilemap.size;
        return tileSize.x * tileSize.y * tileSize.z;
    }

    bool isTileMapFull()
    {
        TileBase[] tileblock = this.tilemap.GetTilesBlock(this.tilemap.cellBounds);
        for (int i = 0; i < tileblock.Length; i++)
            if (tileblock[i] == null)
                return false;
        return true;
    }

    void lose()
    {
        SceneController.lose();
    }

    void win()
    {
        SceneController.win();
    }

    void addGrade(int gradeAmount)
    {
        SceneController.grade += gradeAmount;
        grade.text = "分數：" + SceneController.grade;
    }
}