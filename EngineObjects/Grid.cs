namespace GameGrid
{
    public class Grid
    {
        public GridItem[,] gridObjects = new GridItem[20,20];
    }
    public class GridItem
    {
        public readonly int x,y;
        public GridItem(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
