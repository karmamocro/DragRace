using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DragRacerPrep
{
    class DragRacerBase
    {
        #region stappen voor het gebruik
        //beschrijf zelf hoe je het aanpakt
        #endregion

        #region beschikbare variabelen
        int tunedRacerMaxSpeed = 0; //willekeurige waarde om snelheid te bepalen
        double stepSizePerTick = 0;
        double actualPositionInLane = 0; //positie van de racer, niet afgerond, dit is dus de ECHTE positie
        int actualPositionInLaneRounded; //afgerond omdat posities van form items met pixels werken
        static Random generateRacerSpeed = new Random();

        public int endPos;
        #endregion

        #region variabelen en methoden nog nodig
        //racer is gefinished, bool
        public bool flagged = false;
        //reset finished racer, setter
        public int opgeslagenPos;

        public int opgeslagenTijd;
        #endregion

        #region instellen
        /// <summary>
        /// instellen snelheid van een racer: neem default tussen 500 en 800
        /// </summary>
        /// <param name="a_minValue"></param>
        /// <param name="a_maxValue"></param>
        public void setSpeed(int a_speedWithBadTuning, int a_speedWithGoodTuning)
        {
            

            tunedRacerMaxSpeed = generateRacerSpeed.Next(a_speedWithBadTuning, a_speedWithGoodTuning);
        }

        /// <summary>
        /// de stapgrootte bij elke timer tick, berekend op basis van de baanlengte
        /// </summary>
        /// <param name="a_laneLengthToFinishLine"></param>
        public void calculateStepSize(double a_laneLengthToFinishLine)
        {
            stepSizePerTick = tunedRacerMaxSpeed / a_laneLengthToFinishLine;
        }

        /// <summary>
        /// elke tick wordt berekend waar de racer is, in echte pixels
        /// </summary>
        /// <param name="a_stepsPerTick"></param>
        public void calculateCurrentPosition()
        {
            //positie in doubles berekenen
            //met de volledige getallen(inclusief decimalen) rekenen
            //eerst doubles optellen, dan pas omzetten naar een int voor de echte positie
            actualPositionInLane = actualPositionInLane + stepSizePerTick;

            //automatische afronding
            actualPositionInLaneRounded = Convert.ToInt32(actualPositionInLane);
        }

        /// <summary>
        /// echte positie van de racer, de positie van de picturebox is hiervan afgeleid
        /// </summary>
        /// <param name="a_startPosition"></param>
        public void resetPosition(int a_startPosition)
        {
            actualPositionInLane = a_startPosition;
        }

        //public initracer(int a_resetpos, int a_min, int a_max, int a_calcstep)
       // {
         //   resetPosition(a_resetpos);
        //    calculateCurrentPosition();
        //    setSpeed(a_min, a_max);
         //   calculateStepSize(a_calcstep);

        //    return;

       // }

        #endregion

        #region ophalen gegevens van de racer

        /// <summary>
        /// wat is de max snelheid bij de huidige motor tuning
        /// </summary>
        public String GetTunedRacerSpeed
        {
            get
            {
                return Convert.ToString(tunedRacerMaxSpeed);
            }
        }

        /// <summary>
        /// aantal stappen bij elke timer tick
        /// </summary>
        public String GetStepSizePerTick
        {
            get
            {
                return Convert.ToString(stepSizePerTick);
            }
        }

        /// <summary>
        /// positie na het huidige aantal timer ticks, dus b.v. 10 ticks is 10 stappen van x afstand
        /// </summary>
        public int GetActualPositionInLaneRounded
        {
            get
            {
                return actualPositionInLaneRounded;
            }
        }
        #endregion
    }
}
