using System.Collections.Generic;

namespace BokInterface.Weapons {
    /// <summary>Class for Shinbok sword attack patterns lists</summary>
    class ShinbokSwordAttackPatterns {

        public Dictionary<string, ShinbokSwordAttackPattern> All = [];

        public ShinbokSwordAttackPatterns() {
            InitAll();
        }

        ///<summary>Init the full list containing all sword attack patterns</summary>
        private void InitAll() {

            /*
                How to read patterns :

                Letters corresponds to the attack type :
                    V = Vertical slash (equivalent of Bok 2 hammer)
                    H = Horizontal slash (equivalent of Bok 2 sword)
                    P = Piercing / Thrusting (equivalent of Bok 2 spear)

                Number corresponds to the recovery time after an attack.
                The higher the number, the longer it takes.

                Dashes corresponds to the delay before the next attack.
                The more dashes, the longer the delay.

                For example "--H2------P6" indicates a short delay before a fast horizontal slash,
                followed by a long delayed piercing attack with a long recovery.

                "?" indicates that the pattern should be looked into.
                Theses patterns seems to be duplicates, however it doesn't make sense to have duplicates for attack patterns at all.
                It's possible that there is a difference in frames, in which case it's hard to notice by just looking at the animation itself.

                Source : https://gamefaqs.gamespot.com/gba/926529-shin-bokura-no-taiyou-gyakushuu-no-sabata/faqs/43882
            */

            All.Add("P1----P3", new ShinbokSwordAttackPattern("P1----P3", 0));
            All.Add("P1,P2------P4", new ShinbokSwordAttackPattern("P1,P2------P4", 1));
            All.Add("P1,P2------H4", new ShinbokSwordAttackPattern("P1,P2------H4", 2));
            All.Add("P1,P2,P4", new ShinbokSwordAttackPattern("P1,P2,P4", 3));
            All.Add("P1,P2,P3------P4", new ShinbokSwordAttackPattern("P1,P2,P3------P4", 4));
            All.Add("P1,P2,P3------H4", new ShinbokSwordAttackPattern("P1,P2,P3------H4", 5));
            All.Add("P1,P2,P3,P4------P6", new ShinbokSwordAttackPattern("P1,P2,P3,P4------P6", 6));
            All.Add("P1,P2,P3,P4------H6", new ShinbokSwordAttackPattern("P1,P2,P3,P4------H6", 7));
            All.Add("P1,P2,P3,H4------V6", new ShinbokSwordAttackPattern("P1,P2,P3,H4------V6", 8));
            All.Add("P1,P2,H3,V4------P4", new ShinbokSwordAttackPattern("P1,P2,H3,V4------P4", 9));
            All.Add("H3", new ShinbokSwordAttackPattern("H3", 10));
            All.Add("H2,H3", new ShinbokSwordAttackPattern("H2,H3", 11));
            All.Add("H2,H3------H6", new ShinbokSwordAttackPattern("H2,H3------H6", 12));
            All.Add("H2,H3------V6", new ShinbokSwordAttackPattern("H2,H3------V6", 13));
            All.Add("H2,H3------V6,H6", new ShinbokSwordAttackPattern("H2,H3------V6,H6", 14));
            All.Add("H2,H3--H6------V6", new ShinbokSwordAttackPattern("H2,H3--H6------V6", 15));
            All.Add("H2,H3--H4----H4------V6", new ShinbokSwordAttackPattern("H2,H3--H4----H4------V6", 16));
            All.Add("H2--H3----H4", new ShinbokSwordAttackPattern("H2--H3----H4", 17));
            All.Add("H2--H3----H4------H6", new ShinbokSwordAttackPattern("H2--H3----H4------H6", 18));
            All.Add("H2--H3----H4------H4", new ShinbokSwordAttackPattern("H2--H3----H4------H4", 19));
            All.Add("H2,H3--H6----H6------V6", new ShinbokSwordAttackPattern("H2,H3--H6----H6------V6", 20));
            All.Add("H2,H3,H4--H4----V6", new ShinbokSwordAttackPattern("H2,H3,H4--H4----V6", 21));
            All.Add("H2,H3,H4--V4----H6", new ShinbokSwordAttackPattern("H2,H3,H4--V4----H6", 22));
            All.Add("H3 ?", new ShinbokSwordAttackPattern("H3 ?", 23));
            All.Add("H2------V4", new ShinbokSwordAttackPattern("H2------V4", 24));
            All.Add("H2--V3", new ShinbokSwordAttackPattern("H2--V3", 25));
            All.Add("H2--V2----P6", new ShinbokSwordAttackPattern("H2--V2----P6", 26));
            All.Add("H2----H2--V4", new ShinbokSwordAttackPattern("H2----H2--V4", 27));
            All.Add("H2--V2----H4", new ShinbokSwordAttackPattern("H2--V2----H4", 28));
            All.Add("H2--H2--V4----P6", new ShinbokSwordAttackPattern("H2--H2--V4----P6", 29));
            All.Add("H2,H2--V4----H4", new ShinbokSwordAttackPattern("H2,H2--V4----H4", 30));
            All.Add("H2,V2----H4--V4", new ShinbokSwordAttackPattern("H2,V2----H4--V4", 31));
            All.Add("H2,V2----H4----H4----P6", new ShinbokSwordAttackPattern("H2,V2----H4----H4----P6", 32));
            All.Add("H2,V2----P4----H4----V6", new ShinbokSwordAttackPattern("H2,V2----P4----H4----V6", 33));
            All.Add("H2,V2----H4--V4----P4", new ShinbokSwordAttackPattern("H2,V2----H4--V4----P4", 34));
            All.Add("--V4", new ShinbokSwordAttackPattern("--V4", 35));
            All.Add("V4", new ShinbokSwordAttackPattern("V4", 36));
            All.Add("--V4--H6", new ShinbokSwordAttackPattern("--V4--H6", 37));
            All.Add("V4--H6", new ShinbokSwordAttackPattern("V4--H6", 38));
            All.Add("--V4--H4----H6", new ShinbokSwordAttackPattern("--V4--H4----H6", 39));
            All.Add("--V4--H4--V6", new ShinbokSwordAttackPattern("--V4--H4--V6", 40));
            All.Add("--V4--H4--H4------P6", new ShinbokSwordAttackPattern("--V4--H4--H4------P6", 41));
            All.Add("V4--H4--H4------P6", new ShinbokSwordAttackPattern("V4--H4--H4------P6", 42));
            All.Add("V4--H4--H4--V4--P6", new ShinbokSwordAttackPattern("V4--H4--H4--V4--P6", 43));
            All.Add("------V4--V6", new ShinbokSwordAttackPattern("------V4--V6", 44));
            All.Add("------V4--V6 ?", new ShinbokSwordAttackPattern("------V4--V6 ?", 45));
            All.Add("------V4--V4------V6", new ShinbokSwordAttackPattern("------V4--V4------V6", 46));
            All.Add("----V4--V4------V6", new ShinbokSwordAttackPattern("----V4--V4------V6", 47));
            All.Add("----V4--V4--V4------V6", new ShinbokSwordAttackPattern("----V4--V4--V4------V6", 48));
            All.Add("V2,H3", new ShinbokSwordAttackPattern("V2,H3", 49));
            All.Add("V2,H3--P4", new ShinbokSwordAttackPattern("V2,H3--P4", 50));
            All.Add("V2,H3,V3--P4", new ShinbokSwordAttackPattern("V2,H3,V3--P4", 51));
            All.Add("V2,H3,H3,H4,V4", new ShinbokSwordAttackPattern("V2,H3,H3,H4,V4", 52));
            All.Add("V2,H3--H3,V4--P4", new ShinbokSwordAttackPattern("V2,H3--H3,V4--P4", 53));
            All.Add("V2,H3,V3--H4--P4", new ShinbokSwordAttackPattern("V2,H3,V3--H4--P4", 54));
            All.Add("H2--P4", new ShinbokSwordAttackPattern("H2--P4", 55));
            All.Add("H2,V4", new ShinbokSwordAttackPattern("H2,V4", 56));
            All.Add("H2,V4--P4", new ShinbokSwordAttackPattern("H2,V4--P4", 57));
            All.Add("H2,V4,H4--P4", new ShinbokSwordAttackPattern("H2,V4,H4--P4", 58));
            // All.Add("59", new ShinbokSwordAttackPattern("59", 59)); // Bugged : animation gets cancelled
            // All.Add("60", new ShinbokSwordAttackPattern("60", 60)); // Bugged : animation gets cancelled & crashes the game, probably because this is already an inexistant pattern
        }
    }
}
