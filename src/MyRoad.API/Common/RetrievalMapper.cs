using Riok.Mapperly.Abstractions;
using Sieve.Models;

namespace MyRoad.API.Common;

[Mapper]
public static partial class RetrievalMapper
{
    public static partial SieveModel ToSieveModel(this RetrievalRequest request);
}