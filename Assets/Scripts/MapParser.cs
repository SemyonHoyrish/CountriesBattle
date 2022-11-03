using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapParser// : MonoBehaviour
{

    public struct Map
    {
        public int Length;
        public int Width;

        public int[][] Data;

        public Map(int length, int width, int[][] data)
        {
            Length = length;
            Width = width;
            Data = data;
        }
    }


    public static Map Parse(string mapName)
    {
        string filename = Directory.GetCurrentDirectory() + @"\Assets\Maps\" + mapName + ".cbmap";
        
        if (!File.Exists(filename))
            throw new System.Exception("No such map found! " + mapName);

        string[] lines = File.ReadAllLines(filename);
        int width = int.Parse(lines[0]);
        int length = int.Parse(lines[1]);
        int[][] map = new int[length][];
        for(int z = 0; z < length; ++z)
        {
            int[] row = new int[width];
            string[] string_row = lines[z + 2].Split(' ');
            for(int x = 0; x < width; ++x)
            {
                row[x] = int.Parse( string_row[x] );
            }
            map[length - 1 - z] = row;
        }

        return new Map(length, width, map);
    }

}
