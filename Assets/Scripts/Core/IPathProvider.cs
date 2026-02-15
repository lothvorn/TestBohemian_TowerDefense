using UnityEngine;

namespace Core
{
    public interface IPathProvider
    {
        int PointCount { get; }
        Vector3 GetPoint(int index);
    }
}
