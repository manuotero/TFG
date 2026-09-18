

using System;
using System.Collections.Generic;

public class Floor
{
    public Floor(int seed, int columns, int rows, int alg, int specialTile)
    {
        tileset = new Tile[columns, rows];
        Columns = columns;
        Rows = rows;
        
        for(int i = 0; i < tileset.GetLength(1); i++)
        {
            for(int j = 0; j < tileset.GetLength(0); j++)
            {
                tileset[j, i] = new Tile(3);
            }
        }

        List<(int, int)> roomCenters;

        switch (alg)
        {
            case 0: roomCenters = Alg1(seed);
                    SpecialTerrainGenerator(seed, specialTile);
            break;
            default: roomCenters = Alg1(seed);
            break;
        }

        PathGenerator(roomCenters);
    }

    public int[] GenerateSimpleTilemap()
    {
        int[] tilemap = new int[Count];
        int index = 0;

        for(int y = 0; y < tileset.GetLength(1); y++)
        {
            for(int x = 0; x < tileset.GetLength(0); x++)
            {
                var tileType = tileset[x, y].GetTileType();

                switch(tileType)
                {
                    case TileType.GROUND:
                        tilemap[index] = 16;
                        break;

                    default:
                        tilemap[index] = TileAdjacency(x, y, tileType);
                        break;
                } 

                index++;
            }
        }
        return tilemap;
    }

    public (int, int)[] GenerateComplexTilemap()
    {
        (int, int)[] tilemap = new (int, int)[Count];
        int index = 0;
        
        for(int y = 0; y < tileset.GetLength(1); y++)
        {
            for(int x = 0; x < tileset.GetLength(0); x++)
            {
                var tileType = tileset[x, y].GetTileType();

                switch(tileType)
                {
                    case TileType.GROUND:
                        tilemap[index] = (0, 16);
                        break;
                    case TileType.WALL:
                        tilemap[index] = (0, TileAdjacency(x, y, tileType));
                        break;
                    default:
                        tilemap[index] = (1, TileAdjacency(x, y, tileType));
                        break;
                } 

                index++;
            }
        }
        return tilemap;
    }

    private List<(int, int)> Alg1(int seed)
    {
        List<(int, int)> roomCenter = new List<(int, int)>();

        Random rnd = new Random(seed);

        int roomNum = rnd.Next(5, 10);

        int roomLenght = roomNum switch
        {
            5 => tileset.GetLength(1)/2,
            6 => tileset.GetLength(1)/2,
            7 => tileset.GetLength(1)/3,
            8 => tileset.GetLength(1)/3,
            9 => tileset.GetLength(1)/4,
            _ => tileset.GetLength(1)/5
        };

        for (int i = 0; i < roomNum; i++)
        {
            Tile[,] room = RoomGenerator.RndRoomGen(rnd.Next(), roomLenght);

            (int, int) startPoint = (rnd.Next(1, tileset.GetLength(0) - roomLenght - 1), 
                                    rnd.Next(1, tileset.GetLength(1) - roomLenght - 1));

            (int, int) endPoint = (startPoint.Item1 + roomLenght - 1,
                                    startPoint.Item2 + roomLenght - 1);      

            (int, int) roomPos = (0, 0);

            for(int floorPosY = startPoint.Item2; floorPosY < endPoint.Item2; floorPosY++)
            {
                for (int floorPosX = startPoint.Item1; floorPosX < endPoint.Item1; floorPosX++)
                {
                    Tile roomTile = room[roomPos.Item1, roomPos.Item2];

                    if (roomTile.GetTileType() == TileType.GROUND)
                    {
                        tileset[floorPosX, floorPosY].setTileType(0);
                    }

                    if (roomPos.Item1 == (room.GetLength(0) - 1)/2 &&
                        roomPos.Item2 == (room.GetLength(1) - 1) / 2)
                    {
                        roomCenter.Add((floorPosX, floorPosY));
                    }

                    roomPos.Item1++;
                }

                roomPos.Item1 = 0;
                roomPos.Item2++;
            }   
        }
        return roomCenter;
    }

    private void SpecialTerrainGenerator(int seed, int tileType)
    {
        Random rnd = new Random(seed);
        int rndGen = rnd.Next(100, 120);

        for (int x = 0; x < rndGen; x++)
        {
            int rndSize = rnd.Next(8, 15);
            (int x, int y) startPoint = (rnd.Next(0, Columns - rndSize), 
                                    rnd.Next(0, Rows - rndSize));
            (int x, int y) endPoint = (startPoint.Item1 + rndSize,
                                    startPoint.Item2 + rndSize);
            
            for (int b = startPoint.y; b < endPoint.y; b++)
            {
                for (int a = startPoint.x; a < endPoint.x; a++)
                {
                    if (tileset[a, b].GetTileType() != TileType.GROUND)
                    {
                        int genTile = rnd.Next(0, 10);
                        if (genTile > 1)
                        {
                            tileset[a, b].setTileType(tileType);   
                        }
                    }
                }
            }
        }
    }

    private void PathGenerator(List<(int, int)> roomCenter)
    {
        var pathList = PathFinder.HallwayPathFinder(roomCenter);

        foreach(var point in pathList)
        {
            if (tileset[point.Item1, point.Item2].GetTileType() != TileType.GROUND)
                tileset[point.Item1, point.Item2].setTileType(0);
        }
    }

    private TileType? GetTileTypeAt(int x, int y)
    {
        if (x < 0 || x >= Columns || y < 0 || y >= Rows)
        {
            return null;
        }else
            return tileset[x, y].GetTileType();
    }

    private int TileAdjacency(int x, int y, TileType tile)
    {  
        int count = 0;
        var upper = GetTileTypeAt(x, y - 1);
        var under = GetTileTypeAt(x, y + 1);
        var right = GetTileTypeAt(x + 1, y);
        var left = GetTileTypeAt(x - 1, y);

        if (upper != tile && upper != null)
        {
            count += 1;
        }
        if (under != tile && under != null)
        {
            count += 2;
        }
        if (right != tile && right != null)
        {
            count += 4;
        }
        if (left != tile && left != null)
        {
            count += 8;
        }

        return count;
    }

    private Tile[,] tileset {get;}
    private int Columns {get;}
    private int Rows {get;}
    private int Count => Columns * Rows;
}