using System;
using System.Collections.Generic;
using System.Text;

namespace RobofestApp.Models
{
    public class TeamMatchStorage
    {
        public int[] BottleScores = new int[5];
        public int[] BallScores = new int[4];
        //WHITE, ORANGE, INVALID, REMAINING
        public int[] GAMEScores = new int[2];
        //ENDGAME, INTACT
        public int FieldReset;
        public int Field;
        public int TotalScore;
    }
}
