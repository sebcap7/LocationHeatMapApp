using SQLite;

namespace Locat***HeatMapApp.Models;

public class***cationRecord
{
    [PrimaryKey]
*** [AutoIncrement]
    public int ***{ get; set; }

    public double***titude { get; set; }

    public***uble Longitude { get; set; }

  ***ublic DateTime Timestamp { get; ***; }
}