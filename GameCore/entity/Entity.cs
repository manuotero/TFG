


using System;

public class Entity
{

    public Entity(String name, 
                    String file,
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

    public String EntName {get; set;}

    private int _baseHp {get; init;}
    private int _baseMl {get; init;}
    private int _baseRg {get; init;}
    private int _basePa {get; init;}
    private int _baseBa {get; init;}
    private int _baseIn {get; init;}
    private int[] _spriteSet {get; init;} 

    public int Level {get; set;}
    public int MaxHp => (int)(_baseHp * Level * 0.5);
    public int ActHp {get; set;}
    public int Ml => (int)(_baseMl * Level * 0.02 * MlMod);
    public int Rg => (int)(_baseRg * Level * 0.02 * RGMod);
    public int Pa => (int)(_basePa * Level * 0.02 * PaMod);
    public int Ba => (int)(_baseBa * Level * 0.02 * BaMod);
    public int In => (int)(_baseIn * Level * 0.02 * InMod);

    public float MlMod = 1;
    public float RGMod = 1;
    public float PaMod = 1;
    public float BaMod = 1;
    public float InMod = 1;

    public (int, int) InitPos {get; set;}

    /*
    Lista de ataques
    */

    /*
    Lista de habilidades
    */
}