using System;
using System.Text;

public class GameEntity
{

    public GameEntity(String name,
                    int bHp,
                    int bMl,
                    int bRg,
                    int bPa,
                    int bBa,
                    int bIn,
                    int level)
    {
        EntName = name;

        _baseHp = bHp;
        _baseMl = bMl;
        _baseRg = bRg;
        _basePa = bPa;
        _baseBa = bBa;
        _baseIn = bIn;

        Level = level;
    }

    public (int, int) SpriteLocation(int size)
    {
        return (size * ActPos.x, size * ActPos.y);
    }

    override public String ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Name: " + EntName).AppendLine();
        sb.Append("Base HP: " + _baseHp).Append(", Total HP: " + MaxHp).AppendLine();
        sb.Append("Base ML:" + _baseMl).Append(", Total ML: " + Ml).AppendLine();
        sb.Append("Base Rg:" + _baseRg).Append(", Total Rg: " + Rg).AppendLine();
        sb.Append("Base Pa:" + _baseMl).Append(", Total Pa: " + Pa).AppendLine();
        sb.Append("Base Ba:" + _baseMl).Append(", Total Ba: " + Ba).AppendLine();
        sb.Append("Base In:" + _baseMl).Append(", Total In: " + In).AppendLine();

        return sb.ToString();
    }

    public String EntName {get; set;}
    private readonly String SpriteFile;
    private int _baseHp {get; init;}
    private int _baseMl {get; init;}
    private int _baseRg {get; init;}
    private int _basePa {get; init;}
    private int _baseBa {get; init;}
    private int _baseIn {get; init;}

    public int Level {get; set;}
    public int MaxHp => (int)(_baseHp * Level * 0.025);
    public int ActHp {get; set;}
    public int Ml => (int)(_baseMl * Level * 0.02);
    public int Rg => (int)(_baseRg * Level * 0.02);
    public int Pa => (int)(_basePa * Level * 0.02);
    public int Ba => (int)(_baseBa * Level * 0.02);
    public int In => (int)(_baseIn * Level * 0.02);

    public float MlMod = 1;
    public float RGMod = 1;
    public float PaMod = 1;
    public float BaMod = 1;
    public float InMod = 1;

    public (int x, int y) ActPos {get; set;}

    /*
    Lista de ataques
    */

    /*
    Lista de habilidades
    */
}