﻿using System.Collections;
using System.Collections.Generic;
using Nashet.Conditions;
using Nashet.ValueSpace;

namespace Nashet.EconomicSimulation.Reforms
{
    public class Serfdom : AbstractReform
    {
        protected SerfValue typedValue;

        public static SerfValue SerfdomAllowed;
        public static SerfValue Brutal;

        public Procent AristocratTax { get { return typedValue.AristocratTax; } }

        public static SerfValue Abolished = new SerfValue("Abolished", "- Abolished with no obligations", 2,
            new DoubleConditionsList(new List<Condition> { Invention.IndividualRightsInvented, Condition.IsNotImplemented }));

        public static SerfValue AbolishedWithLandPayment = new SerfValue("Abolished with land payment", "- Peasants are personally free now but they have to pay debt for land", 3,
            new DoubleConditionsList(new List<Condition>
            {
            Invention.IndividualRightsInvented,Invention.BankingInvented, Condition.IsNotImplemented
            }));

        public static SerfValue AbolishedAndNationalized = new SerfValue("Abolished and Nationalized land", "- Aristocrats loose property", 4,
            new DoubleConditionsList(new List<Condition>
            {
            Government.isProletarianDictatorship, Condition.IsNotImplemented
            }));

        public Serfdom(Country country) : base("Serfdom", "- Aristocratic Privileges", country, new List<IReformValue> { SerfdomAllowed, Brutal, Abolished, AbolishedWithLandPayment, AbolishedAndNationalized })
        {
            if (SerfdomAllowed == null)
                SerfdomAllowed = new SerfValue("Allowed", "- Peasants and other plebes pay 10% of income to Aristocrats", 1,
                    new DoubleConditionsList(new List<Condition>
                    {
            Economy.isNotMarket,  Condition.IsNotImplemented
                    }));
            if (Brutal == null)
                Brutal = new SerfValue("Brutal", "- Peasants and other plebes pay 20% of income to Aristocrats", 0,
                new DoubleConditionsList(new List<Condition>
                {
            Economy.isNotMarket, Condition.IsNotImplemented
                }));

            SetValue(SerfdomAllowed);
        }


        public void SetValue(SerfValue selectedReform)
        {
            base.SetValue(selectedReform);
            typedValue = selectedReform;
        }


        public static Condition IsAbolishedInAnyWay = new Condition(x => (x as Country).serfdom.typedValue == Abolished
        || (x as Country).serfdom.typedValue == AbolishedAndNationalized || (x as Country).serfdom.typedValue == AbolishedWithLandPayment,
            "Serfdom is abolished", true);

        public static Condition IsNotAbolishedInAnyWay = new Condition(x => (x as Country).serfdom.typedValue == SerfdomAllowed
        || (x as Country).serfdom.typedValue == Brutal,
            "Serfdom is in power", true);


        public class SerfValue : NamedReformValue
        {
            //private static Procent brutalTax = new Procent(0.2f);
            //private static Procent allowedTax = new Procent(0.1f);
            //private static Procent nullTax = new Procent(0.0f);

            public Procent AristocratTax { get; protected set; }

            internal SerfValue(string name, string description, int id, DoubleConditionsList condition) : base(name, description, id, condition)
            {
                // this.allowed = condition;
            }

            //public override bool isAvailable(Country country)
            //{
            //    SerfValue requested = this;

            //    if ((requested.ID == 4) && country.Science.IsInvented(Invention.Collectivism) && (country.serfdom.status.ID == 0 || country.serfdom.status.ID == 1 || country.serfdom.status.ID == 4))
            //        return true;
            //    else
            //    if ((requested.ID == 3) && country.Science.IsInvented(Invention.Banking) && (country.serfdom.status.ID == 0 || country.serfdom.status.ID == 1 || country.serfdom.status.ID == 3))
            //        return true;
            //    else
            //    if ((requested.ID == 2) && (country.serfdom.status.ID == 0 || country.serfdom.status.ID == 1 || country.serfdom.status.ID == 2))
            //        return true;
            //    else
            //        if ((requested.ID == 1) && (country.serfdom.status.ID == 0 || country.serfdom.status.ID == 1))
            //        return true;
            //    else
            //    if ((requested.ID == 0))
            //        return true;
            //    else
            //        return false;
            //}



            //public Procent getTax()
            //{
            //    if (this == Brutal)
            //        return brutalTax;
            //    else
            //        if (this == SerfdomAllowed)
            //        return allowedTax;
            //    else
            //        return nullTax;
            //}
            public override Procent howIsItGoodForPop(PopUnit pop)
            {
                Procent result;
                int change = RelativeConservatism(pop.Country.serfdom.typedValue); //positive - more liberal
                if (pop.Type == PopType.Aristocrats)
                {
                    if (change > 0)
                        result = new Procent(0f);
                    else
                        result = new Procent(1f);
                }
                else
                {
                    if (change > 0)
                        result = new Procent(1f);
                    else
                        result = new Procent(0f);
                }
                return result;
            }
        }
    }
}