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
        public int StartingX { get; set; }
        public int StartingY { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public Neighbor[] Neighbors { get; set; }
        public LowResolutionCell[] LowResolutionMap { get; set; }
        public string Orientation { get; set; }
    }

    public class Neighbor
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Difficulty { get; set; }
    }

    public class LowResolutionCell
    {
        public int LowerLeftX { get; set; }
        public int LowerLeftY { get; set; }
        public int UpperRightX { get; set; }
        public int UpperRightY { get; set; }
        public int AverageDifficulty { get; set; }
    }
    public class StatusResult
    {
        public string status { get; set; }
    }
    public class MoveResponse
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int BatteryLevel { get; set; }
        public Neighbor[] Neighbors { get; set; }
        public string Message { get; set; }
        public string Orientation { get; set; }
    }
    public class IngenuityMoveResponse
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int BatteryLevel { get; set; }
        public Neighbor[] Neighbors { get; set; }
        public string Message { get; set; }
    }
}
