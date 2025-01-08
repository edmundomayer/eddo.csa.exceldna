using ExcelDna.Integration;

namespace eddo.csa.exceldna.Functions
{
    public class DailyFunctions
    {
        #region Methods
        private static TimeSpan ToTimeSpan( string timeSpan )
        {
            var days = int.Parse( timeSpan.Substring( 1, 2 ) );
            var hours = int.Parse( timeSpan.Substring( 4, 2 ) );
            var minutes = int.Parse( timeSpan.Substring( 7, 2 ) );
            var seconds = int.Parse( timeSpan.Substring( 10, 2 ) );
            var milliseconds = int.Parse( timeSpan.Substring( 13, 3 ) );

            var result = new TimeSpan( days, hours, minutes, seconds, milliseconds );

            return result;
        }
        #endregion Methods


        #region Excel Public Functions
        [ExcelFunction( Description = "Calculate the TimeStamp difference between two TimeSpams. The result is rightTimestamp - leftTimeStamp" )]
        public string TimeStampDifference( [ExcelArgument( Name = "leftTimeStamp", Description = "The left TimeStamp for comparison" )] string leftTimeStamp,
                                            [ExcelArgument( Name = "rightTimestamp", Description = "The right TimeStamp to compare" )] string rightTimestamp,
                                            [ExcelArgument( Name = "avoidNegatives", Description = "If true, replace negatives with zero" )] bool setNegativeAsZero = false )
        {
            var resultTicks = TicksDifference( leftTimeStamp, rightTimestamp, setNegativeAsZero );

            var result = new TimeSpan( resultTicks );

            return result.ToString();
        }

        [ExcelFunction( Description = "Calculate the Ticks (long) difference between two TimeSpams. The result is rightTimestamp - leftTimeStamp" )]
        public long TicksDifference( [ExcelArgument( Name = "leftTimeStamp", Description = "The left TimeStamp for comparison" )] string leftTimeStamp,
                                    [ExcelArgument( Name = "rightTimestamp", Description = "The right TimeStamp to compare" )] string rightTimestamp,
                                    [ExcelArgument( Name = "avoidNegatives", Description = "If true, replace negatives with zero" )] bool setNegativeAsZero = false )
        {
            var left = ToTimeSpan( leftTimeStamp );
            var rigth = ToTimeSpan( rightTimestamp );

            var leftTicks = left.Ticks;
            var rightTicks = rigth.Ticks;

            var result = rightTicks - leftTicks;

            return setNegativeAsZero ? ( rightTicks - leftTicks < 0 ? 0 : rightTicks - leftTicks ) : rightTicks - leftTicks;
        }
        #endregion Excel Public Functions
    }
}
