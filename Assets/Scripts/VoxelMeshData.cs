using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VoxelMeshData
{
    //int[,,] data = new int[,,] {    { { 0, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } },
    //                                { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } },
    //                                { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 0 } }, };
    int[,,] data;

    public VoxelMeshData(int[,,]data)
    {
        this.data = data;
    }

    public int Width
    {
        get { return data.GetLength(0); }
    }

    public int Height
    {
        get { return data.GetLength(1); }
    }

    public int Depth
    {
        get { return data.GetLength(2); }
    }

    public int GetCell(int x, int y, int z)
    {
        return data[x, y, z];
    }

    public int getNeighbour(int x, int y, int z, int dir)
    {
        DataCoordinate offsetToCheck = offsets[dir];
        DataCoordinate NeighbourCoord = new DataCoordinate(x + offsetToCheck.x, y + offsetToCheck.y, z + offsetToCheck.z);

        if (NeighbourCoord.x < 0 || NeighbourCoord.x >= Width  ||
            NeighbourCoord.y < 0 || NeighbourCoord.y >= Height ||
            NeighbourCoord.z < 0 || NeighbourCoord.z >= Depth)
        {
            return 0;
        }
        else {
            return GetCell(NeighbourCoord.x, NeighbourCoord.y, NeighbourCoord.z);
        }
    }

    struct DataCoordinate
    {
        public int x;
        public int y;
        public int z;

        public DataCoordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    DataCoordinate[] offsets =
    {
        new DataCoordinate(0,0,1),
        new DataCoordinate(1,0,0),
        new DataCoordinate(0,0,-1),
        new DataCoordinate(-1,0,0),
        new DataCoordinate(0,1,0),
        new DataCoordinate(0,-1,0),
    };
}