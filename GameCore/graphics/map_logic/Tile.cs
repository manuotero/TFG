public class Tile
{
    public Tile(int tile)
    {
        setTileType(tile);
    }
    public TileType GetTileType()
    {
        return tileType;
    }

    public void setTileType(int tile)
    {
        switch (tile)
        {
            case 0:
                tileType = TileType.GROUND;
            break;
            
            case 1:
                tileType = TileType.WATER;
            break;
            
            case 2:
                tileType = TileType.AIR;
            break;
            
            case 3:
                tileType = TileType.WALL;
            break;

            default:
                tileType = TileType.VOID;
            break;
        }
    }

    override
    public string ToString()
    {
        if (tileType == TileType.VOID)
        
            return "   ";
        else
            return "[" + tileType.ToString()[0] + "]";
    }

    private TileType tileType {get; set;}
    //private Item tileItem {get; set;}
}

public enum TileType
{
    GROUND,
    WATER,
    AIR,
    VOID,
    WALL
}