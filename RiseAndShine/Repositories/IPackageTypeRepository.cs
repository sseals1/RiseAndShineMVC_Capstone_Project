using Microsoft.Data.SqlClient;
using RiseAndShine.Models;
using System.Collections.Generic;

namespace RiseAndShine.Repositories
{
    public interface IPackageTypeRepository
    {
        SqlConnection Connection { get; }
        List<PackageType> GetAll();
    }
}