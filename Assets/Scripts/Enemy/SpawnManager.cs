using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject enemyPrefab;

    public List<Enemy> enemies = new List<Enemy>();

    private Vector2[,] matrix;
    private Vector2 startPoint = new Vector2(-2f, 4.5f);
    private float step = .5f;
    private int numRows = 5;
    private int numCols = 9;

    public enum shapeType
    {
        Square,
        Rectangle,
        Diamond,
        Triangle

    }
    private Dictionary<shapeType, List<Vector2>> shapeDir = new Dictionary<shapeType, List<Vector2>>();
    void Start()
    {
        matrix = new Vector2[numRows, numCols];
        DrawMatrix();
        CaculateShape();
        SpawnEnemy();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        if (shapeDir.ContainsKey(shapeType.Square))
        {
            List<Vector2> squareList = shapeDir[shapeType.Square];
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].SetTargetPosition(squareList[i]);
            }
        }
        yield return new WaitForSeconds(5f);
        if (shapeDir.ContainsKey(shapeType.Diamond))
        {
            List<Vector2> squareList = shapeDir[shapeType.Diamond];
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].SetTargetPosition(squareList[i]);
            }
        }
        yield return new WaitForSeconds(5f);
        if (shapeDir.ContainsKey(shapeType.Triangle))
        {
            List<Vector2> squareList = shapeDir[shapeType.Triangle];
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].SetTargetPosition(squareList[i]);
            }
        }
        yield return new WaitForSeconds(5f);
        if (shapeDir.ContainsKey(shapeType.Rectangle))
        {
            List<Vector2> squareList = shapeDir[shapeType.Rectangle];
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].SetTargetPosition(squareList[i]);
            }
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void CaculateShape()
    {
        //Square
        int centerX = numCols / 2;
        int centerY = numRows / 2;
        List<Vector2> newListSquare = new List<Vector2>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int row = centerY - 1 + i;
                int col = centerX - 1 + j;
                newListSquare.Add(matrix[row, col]);

            }
        }
        shapeDir.Add(shapeType.Square, newListSquare);
        int center = numCols / 2;

        //Diamond
        List<Vector2> newListDiamond = new List<Vector2>();
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                if (j == center)
                {
                    if (i == 0 || i == numRows - 1) newListDiamond.Add(matrix[i, j]);
                    else if (i == 2)
                    {
                        newListDiamond.Add(matrix[i, j - i - 1]);
                        newListDiamond.Add(matrix[i, j - i + 1]);
                        newListDiamond.Add(matrix[i, j - i]);
                        newListDiamond.Add(matrix[i, j + i - 1]);
                        newListDiamond.Add(matrix[i, j + i + 1]);
                        newListDiamond.Add(matrix[i, j + i]);
                    }
                    else if (i == 1)
                    {
                        newListDiamond.Add(matrix[i, j - i - 1]);
                        newListDiamond.Add(matrix[i, j - i]);
                        newListDiamond.Add(matrix[i, j + i + 1]);
                        newListDiamond.Add(matrix[i, j + i]);
                    }
                    else if (i == 3)
                    {
                        newListDiamond.Add(matrix[i, j - i + 2]);
                        newListDiamond.Add(matrix[i, j - i + 1]);
                        newListDiamond.Add(matrix[i, j + i - 2]);
                        newListDiamond.Add(matrix[i, j + i - 1]);
                    }
                }
            }
        }
        shapeDir.Add(shapeType.Diamond, newListDiamond);

        //Triangle
        List<Vector2> newListTriangle = new List<Vector2>();
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                if (j == center)
                {
                    if (i == 0) newListTriangle.Add(matrix[i, j]);
                    else if (i < numRows - 1)
                    {
                        newListTriangle.Add(matrix[i, j - i]);
                        newListTriangle.Add(matrix[i, j + i]);
                    }
                }
                if (i == numRows - 1)
                {
                    newListTriangle.Add(matrix[i, j]);
                }
            }
        }
        shapeDir.Add(shapeType.Triangle, newListTriangle);
        //Rectangle
        List<Vector2> newListRectangle = new List<Vector2>();
        for (int i = 0; i < numRows - 2; i++)
        {
            for (int j = 1; j < numCols - 1; j++)
            {
                if (i != 1)
                {
                    newListRectangle.Add(matrix[i, j]);
                }
                else if (j == 1 || j == numCols - 2)
                {
                    newListRectangle.Add(matrix[i, j]);
                }
            }
        }
        shapeDir.Add(shapeType.Rectangle, newListRectangle);
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.SetParent(transform);
            enemies.Add(enemy.GetComponent<Enemy>());
        }
    }

    private void DrawMatrix()
    {
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                float x = startPoint.x + col * step;
                float y = startPoint.y - row * step;
                matrix[row, col] = new Vector2(x, y);
            }
        }
    }
}
