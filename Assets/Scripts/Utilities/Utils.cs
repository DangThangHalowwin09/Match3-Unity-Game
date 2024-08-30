using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;
using static NormalItem;

public class Utils
{
    public static NormalItem.eNormalType GetRandomNormalType()
    {
        Array values = Enum.GetValues(typeof(NormalItem.eNormalType));
        NormalItem.eNormalType result = (NormalItem.eNormalType)values.GetValue(URandom.Range(0, values.Length));

        return result;
    }

    public static NormalItem.eNormalType GetRandomNormalTypeExcept(NormalItem.eNormalType[] types)
    {
        List<NormalItem.eNormalType> list = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        int rnd = URandom.Range(0, list.Count);
        NormalItem.eNormalType result = list[rnd];

        return result;
    }
    private static void AddCellToSurroudingList(Cell cell, List<NormalItem.eNormalType> surroudingList)
    {
        if (cell != null)
        {
            NormalItem normalItem = cell.Item as NormalItem;
            if (normalItem != null)
            {
                surroudingList.Add((normalItem).ItemType);
            }
        }
    }
    public static NormalItem.eNormalType GetSuitableType(int x, int y, Cell[,] m_cells)
    {
    
        int boardSizeX = m_cells.GetLength(0);
        int boardSizeY = m_cells.GetLength(1);
        List<NormalItem.eNormalType> listeNormalTypes = new List<NormalItem.eNormalType>();
        List<NormalItem.eNormalType> listSurroundingeNormalType = new List<NormalItem.eNormalType>(); 
     

        AddCellToSurroudingList(m_cells[x, y].NeighbourUp, listSurroundingeNormalType);
        AddCellToSurroudingList(m_cells[x, y].NeighbourBottom, listSurroundingeNormalType);
        AddCellToSurroudingList(m_cells[x, y].NeighbourLeft, listSurroundingeNormalType);
        AddCellToSurroudingList(m_cells[x, y].NeighbourRight, listSurroundingeNormalType);
      
        for (int i = 0; i < boardSizeX; i++)
        {
            for (int j = 0; j < boardSizeY; j++)
            {
                NormalItem normalItem = m_cells[i, j].Item as NormalItem;
                if (normalItem != null)
                {
                    listeNormalTypes.Add((normalItem).ItemType);
                }
            }
        }

        List<NormalItem.eNormalType> listeNormalTypesNotSorroudingeNormalType = listeNormalTypes.Except(listSurroundingeNormalType).ToList();
        NormalItem.eNormalType result = listeNormalTypesNotSorroudingeNormalType.GroupBy(x => x)
                                       .OrderBy(g => g.Count())
                                       .Select(g => g.Key)
                                       .First();
        return result;
    }
}
