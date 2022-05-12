using System.Numerics;

namespace API.Entity
{
    public interface IEntity
    {
        Vector3 Pos { get; }

        Vector3 Front { get; set; }

        Vector3 Dir { get; set; }
    }
}