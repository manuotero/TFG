

using System;

public static class RoomGenerator
{
    private static Tile[,] BaseRoom(int length)
    {
        Tile[,] tileset = new Tile[length, length];
        
        for(int i = 0; i < tileset.GetLength(1); i++)
        {
            for(int j = 0; j < tileset.GetLength(0); j++)
            {
                tileset[j, i] = new Tile(10);
            }
        }

        return tileset;
    }

    public static Tile[,] RndRoomGen(int seed, int length)
    {
        Tile[,] tileset = BaseRoom(length);
        Random rnd = new Random(seed);
        int squareNum = rnd.Next(2, 7);
        int squareSize = squareNum switch
      {
         2 => tileset.GetLength(0) - 2,
         3 => tileset.GetLength(0) - 3,
         4 => tileset.GetLength(0) - 4,
         5 => tileset.GetLength(0) - 5,
         6 => tileset.GetLength(0) - 6,
         _ => tileset.GetLength(0) - 1
      };

        for (int i = 0; i < squareNum; i++)
        {
            int initXPos = rnd.Next(0, tileset.GetLength(0) - squareSize);
            int initYPos = rnd.Next(0, tileset.GetLength(1) - squareSize);

            int endXPos = initXPos + squareSize;
            int endYPos = initYPos + squareSize;

            for (int y = initYPos; y < endYPos; y++)
            {
                for(int x = initXPos; x  < endXPos; x++)
                {
                    if (tileset[x, y].GetTileType().Equals(TileType.VOID))
                    {
                        tileset[x, y].setTileType(0);
                    }
                }
            }
        }
        int xCenter = (tileset.GetLength(0) - 1)/2;
        int yCenter = (tileset.GetLength(0) - 1)/2;

        if (tileset[xCenter, yCenter]
        .GetTileType() == TileType.VOID)
        {
            for(int i = yCenter - 1; i <= yCenter + 1; i++)
            {
                for (int j = xCenter - 1; j <= yCenter + 1; j++)
                {
                    tileset[j, i].setTileType(0);
                }
            }
        }

        return tileset;
    }

    public static Tile[,] SquareRoomGen(int seed, int length)
    {
        Tile[,] tileset = BaseRoom(length);
        Random rnd = new Random(seed);
        int xCenter = (tileset.GetLength(0) - 1)/2;
        int yCenter = (tileset.GetLength(1) - 1)/2;
        int size = rnd.Next(1, xCenter + 1);

        int initXPos = xCenter - size;
        int initYPos = yCenter - size;

        for (int i = initYPos; i <= initYPos + size*2; i++)
        {
            for (int j = initXPos; j <= initXPos + size*2; j++)
            {
                tileset[j, i].setTileType(0);
            }
        }

        return tileset;
    }

    public static Tile[,] DiamondRoomGen(int seed, int length)
    {
        Tile[,] tileset = BaseRoom(length);
        Random rnd = new Random(seed);
        int xCenter = (tileset.GetLength(0) - 1)/2;
        int yCenter = (tileset.GetLength(1) - 1)/2;
        int size = rnd.Next(2, xCenter + 1);

        int initXPos = xCenter;
        int initYPos = yCenter - size;

        int varSize = 0;

        for (int i = initYPos; i < tileset.GetLength(1) ; i++)
        {
            for (int j = initXPos; j <= initXPos + varSize; j++)
            {
                tileset[j, i].setTileType(0);
            }

            if (i < yCenter)
            {
                varSize += 2;
                initXPos--;
            }
            else
            {
                varSize -= 2;
                initXPos++;
            }
        }

        return tileset;
    }

    /*public static Tile[,] specialRoomGen()
    {
        
    }*/

}