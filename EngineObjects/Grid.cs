using System;
using System.Numerics;
using GlBindings;
using openGlTest;
using openGlTest.EngineObjects;

namespace GameGrid
{
    public class Grid
    {
        public Land[,] gridObjects = new Land[20, 20];

        internal void Start()
        {
            for (int x = 0; x < gridObjects.GetLength(0); x++)
            {
                for (int y = 0; y < gridObjects.GetLength(1); y++)
                {
                    gridObjects[x,y] = new Land(x,y,GridFloorType.BARREN);
                    Renderer.subscribeToRender.Add(gridObjects[x,y].sprite);
                }
            }
        }
        internal void Update()
        {
        }
    }
    public class Land
    {
        public readonly int x, y;
        public Land(int x, int y, GridFloorType type)
        {
            this.x = x;
            this.y = y;
            switch (type)
            {
                case GridFloorType.BARREN:
                    sprite = new Sprite(TextureManager.GetTexture("land_barren.png"),Program.shaderProgram);
                    break;
                default:
                    break;
            }
            if(sprite != null)
            {
                sprite.transform.position = new Vector3(x,y,0);
            }
        }
        public Sprite sprite;
    }
    public enum GridFloorType
    {
        NONE = 0,
        BARREN = 1,
        WALL = 2
    }
}
