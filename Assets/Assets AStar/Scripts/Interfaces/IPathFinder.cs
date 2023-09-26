using System.Collections.Generic;


public interface IPathFinder
{
    IList<Cell> FindPathOnMap(Cell cellStart, Cell cellEnd, Map1 map);
}

