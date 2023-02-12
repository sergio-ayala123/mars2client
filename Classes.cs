using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsClient
{

    public class JoinResponse
    {
        public string Token { get; set; }
        public int StartingRow { get; set; }
        public int StartingColumn { get; set; }
        public int TargetRow { get; set; }
        public int TargetColumn { get; set; }
        public Neighbor[] Neighbors { get; set; }
        public LowResolutionCell[] LowResolutionMap { get; set; }
        public string Orientation { get; set; }
    }

    public class Neighbor
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Difficulty { get; set; }
    }

    public class LowResolutionCell
    {
        public int LowerLeftRow { get; set; }
        public int LowerLeftColumn { get; set; }
        public int UpperRightRow { get; set; }
        public int UpperRightColumn { get; set; }
        public int AverageDifficulty { get; set; }
    }
    public class StatusResult
    {
        public string status { get; set; }
    }
    public class MoveResponse
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int BatteryLevel { get; set; }
        public Neighbor[] Neighbors { get; set; }
        public string Message { get; set; }
        public string Orientation { get; set; }
    }
    public class IngenuityMoveResponse
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int BatteryLevel { get; set; }
        public Neighbor[] Neighbors { get; set; }
        public string Message { get; set; }
    }
}
