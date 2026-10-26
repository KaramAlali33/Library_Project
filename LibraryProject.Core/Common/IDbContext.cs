using System.Data.Common;

namespace LibraryProject.Core.Common
{
    public interface IDbContext
    {
        DbConnection Connection { get; }
    }
}
