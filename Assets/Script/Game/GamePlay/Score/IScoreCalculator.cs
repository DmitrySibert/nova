using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Game.GamePlay.Score
{
    public interface IScoreCalculator
    {
        void Start();
        void AddScores(float val);
        float End();
    }
}
