using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject enemyPrefab;

    public List<Enemy> enemies = new List<Enemy>();

    private Vector2[,] matrix;
    private Vector2 startPoint = new Vector2(-2.4f, 4.5f);
    private float step = .6f;
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
        CalculateShape();
        GenerateEnemies();
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
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void CalculateShape()
    {
        int centerCol = numCols / 2;
        int centerRow = numRows / 2;


        shapeDir.Add(shapeType.Square, GenerateShape(centerRow, centerCol, 4, 4));

        shapeDir.Add(shapeType.Diamond, GenerateDiamond(centerRow, centerCol));

        shapeDir.Add(shapeType.Triangle, GenerateTriangle(centerRow, centerCol));

        shapeDir.Add(shapeType.Rectangle, GenerateRectangle());
    }

    private List<Vector2> GenerateShape(int centerY, int centerX, int rows, int cols)
    {
        List<Vector2> newList = new List<Vector2>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int row = centerY - rows / 2 + i;
                int col = centerX - cols / 2 + j;
                newList.Add(matrix[row, col]);
            }
        }

        return newList;
    }

    private List<Vector2> GenerateDiamond(int centerRow, int centerCol)
    {
        List<Vector2> newList = new List<Vector2>();

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                if (j == centerCol)
                {
                    if (i == 0 || i == numRows - 1) newList.Add(matrix[i, j]);
                    else if (i == 2)
                    {
                        newList.Add(matrix[i, j - i - 1]);
                        newList.Add(matrix[i, j - i]);
                        newList.Add(matrix[i, j - i + 1]);
                        newList.Add(matrix[i, j + i - 1]);
                        newList.Add(matrix[i, j + i]);
                        newList.Add(matrix[i, j + i + 1]);
                    }
                    else if (i == 1)
                    {
                        newList.Add(matrix[i, j - i - 1]);
                        newList.Add(matrix[i, j - i]);
                        newList.Add(matrix[i, j + i + 1]);
                        newList.Add(matrix[i, j + i]);
                    }
                    else if (i == 3)
                    {
                        newList.Add(matrix[i, j + i - 1]);
                        newList.Add(matrix[i, j - i + 1]);
                        newList.Add(matrix[i, j + i - 2]);
                        newList.Add(matrix[i, j - i + 2]);
                    }
                }
            }
        }
        return newList;
    }

    private List<Vector2> GenerateTriangle(int centerRow, int centerCol)
    {
        List<Vector2> newList = new List<Vector2>();

        for (int i = 0; i < numRows; i++)
        {
            int startCol = Math.Max(centerCol - i, 0);
            int endCol = Math.Min(centerCol + i, numCols - 1);

            for (int j = startCol; j <= endCol; j++)
            {
                if (j == startCol || j == endCol || i == numRows - 1)
                {
                    newList.Add(matrix[i, j]);
                }
            }
        }

        return newList;
    }


    private List<Vector2> GenerateRectangle()
    {
        List<Vector2> newList = new List<Vector2>();

        for (int i = 0; i < numRows - 2; i++)
        {
            for (int j = 1; j < numCols - 1; j++)
            {
                if (i != 1 || j == 1 || j == numCols - 2)
                {
                    newList.Add(matrix[i, j]);
                }
            }
        }

        return newList;
    }

    private void GenerateEnemies()
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
