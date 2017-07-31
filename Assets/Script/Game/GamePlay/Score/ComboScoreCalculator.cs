using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Game.GamePlay.Score
{
    public class ComboScoreCalculator : IScoreCalculator
    {
        private float curScoreCount;
        private int multiplier;

        public void Start()
        {
            curScoreCount = 0;
            multiplier = 1;
        }

        public void AddScores(float val)
        {
            curScoreCount += val * multiplier;
            ++multiplier; 
        }

        public float End()
        {
            return curScoreCount;
        }
    }
}
