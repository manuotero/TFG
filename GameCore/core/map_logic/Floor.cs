

using System;
using System.Collections.Generic;

public class Floor
{
    public Floor(int seed, int columns, int rows, int alg)
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

        switch (alg)
        {
            case 1: Alg1(seed);
            break;
            default:
            break;
        }
    }

    public int[] GenerateTilemap()
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
                        tilemap[index] = 01;
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

    private void Alg1(int seed)
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

        PathGenerator(roomCenter);
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

        switch (count)
        {
            //Up
            case 1:
                return 09;
            
            //Down
            case 2:
                return 07;
            
            //Up down
            case 3:
                return 13;

            //Right
            case 4:
                return 08;
            
            //Up right
            case 5:
                return 04;

            //Under rigth
            case 6:
                return 03;

            //U left
            case 7:
                return 16;

            //Left
            case 8:
                return 10;

            //Left up
            case 9:
                return 05;

            //Left down
            case 10:
                return 06;

            //U right
            case 11:
                return 14;

            //Rigth left
            case 12:
                return 12;

            //Reverse u
            case 13:
                return 00;

            //U
            case 14:
                return 15;

            //All
            case 15:
                return 02;

            default:
                return 11;
        }
    }

    private Tile[,] tileset {get;}
    private int Columns {get;}
    private int Rows {get;}
    private int Count => Columns * Rows;
}